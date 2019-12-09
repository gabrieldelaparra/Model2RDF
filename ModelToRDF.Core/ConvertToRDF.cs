using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RDFExtensions;
using VDS.RDF;

namespace ModelToRDF.Core
{
    //TODO: Test
    public static class ConvertToRDF
    {
        public static string DefaultIri { get; set; } = @"http://model2.rdf/";
        
        //TODO: Should convert to a custom object (json/dictionary?), to avoid cyclic calls.
        public static Graph ToRDFGraph(this object model)
        {
            var graph = new Graph();
            model.ToRDFGraph(graph);
            return graph;
        }
        
        internal static void ToRDFGraph(this object model, Graph graph)
        {
            if (model is null) return;

            var id = model.GetId();

            var entityNode = id.ToUriNode(DefaultIri);

            model.ToRDFGraph(graph, entityNode);
        }

        //TODO: This should be external. The user should be able to specify what is the Id.
        internal static string GetId(this object model)
        {
            var type = model.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var idProperty = properties.FirstOrDefault(x => x.Name.Equals("Id"));
            return idProperty != null ? idProperty.GetValue(model).ToString() : Guid.NewGuid().ToString();
        }

        //TODO: This should be external. The user should be able to specify which properties to map and how.
        //TODO: Add ClassName as a Predicate, with the 'model.GetType().Name'
        internal static void ToRDFGraph(this object model, Graph graph, IUriNode entityNode)
        {
            if (model is null) return;

            var type = model.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanRead);

            foreach (var property in properties)
            {
                if (property.Name.Equals("Id"))
                {
                    graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), property.GetValue(model).ToString().ToLiteralNode());
                }
                else if (property.Name.EndsWith("Id"))
                {
                    graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), property.GetValue(model).ToString().ToUriNode(DefaultIri));
                }
                else if (property.PropertyType == typeof(string))
                {
                    graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), property.GetValue(model).ToString().ToLiteralNode());
                }
                //TODO: It should not be a literal node
                else if (property.PropertyType == typeof(int))
                {
                    graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), property.GetValue(model).ToString().ToLiteralNode());
                }
                //TODO: It should not be a literal node
                else if (property.PropertyType == typeof(double))
                {
                    graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), property.GetValue(model).ToString().ToLiteralNode());
                }
                //TODO: It should not be a literal node
                else if (property.PropertyType == typeof(bool))
                {
                    graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), property.GetValue(model).ToString().ToLiteralNode());
                }
                //TODO: Add Type DateTime
                //TODO: Make recursive call here, for all types.
                else if (property.PropertyType.IsArray)
                {
                    var elementType = property.PropertyType.GetElementType();
                    if (elementType == typeof(string))
                    {
                        var stringValues = (IEnumerable<string>)property.GetValue(model);
                        foreach (var stringValue in stringValues)
                        {
                            graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), stringValue.ToLiteralNode());
                        }
                    }
                    else if (property.Name.EndsWith("Ids"))
                    {
                        var intValues = (int?[])property.GetValue(model);
                        foreach (var intValue in intValues)
                        {
                            graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), intValue.ToString().ToUriNode(DefaultIri));
                        }
                    }
                    else
                    {
                        var arrayValues = (IEnumerable<object>)property.GetValue(model);
                        foreach (var arrayValue in arrayValues)
                        {
                            arrayValue.ToRDFGraph(graph);
                        }
                    }
                }
                else //Custom Class Instance Object
                {
                    var id = Guid.NewGuid().ToString().ToUriNode(DefaultIri);
                    graph.Assert(entityNode, property.Name.ToUriNode(DefaultIri), id);
                    property.GetValue(model).ToRDFGraph(graph, id);
                }
            }
        }
    }
}
