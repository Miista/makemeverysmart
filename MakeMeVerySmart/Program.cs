using System;
using System.Collections.Generic;
using System.Linq;
using Thesaurus;

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
                if ( string.IsNullOrEmpty( input ) || string.IsNullOrWhiteSpace( input ))
                {
                    Environment.Exit( 0 );
                }
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
                            if ( int.TryParse( input, out selection ) )
                            {
                                var option = options[selection];
                                Config.Options[option] = !Config.Options[option];
                                goto case 0;
                            }
                            break;
                        case 1:
                            Console.Write( "Please input sentence:" );
                            input = Console.ReadLine();
                            if ( string.IsNullOrWhiteSpace( input )
                                 || string.IsNullOrEmpty( input ) )
                            {
                                goto case 1;
                            }
                            var trimmedInput = input.Trim();
                            var makeMeVerySmart = MakeMeVerySmart( trimmedInput );
                            Console.WriteLine( makeMeVerySmart );
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        private static string MakeMeVerySmart(string sentence)
        {
            var words = sentence.Split( ' ' );
            var api = new global::Thesaurus.Thesaurus();
            var chosenWords = new List<string>();
            foreach (var word in words)
            {
                if ( _ignores.Contains( word ) )
                {
                    chosenWords.Add( word );
                    continue;
                }
                var result = api.GetUsages( word );
                if ( result.Count > 1 )
                {
                    var synonyms = GetSynonymList( word, result );
                    var synonym = ChooseTheWord( synonyms.ToList() );
                    chosenWords.Add( synonym );
                }
                else if ( result.Count == 1 )
                {
                    var synonyms = result.First().Synonyms;
                    var synonym = ChooseTheWord( synonyms.ToList() );
                    chosenWords.Add( synonym );
                }
                else
                {
                    chosenWords.Add( word );
                }
            }
            return string.Join( " ", chosenWords );
        }

        private static IReadOnlyList<IWord> GetSynonymList(string word, List<IUsage> usages)
        {
            if ( Config.Options[Config.OptionWarnOnMultipleUsages] )
            {
                Console.WriteLine( $"Multiple usages found for \"{word}\". Please select the most fitting:" );
                for (var i = 0; i < usages.Count; i++)
                {
                    Console.WriteLine( $"[{i}] {usages[i].Text}" );
                }
                var input = Console.ReadLine();
                if ( input != null )
                {
                    var selection = int.Parse( input );
                    return usages[selection].Synonyms;
                }
            }
            return usages.First().Synonyms;
        }

        private static string ChooseTheWord(List<IWord> synonyms)
        {
            if ( Config.Options[Config.OptionExcludeWordsWithSpaces] )
            {
                synonyms.RemoveAll( s => s.Text.Contains( " " ) );
            }
            if ( synonyms.Count == 0 )
            {
                return null;
            }
            if ( Config.Options[Config.OptionSelectRandom] )
            {
                return Selections.RandomWord( synonyms.Select( s => s.Text )
                                                      .ToList() );
            }
            if ( Config.Options[Config.OptionSelectLongestWord] )
            {
                return Selections.LongestWord( synonyms.Select( s => s.Text )
                                                       .ToList() );
            }
            return synonyms.First().Text;
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

    public class Selections
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
