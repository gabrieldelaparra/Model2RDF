using System.Dynamic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace ModelToRDF.Core
{
    //TODO: Test
    public static class Deserialization
    {
        public static dynamic DeserializeXml(this string xmlFilename)
        {
            //if (!File.Exists(xmlFilename)) return null; //Let it throw the exceptions if any.
            var doc = XDocument.Load(xmlFilename);
            var jsonText = JsonConvert.SerializeXNode(doc);
            return DeserializeJsonString(jsonText);
        }

        public static dynamic DeserializeJson(this string jsonFilename)
        {
            var jsonText = File.ReadAllText(jsonFilename);
            return DeserializeJsonString(jsonText);
        }

        private static dynamic DeserializeJsonString(this string jsonText)
        {
            return JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
        }
    }
}
