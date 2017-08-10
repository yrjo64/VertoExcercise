using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VertoExcercise.Calculation;

namespace VertoExcercise.Models
{
    public class LevenshteinMetrics
    {
        public LevenshteinMetrics(string titleS, string titleT)
        {
            S = titleS;
            T = titleT;
            Distance = new Levenshtein().Distance(S, T);
        }

        public string S { get; set; }
        public string T { get; set; }
        public int Distance { get; set; }
    }

    public class ImageTitles
    {
        public string SearchQuery { get; set; }
        public List<LevenshteinMetrics> SimilarTitles { get; set; }
    }
}