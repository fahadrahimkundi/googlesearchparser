using SEO.Lib.Objects.Business;
using SEO.Lib.Parser;
using SEO.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SEO.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParser _Parser;

        public HomeController(IParser p_Parser)
        {
            this._Parser = p_Parser;
        }

        public ActionResult Parser(string siteURL, string keyword)
        {
            try
            {
                //Storing params to ViewBag to send them back for the view
                ViewBag.keyword = keyword;
                ViewBag.siteURL = siteURL;

                //If params are empty then return 0
                if (string.IsNullOrEmpty(siteURL) || string.IsNullOrEmpty(keyword))
                {
                    ViewBag.Results = "0";
                }
                else
                {
                    // Calling parser method for processing
                    List<SearchRecord> searchRecords = _Parser.FindTopPosition(siteURL, keyword, Constants.SEARCHPARAMS.MAX_TOP_POSTIONS);

                    //If get records then concat the positions else return 0
                    if (searchRecords != null && searchRecords.Count > 0)
                    {
                        string l_Results = string.Empty;

                        foreach (SearchRecord record in searchRecords)
                        {
                            l_Results = string.IsNullOrEmpty(l_Results) ? record.Position.ToString() : (l_Results + "," + record.Position);
                        }

                        ViewBag.Results = l_Results;
                    }
                    else
                    {
                        ViewBag.Results = "0";
                    }
                }
            }
            catch (Exception e)
            {
                /*
                 * In case of exception like network issue
                    Log exception
                    Return 0
                    Return Error Message
                NOTE Currently return system error messages
                */

                Log4netWrapper.Error(e);

                ViewBag.Results = "0";
                ViewBag.Message = e.Message;
            }

            return View();
        }
    }
}