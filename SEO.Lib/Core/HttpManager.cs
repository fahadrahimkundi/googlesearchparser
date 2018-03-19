using SEO.Lib.Utilities;
using System;
using System.IO;
using System.Net;

namespace SEO.Lib.Core
{
    public class HttpManager : INetworkManager
    {
        public string GetWebResponse(string p_URL)
        {
            try
            {
                //If URL is empty then don't proceed and throw exception
                if (string.IsNullOrEmpty(p_URL))
                {
                    throw new ArgumentNullException(Constants.ExceptionParams.URL, Constants.ErrorMessages.HttpManager_URL_MSG);
                }

                HttpWebRequest l_Request = (HttpWebRequest)WebRequest.Create(p_URL);

                using (HttpWebResponse l_Response = (HttpWebResponse)l_Request.GetResponse())
                {
                    using (Stream l_Stream = l_Response.GetResponseStream())
                    {
                        using (StreamReader l_Reader = new StreamReader(l_Stream))
                        {
                            return l_Reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log4netWrapper.Error(e);
                throw e;
            }
        }
    }
}