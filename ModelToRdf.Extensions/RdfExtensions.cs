using System;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Nodes;

namespace ModelToRdf.Extensions
{
    //TODO: Test
    public static class RdfExtensions
    {
        public static (INode subject, INode predicate, INode ntObject) AsTuple(this Triple triple) =>
                (triple.Subject, triple.Predicate, triple.Object);

        public static string GetUriIdentifier(this string input) {
            var split = input.Split('/');
            return split.Any() ? split.Last() : input;
        }

        public static bool IsLiteral(this INode node) => node.NodeType.Equals(NodeType.Literal);
        public static ILiteralNode ToLiteralNode(this string value) => new NodeFactory().CreateLiteralNode(value);
        public static ILiteralNode ToLiteralNode(this bool value) => new NodeFactory().CreateLiteralNode(value.ToString());

        public static Uri ToUri(this string value, string iri) => UriFactory.Create($"{iri}{value}");
        public static Uri ToUri(this int value, string iri) => UriFactory.Create($"{iri}{value}");
        public static Uri ToUri(this string value) => UriFactory.Create(value);
        public static Uri ToUri(this int value) => UriFactory.Create(value.ToString());
        public static IUriNode ToUriNode(this string value) => new NodeFactory().CreateUriNode(value.ToUri());
        public static IUriNode ToUriNode(this int value) => new NodeFactory().CreateUriNode(value.ToUri());

        public static IUriNode ToUriNode(this string value, string iri) =>
                new NodeFactory().CreateUriNode(value.ToUri(iri));

        public static IUriNode ToUriNode(this int value, string iri) =>
                new NodeFactory().CreateUriNode(value.ToUri(iri));
    }
}