using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VertoExcercise.POCO
{
    public class WikiImagesContinue
    {
        public string Imcontinue { get; set; }
        public string Continue { get; set; }
    }

    public class Image
    {
        public int Ns { get; set; }
        public string Title { get; set; }
    }

    public class WikiImagesInnerPage
    {
        public int Pageid { get; set; }
        public int Ns { get; set; }
        public string Title { get; set; }
        public List<Image> Images { get; set; }
    }

    public class Pages
    {
        public WikiImagesInnerPage WikiImagesInnerPage { get; set; }
    }

    public class Query
    {
        public Pages pages { get; set; }
    }

    public class WikiImagesRoot
    {
        public WikiImagesContinue Continue { get; set; }
        public Query query { get; set; }
    }
}