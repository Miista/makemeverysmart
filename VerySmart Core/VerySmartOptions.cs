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
    }
}