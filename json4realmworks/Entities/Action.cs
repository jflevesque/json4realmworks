namespace json4realmworks.Entities
{
    public class Action
    {
        public string name { get; set; }
        public string desc { get; set; }
        public int attack_bonus { get; set; }
        public string damage_dice { get; set; }
        public int? damage_bonus { get; set; }
    }
}