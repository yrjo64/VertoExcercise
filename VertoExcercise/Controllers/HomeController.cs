﻿using System;
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
                var innerPage = client.GetWikiImages(geoSearch.Pageid);
                var innerResult = innerPage;
                foreach (var wikiImage in  innerResult.Images)
                {
                    titles.Add(wikiImage.Title);
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