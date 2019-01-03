using System.Collections.Generic;

namespace dndsanitizer.Entities
{
    public class Spell
    {
        public string casting_time { get; set; }
        public List<string> classes { get; set; }
        public SpellComponents components { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public string level { get; set; }
        public string name { get; set; }
        public string range { get; set; }
        public bool ritual { get; set; }
        public string school { get; set; }
        public List<string> tags { get; set; }
        public string type { get; set; }
        public string higher_levels { get; set; }
    }
}
