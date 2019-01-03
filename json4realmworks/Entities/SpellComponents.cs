using System.Collections.Generic;

namespace json4realmworks.Entities
{
    public class SpellComponents
    {
        public bool material { get; set; }
        public string raw { get; set; }
        public bool somatic { get; set; }
        public bool verbal { get; set; }
        public List<string> materials_needed { get; set; }
    }
}