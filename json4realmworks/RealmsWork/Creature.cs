using dndsanitizer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dndsanitizer.RealmsWork
{
    public class Creature
    {
        public Creature(Monster monster)
        {
            name = monster.name;
            size = monster.size;
            type = monster.type;
            subtype = monster.subtype;
            alignment = monster.alignment;
            armor_class = monster.armor_class;
            hit_points = BuildHitPoints(monster);
            speed = monster.speed;
            ability_scores = BuildAbilityScoreTable(monster);
            saving_throws = BuildSavingThrowsTable(monster);
            skills = ConcatenateSkills(monster);
            damage_vulnerabilities = monster.damage_vulnerabilities;
            damage_resistances = monster.damage_resistances;
            damage_immunities = monster.damage_immunities;
            condition_immunities = monster.condition_immunities;
            senses = monster.senses;
            languages = monster.languages;
            challenge_rating = "CR " + monster.challenge_rating;

            special_abilities = ConcatenateSpecialAbilities(monster);
            reactions = actions = ConcatenateReactions(monster);
            actions = ConcatenateActions(monster);
            legendary_actions = ConcatenateLegendaryActions(monster);
        }

        private static string ConcatenateSpecialAbilities(Monster monster)
        {
            if (monster.special_abilities == null)
                return null;

            return HtmlFormatter.JoinAsParagraphs(monster.special_abilities.Select(HtmlFormatter.Format));
        }

        private static string ConcatenateReactions(Monster monster)
        {
            if (monster.reactions == null)
                return null;

            return HtmlFormatter.JoinAsParagraphs(monster.reactions.Select(HtmlFormatter.Format));
        }

        private static string ConcatenateActions(Monster monster)
        {
            if (monster.actions == null)
                return null;

            return HtmlFormatter.JoinAsParagraphs(monster.actions.Select(HtmlFormatter.Format));
        }

        private static string ConcatenateLegendaryActions(Monster monster)
        {
            if (monster.legendary_actions == null)
                return null;

            return HtmlFormatter.JoinAsParagraphs(monster.legendary_actions.Select(HtmlFormatter.Format));
        }

        public string name { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public string alignment { get; set; }
        public int armor_class { get; set; }
        public string hit_points { get; set;}
        public string speed { get; set; }
        public string ability_scores { get; set; }
        public string saving_throws { get; set; }
        public string skills { get; set; }
        public string damage_vulnerabilities { get; set; }
        public string damage_resistances { get; set; }
        public string damage_immunities { get; set; }
        public string condition_immunities { get; set; }
        public string senses { get; set; }
        public string languages { get; set; }
        public string challenge_rating { get; set; }
        public string special_abilities { get; set; }
        public string actions { get; set; }
        public string legendary_actions { get; set; }
        public string reactions { get; set; }

        private static string BuildAbilityScoreTable(Monster monster)
        {
            return "<table><tr><td>STR</td><td>DEX</td><td>CON</td><td>INT</td><td>WIS</td><td>CHA</td></tr>" +
                $"<tr><td>{FormatAbilityScore(monster.strength)}</td><td>{FormatAbilityScore(monster.dexterity)}</td><td>{FormatAbilityScore(monster.constitution)}</td>" +
                $"<td>{FormatAbilityScore(monster.intelligence)}</td><td>{FormatAbilityScore(monster.wisdom)}</td><td>{FormatAbilityScore(monster.charisma)}</td></tr></table>";
        }

        private static string FormatAbilityScore(int abilityScore)
        {
            var modifier = CalculateModifier(abilityScore);
            var formattedModifier = FormatModifier(modifier);
            if (string.IsNullOrEmpty(formattedModifier))
                return $"{abilityScore}";

            return  $"{abilityScore} ({formattedModifier})";
        }

        private string BuildSavingThrowsTable(Monster monster)
        {
            return "<table><tr><td>STR</td><td>DEX</td><td>CON</td><td>INT</td><td>WIS</td><td>CHA</td></tr>" +
                $"<tr><td>{GetSaveModifier(monster.strength, monster.strength_save)}</td><td>{GetSaveModifier(monster.dexterity, monster.dexterity_save)}</td><td>{GetSaveModifier(monster.constitution, monster.constitution_save)}</td>" +
                $"<td>{GetSaveModifier(monster.intelligence, monster.intelligence_save)}</td><td>{GetSaveModifier(monster.wisdom, monster.wisdom_save)}</td><td>{GetSaveModifier(monster.charisma, monster.charisma_save)}</td></tr></table>";
        }

        private static string BuildHitPoints(Monster monster)
        {
            var modifiers = CalculateHitPointsModifier(monster);
            var formatHitDice = FormatHitDice(monster, modifiers);
            return $"{monster.hit_points} ({formatHitDice})";
        }

        private static string FormatHitDice(Monster monster, string modifiers)
        {
            return monster.hit_dice + (string.IsNullOrEmpty(modifiers) ? string.Empty : " " + modifiers);
        }

        private static int CalculateModifier(int abilityScore)
        {
            return abilityScore / 2 - 5;
        }

        private static string FormatModifier(int? value)
        {
            if (!value.HasValue)
                return string.Empty;

            string sign = value >= 0 ? "+" : "-";
            return $"{sign}{Math.Abs(value.Value)}";
        }

        private static string CalculateHitPointsModifier(Monster monster)
        {
            if (string.IsNullOrEmpty(monster.hit_dice))
                return string.Empty;

            var modifier = CalculateModifier(monster.constitution);
            if (modifier == 0)
                return string.Empty;

            var nbHitDice = Int32.Parse(monster.hit_dice.Substring(0, monster.hit_dice.IndexOf('d')));
            var formattedModifier = FormatModifier(nbHitDice * modifier);
            if (formattedModifier.Equals("+0"))
                return string.Empty;

            formattedModifier = formattedModifier.Insert(1, " ");
            return $"{formattedModifier}";
        }

        private static string GetSaveModifier(int score, int? save)
        {
            if (save.HasValue)
            {
                return FormatModifier(save.Value);
            }

            var modifier = CalculateModifier(score);
            return FormatModifier(modifier);
        }

        private static string ConcatenateSkills(Monster monster)
        {
            var proficientSkills = new List<(string skillName, string skillModifier)>();
            proficientSkills.Add(("Acrobatics", FormatModifier(monster.acrobatics)));
            proficientSkills.Add(("Arcana", FormatModifier(monster.arcana)));
            proficientSkills.Add(("Athletics", FormatModifier(monster.athletics)));
            proficientSkills.Add(("Deception", FormatModifier(monster.deception)));
            proficientSkills.Add(("History", FormatModifier(monster.history)));
            proficientSkills.Add(("Insight", FormatModifier(monster.insight)));
            proficientSkills.Add(("Intimidation", FormatModifier(monster.intimidation)));
            proficientSkills.Add(("Investigation", FormatModifier(monster.investigation)));
            proficientSkills.Add(("Medicine", FormatModifier(monster.medicine)));
            proficientSkills.Add(("Nature", FormatModifier(monster.nature)));
            proficientSkills.Add(("Perception", FormatModifier(monster.perception)));
            proficientSkills.Add(("Performance", FormatModifier(monster.performance)));
            proficientSkills.Add(("Persuasion", FormatModifier(monster.persuasion)));
            proficientSkills.Add(("Religion", FormatModifier(monster.religion)));
            proficientSkills.Add(("Stealth", FormatModifier(monster.stealth)));
            proficientSkills.Add(("Survival", FormatModifier(monster.survival)));

            return String.Join(", ", proficientSkills.Where(tuple => !string.IsNullOrEmpty(tuple.skillModifier)).Select(tuple => $"{tuple.skillName} {tuple.skillModifier}"));
        }
    }
}
