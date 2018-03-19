using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEO.Lib.Core;
using SEO.Lib.Parser;
using SEO.Lib.Utilities;

namespace SEO.Lib.Tests
{
    [TestClass]
    public class LibCoreTest
    {
        [TestMethod]
        public void HttpManager_ArgumentNullExceptionWithEmptyURL()
        {
            try
            {
                var l_HttpManager = new HttpManager();

                string l_URL = string.Empty;

                l_HttpManager.GetWebResponse(l_URL);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(Constants.ExceptionParams.URL, ex.ParamName);
            }
        }

        [TestMethod]
        public void HttpManager_ResultWithProperURL()
        {
            var l_HttpManager = new HttpManager();

            string l_URL = "https://www.google.com.au/search?num=100&q=development";

            string l_Result = l_HttpManager.GetWebResponse(l_URL);

            Assert.IsNotNull(l_Result);
        }

        [TestMethod]
        public void InvalidUrl_InvalidKeyword()
        {
            IParser l_Parser = new GoogleParser(new HttpManager());

            var l_Result = l_Parser.FindTopPosition("invalidURL", "xya", 100);

            Assert.AreEqual(0, l_Result.Count);
        }
        [TestMethod]
        public void ValidUrl_InvalidKeyword()
        {
            IParser parser = new GoogleParser(new HttpManager());

            var l_Result = parser.FindTopPosition("invalidURL", "xya", 100);

            Assert.AreEqual(0, l_Result.Count);
        }
        [TestMethod]
        public void InvalidUrl_ValidKeyword()
        {
            IParser l_Parser = new GoogleParser(new HttpManager());

            var l_Result = l_Parser.FindTopPosition("invalidURL", "xya", 100);

            Assert.AreEqual(0, l_Result.Count);
        }
        [TestMethod]
        public void ValidUrl_ValidKeyword()
        {
            IParser l_Parser = new GoogleParser(new HttpManager());

            var l_Result = l_Parser.FindTopPosition("http://www.espncricinfo.com", "cricket", 100);

            Assert.AreEqual(1, l_Result.Count);
        }
    }
}
