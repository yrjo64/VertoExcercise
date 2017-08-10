using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VertoExcercise.POCO;

namespace VertoExcercise
{
    public class WebClients
    {
        private string rootUrl = "https://en.wikipedia.org/w/api.php?action=query&";
        public string SearchQuery { get; private set; }

        public WikiGeoSearchRoot GetWikiGeoSearch()
        {
            WikiGeoSearchRoot geoSearch = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    var queryParams =
                        "format=json&list=geosearch&gscoord=37.786952%7C-122.399523&gsradius=10000&gslimit=50";
                    // 
                    SearchQuery = rootUrl + queryParams;

                    // open and read from the supplied URI



                    Stream stream = webClient.OpenRead(new Uri(SearchQuery));
                    StreamReader reader = new StreamReader(stream);
                    string response = reader.ReadToEnd();
                    Console.WriteLine(response.ToString());
                    if (response != null)
                        geoSearch = JsonConvert.DeserializeObject<WikiGeoSearchRoot>(response);
                    return geoSearch;
                }
            }
            catch (WebException exception)
            {
                throw new WebException(
                    "An error has occurred while calling GetSampleClass method: " + exception.Message);
            }
        }

        public WikiImagesInnerPage GetWikiImages(int pageid)
        {
            WikiImagesInnerPage innerPage = new WikiImagesInnerPage();
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    var queryParams =
                        "prop=images&pageids=" + pageid + "&format=json";
                    // 
                    Stream stream = webClient.OpenRead(rootUrl + queryParams);
                    StreamReader reader = new StreamReader(stream);
                    string response = reader.ReadToEnd();
                    Console.WriteLine("PageId = " + pageid + " " + response.ToString());
                    JObject imageSearch = JObject.Parse(response);
                    // get JSON result objects into a list
                    IList<JToken> results = imageSearch["query"]["pages"][Convert.ToString(pageid)]["images"].Children().ToList();
                    IList<Image> searchResults = new List<Image>();
                    foreach (JToken result in results)
                    {
                        // JToken.ToObject is a helper method that uses JsonSerializer internally
                        Image searchResult = result.ToObject<Image>();
                        searchResults.Add(searchResult);
                    }
                    innerPage.Pageid = pageid;
                    innerPage.Images = searchResults.ToList();
                    return innerPage;
                }
            }
            catch (WebException exception)
            {
                throw new WebException(
                    "An error has occurred while calling GetSampleClass method: " + exception.Message);
            }
        }
    }
}