//using VDS.RDF.Writing;
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
        public void ConvertXmlToRDF()
        {
            var xmlFile = @"Resources/test1.xml";
            var jsonDictionary = xmlFile.XmlOrJsonFilenameToJsonData();
            var graph = jsonDictionary.ToRDFGraph();

            Assert.NotNull(graph);

            //var writer = new NTriplesWriter() { SortTriples = true };
            //writer.Save(graph, @"graph.xml.nt");
        }
    }
}
