using json4realmworks.Json;
using json4realmworks.RealmsWork;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace json4realmworkstests
{
    public class SpellTest
    {
        [Fact]
        public void GivenASpell_WhenConstructedFromASpell_ThenTheSpellIsConvertedProperly()
        {
            var spellInJson = TestHelpers.ReadFile(TestHelpers.SpellFilename);
            var spellFromJson = JsonHelpers.ParseSpells(spellInJson).First();
            var spell = new Spell(spellFromJson);

            spell.name.Should().Be("Hex");
            spell.classes.Should().Be("warlock");
            spell.level.Should().Be("1st");
            spell.school.Should().Be("enchantment");
            spell.ritual.Should().BeNull();
            spell.casting_time.Should().Be("1 bonus action");
            spell.range.Should().Be("90 feet");
            spell.components.Should().Be("V, S, M (the petrified eye of a newt)");
            spell.duration.Should().Be("Concentration, up to 1 hour");
            spell.description.Should().Be("<p class=\"RWDefault\"><span class=\"RWSnippet\">You place a curse on a creature that you can see within range. Until the spell ends, you deal an extra 1d6 necrotic damage to the target whenever you hit it with an attack. Also choose one ability when you cast the spell. The target has disadvantage on ability checks made with the chosen ability.</span></p><p class=\"RWDefault\"><span class=\"RWSnippet\">If the target drops to 0 hit points before this spell ends, you can use a bonus action on a subsequent turn of yours to curse a new creature.</span></p><p class=\"RWDefault\"><span class=\"RWSnippet\">A remove curse cast on the target ends this spell early.</span></p>");
            spell.higher_levels.Should().Be("<p class=\"RWDefault\"><span class=\"RWSnippet\">When you cast this spell using a spell slot of 3rd or 4th level, you can maintain your concentration on the spell for up to 8 hours. When you use a spell slot of 5th level or higher, you can maintain your concentration on the spell for up to 24 hours.</span></p>");
        }
    }
}
