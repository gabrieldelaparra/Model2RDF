using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ModelToRDF.Core
{
    //TODO: Test
    public static class Deserialization
    {
        public static Dictionary<string, JToken> DeserializeXml(this string xmlFilename)
        {
            var doc = XDocument.Load(xmlFilename);
            var jsonText = JsonConvert.SerializeXNode(doc);
            return DeserializeJsonString(jsonText);
        }

        public static Dictionary<string, JToken> DeserializeJson(this string jsonFilename)
        {
            var jsonText = File.ReadAllText(jsonFilename);
            return DeserializeJsonString(jsonText);
        }

        private static Dictionary<string, JToken> DeserializeJsonString(this string jsonText)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, JToken>>(jsonText);
        }
    }
}
