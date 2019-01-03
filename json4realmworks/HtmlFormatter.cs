using json4realmworks.Entities;
using json4realmworks.RealmsWork;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Action = json4realmworks.Entities.Action;

namespace json4realmworks
{
    public class HtmlFormatter
    {
        private const string ParagraphStart = "<p class=\"RWDefault\"><span class=\"RWSnippet\">";
        private const string ParagraphEnd = "</span></p>";
        private const string LineBreak = ParagraphEnd + ParagraphStart;

        private const string meleeWeaponAttack = "Melee Weapon Attack:";
        private const string rangedWeaponAttack = "Ranged Weapon Attack:";
        private const string hit = "Hit:";
        private const string linkFormat = "linkFormat";
        private const string spellName = "spellName";
        private const string pattern = @"(?<" + linkFormat + @">\*\[(?<" + spellName + @">[A-Za-z0-9'\s]+)\]\([\.\/A-Za-z\-\s\\"+ " \"" + @"0-9\(\)]+\)\*)";
        private static Regex regex = new Regex(pattern);

        public static string Format(object obj)
        {
            throw new NotImplementedException($"Type {obj.GetType().AssemblyQualifiedName} is not currently supported.");
        }

        public static string Format(Action action)
        {
            var description = ApplyAllActionFormatting(action.desc);
            return WrapInAParagraph($"<b><i>{action.name}.</i></b> {description}");
        }

        public static string Format(LegendaryAction action)
        {
            var descriptionWithEscapedNewLines = ReplaceEolByLineBreaks(action.desc);
            return WrapInAParagraph($"<b>{action.name}.</b> {descriptionWithEscapedNewLines}");
        }

        public static string Format(Reaction reaction)
        {
            var descriptionWithEscapedNewLines = ReplaceEolByLineBreaks(reaction.desc);
            return WrapInAParagraph($"<b><i>{reaction.name}.</i></b> {descriptionWithEscapedNewLines}");
        }

        public static string Format(SpecialAbility specialAbility)
        {
            var descriptionWithEscapedNewLines = ReplaceEolByLineBreaks(specialAbility.desc);
            return WrapInAParagraph($"<b><i>{specialAbility.name}.</i></b> {descriptionWithEscapedNewLines}");
        }

        public static string Format(SpellDescription spellDescription)
        {
            if (spellDescription.description == null)
                return null;

            var descriptionWithEscapedNewLines = ReplaceDoubleEolByLineBreak(spellDescription.description);
            descriptionWithEscapedNewLines = ReplaceEolByLineBreaks(descriptionWithEscapedNewLines);
            var descriptionWithoutLinks = RemoveWeirdSpellLinkFormatting(descriptionWithEscapedNewLines);
            return WrapInAParagraph(descriptionWithoutLinks);
        }

        public static string WrapInAParagraph(string content)
        {
            return ParagraphStart + content + ParagraphEnd;
        }

        public static string JoinAsParagraphs(IEnumerable<string> paragraphs)
        {
            return String.Join(string.Empty, paragraphs);
        }

        private static string RemoveWeirdSpellLinkFormatting(string description)
        {
            var matches = regex.Matches(description);

            var result = description;

            foreach (Match match in matches)
            {
                if (match.Groups[linkFormat].Success && match.Groups[spellName].Success)
                {
                    string toBeReplaced = match.Groups[linkFormat].Value;
                    string spellName = match.Groups[HtmlFormatter.spellName].Value;
                    result = result.Replace(toBeReplaced, spellName);
                }
            }

            return result;
        }

        private static string ReplaceDoubleEolByLineBreak(string s)
        {
            return s.Replace("\n\n", LineBreak);
        }

        private static string ReplaceEolByLineBreaks(string s)
        {
            return s.Replace("\n", LineBreak);
        }

        private static string ApplyAllActionFormatting(string description)
        {
            description = ReplaceEolByLineBreaks(description);
            description = AddItalicsAroundSearchCriteria(description, meleeWeaponAttack);
            description = AddItalicsAroundSearchCriteria(description, rangedWeaponAttack);
            description = AddItalicsAroundSearchCriteria(description, hit);

            return description;
        }

        private static string AddItalicsAroundSearchCriteria(string description, string searchString)
        {
            return description.Replace(searchString, $"<i>{searchString}</i>");
        }

    }
}