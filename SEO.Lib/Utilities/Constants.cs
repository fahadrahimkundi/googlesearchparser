namespace SEO.Lib.Utilities
{
    static public class Constants
    {
        static public class REGEX
        {
            public const string DIV_REGEX = "<div class=\"g\".*?>(.*?)(<\\/div>)";
            public const string H3_REGEX = "<h3 class=\"r\".*?>(.*?)(<\\/h3>)";
            public const string ATAG_REGEX = "<a.*?>(.*?)(<\\/a>)";
        }

        static public class RegexIndex
        {
            public const int GROUP = 1;
            public const int VALUE = 0;
        }

        static public class SEARCHPARAMS
        {
            public const string GOOGLE_SEARCH_URL = "https://www.google.com.au/search?";
            public const int MAX_TOP_POSTIONS = 100;
        }

        static public class ErrorMessages
        {
            public const string NetworkManager_MSG = "NetworkManager is not initialized";
            public const string HttpManager_URL_MSG = "URL must be initialized for Http Manager";
        }

        static public class ExceptionParams
        {
            public const string URL = "URL";
            public const string HttpManager_URL_MSG = "URL must be initialized for Http Manager";
        }
    }
}