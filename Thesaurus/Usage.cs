using System.Collections.Generic;

namespace Thesaurus
{
    public interface IUsage
    {
        string Text { get; }
        WordType Type { get; }
        IReadOnlyList<IWord> Synonyms { get; }
    }

    internal class Usage : IUsage
    {
        public string Text { get; set; }
        public WordType Type { get; set; }
        public IReadOnlyList<IWord> Synonyms { get; set; }
    }

    public enum WordType
    {
        Verb,
        Adjective,
        Noun,
        Preposition,
        Unknown,
        Adverbium
    }
}
