using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssessmentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ResultModel> Results = new List<ResultModel>();

            List<BestResultModel> BestResults = new List<BestResultModel>();

            SearchUtil util = new SearchUtil();

            Console.WriteLine("Search Coding Assessment Test");
            Console.WriteLine("\n");

            for (int i = 0; i < args.Count(); i++)
            {
                string SearchWord = args[i];
                Console.WriteLine("Querying for:" + SearchWord);

                float GoogleResult = util.SearchGoogle(SearchWord);
                if (GoogleResult == -1)
                {
                    Console.WriteLine("Error Searching in Google: " + util.ErrorMessage);
                }

                BestResults.Add(new BestResultModel()
                {
                    SeachWord = SearchWord,
                    SearchResult = GoogleResult
                });

                float BingResult = util.SearchBing(SearchWord);
                if (BingResult == -1)
                {
                    Console.WriteLine("Error Searching in Bing: " + util.ErrorMessage);
                }

                BestResults.Add(new BestResultModel()
                {
                    SeachWord = SearchWord,
                    SearchResult = BingResult
                });

                Results.Add(new ResultModel()
                {
                    SearchWord = SearchWord,
                    GoogleResult = GoogleResult,
                    BingResult = BingResult
                });

                Console.WriteLine(args[i] + ": Google: " + GoogleResult.ToString() + " Bing: " + BingResult.ToString());
            }

            string GoogleBestResult = Results.Where(x => x.GoogleResult > 0).OrderByDescending(x => x.GoogleResult).Select(x => x.SearchWord).FirstOrDefault();
            string BingBestResult = Results.Where(x => x.BingResult > 0).OrderByDescending(x => x.BingResult).Select(x => x.SearchWord).FirstOrDefault();
            string BestOfTheBestResult = BestResults.Where(x => x.SearchResult > 0).OrderByDescending(x => x.SearchResult).Select(x => x.SeachWord).FirstOrDefault();
            Console.WriteLine("\n");
            Console.WriteLine("Google Winner: " + GoogleBestResult);
            Console.WriteLine("Bing Winner: " + BingBestResult);
            Console.WriteLine("Best of the Best: " + BestOfTheBestResult);
            Console.WriteLine("Press [ENTER] to exit");
            Console.ReadLine();
        }
    }
}
