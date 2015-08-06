using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace MakeMeVerySmart
{
    public interface IThesaurus
    {
        ThesaurusEntry GetEntry(string lookupText);
    }


    internal class Thesaurus : IThesaurus
    {
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
}