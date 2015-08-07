using System.Collections.Generic;

namespace Thesaurus
{
    public interface IUsage
    {
        string Text { get; }
        WordType Type { get; }
        IReadOnlyList<string> Synonyms { get; }
    }

    internal class Usage : IUsage
    {
        public string Text { get; set; }
        public WordType Type { get; set; }
        public IReadOnlyList<string> Synonyms { get; set; }
    }

    public enum WordType
    {
        Verb,
        Adjective,
        Noun,
        Unknown
    }
}
