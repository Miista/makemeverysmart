using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeMeVerySmart
{
    internal class Program
    {
        private static readonly IReadOnlyList<string> _ignores = new List<string>
        {
            "i",
            "am"
        };

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine( "[0] Change config" );
                Console.WriteLine( "[1] Make Me VerySmart" );
                var input = Console.ReadLine();
                if ( input != null )
                {
                    int selection;
                    if ( int.TryParse( input, out selection ) )
                    {
                        switch (selection)
                        {
                            case 0:
                                Console.WriteLine( "<< Config (any letter = exit) >>" );
                                var options = Config.Options.Keys.ToList();
                                for (var i = 0; i < options.Count; i++)
                                {
                                    var option = options[i];
                                    var value = Config.Options[option];
                                    Console.WriteLine( $"[{i}] {option}. {value}" );
                                }
                                Console.Write( "Toggle option: " );
                                input = Console.ReadLine();
                                if ( input != null )
                                {
                                    if ( int.TryParse( input, out selection ) )
                                    {
                                        var option = options[selection];
                                        Config.Options[option] = !Config.Options[option];
                                        goto case 0;
                                    }
                                }
                                break;
                            case 1:
                                Console.Write("Please input sentence:");
                                input = Console.ReadLine();
                                if (input != null)
                                {
                                    var trimmedInput = input.Trim();
                                    var makeMeVerySmart = MakeMeVerySmart(trimmedInput);
                                    Console.WriteLine(makeMeVerySmart);
                                }
                                break;
                            default:
                                continue;
                        }
                    }
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
            if ( Config.Options[Config.OptionWarnOnMultipleUsages] )
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
            if ( Config.Options[Config.OptionExcludeWordsWithSpaces] )
            {
                synonyms.RemoveAll( s => s.Contains( " " ) );
            }
            if ( synonyms.Count == 0 )
            {
                return null;
            }
            if ( Config.Options[Config.OptionSelectRandom] )
            {
                return Selections.RandomWord( synonyms );
            }
            if ( Config.Options[Config.OptionSelectLongestWord] )
            {
                return Selections.LongestWord( synonyms );
            }
            return synonyms.First();
        }
    }

    internal class Config
    {
        public const string OptionWarnOnMultipleUsages = "Warn when multiple usages exists";
        public const string OptionSelectLongestWord = "Select the longest word";
        public const string OptionSelectRandom = "Select a random word";
        public const string OptionExcludeWordsWithSpaces = "Exclude words with spaces";

        public static IDictionary<string, bool> Options { get; } = new Dictionary<string, bool>()
        {
            { OptionWarnOnMultipleUsages, false },
            { OptionSelectLongestWord, false },
            { OptionSelectRandom, false },
            { OptionExcludeWordsWithSpaces, false }
        };
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
