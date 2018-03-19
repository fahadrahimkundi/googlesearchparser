using SEO.Lib.Core;
using SEO.Lib.Objects.Business;
using SEO.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace SEO.Lib.Parser
{
    public class GoogleParser : IParser
    {
        #region "Members Variables"

        private INetworkManager _NetworkManager;

        #endregion

        #region "Constructor"

        public GoogleParser(INetworkManager p_NetworkManager)
        {
            if(p_NetworkManager == null)
            {
                throw new ArgumentNullException(Constants.ErrorMessages.NetworkManager_MSG);
            }
            else
            {
                _NetworkManager = p_NetworkManager;
            }
        }

        #endregion

        #region "Private Methods"

        private string ParserUrl(string p_URL, string p_Keyword, int p_MaxRecords)
        {
            return p_URL
                + @"num=" + p_MaxRecords
                + @"&q=" + HttpUtility.UrlEncode(p_Keyword);
        }
        
        private List<SearchRecord> ParseResults(string p_WebResponse, string p_SiteURL)
        {
            int l_SitePosition = 1;
            List<SearchRecord> l_SearchRecords = new List<SearchRecord>();

            //Get DIV level tags from html
            foreach (Match l_GroupMatch in Regex.Matches(p_WebResponse, Constants.REGEX.DIV_REGEX))
            {
                if (l_GroupMatch != null && l_GroupMatch.Groups.Count > 0)
                {
                    //Get H3 level tags from html
                    foreach (Match l_ResultMatch in Regex.Matches(l_GroupMatch.Groups[Constants.RegexIndex.GROUP].Value, Constants.REGEX.H3_REGEX))
                    {
                        if (l_ResultMatch != null && l_ResultMatch.Groups.Count > 0)
                        {
                            //Get A level tags from html
                            MatchCollection l_LinktMatchs = Regex.Matches(l_ResultMatch.Groups[Constants.RegexIndex.GROUP].Value, Constants.REGEX.ATAG_REGEX);

                            if (l_LinktMatchs.Count > 0 && l_LinktMatchs[Constants.RegexIndex.VALUE].Groups.Count > 0)
                            {
                                if (l_LinktMatchs[Constants.RegexIndex.VALUE].Value.Contains(p_SiteURL))
                                {
                                    Log4netWrapper.Debug(string.Format("Site link [ {0} ] found in URL [ {1} ]", p_SiteURL, l_LinktMatchs[Constants.RegexIndex.VALUE].Value));

                                    SearchRecord l_MatchRecord = new SearchRecord();

                                    l_MatchRecord.URLDetails = l_LinktMatchs[Constants.RegexIndex.VALUE].Value;
                                    l_MatchRecord.Position = l_SitePosition;

                                    l_SearchRecords.Add(l_MatchRecord);
                                }
                            }
                        }
                    }
                }

                l_SitePosition++;
            }

            return l_SearchRecords;
        }

        #endregion

        #region "Member Functions"
        public List<SearchRecord> FindTopPosition(string p_SiteURL, string p_Keyword, int p_MaxRecords)
        {
            string l_Url = ParserUrl(Constants.SEARCHPARAMS.GOOGLE_SEARCH_URL, p_Keyword, p_MaxRecords);

            Log4netWrapper.Debug("Complete google Search URL with params : " + l_Url);
            
            string l_WebResponse = _NetworkManager.GetWebResponse(l_Url);

            if (!string.IsNullOrEmpty(l_WebResponse))
            {
                Log4netWrapper.Debug("Web response found for : " + l_Url);

                return ParseResults(l_WebResponse, p_SiteURL);
            }
            else
            {
                Log4netWrapper.Debug("Web response not found for : " + l_Url);
                return null;
            }
        }

        #endregion
    }
}