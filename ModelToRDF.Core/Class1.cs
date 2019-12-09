using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VDS.RDF;

namespace ModelToRDF.Core
{
    public static class Class1
    {
        public static Graph ToGraph(this object e3Object)
        {
            var graph = new Graph();
            e3Object.ToGraph(graph);
            return graph;
        }
        public static void ToGraph(this object e3Object, Graph graph)
        {

            if (e3Object is null) return;

            var properties = e3Object.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var id = properties.Any(x => x.Name.Equals("Id"))
                    ? properties.FirstOrDefault(x => x.Name.Equals("Id"))?.GetValue(e3Object).ToString()
                    : Guid.NewGuid().ToString();

            var entityNode = id.ToE3UriNode();

            e3Object.ToGraph(graph, entityNode);
        }

        public static void ToGraph(this object e3Object, Graph graph, IUriNode entityNode)
        {
            if (e3Object is null) return;

            var properties = e3Object.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanRead && x.Name != "SyncRoot");

            foreach (var property in properties)
            {
                if (property.Name.Equals("Id") || property.Name.Equals("GivenId"))
                {
                    graph.Assert(entityNode, property.Name.ToE3UriNode(), property.GetValue(e3Object).ToString().ToLiteralNode());
                }
                else if (property.Name.EndsWith("Id"))
                {
                    graph.Assert(entityNode, property.Name.ToE3UriNode(), property.GetValue(e3Object).ToString().ToE3UriNode());
                }
                else if (property.PropertyType == typeof(int))
                {
                    //Maybe it should not be a literal node
                    graph.Assert(entityNode, property.Name.ToE3UriNode(), property.GetValue(e3Object).ToString().ToLiteralNode());
                }
                else if (property.PropertyType == typeof(double))
                {
                    graph.Assert(entityNode, property.Name.ToE3UriNode(), property.GetValue(e3Object).ToString().ToLiteralNode());
                }
                else if (property.PropertyType == typeof(string))
                {
                    graph.Assert(entityNode, property.Name.ToE3UriNode(), property.GetValue(e3Object).ToString().ToLiteralNode());
                }
                else if (property.PropertyType == typeof(bool))
                {
                    graph.Assert(entityNode, property.Name.ToE3UriNode(), property.GetValue(e3Object).ToString().ToLiteralNode());
                }
                else if (property.PropertyType.IsArray)
                {
                    var elementType = property.PropertyType.GetElementType();
                    if (elementType == typeof(string))
                    {
                        var stringValues = (IEnumerable<string>)property.GetValue(e3Object);
                        foreach (var stringValue in stringValues)
                        {
                            graph.Assert(entityNode, property.Name.ToE3UriNode(), stringValue.ToLiteralNode());
                        }
                    }
                    else if (property.Name.EndsWith("Ids"))
                    {
                        var intValues = (int?[])property.GetValue(e3Object);
                        foreach (var intValue in intValues)
                        {
                            graph.Assert(entityNode, property.Name.ToE3UriNode(), intValue.ToString().ToE3UriNode());
                        }
                    }
                    else
                    {
                        var arrayValues = (IEnumerable<object>)property.GetValue(e3Object);
                        foreach (var arrayValue in arrayValues)
                        {
                            arrayValue.ToGraph(graph);
                        }
                    }
                }
                else
                {
                    var id = Guid.NewGuid().ToString().ToE3UriNode();
                    graph.Assert(entityNode, property.Name.ToE3UriNode(), id);
                    property.GetValue(e3Object).ToGraph(graph, id);
                }
            }
        }
    }
}
