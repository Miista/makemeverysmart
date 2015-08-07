using System.Collections.Generic;

namespace Thesaurus
{
    public interface IThesaurus
    {
        List<IUsage> GetUsages(string term);
    }
}