using json4realmworks.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace json4realmworks.Json
{
    public class JsonHelpers
    {
        public static IEnumerable<Monster> ParseMonsters(string data)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Monster>>(data);
        }

        public static IEnumerable<Entities.Spell> ParseSpells(string data)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Entities.Spell>>(data);
        }

        public static string Serialize(IEnumerable<Object> items)
        {
            var serializer = new JsonSerializer() {NullValueHandling = NullValueHandling.Ignore};
            var jarray = (JArray)JToken.FromObject(items, serializer);
            return jarray.ToString();
        }
    }
}
