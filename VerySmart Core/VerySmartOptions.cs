using System.Collections.Generic;

namespace VerySmart_Core
{
    public enum SynonymSelectionMode
    {
        Longest,
        Random
    }

    public class VerySmartOptions
    {
        /// <summary>
        ///     Defaults to Longest
        /// </summary>
        public SynonymSelectionMode SynonymSelectionMode { get; set; }

        public WordComplexity Complexity { get; set; }

        public List<string> IgnoreList { get; } = new List<string>
        {
            "the",
            "i",
            "am",
            "and",
            "or"
        };
    }

    /// <summary>
    ///     Enum values match 1-to-1 with the values from Thesaurus.WordComplexity.
    /// </summary>
    public enum WordComplexity
    {
        All = 0,
        LowComplexity = 1,
        MediumComplexity = 2,
        HighComplexity = 3
    }
}