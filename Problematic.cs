using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSyllable
{
    public static class Problematic
    {
        private static Dictionary<string, int> _rules = new Dictionary<string, int>();
        public static void Initialize()
        {
            if (_rules.Values.Count > 0)
            {
                return;
            }
            Dictionary<string, int> rv = new Dictionary<string, int>();
            rv.Add("abalone", 4);
            rv.Add("abare", 3);
            rv.Add("abbruzzese", 4);
            rv.Add("abed", 2);
            rv.Add("aborigine", 5);
            rv.Add("abruzzese", 4);
            rv.Add("acreage", 3);
            rv.Add("adame", 3);
            rv.Add("adieu", 2);
            rv.Add("adobe", 3);
            rv.Add("anemone", 4);
            rv.Add("anyone", 3);
            rv.Add("apache", 3);
            rv.Add("aphrodite", 4);
            rv.Add("apostrophe", 4);
            rv.Add("ariadne", 4);
            rv.Add("cafe", 2);
            rv.Add("calliope", 4);
            rv.Add("catastrophe", 4);
            rv.Add("chile", 2);
            rv.Add("chloe", 2);
            rv.Add("circe", 2);
            rv.Add("coyote", 3);
            rv.Add("daphne", 2);
            rv.Add("epitome", 4);
            rv.Add("eurydice", 4);
            rv.Add("euterpe", 3);
            rv.Add("every", 2);
            rv.Add("everywhere", 3);
            rv.Add("forever", 3);
            rv.Add("gethsemane", 4);
            rv.Add("guacamole", 4);
            rv.Add("hermione", 4);
            rv.Add("hyperbole", 4);
            rv.Add("jesse", 2);
            rv.Add("jukebox", 2);
            rv.Add("karate", 3);
            rv.Add("machete", 3);
            rv.Add("maybe", 2);
            rv.Add("naive", 2);
            rv.Add("newlywed", 3);
            rv.Add("penelope", 4);
            rv.Add("people", 2);
            rv.Add("persephone", 4);
            rv.Add("phoebe", 2);
            rv.Add("pulse", 1);
            rv.Add("queue", 1);
            rv.Add("recipe", 3);
            rv.Add("riverbed", 3);
            rv.Add("sesame", 3);
            rv.Add("shoreline", 2);
            rv.Add("simile", 3);
            rv.Add("snuffleupagus", 5);
            rv.Add("sometimes", 2);
            rv.Add("syncope", 3);
            rv.Add("tamale", 3);
            rv.Add("waterbed", 3);
            rv.Add("wednesday", 2);
            rv.Add("yosemite", 4);
            rv.Add("zoe", 2);
            _rules = rv;
        }
        public static Dictionary<string, int> Rules
        {
            get
            {
                Initialize();
                return _rules;
            }
        }
    }
}
