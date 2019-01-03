using json4realmworks;
using json4realmworks.Entities;
using json4realmworks.RealmsWork;
using FluentAssertions;
using System;
using Xunit;
using Action = json4realmworks.Entities.Action;

namespace json4realmworkstests
{
    public class HtmlFormatterTest
    {
        [Fact]
        public void GivenAnHtmlFormatter_WhenFormattingASpecialAbility_ThenFormatIsRespected()
        {
            var specialAbility = new SpecialAbility() {name = "Ability", desc = "Description"};
            var expected = $"<p class=\"RWDefault\"><span class=\"RWSnippet\"><b><i>{specialAbility.name}.</i></b> {specialAbility.desc}</span></p>";

            var actual = HtmlFormatter.Format(specialAbility);

            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAnHtmlFormatter_WhenFormattingAnAction_ThenFormatIsRespected()
        {
            var action = new Action()
            {
                name = "Touch",
                desc =
                    "Melee Weapon Attack: +4 to hit, reach 5 ft., one target. Hit: 7 (2d6) fire damage. If the target is a creature or a flammable object, it ignites. " +
                    "Until a creature takes an action to douse the fire, the creature takes 3 (1d6) fire damage at the end of each of its turns."
            };
            var expected =
                $"<p class=\"RWDefault\"><span class=\"RWSnippet\"><b><i>{action.name}.</i></b> <i>Melee Weapon Attack:</i> +4 to hit, reach 5 ft., one target. <i>Hit:</i> 7 (2d6) fire damage. If the target is a creature or a flammable object, it ignites. " +
                "Until a creature takes an action to douse the fire, the creature takes 3 (1d6) fire damage at the end of each of its turns.</span></p>";

            var actual = HtmlFormatter.Format(action);

            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAnHtmlFormatter_WhenFormattingALegendaryAction_ThenFormatIsRespected()
        {
            var action = new LegendaryAction()
            {
                name = "Move",
                desc = "The vampire moves up to its speed without provoking opportunity attacks."
            };
            var expected = $"<p class=\"RWDefault\"><span class=\"RWSnippet\"><b>{action.name}.</b> {action.desc}</span></p>";

            var actual = HtmlFormatter.Format(action);

            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAnHtmlFormatter_WhenFormattingAReaction_ThenFormatIsRespected()
        {
            var reaction = new Reaction()
            {
                name = "Parry",
                desc = "The captain adds 2 to its AC against one melee attack that would hit it.To do so, the captain must see the attacker and be wielding a melee weapon."
            };
            var expected = $"<p class=\"RWDefault\"><span class=\"RWSnippet\"><b><i>{reaction.name}.</i></b> {reaction.desc}</span></p>";

            var actual = HtmlFormatter.Format(reaction);

            actual.Should().Be(expected);
        }

        [Fact]
        public void GivenAnHtmlFormatter__WhenFormattingAnEmptySpellDescription_ThenNullIsReturned()
        {
            var actual = HtmlFormatter.Format(new SpellDescription(null));

            actual.Should().BeNull();
        }

        [Fact]
        public void GivenAnHtmlFormatter_WhenFormattingAnObject_ThenAnErrorIsReported()
        {
            var obj = new Object();
            Assert.Throws<NotImplementedException>(() => HtmlFormatter.Format(obj));
        }
    }
}
