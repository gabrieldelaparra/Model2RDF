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
            var jsonFile = @"C:\Dev\00-Testing\SubAssemblies\Export\1KHF357506_AF1_UHK1_FEND.json";
            var jsonDictionary = jsonFile.DeserializeJson();
            var graph = jsonDictionary.ToRDFGraph();
            var writer = new NTriplesWriter() { SortTriples = true };
            writer.Save(graph, $"{jsonFile}.nt");
        }

        [Fact]
        public void ConvertXMLToRDF()
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
