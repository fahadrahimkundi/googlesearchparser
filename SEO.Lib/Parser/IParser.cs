using SEO.Lib.Objects.Business;
using System.Collections.Generic;

namespace SEO.Lib.Parser
{
    public interface IParser
    {
        List<SearchRecord> FindTopPosition(string siteURL, string keyword, int maxRecords);
    }
}