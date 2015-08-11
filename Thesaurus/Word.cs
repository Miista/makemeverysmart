namespace Thesaurus
{
    public interface IWord
    {
        string Text { get; }
        WordComplexity Complexity { get; }
    }

    public enum WordComplexity
    {
        LowComplexity = 1,
        MediumComplexity = 2,
        HighComplexity = 3
    }

    public class Word : IWord
    {
        public string Text { get; internal set; }
        public WordComplexity Complexity { get; internal set; }
    }
}
