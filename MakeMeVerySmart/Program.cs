using System;
using System.Collections.Generic;
using System.Linq;
using Thesaurus;
using VerySmart_Core;
using WordComplexity = VerySmart_Core.WordComplexity;

namespace MakeMeVerySmart
{
    internal class Program
    {
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
            var generator = new VerySmartGenerator
            {
                UsageResolver = GetSynonymList
            };
            var options = new VerySmartOptions
            {
                SynonymSelectionMode = Config.Options[Config.OptionSelectRandom]
                    ? SynonymSelectionMode.Random
                    : SynonymSelectionMode.Longest,
                Complexity = WordComplexity.All
            };
            generator.Options = options;
            return generator.MakeMeVerySmart( sentence );
        }

        private static IUsage GetSynonymList(string word, List<IUsage> usages)
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
                    return usages[selection];
                }
            }
            return usages.First();
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

    public class ThesaurusEntry
    {
        public string Text { get; set; }
        public IDictionary<string, List<string>> Usages { get; internal set; } = new Dictionary<string, List<string>>();
    }
}
