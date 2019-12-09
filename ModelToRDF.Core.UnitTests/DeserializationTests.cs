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
        public void Test1()
        {
            var jsonFile = @"C:\Dev\PGGA\PGGA.E3.RDF.UnitTest\Resources\test3.json";
            var jsonDictionary = jsonFile.DeserializeJson();
            var graph = jsonDictionary.ToRDFGraph();
            var writer = new NTriplesWriter();
            writer.Save(graph, @"graph3.nt");
        }
    }
}
