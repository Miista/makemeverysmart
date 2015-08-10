using System.Collections.Generic;

namespace VerySmart_Core
{
    public enum SynonymSelectionMode
    {
        Longest,
        Random,
        Complexity
    }

    public class VerySmartOptions
    {
        /// <summary>
        ///     Defaults to Longest
        /// </summary>
        public SynonymSelectionMode SynonymSelectionMode { get; set; }

        public List<string> IgnoreList { get; } = new List<string>
        {
            "the",
            "i",
            "am",
            "and",
            "or"
        };
    }
}