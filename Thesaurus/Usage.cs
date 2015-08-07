namespace Thesaurus
{
    public interface IUsage
    {
        string Text { get; }
        WordType Type { get; }
    }

    public class Usage : IUsage
    {
        public string Text { get; }
        public WordType Type { get; }
    }

    public enum WordType
    {
        Verb,
        Adjective,
        Noun
    }
}
