using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssessmentTest
{
    public class SearchUtil
    {
        public string ErrorMessage { get; set; }
        public float SearchGoogle(string Search)
        {
            float Result = -1;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/customsearch/v1?q=" + Search + "&key=AIzaSyDcxygs5EMusfGzEi1O9drH8qKz1gqUggw&cx=005430748436740110835:6b0qwxqidpk");

                request.Method = "GET";

                var response = (HttpWebResponse)request.GetResponse();

                string content = string.Empty;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }

                Result = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(content)["searchInformation"]["totalResults"];
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Result = -1;
            }

            return Result;
        }

        public float SearchBing(string Search)
        {
            float Result = -1;

            string AccessKey = "aeb2667e34634cc088a41c5a10eecac1";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://api.cognitive.microsoft.com/bing/v7.0/search?q=" + Uri.EscapeDataString(Search));

                request.Method = "GET";
                request.Headers["Ocp-Apim-Subscription-Key"] = AccessKey;
                var response = (HttpWebResponse)request.GetResponse();

                string content = string.Empty;
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }

                Result = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(content)["webPages"]["totalEstimatedMatches"];

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Result = -1;
            }

            return Result;
        }
    }
}
