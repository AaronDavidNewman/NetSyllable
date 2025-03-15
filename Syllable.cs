using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Tracing;

namespace NetSyllable
{
    public static class SyllableCounter
    {
        private static Regex _monosyllabicOne = null;
        public static Regex MonosyllabicOne
        {
            get
            {
                if (_monosyllabicOne != null)
                {
                    return _monosyllabicOne;
                }
                string[] monosyllabicOne =
                {
                    "awe($|d|so)",
                    "cia(?:l|$)",
                    "tia",
                    "cius",
                    "cious",
                    "[^aeiou]giu",
                    "[aeiouy][^aeiouy]ion",
                    "iou",
                    "sia$",
                    "eous$",
                    "[oa]gue$",
                    ".[^aeiuoycgltdb]{2,}ed$",
                    ".ely$",
                    "^jua",
                    "uai",
                    "eau",
                    "^busi$"
                };
                string[] monosyllabicOnePast =
                {
                    "[bcfgklmnprsvwxyz]",
                    "ch",
                    "dg",
                    "g[hn]",
                    "lch",
                    "l[lv]",
                    "mm",
                    "nch",
                    "n[cgn]",
                    "r[bcnsv]",
                    "squ",
                    "s[chkls]",
                    "th"
                };
                string[] monosyllabicOnePlural =
                {
                    "[bdfklmnprstvy]",
                    "ch",
                    "g[hn]",
                    "lch",
                    "l[lv]",
                    "mm",
                    "nch",
                    "nn",
                    "r[nsv]",
                    "squ",
                    "s[cklst]",
                    "th"
                };
                 
                string pastJoins = "(?:[aeiouy](?:" + string.Join("|", monosyllabicOnePast) + ")ed$)";
                string pluralJoins = "(?:[aeiouy](?:" + string.Join("|", monosyllabicOnePlural) + ")es$)";
                string joins = string.Join("|", monosyllabicOne) + $"|{pastJoins}|{pluralJoins}";
                _monosyllabicOne = new Regex(joins);
                return _monosyllabicOne;
            }
        }
        private static Regex _monosyllabicTwo = null;
        public static Regex MonosyllabicTwo
        {
            get
            {
                if (_monosyllabicTwo != null)
                {
                    return _monosyllabicTwo;
                }
                string[] monosyllabicTwo =
                {
                "[bcdfgklmnprstvyz]",
                "ch",
                "dg",
                "g[hn]",
                "l[lv]",
                "mm",
                "n[cgns]",
                "r[cnsv]",
                "squ",
                "s[cklst]",
                "th"
                };
                string joins = string.Join("|", monosyllabicTwo);
                _monosyllabicTwo = new Regex($"[aeiouy](?:{joins})e$");
                return _monosyllabicTwo;
            }
        }
        private static Regex _doublesyllabicOne = null;
        public static Regex DoublesyllabicOne
        {
            get
            {
                if (_doublesyllabicOne != null)
                {
                    return _doublesyllabicOne;
                }

                string[] doubleSyllabicOne =
                {
                    "([^aeiouy])\\1l",
                    "[^aeiouy]ie(?:r|s?t)",
                    "[aeiouym]bl",
                    "eo",
                    "ism",
                    "asm",
                    "thm",
                    "dnt",
                    "snt",
                    "uity",
                    "dea",
                    "gean",
                    "oa",
                    "ua",
                    "react?",
                    "orbed", // Cancel `".[^aeiuoycgltdb]{2,}ed$",`
                    "shred", // Cancel `".[^aeiuoycgltdb]{2,}ed$",`
                    "eings?",
                    "[aeiouy]sh?e[rs]"
                };
                string joins = string.Join("|", doubleSyllabicOne);
                _doublesyllabicOne = new Regex($"(?:{joins})$");
                return _doublesyllabicOne;
            }
        }
        private static Regex _doublesyllabicTwo = null;
        public static Regex DoublesyllabicTwo
        {
            get
            {
                if (_doublesyllabicTwo != null)
                {
                    return _doublesyllabicTwo;
                }
                string[] doubleSyllabicTwo =
                {
                "creat(?!u)",
                "[^gq]ua[^auieo]",
                "[aeiou]{3}",
                "^(?:ia|mc|coa[dglx].)",
                "^re(app|es|im|us)",
                "(th|d)eist"
            };
                string joins = string.Join("|", doubleSyllabicTwo);
                _doublesyllabicTwo = new Regex(joins);
                return _doublesyllabicTwo;
            }
        }
        private static Regex _doublesyllabicThree = null;
        public static Regex DoublesyllabicThree
        {
            get
            {
                if (_doublesyllabicThree != null)
                {
                    return _doublesyllabicThree;
                }
                string[] doubleSyllabicThree =
                {
                "[^aeiou]y[ae]",
                "[^l]lien",
                "riet",
                "dien",
                "iu",
                "io",
                "ii",
                "uen",
                "[aeilotu]real",
                "real[aeilotu]",
                "iell",
                "eo[^aeiou]",
                "[aeiou]y[aeiou]"
            };
                string joins = string.Join("|", doubleSyllabicThree);
                _doublesyllabicThree = new Regex(joins);
                return _doublesyllabicThree;
            }
        }
        private static Regex _doublesyllabicFour = null;
        public static Regex DoublesyllabicFour
        {
            get
            {

                if (_doublesyllabicFour != null)
                {
                    return (_doublesyllabicFour);
                }
                _doublesyllabicFour = new Regex("[^s]ia");
                return _doublesyllabicFour;
            }
        }
        private static Regex _expressionSingle = null;
        public static Regex ExpressionSingle
        {
            get
            {
                if (_expressionSingle != null)
                {
                    return _expressionSingle;
                }
                string[] startword =
                {
                "un",
                "fore",
                "ware",
                "none?",
                "out",
                "post",
                "sub",
                "pre",
                "pro",
                "dis",
                "side",
                "some"
                };
                string[] endword =
                {
                "ly",
                "less",
                "some",
                "ful",
                "ers?",
                "ness",
                "cians?",
                "ments?",
                "ettes?",
                "villes?",
                "ships?",
                "sides?",
                "ports?",
                "shires?",
                "[gnst]ion(?:ed|s)?"
                };
                string joins = "^(?:" + string.Join("|", startword) + ")|(?:" + string.Join("|", endword) + ")$";
                _expressionSingle = new Regex(joins);
                return _expressionSingle;
            }
        }
        private static Regex _expressionDouble = null;
        public static Regex ExpressionDouble
        {
            get
            {
                if (_expressionDouble != null)
                {
                    return _expressionDouble;
                }
                string[] regex =
                {
                "above",
                "anti",
                "ante",
                "counter",
                "hyper",
                "afore",
                "agri",
                "infra",
                "intra",
                "inter",
                "over",
                "semi",
                "ultra",
                "under",
                "extra",
                "dia",
                "micro",
                "mega",
                "kilo",
                "pico",
                "nano",
                "macro",
                "somer"
                };
                string joins = "^(?:" + string.Join("|", regex) + ")|" +
                    "(?:fully|berry|woman|women|edly|union|((?:[bcdfghjklmnpqrstvwxz])|[aeiou])ye?ing)$";
                _expressionDouble = new Regex(joins);
                return _expressionDouble;
            }
        }
        private static Regex _expressionTriple = null;
        public static Regex ExpressionTriple
        {
            get
            {
                if (_expressionTriple != null)
                {
                    return _expressionTriple;
                }
                _expressionTriple = new Regex("(creations?|ology|ologist|onomy|onomist)$");
                return _expressionTriple;
            }
        }
        public static string ReplaceFactory(Regex regex, int adds, string value, ref int count)
        {
            var matches = regex.Matches(value);
            count += adds * matches.Count;
            value = Regex.Replace(value, regex.ToString(), "");
            return value;
        }
        public static void Syllable(string input, ref int syllableCount, ref int wordCount)
        {
            string value = Regex.Replace(input, "['’]", "").ToLower();
            syllableCount = 0;
            wordCount = 0;
            string[] words = Regex.Split(value, "\\b");
            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    int delta = One(Regex.Replace(word, "[^a-z]", ""));
                    if (delta > 0)
                    {
                        syllableCount += delta;
                        wordCount += 1;
                    }
                }
            }
        }
        public static bool dbgLog = false;
        public static void Log(string message) 
        { 
            if (dbgLog)
            {
                Console.WriteLine(message);
            }
        }
        public static int One(string value)
        {
            int count = 0;
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            if (value.Length < 3)
            {
                return 1;
            }
            if (Problematic.Rules.ContainsKey(value))
            {
                return Problematic.Rules[value];
            }
            string singular = Pluralizer.Pluralize(value);
            if (Problematic.Rules.ContainsKey(singular))
            {
                return Problematic.Rules[singular];
            }
            value = ReplaceFactory(ExpressionTriple, 3, value, ref count);
            value = ReplaceFactory(ExpressionDouble, 2, value, ref count);
            value = ReplaceFactory(ExpressionSingle, 1, value, ref count);
            Log($"value  after expressions is {value} count is {count}");
            string[] parts = Regex.Split(value, "[^aeiouy]+");
            int index = 0;
            while (index < parts.Length)
            {
                if (!string.IsNullOrEmpty(parts[index]))
                {
                    count += 1;
                    Log($"part is {parts[index]} count is {count}");
                }
                index += 1;
            }
            ReplaceFactory(MonosyllabicOne, -1, value, ref count);
            Log($"value  after Monosyllabic One is {value} count is {count}");
            ReplaceFactory(MonosyllabicTwo, -1, value, ref count);
            Log($"value  after Monosyllabic Two is {value} count is {count}");

            ReplaceFactory(DoublesyllabicOne, 1, value, ref count);
            Log($"value  after DS1 is {value} count is {count}");
            ReplaceFactory(DoublesyllabicTwo, 1, value, ref count);
            Log($"value  after DS2 is {value} count is {count}");
            ReplaceFactory(DoublesyllabicThree, 1, value, ref count);
            Log($"value  after DS3 is {value} count is {count}");
            ReplaceFactory(DoublesyllabicFour, 1, value, ref count);
            return int.Max(1, count);
        }
    }
}
