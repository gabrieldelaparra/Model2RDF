using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using VDS.RDF.Writing;
using Xunit;

namespace ModelToRDF.Core.UnitTests
{
    public class DeserializationTests
    {
        [Fact]
        public void ConvertJsonToRDF()
        {
            var jsonFile = @"C:\Dev\PGGA\PGGA.E3.RDF.UnitTest\Resources\test3.json";
            var jsonDictionary = jsonFile.DeserializeJson();
            var graph = jsonDictionary.ToRDFGraph();
            var writer = new NTriplesWriter() { SortTriples = true };
            writer.Save(graph, @"graph3.nt");
        }

        [Fact]
        public void ConvertXMLToRDF()
        {
            var xmlFile = @"C:\Users\CHGADEL1\Desktop\Projects\21878\sample.pcmm";
            var jsonDictionary = xmlFile.DeserializeXml();
            var graph = jsonDictionary.ToRDFGraph();
            var writer = new NTriplesWriter() { SortTriples = true };
            writer.Save(graph, @"C:\Users\CHGADEL1\Desktop\Projects\21878\xmlRdf.nt");
        }
    }
}
