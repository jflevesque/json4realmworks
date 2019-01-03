using json4realmworks.Json;
using json4realmworks.RealmsWork;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace json4realmworkstests
{
    public class JsonHelpersTest
    {
        private const int CancellationTimeoutInMilliseconds = 1000;
        private const string SanitizedMonsterFilename = "JSON files/sanitizedMonster.json";

        public string LoadMonsterFile()
        {
            return TestHelpers.ReadFile(TestHelpers.MonsterFilename);
        }

        public string LoadSpellFile()
        {
            return TestHelpers.ReadFile(TestHelpers.SpellFilename);
        }

        [Fact]
        public void GivenAJsonFileContainingMonsters_WhenParsing_ThenMonstersAreReturned()
        {
            var data = LoadMonsterFile();

            var monsters = JsonHelpers.ParseMonsters(data);

            monsters.Should().NotBeEmpty("Monster.json contains 1 monster in it");
        }

        [Fact]
        public void GivenARealmWorksCreature_WhenSavingToAFile_ThenTheJsonFileMatchesOurExpectedOutput()
        {
            var expectedString = TestHelpers.ReadFile(SanitizedMonsterFilename);
            var data = LoadMonsterFile();

            var monsters = JsonHelpers.ParseMonsters(data);
            var creatures = monsters.Select(monster => new Creature(monster));
            var outputString = JsonHelpers.Serialize(creatures);

            outputString.Should().Be(expectedString);
        }

        [Fact]
        public void GivenAJsonFileContainingSpells_WhenParsing_ThenSpellsAreReturned()
        {
            var data = LoadSpellFile();

            var spells = JsonHelpers.ParseSpells(data);

            spells.Should().NotBeEmpty("Spell.json contains 1 spell in it");
        }
    }
}
