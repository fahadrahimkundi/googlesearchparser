using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEO.Lib.Core;
using SEO.Lib.Parser;
using SEO.Web.Controllers;
using System.Web.Mvc;

namespace SEO.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Parser()
        {
            // Arrange
            IParser l_Parser = new GoogleParser(new HttpManager());
            string l_SiteURL = "http://www.espncricinfo.com";
            string l_Keyword = "cricket";
            HomeController l_Controller = new HomeController(l_Parser);

            // Act
            ViewResult l_Result = l_Controller.Parser(l_SiteURL, l_Keyword) as ViewResult;

            // Assert
            Assert.IsNotNull(l_Result);
        }
    }
}