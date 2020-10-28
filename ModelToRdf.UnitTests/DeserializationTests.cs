using System.IO;
using System.Linq;
using VDS.RDF.Writing;
using Xunit;

namespace ModelToRdf.UnitTests
{
    public class DeserializationTests
    {
        [Fact]
        public void ConvertSample1ToRDF()
        {
            //var inputFile = @"Resources/sample1.pcmm";
            var inputFile = @"C:\Dev\PGGA\PGGA.PCM.UnitTests\bin\Debug\REL670_2.2SVK-TBNL-PW223_ED1-WithSignalIds.pcmm";
            var jsonDictionary = inputFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
            graph.SerializeNTriples("REL670_2.2SVK-TBNL-PW223_ED1.nt");
            
        }
        [Fact]
        public void ConvertSample2ToRDF()
        {
            const string inputFile = @"Resources/sample2.pcmm";
            var jsonDictionary = inputFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }
        [Fact]
        public void ConvertSample3ToRDF()
        {
            const string inputFile = @"Resources/sample3.json";
            var jsonDictionary = inputFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }

        [Fact]
        public void ConvertSample4ToRDF()
        {
            var inputFile = @"Resources/sample4.json";
            var jsonDictionary = inputFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }

        [Fact]
        public void ConvertSample5ToRDF()
        {
            var inputFile = @"Resources/sample5.xml";
            var jsonDictionary = inputFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }
        [Fact]
        public void ConvertSample6ToRDF()
        {
            var inputFile = @"Resources/sample6.json";
            var jsonDictionary = inputFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }
    }
}
