namespace Thesaurus
{
    public interface IWord
    {
        string Text { get; }
        int Complexity { get; }
    }

    public class Word : IWord
    {
        public string Text { get; internal set; }
        public int Complexity { get; internal set; }
    }
}
