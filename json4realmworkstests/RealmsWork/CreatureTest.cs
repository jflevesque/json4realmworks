using dndsanitizer.Entities;
using dndsanitizer.RealmsWork;
using FluentAssertions;
using Xunit;

namespace dndsanitizertests
{
    public class CreatureTest
    {
        [Fact]
        public void GivenARealmsWorkCreature_WhenConstructedFromAMonster_ThenFieldsAreSetProperly()
        {
            var monster = new Monster() {constitution = 12, hit_points = 75, hit_dice = "10d12"};
            var creature = new Creature(monster);

            var actual = creature.hit_points;

            actual.Should().Be("75 (10d12 + 10)");
        }
    }
}
