using dndsanitizer;
using dndsanitizer.RealmsWork;
using FluentAssertions;
using Xunit;

namespace dndsanitizertests
{
    public class SpellDescriptionTest
    {
        [Fact]
        public void GivenASpellDescription_WhenRemovingLinks_ThenLinkIsReplacedByTheSpellName()
        {
            var input =
                "You touch a corpse or other remains. For the duration, the target is protected from decay and can't become undead.\n\nThe spell also effectively extends the time limit on raising the target from the dead, since days spent under the influence of this spell don't count against the time limit of spells such as *[raise dead](../raise-dead/ \"raise dead (lvl 5)\")*.";
            var expected =
                "<p class=\"RWDefault\"><span class=\"RWSnippet\">You touch a corpse or other remains. For the duration, the target is protected from decay and can't become undead.</span></p><p class=\"RWDefault\"><span class=\"RWSnippet\">The spell also effectively extends the time limit on raising the target from the dead, since days spent under the influence of this spell don't count against the time limit of spells such as raise dead.</span></p>";

            var actual = HtmlFormatter.Format(new SpellDescription(input));
            actual.Should().Be(expected);
        }


        [Fact]
        public void GivenASpellDescription_WithMultipleOccurrences_WhenRemovingLinks_ThenAllLinksAreReplacedByTheSpellName()
        {
            var input =
                "A thin green ray springs from your pointing finger to a target that you can see within range. The target can be a creature, an object, or a creation of magical force, such as the wall created by *[wall of force](../wall-of-force/ \"wall of force (lvl 5)\")*.\n\nA creature targeted by this spell must make a Dexterity saving throw. On a failed save, the target takes 10d6 + 40 force damage. If this damage reduces the target to 0 hit points, it is disintegrated.\n\nA disintegrated creature and everything it is wearing and carrying, except magic items, are reduced to a pile of fine gray dust. The creature can be restored to life only by means of a *[true resurrection](../true-resurrection/ \"true resurrection (lvl 9)\")* or a *[wish](../wish/ \"wish (lvl 9)\")* spell.\n\nThis spell automatically disintegrates a Large or smaller nonmagical object or a creation of magical force. If the target is a Huge or larger object or creation of force, this spell disintegrates a 10-foot-cube portion of it. A magic item is unaffected by this spell.";
            var expected =
                "<p class=\"RWDefault\"><span class=\"RWSnippet\">A thin green ray springs from your pointing finger to a target that you can see within range. The target can be a creature, an object, or a creation of magical force, such as the wall created by wall of force.</span></p><p class=\"RWDefault\"><span class=\"RWSnippet\">A creature targeted by this spell must make a Dexterity saving throw. On a failed save, the target takes 10d6 + 40 force damage. If this damage reduces the target to 0 hit points, it is disintegrated.</span></p><p class=\"RWDefault\"><span class=\"RWSnippet\">A disintegrated creature and everything it is wearing and carrying, except magic items, are reduced to a pile of fine gray dust. The creature can be restored to life only by means of a true resurrection or a wish spell.</span></p><p class=\"RWDefault\"><span class=\"RWSnippet\">This spell automatically disintegrates a Large or smaller nonmagical object or a creation of magical force. If the target is a Huge or larger object or creation of force, this spell disintegrates a 10-foot-cube portion of it. A magic item is unaffected by this spell.</span></p>";

            var actual = HtmlFormatter.Format(new SpellDescription(input));
            actual.Should().Be(expected);
        }
    }
}
