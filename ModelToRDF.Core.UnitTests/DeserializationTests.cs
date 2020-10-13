using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VDS.RDF.Writing;
using Xunit;

namespace ModelToRDF.Core.UnitTests
{
    public class DeserializationTests
    {
        [Fact]
        public void ConvertJsonToRDF()
        {
            var jsonFile = @"Resources/test1.json";
            var jsonDictionary = jsonFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();

            Assert.NotNull(graph);

            //var writer = new NTriplesWriter() { SortTriples = true };
            //writer.Save(graph, @"graph.json.nt");
        }

        [Fact]
        public void NonRes_ConvertJsonToRDF()
        {
            var jsonFile = @"C:\Dev\PGGA\PGGA.E3.RDF.UnitTest\Resources\test3.json";
            var jsonDictionary = jsonFile.DeserializeJson();
            var graph = jsonDictionary.ToRDFGraph();
            var writer = new NTriplesWriter() { SortTriples = true };
            writer.Save(graph, @"graph3.nt");
        }

        [Fact]
        public void ConvertXmlToRDF()
        {
            var xmlFile = @"C:\Users\CHGADEL1\Desktop\Projects\21878\sample2.pcmm";
            var jsonDictionary = xmlFile.DeserializeXml();
            var graph = jsonDictionary.ToRDFGraph();
            var writer = new NTriplesWriter() { SortTriples = true };
            writer.Save(graph, @"C:\Users\CHGADEL1\Desktop\Projects\21878\xmlRdf2.nt");
        }

        public void NonRes_ConvertXMLToRDF()
        {
            var xmlFile = @"C:\Users\CHGADEL1\Desktop\ASK - Common BCU IED\REC670_Template1.pcmm";
            var jsonDictionary = xmlFile.DeserializeXml();
            var graph = jsonDictionary.ToRDFGraph();
            var writer = new NTriplesWriter() { SortTriples = true };
            writer.Save(graph, @"C:\Users\CHGADEL1\Desktop\ASK - Common BCU IED\xmlRdf2.nt");
        }

        [Fact]
        public void Z0_AllPCMMToRDF()
        {
            const string path = @"C:\Users\CHGADEL1\Desktop\Projects\21878\PCMM\";
            var files = Directory.EnumerateFiles(path, "*.pcmm", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var jsonDictionary = file.DeserializeXml();
                var graph = jsonDictionary.ToRDFGraph();
                var writer = new NTriplesWriter() { SortTriples = true };
                writer.Save(graph, $"{file}.nt");
            }
        }

        [Fact]
        public void Z0_AllJsonToRDF()
        {
            const string path = @"C:\Users\CHGADEL1\Desktop\SubAssemblies\Export\JSON\";
            var files = Directory.EnumerateFiles(path, "*.json", SearchOption.AllDirectories);

            foreach (var file in files.Skip(69))
            {
                var jsonDictionary = file.DeserializeJson();
                var graph = jsonDictionary.ToRDFGraph();
                var writer = new NTriplesWriter() { SortTriples = true };
                writer.Save(graph, $"{file}.nt");
            }
        }
    }
}
