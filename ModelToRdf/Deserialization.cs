using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ModelToRdf
{
    //TODO: Test
    public static class Deserialization
    {
        public static Dictionary<string, JToken> XmlOrJsonFilenameToJsonData(this string inputFilename) {
            var inputText = File.ReadAllText(inputFilename);
            return XmlOrJsonTextToJsonData(inputText);
        }

        public static Dictionary<string, JToken> XmlOrJsonTextToJsonData(this string inputText) {
            var jsonString = XmlOrJsonToJsonString(inputText);
            return DeserializeJsonString(jsonString);
        }

        private static Dictionary<string, JToken> DeserializeJsonString(this string jsonText) {
            return JsonConvert.DeserializeObject<Dictionary<string, JToken>>(jsonText);
        }

        private static string XmlOrJsonToJsonString(this string inputText) {
            var output = inputText;
            try {
                var doc = XDocument.Parse(inputText);
                output = JsonConvert.SerializeXNode(doc);
            }
            catch (Exception ex) {
                Debug.Write(ex);
            }

            return output;
        }
    }
}