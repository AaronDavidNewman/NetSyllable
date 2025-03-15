using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace NetSyllable
{
    public class PluralRule
    {
        public string Rule;
        public string Replacement;
        public static PluralRule Create(string regex, string replacement)
        {
            return new PluralRule
            {
                Rule = regex,
                Replacement = replacement
            };
        }
    }
    public static class Pluralizer
    {
        public static Dictionary<string, string> IrregularPlurals = new Dictionary<string, string>();
        public static Dictionary<string, string> IrregularSingles = new Dictionary<string, string>();
        public static Dictionary<string, string> UncountableStrings = new Dictionary<string, string>();
        public static List<Regex> UncountableRegex = new List<Regex>();
        public static List<PluralRule> PluralRules = new List<PluralRule>();
        public static void InitializeRules()
        {
            IrregularPlurals.Add("I", "we");
            IrregularPlurals.Add("me", "us");
            IrregularPlurals.Add("he", "they");
            IrregularPlurals.Add("she", "they");
            IrregularPlurals.Add("them", "them");
            IrregularPlurals.Add("myself", "ourselves");
            IrregularPlurals.Add("yourself", "yourselves");
            IrregularPlurals.Add("itself", "themselves");
            IrregularPlurals.Add("herself", "themselves");
            IrregularPlurals.Add("himself", "themselves");
            IrregularPlurals.Add("themself", "themselves");
            IrregularPlurals.Add("is", "are");
            IrregularPlurals.Add("was", "were");
            IrregularPlurals.Add("has", "have");
            IrregularPlurals.Add("this", "these");
            IrregularPlurals.Add("that", "those");
            IrregularPlurals.Add("my", "our");
            IrregularPlurals.Add("its", "their");
            IrregularPlurals.Add("his", "their");
            IrregularPlurals.Add("her", "their");
            // Words ending in with a consonant and `o`.
            IrregularPlurals.Add("echo", "echoes");
            IrregularPlurals.Add("dingo", "dingoes");
            IrregularPlurals.Add("volcano", "volcanoes");
            IrregularPlurals.Add("tornado", "tornadoes");
            IrregularPlurals.Add("torpedo", "torpedoes");
            // Ends with `us`.
            IrregularPlurals.Add("genus", "genera");
            IrregularPlurals.Add("viscus", "viscera");
            // Ends with `ma`.
            IrregularPlurals.Add("stigma", "stigmata");
            IrregularPlurals.Add("stoma", "stomata");
            IrregularPlurals.Add("dogma", "dogmata");
            IrregularPlurals.Add("lemma", "lemmata");
            IrregularPlurals.Add("schema", "schemata");
            IrregularPlurals.Add("anathema", "anathemata");
            // Other irregular rules.
            IrregularPlurals.Add("ox", "oxen");
            IrregularPlurals.Add("axe", "axes");
            IrregularPlurals.Add("die", "dice");
            IrregularPlurals.Add("yes", "yeses");
            IrregularPlurals.Add("foot", "feet");
            IrregularPlurals.Add("eave", "eaves");
            IrregularPlurals.Add("goose", "geese");
            IrregularPlurals.Add("tooth", "teeth");
            IrregularPlurals.Add("quiz", "quizzes");
            IrregularPlurals.Add("human", "humans");
            IrregularPlurals.Add("proof", "proofs");
            IrregularPlurals.Add("caIrregularPluralse", "caIrregularPluralses");
            IrregularPlurals.Add("valve", "valves");
            IrregularPlurals.Add("looey", "looies");
            IrregularPlurals.Add("thief", "thieves");
            IrregularPlurals.Add("groove", "grooves");
            IrregularPlurals.Add("pickaxe", "pickaxes");
            IrregularPlurals.Add("passerby", "passersby");
            IrregularPlurals.Add("canvas", "canvases");
            IEnumerable<string> singles = IrregularPlurals.Keys;
            foreach (string key in singles)
            {
                IrregularSingles[IrregularPlurals[key]] = key;
            }
            string[] uncountables =
            {
                "adulthood",
                "advice",
                "agenda",
                "aid",
                "aircraft",
                "alcohol",
                "ammo",
                "analytics",
                "anime",
                "athletics",
                "audio",
                "bison",
                "blood",
                "bream",
                "buffalo",
                "butter",
                "carp",
                "cash",
                "chassis",
                "chess",
                "clothing",
                "cod",
                "commerce",
                "cooperation",
                "corps",
                "debris",
                "diabetes",
                "digestion",
                "elk",
                "energy",
                "equipment",
                "excretion",
                "expertise",
                "firmware",
                "flounder",
                "fun",
                "gallows",
                "garbage",
                "graffiti",
                "hardware",
                "headquarters",
                "health",
                "herpes",
                "highjinks",
                "homework",
                "housework",
                "information",
                "jeans",
                "justice",
                "kudos",
                "labour",
                "literature",
                "machinery",
                "mackerel",
                "mail",
                "media",
                "mews",
                "moose",
                "music",
                "mud",
                "manga",
                "news",
                "only",
                "personnel",
                "pike",
                "plankton",
                "pliers",
                "police",
                "pollution",
                "premises",
                "rain",
                "research",
                "rice",
                "salmon",
                "scissors",
                "series",
                "sewage",
                "shambles",
                "shrimp",
                "software",
                "staff",
                "swine",
                "tennis",
                "traffic",
                "transportation",
                "trout",
                "tuna",
                "wealth",
                "welfare",
                "whiting",
                "wildebeest",
                "wildlife",
                "you",
            };
            string[] uncountableRegex =
            {
                "deer$",
                "fish$",
                "measles$",
                "o[iu]s$",
                "pox$",
                "sheep$"
            };
            foreach (string str in uncountables)
            {
                UncountableStrings.Add(str, str);
            }
            foreach (string str in uncountableRegex) {
                UncountableRegex.Add(new Regex(str));
            }
            PluralRules.Add(PluralRule.Create("s$", ""));
            PluralRules.Add(PluralRule.Create("(ss)$", "$1"));
            PluralRules.Add(PluralRule.Create("(wi|kni|(?:after|half|high|low|mid|non|night|[^\\w]|^)li)ves$", "$1fe"));
            PluralRules.Add(PluralRule.Create("(ar|(?:wo|[ae])l|[eo][ao])ves$", "$1f"));
            PluralRules.Add(PluralRule.Create("ies$", "y"));
            PluralRules.Add(PluralRule.Create("(dg|ss|ois|lk|ok|wn|mb|th|ch|ec|oal|is|ck|ix|sser|ts|wb)ies$", "$1ie"));
            PluralRules.Add(PluralRule.Create("\\b(l|(?:neck|cross|hog|aun)?t|coll|faer|food|gen|goon|group|hipp|junk|vegg|(?:pork)?p|charl|calor|cut)ies$", "$1ie"));
            PluralRules.Add(PluralRule.Create("\\b(mon|smil)ies$", "$1ey"));
            PluralRules.Add(PluralRule.Create("\\b((?:tit)?m|l)ice$", "$1ouse"));
            PluralRules.Add(PluralRule.Create("(seraph|cherub)im$", "$1"));
            PluralRules.Add(PluralRule.Create("(x|ch|ss|sh|zz|tto|go|cho|alias|[^aou]us|t[lm]as|gas|(?:her|at|gr)o|[aeiou]ris)(?:es)?$", "$1"));
            PluralRules.Add(PluralRule.Create("(analy|diagno|parenthe|progno|synop|the|empha|cri|ne)(?:sis|ses)$", "$1sis"));
            PluralRules.Add(PluralRule.Create("(movie|twelve|abuse|e[mn]u)s$", "$1"));
            PluralRules.Add(PluralRule.Create("(test)(?:is|es)$", "$1is"));
            PluralRules.Add(PluralRule.Create("(alumn|syllab|vir|radi|nucle|fung|cact|stimul|termin|bacill|foc|uter|loc|strat)(?:us|i)$", "$1us"));
            PluralRules.Add(PluralRule.Create("(agend|addend|millenni|dat|extrem|bacteri|desiderat|strat|candelabr|errat|ov|symposi|curricul|quor)a$", "$1um"));
            PluralRules.Add(PluralRule.Create("(apheli|hyperbat|periheli|asyndet|noumen|phenomen|criteri|organ|prolegomen|hedr|automat)a$", "$1on"));
            PluralRules.Add(PluralRule.Create("(alumn|alg|vertebr)ae$", "$1a"));
            PluralRules.Add(PluralRule.Create("(cod|mur|sil|vert|ind)ices$", "$1ex"));
            PluralRules.Add(PluralRule.Create("(matr|append)ices$", "$1ix"));
            PluralRules.Add(PluralRule.Create("(pe)(rson|ople)$", "$1rson"));
            PluralRules.Add(PluralRule.Create("(child)ren$", "$1"));
            PluralRules.Add(PluralRule.Create("(eau)x?$", "$1"));
            PluralRules.Add(PluralRule.Create("men$", "man"));
        }
        public static string Pluralize(string value)
        {
            if (PluralRules.Count == 0)
            {
                Pluralizer.InitializeRules();
            }
            if (UncountableStrings.ContainsKey(value))
            {
                return value;
            }
            foreach (Regex re in UncountableRegex)
            {
                if (re.IsMatch(value))
                {
                    return value;
                }
            }
            if (IrregularPlurals.ContainsKey(value))
            {
                return IrregularPlurals[value];
            }
            string rv = value;
            foreach (PluralRule rule in PluralRules)
            {
                rv = Regex.Replace(value, rule.Rule, rule.Replacement);
                if (value != rv)
                {
                    return rv;
                }
            }
            return value;
        }
    }
}
