using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VertoExcercise.Models;

namespace VertoExcercise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "";
            var client = new WebClients();
            ImageTitles imageTitles = new ImageTitles();

            var firstQuery = client.GetWikiGeoSearch();
            imageTitles.SearchQuery = client.SearchQuery;
            var wikiGeoSearchRoot = firstQuery;
            var wikiGeoSearchQuery = wikiGeoSearchRoot.query;
            var wikiGeoSearches = wikiGeoSearchQuery.Geosearch;

            List<string> titles = new List<string>();
            foreach (var geoSearch in wikiGeoSearches)
            {
                var innerResult = client.GetWikiImages(geoSearch.Pageid);
                
                string title = string.Empty;
                foreach (var wikiImage in  innerResult.Images)
                {
                    if (wikiImage.Title.StartsWith("File:"))
                        title = wikiImage.Title.Substring("File:".Length );
                    else
                        title = wikiImage.Title;
                    titles.Add(title);
                }
            }


            var fullImageList = getSimilarTitles(titles);
            List<LevenshteinMetrics> sortedImages = fullImageList.OrderBy(m => m.Distance).Take(50).ToList();
            imageTitles.SimilarTitles = sortedImages;
            return View(imageTitles);
        }

        private List<LevenshteinMetrics> getSimilarTitles(List<string> titles)
        {
            List<LevenshteinMetrics> results = new List<LevenshteinMetrics>();
            foreach (var titleS in titles)
            {
                foreach (var titleT in titles)
                {
                    if (titleS.Equals(titleT))
                        continue;
                    LevenshteinMetrics metric = new LevenshteinMetrics(titleS, titleT);
                    results.Add(metric);
                }
            }
            return results;
        }



        public ActionResult About()
        {
            ViewData["Message"] = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["Message"] = "";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}