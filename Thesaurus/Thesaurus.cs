using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace Thesaurus
{
    public class Thesaurus : IThesaurus
    {
        [NotNull]
        public List<IUsage> GetUsages([NotNull] string term)
        {
            var request = WebRequest.Create( $"http://www.thesaurus.com/browse/{term}" );
            using (var response = request.GetResponse()
                                         .GetResponseStream())
            {
                if ( response != null )
                {
                    var reader = new StreamReader( response );
                    var doc = new HtmlDocument();
                    doc.Load( reader );
                    var nodes = doc.DocumentNode.SelectNodes( "//div[@id='words-gallery']//li//a" );
                    if ( nodes != null )
                    {
                        return nodes.Select( CreateUsageNode )
                                    .ToList();
                    }
                }
            }
            return new List<IUsage>();
        }

        private static IUsage CreateUsageNode(HtmlNode node)
        {
            var typeNode = node.SelectSingleNode( ".//em/text()" );
            var textNode = node.SelectSingleNode( ".//strong/text()" );
            return new Usage
            {
                Text = textNode.InnerText,
                Type = GetWordType( typeNode ),
                Synonyms = ExtractSynonyms( node )
            };
        }

        private static IReadOnlyList<string> ExtractSynonyms(HtmlNode node)
        {
            var c = node.Id.Last()
                        .ToString();
            int tabPosition;
            if ( int.TryParse( c, out tabPosition ) )
            {
                var synonyms =
                    node.SelectNodes(
                        $"//div[@id='synonyms-{tabPosition}']//div[@class='relevancy-list']//span[@class='text']/text()" );
                return synonyms.Select( n => n.InnerText )
                               .ToList();
            }
            return new List<string>();
        }

        private static WordType GetWordType(HtmlNode typeNode)
        {
            switch (typeNode.InnerText.ToLower())
            {
                case "adj":
                    return WordType.Adjective;
                case "verb":
                    return WordType.Verb;
                case "noun":
                    return WordType.Noun;
                case "prep":
                    return WordType.Preposition;
                case "adv":
                    return WordType.Adverbium;
                default:
                    return WordType.Unknown;
            }
        }
    }
}
