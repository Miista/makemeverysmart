using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace MakeMeVerySmart
{
    internal class Program
    {
        private static bool _selectLongestWord = false;
        private static bool _selectRandomWord = true;
        private static bool _excludeWordsWithSpaces = true;
        private static bool _warnOnMultipleUsages = true;

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
                var result = api.GetEntry( word );
                if ( result.Usages.Keys.Count > 1 )
                {
                    var synonym = FindGoodSynonym( result, word );
                    chosenWords.Add( synonym );
                }
                else if ( result.Usages.Keys.Count == 1 )
                {
                    var synonym = ChooseTheWord( result.Usages.Values.First() );
                    chosenWords.Add( synonym );
                }
                else
                {
                    chosenWords.Add( word );
                }
            }
            return string.Join( " ", chosenWords );
        }

        private static string FindGoodSynonym(ThesaurusEntry result, string word)
        {
            var synonyms = result.Usages.Values.First();
            if ( _warnOnMultipleUsages )
            {
                Console.WriteLine( $"Multiple usages found for \"{word}\". Please select the most fitting:" );
                var keys = result.Usages.Keys.ToList();
                for (var i = 0; i < keys.Count; i++)
                {
                    Console.WriteLine( $"[{i}] {keys[i]}" );
                }
                var input = Console.ReadLine();
                if ( input != null )
                {
                    var selection = int.Parse( input );
                    synonyms = result.Usages[keys[selection]];
                }
            }
            if ( _excludeWordsWithSpaces )
            {
                synonyms.RemoveAll( s => s.Contains( " " ) );
            }
            return ChooseTheWord( synonyms ) ?? word;
        }

        private static string ChooseTheWord(IReadOnlyList<string> synonyms)
        {
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

    public interface IThesaurus
    {
        IReadOnlyList<string> GetSynonymsDeprecated(string word);

        ThesaurusEntry GetEntry(string lookupText);
    }

    internal class Thesaurus : IThesaurus
    {
        public IReadOnlyList<string> GetSynonymsDeprecated(string word)
        {
            var webRequest = WebRequest.Create( $"http://www.thesaurus.com/browse/{word}" );
            var htmlDocument = new HtmlDocument();
            var responseStream = webRequest.GetResponse().GetResponseStream();
            if ( responseStream == null )
            {
                return new List<string>();
            }
            var streamReader = new StreamReader( responseStream );
            htmlDocument.Load( streamReader );
            var enumerable =
                htmlDocument.DocumentNode.SelectNodes(
                    "//div[@id='synonyms-0']//div[@class='relevancy-list']//ul//li//a//span[@class='text']" );
            if ( enumerable == null )
            {
                return new List<string>();
            }
            var synonyms = enumerable.Select( n => n.InnerText );
            return synonyms.ToList();
        }

        public ThesaurusEntry GetEntry(string lookupText)
        {
            var entry = new ThesaurusEntry();
            entry.Text = lookupText;

            var webRequest = WebRequest.Create( $"http://www.thesaurus.com/browse/{lookupText}" );
            using (var responseStream = webRequest.GetResponse().GetResponseStream())
            {
                if ( responseStream == null )
                {
                    return entry;
                }
                var streamReader = new StreamReader( responseStream );
                var htmlDocument = new HtmlDocument();
                htmlDocument.Load( streamReader );

                entry.Usages = ExtractSynonyms( htmlDocument );
            }
            return entry;
        }

        private IDictionary<string, List<string>> ExtractSynonyms(HtmlDocument doc)
        {
            var dictionary = new Dictionary<string, List<string>>();
            var usages =
                doc.DocumentNode.SelectNodes( "//div[@id='words-gallery']//div//a//strong//text()" );
            if ( usages == null )
            {
                return dictionary;
            }

            var usagesKeys = usages.Select( n => n.InnerText ).ToList();
            for (var i = 0; i < usagesKeys.Count; i++)
            {
                var key = usagesKeys[i];
                var synonyms = doc.DocumentNode.SelectNodes(
                    $"//div[@id='synonyms-{i}']//div[@class='relevancy-list']//a//span[@class='text']" );

                dictionary[key] = synonyms.Select( n => n.InnerText ).ToList();
            }
            return dictionary;
        }
    }

    public class ThesaurusEntry
    {
        public string Text { get; set; }
        public IDictionary<string, List<string>> Usages { get; internal set; } = new Dictionary<string, List<string>>();
    }
}
