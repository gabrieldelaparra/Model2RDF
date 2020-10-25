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
            var xmlFile = @"Resources/sample1.pcmm";
            var jsonDictionary = xmlFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }
        [Fact]
        public void ConvertSample2ToRDF()
        {
            const string file = @"Resources/sample2.pcmm";
            var jsonDictionary = file.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }
        [Fact]
        public void ConvertSample3ToRDF()
        {
            const string file = @"Resources/sample3.json";
            var jsonDictionary = file.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }

        [Fact]
        public void ConvertSample4ToRDF()
        {
            var jsonFile = @"Resources/sample4.json";
            var jsonDictionary = jsonFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }

        [Fact]
        public void ConvertSample5ToRDF()
        {
            var jsonFile = @"Resources/sample5.xml";
            var jsonDictionary = jsonFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }
        [Fact]
        public void ConvertSample6ToRDF()
        {
            var jsonFile = @"Resources/sample6.json";
            var jsonDictionary = jsonFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();
            Assert.NotNull(graph);
        }
    }
}
