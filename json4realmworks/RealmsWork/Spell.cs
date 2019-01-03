using System;
using System.Collections.Generic;

namespace json4realmworks.RealmsWork
{
    public class Spell
    {
        public string name { get; set; }
        public string classes { get; set; }
        public string level { get; set; }
        public string school { get; set; }
        public string ritual { get; set; }
        public string casting_time { get; set; }
        public string range { get; set; }
        public string components { get; set; }
        public string duration { get; set; }
        public string description { get; set; }
        public string higher_levels { get; set; }

        public Spell(Entities.Spell spell)
        {
            name = spell.name;
            classes = ConsolidateList(spell.classes);
            level = ConvertToRealmWorksLevel(spell.level);
            school = spell.school;
            ritual = spell.ritual ? "Ritual" : null;
            casting_time = spell.casting_time;
            range = spell.range;
            components = spell.components.raw;
            duration = spell.duration;
            description = HtmlFormatter.Format(new SpellDescription(spell.description));
            higher_levels = HtmlFormatter.Format(new SpellDescription(spell.higher_levels));
        }

        private string ConvertToRealmWorksLevel(string level)
        {
            if ("cantrip".Equals(level.ToLowerInvariant()))
                return "cantrip";
            if ("1".Equals(level))
                return "1st";
            if ("2".Equals(level))
                return "2nd";
            if ("3".Equals(level))
                return "3rd";
            return level + "th";
        }

        private static string ConsolidateList(List<string> values)
        {
            return String.Join(", ", values);
        }
    }
}
