using System;
using System.Collections.Generic;
using System.Linq;

namespace VerySmart_Core
{
    public class Selections
    {
        public static string LongestWord(IReadOnlyList<string> list)
            => list.Aggregate( (s1, s2) => s1.Length < s2.Length
                ? s2
                : s1 );

        public static string RandomWord(IReadOnlyList<string> synonyms)
        {
            var random = new Random();
            return synonyms[random.Next( 0, synonyms.Count - 1 )];
        }
    }
}
