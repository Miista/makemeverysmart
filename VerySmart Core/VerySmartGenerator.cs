using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Thesaurus;

namespace VerySmart_Core
{
    public delegate void WordMadeSmartHandler(string word);

    public class VerySmartGenerator
    {
        private static readonly List<char> Consonants = new List<char>
        {
            'b',
            'c',
            'd',
            'f',
            'g',
            'h',
            'j',
            'k',
            'l',
            'm',
            'n',
            'p',
            'q',
            'r',
            's',
            't',
            'v',
            'w',
            'x',
            'y',
            'z'
        };

        private readonly Thesaurus.Thesaurus _thesaurus = new Thesaurus.Thesaurus();
        private VerySmartOptions _options;
        private readonly IDictionary<string, List<IUsage>> _cache = new Dictionary<string, List<IUsage>>();

        public VerySmartGenerator()
        {
            UsageResolver = (s, list) => list.First(); // Return the first usage in the list.
            Options = new VerySmartOptions();
        }

        /// <summary>
        /// <returns><c>null</c> if the usage cannot be resolved.</returns>
        /// </summary>
        [NotNull]
        public Func<string, List<IUsage>, IUsage> UsageResolver { get; set; }

        public VerySmartOptions Options
        {
            get { return _options; }
            set
            {
                if ( value == null )
                {
                    return;
                }
                _options = value;
            }
        }

        public event WordMadeSmartHandler WordMadeSmart;

        public string MakeMeVerySmart(string input)
        {
            var stringBuilder = new StringBuilder();
            var terms = input.Split( ' ' );
            foreach (var term in terms)
            {
                if ( Options.IgnoreList.Contains( term.ToLower() ) )
                {
                    stringBuilder.Append( $" {term}" );
                    OnWordMadeSmart( term );
                    continue;
                }

                var verysmartWord = HitUpThesaurus( term );
                stringBuilder.Append( $" {verysmartWord}" );
                OnWordMadeSmart( verysmartWord );
            }

            var verysmartText = stringBuilder.ToString();
            verysmartText = FixGrammar( verysmartText );

            return verysmartText;
        }

        private string HitUpThesaurus(string term)
        {
            var searchTerm = term;

            // If the term contains a dot it's most likely the last word in a sentence.
            // But let's remove the dot, perform the search, and re-apply the dot later
            // when we've found a suitable synonym.
            var hasDot = term.Last() == '.';
            if ( hasDot )
            {
                searchTerm = term.Substring( 0, term.Length - 1 );
            }
            var usages = GetUsages( searchTerm );

            var verysmartWord = term;
            List<IWord> synonyms;
            if ( TryGetSynonyms( term, usages, out synonyms ) )
            {
                verysmartWord = SelectVerySmartWordFromSynonyms( synonyms );
            }
            else
            {
                return term;
            }

            // Re-apply the dot we removed.
            verysmartWord = hasDot ? verysmartWord + "." : verysmartWord;
            return verysmartWord;
        }

        private List<IUsage> GetUsages(string searchTerm)
        {
            if ( _cache.ContainsKey( searchTerm ) )
            {
                return _cache[searchTerm];
            }
            var usages = _thesaurus.GetUsages( searchTerm );
            _cache[searchTerm] = usages;
            return usages;
        }

        private string SelectVerySmartWordFromSynonyms(List<IWord> synonyms)
        {
            // Remove words that contain spaces
            synonyms.RemoveAll( s => s.Text.Contains( ' ' ) );
            var selectionAlgorithm = GetSelectionAlgorithm();
            return selectionAlgorithm( FilterSynonymList( synonyms ) );
        }

        private List<IWord> FilterSynonymList(List<IWord> synonyms)
        {
            if ( _options.Complexity != WordComplexity.All
                 && synonyms.Any( s => (int)s.Complexity == (int)_options.Complexity ) )
            {
                return synonyms.Where( s => (int)s.Complexity == (int)_options.Complexity )
                               .ToList();
            }
            return synonyms;
        }

        private Func<List<IWord>, string> GetSelectionAlgorithm()
        {
            switch (Options.SynonymSelectionMode)
            {
                case SynonymSelectionMode.Longest:
                    return Selections.LongestWord;
                case SynonymSelectionMode.Random:
                    return Selections.RandomWord;
                default:
                    goto case SynonymSelectionMode.Longest;
            }
        }

        /// <summary>
        ///     Returns true if the selected usage has any synonyms.
        ///     If the user selects a usage that has no synonyms, the false is returned.
        /// </summary>
        /// <param name="term"></param>
        /// <param name="usages"></param>
        /// <param name="synonyms"></param>
        /// <returns>True if the selected usage has any synonyms.</returns>
        private bool TryGetSynonyms(string term, List<IUsage> usages, out List<IWord> synonyms)
        {
            switch (usages.Count)
            {
                case 0:
                    synonyms = new List<IWord>();
                    break;
                case 1:
                    synonyms = usages.First()
                                     .Synonyms.ToList();
                    break;
                default:
                    var usage = UsageResolver( term, usages );
                    synonyms = usage?.Synonyms.ToList() ?? new List<IWord>();
                    break;
            }

            return synonyms.Count > 0;
        }

        private static string FixGrammar(string text)
        {
            var chunks = text.Split( ' ' );
            var index = -1;
            while ((index = Array.FindIndex(chunks, index + 1, IsAnOrA)) != -1)
            {
                if ( index + 1 > chunks.Length )
                {
                    break;
                }

                var firstLetterOfNextWord = chunks[index + 1].First();
                var word = chunks[index];
                switch (word)
                {
                    case "a":
                        if ( IsVowel( firstLetterOfNextWord ) )
                        {
                            chunks[index] = "an";
                        }
                        break;
                    case "an":
                        if ( IsConsonant( firstLetterOfNextWord ) )
                        {
                            chunks[index] = "a";
                        }
                        break;
                }
            }

            return string.Join( " ", chunks );
        }

        private static bool IsAnOrA(string s) => s == "a" || s == "an";
        private static bool IsConsonant(char c) => Consonants.Contains( c );
        private static bool IsVowel(char c) => !IsConsonant( c );

        protected virtual void OnWordMadeSmart(string word)
        {
            WordMadeSmart?.Invoke( word );
        }
    }
}
