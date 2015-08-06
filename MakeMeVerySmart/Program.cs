using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeMeVerySmart
{
    internal class Program
    {
        private static bool _selectLongestWord = false;
        private static bool _selectRandomWord = true;
        private static bool _excludeWordsWithSpaces = true;
        private static bool _warnOnMultipleUsages = true;

        private static readonly IReadOnlyList<string> _ignores = new List<string>
        {
            "i",
            "am"
        };

        private static void Main(string[] args)
        {
            Console.WriteLine( "<< Config >>" );
            Console.WriteLine( $"Warn when multiple usages exists: {_warnOnMultipleUsages}" );
            Console.WriteLine( $"Select the longest word: {_selectLongestWord}" );
            Console.WriteLine( $"Select a random word: {_selectRandomWord}" );
            Console.WriteLine( $"Exclude words with spaces: {_excludeWordsWithSpaces}" );
            while (true)
            {
                Console.Write( "Please input sentence:" );
                var input = Console.ReadLine();
                if ( input != null )
                {
                    var trimmedInput = input.Trim();
                    var makeMeVerySmart = MakeMeVerySmart( trimmedInput );
                    Console.WriteLine( makeMeVerySmart );
                }
            }
        }

        private static string MakeMeVerySmart(string sentence)
        {
            var words = sentence.Split( ' ' );
            var api = new Thesaurus();
            var chosenWords = new List<string>();
            foreach (var word in words)
            {
                if ( _ignores.Contains( word ) )
                {
                    chosenWords.Add( word );
                    continue;
                }
                var result = api.GetEntry( word );
                if ( result.Usages.Keys.Count > 1 )
                {
                    var synonyms = GetSynonymList( word, result.Usages );
                    var synonym = ChooseTheWord( synonyms );
                    chosenWords.Add( synonym );
                }
                else if ( result.Usages.Keys.Count == 1 )
                {
                    var synonyms = result.Usages.Values.First();
                    var synonym = ChooseTheWord( synonyms );
                    chosenWords.Add( synonym );
                }
                else
                {
                    chosenWords.Add( word );
                }
            }
            return string.Join( " ", chosenWords );
        }

        private static List<string> GetSynonymList(string word, IDictionary<string, List<string>> usages)
        {
            if ( _warnOnMultipleUsages )
            {
                Console.WriteLine( $"Multiple usages found for \"{word}\". Please select the most fitting:" );
                var keys = usages.Keys.ToList();
                for (var i = 0; i < keys.Count; i++)
                {
                    Console.WriteLine( $"[{i}] {keys[i]}" );
                }
                var input = Console.ReadLine();
                if ( input != null )
                {
                    var selection = int.Parse( input );
                    return usages[keys[selection]];
                }
            }
            return usages.Values.First();
        }

        private static string ChooseTheWord(List<string> synonyms)
        {
            if ( _excludeWordsWithSpaces )
            {
                synonyms.RemoveAll( s => s.Contains( " " ) );
            }
            if ( synonyms.Count == 0 )
            {
                return null;
            }
            if ( _selectRandomWord )
            {
                return Selections.RandomWord( synonyms );
            }
            if ( _selectLongestWord )
            {
                return Selections.LongestWord( synonyms );
            }
            return synonyms.First();
        }
    }

    internal class Selections
    {
        public static string LongestWord(IReadOnlyList<string> list) => list.Aggregate( (s1, s2) => s1.Length < s2.Length
            ? s2
            : s1 );

        public static string RandomWord(IReadOnlyList<string> synonyms)
        {
            var random = new Random();
            return synonyms[random.Next( 0, synonyms.Count - 1 )];
        }
    }

    public class ThesaurusEntry
    {
        public string Text { get; set; }
        public IDictionary<string, List<string>> Usages { get; internal set; } = new Dictionary<string, List<string>>();
    }
}
