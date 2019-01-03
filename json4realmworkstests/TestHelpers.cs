using System.IO;

namespace json4realmworkstests
{
    public static class TestHelpers
    {
        public const string MonsterFilename = "JSON files/monster.json";
        public const string SpellFilename = "JSON files/spell.json";

        public static string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}
