using System;
using System.Collections.Generic;
using System.Linq;
using Thesaurus;

namespace VerySmart_Core
{
    public class Selections
    {
        public static string LongestWord(List<IWord> list)
            => list.Select( w => w.Text )
                   .Aggregate( (s1, s2) => s1.Length < s2.Length
                       ? s2
                       : s1 );

        public static string RandomWord(List<IWord> synonyms)
        {
            var random = new Random();
            return synonyms[random.Next( 0, synonyms.Count - 1 )].Text;
        }

        public static string MostComplex(List<IWord> synonyms)
        {
            var maxComplexity = synonyms.Max(w => w.Complexity);
            return RandomWord( synonyms.Where( w => w.Complexity == maxComplexity ).ToList() );
        }
    }
}
