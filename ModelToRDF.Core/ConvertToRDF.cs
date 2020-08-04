﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using RDFExtensions;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;
using VDS.RDF.Writing.Formatting;

namespace ModelToRDF.Core
{
    //TODO: Test
    public static class ConvertToRDF
    {
        public static string DefaultIri { get; set; } = @"http://model2.rdf/";

        //TODO: Should convert to a custom object (json/dictionary?), to avoid cyclic calls.
        public static Graph ToRDFGraph(this IDictionary<string, JToken> jDictionary, string defaultIri = "")
        {
            if (!string.IsNullOrWhiteSpace(defaultIri)) DefaultIri = defaultIri;
            var graph = new Graph();
            jDictionary.ToRDFGraph(graph);
            return graph;
        }

        public static IEnumerable<string> GraphToNTriples(this Graph graph)
        {
            //Create a formatter
            ITripleFormatter formatter = new NTriplesFormatter();
            
            //Print triples with this formatter
            foreach (Triple t in graph.Triples) {
                yield return t.ToString(formatter);
            }
        }

        internal static void ToRDFGraph(this IDictionary<string, JToken> jDictionary, Graph graph)
        {
            if (jDictionary is null) return;

            var id = jDictionary.GetId();

            var entityNode = id.ToUriNode(DefaultIri);

            jDictionary.ToRDFGraph(graph, entityNode);
        }

        //TODO: This should be external. The user should be able to specify what is the Id.
        internal static string GetId(this IDictionary<string, JToken> jDictionary)
        {
            var idPairs = jDictionary.Where(x => x.Key.ToLower() == "id" || x.Key.ToLower() == "@id");
            if (idPairs.Any())
            {
                return idPairs.FirstOrDefault().Value.ToString();
            }
            return Guid.NewGuid().ToString();
        }

        internal static void ToRDFGraph(this JToken jToken, string key, IUriNode entityNode, Graph graph)
        {
            if (key.ToLower().Equals("id") || key.ToLower().Equals("@id"))
            {
                graph.Assert(entityNode, key.ToUriNode(DefaultIri), jToken.ToString().ToLiteralNode());
            }
            else if (key.ToLower().EndsWith("id") || key.ToLower().EndsWith("ids"))
            {
                graph.Assert(entityNode, key.ToUriNode(DefaultIri), jToken.ToString().ToUriNode(DefaultIri));
            }
            else
            {
                graph.Assert(entityNode, key.ToUriNode(DefaultIri), jToken.ToString().ToLiteralNode());
            }
        }

        //TODO: This should be external. The user should be able to specify which properties to map and how.
        //TODO: Add ClassName as a Predicate, with the 'model.GetType().Name'
        internal static void ToRDFGraph(this IDictionary<string, JToken> model, Graph graph, IUriNode entityNode)
        {
            if (model is null) return;

            var keys = model.Keys;

            foreach (var key in keys)
            {
                var value = model[key] as JToken;
                if (value == null) return;
                
                if (value.Type.ToString().Equals("Array"))
                {
                    if (value is JArray jArray)
                    {
                        foreach (var item in jArray)
                        {
                            if (item is IDictionary<string, JToken> itemDict)
                            {
                                //TODO: Add reference to this object:
                                var id = itemDict.GetId().ToUriNode(DefaultIri); 
                                graph.Assert(entityNode, key.ToUriNode(DefaultIri), id);
                                itemDict.ToRDFGraph(graph, id);
                            }
                            else
                            {
                                if (item is JToken itemToken)
                                {
                                    itemToken.ToRDFGraph(key, entityNode, graph);
                                }
                                else
                                {
                                    throw new Exception($"Unhandled scenario: Key: {key}; Data: {item.ToString()}");
                                }
                            }

                        }
                    }
                    else
                    {
                        throw new Exception($"Unhandled scenario: Key: {key}; Data: {value.ToString()}");
                    }
                }
                else if (value.Type.ToString().Equals("Object"))
                {
                    if (value is IDictionary<string, JToken> itemDict)
                    {
                        var id = Guid.NewGuid().ToString().ToUriNode(DefaultIri);
                        graph.Assert(entityNode, key.ToUriNode(DefaultIri), id);
                        itemDict.ToRDFGraph(graph, id);
                    }
                    else
                    {
                        throw new Exception($"Unhandled scenario: Key: {key}; Data: {value.ToString()}");
                    }
                }
                else
                {
                    value.ToRDFGraph(key, entityNode, graph);
                }
            }
        }
    }
}
