using System.IO;

namespace dndsanitizertests
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
