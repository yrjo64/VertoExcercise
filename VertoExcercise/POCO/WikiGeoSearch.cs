using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VertoExcercise.POCO
{
    public class WikiGeoSearch
    {
        public int Pageid { get; set; }
        public int Ns { get; set; }
        public string Title { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Dist { get; set; }
        public string Primary { get; set; }
    }

    public class WikiGeoSearchQuery
    {
        public List<WikiGeoSearch> Geosearch { get; set; }
    }

    public class WikiGeoSearchRoot
    {
        public string Batchcomplete { get; set; }
        public WikiGeoSearchQuery query { get; set; }
    }
}