using System.Collections.Generic;

namespace Thesaurus
{
    public interface IThesaurus
    {
        ICollection<IUsage> GetUsages(string word);
    }
}