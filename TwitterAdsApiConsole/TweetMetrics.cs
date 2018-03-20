using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using TwitterAdsApiConsole.DTO;

namespace TwitterAdsApiConsole
{
    class TweetMetrics
    {
        static void Main(string[] args)
        {
            string UrlAcct = "https://ads-api.twitter.com/3/accounts";

            jobDTO jobDto;
            IEnumerable<jobDTO> jobsDto;

            // Set up your credentials
            Auth.SetUserCredentials("xZufqSU0Rx5qwvrGLMZdBb4JO"
                , "tY39hY1peZ8eWqsgtJGuahLBBe91TwgcYL9K1bJbVC05TIuEzj"
                , "3296196906-r5ZVcIhYbLQGucXACJEE4qmE8c6OCcpRqXacZAD"
                , "9fi1cndnaBZcTZD9d7LbBnYleg8ByeTkeXcBCDCUQFC5t");

            try
            {
                // Get list of accounts we have access to
                IEnumerable<AccountDTO> acctDTO = TwitterAccessor.ExecuteGETQueryFromPath<IEnumerable<AccountDTO>>(UrlAcct, "data");
                Debug.Print(acctDTO.ToJson());

                string acctUrlBase = "https://ads-api.twitter.com/3/accounts/";
                string statsUrlBase = "https://ads-api.twitter.com/3/stats/accounts/";

                string acctUrlFull = "";
                string jobQueryUrl = "";

                foreach (AccountDTO accountDtoRec in acctDTO)
                {

                    Debug.Print("Name: " + accountDtoRec.Name);

                    if (accountDtoRec.Name != "Wild Fig Data") continue;

                    acctUrlFull = acctUrlBase + accountDtoRec.ID;
                    string scopedTlUrl = acctUrlFull + "/scoped_timeline"
                    + "?objective=TWEET_ENGAGEMENTS";

                    //Get timeline for account
                    IEnumerable<TimeLineDTO> tlDTO = TwitterAccessor.ExecuteGETQueryFromPath<IEnumerable<TimeLineDTO>>(scopedTlUrl, "data");

                    string entityIds20 = "";
                    int i = 1;

                    //Build list of entity IDs, from timeline, to request in report
                    foreach (TimeLineDTO tlDtoRec in tlDTO)
                    {
                        Debug.Print("Tweet id: " + tlDtoRec.IdString);
                        Debug.Print("Tweet text: " + tlDtoRec.Text);

                        //i counter is just here due to issue with sending more than one entity ID; set to two to see problem
                        while (tlDtoRec.InReplyToUserId == null && i <= 1)
                        {
                            entityIds20 += tlDtoRec.IdString + ",";
                            i++;
                        }
                    }

                    entityIds20 = entityIds20.TrimEnd(',');

                    //https://developer.twitter.com/en/docs/ads/analytics/api-reference/asynchronous

                    Debug.Print("IDs: " + entityIds20);

                    DateTime dtNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                    jobQueryUrl = "https://ads-api.twitter.com/2/stats/jobs/accounts/" + accountDtoRec.ID +
                    "?end_time=" + string.Format("{0:s}", dtNow.ToUniversalTime()) + "Z" //2017-10-30T04:00:00Z  UtcDateTime: {11/6/2017 19:08:05}
                    + "&entity=ORGANIC_TWEET"
                    + "&entity_ids=" + entityIds20
                    + "&granularity=TOTAL"
                    + "&metric_groups=ENGAGEMENT"
                    + "&placement=ALL_ON_TWITTER"
                    + "&start_time=" + string.Format("{0:s}", dtNow.AddDays(-7).ToUniversalTime()) + "Z"; //2017-10-29T04:00:00Z

                    Debug.Print(jobQueryUrl);

                    //Create job to get wanted entity data
                    jobDto = TwitterAccessor.ExecutePOSTQueryFromPath<jobDTO>(jobQueryUrl, "data");

                    //GET the job status:
                    jobQueryUrl = "https://ads-api.twitter.com/3/stats/jobs/accounts/" + accountDtoRec.ID;
                    jobsDto = TwitterAccessor.ExecuteGETQueryFromPath<IEnumerable<jobDTO>>(jobQueryUrl, "data");

                    //URLs of job outputs (compressed file archives) to be followed in a browser for this PoC
                    foreach (jobDTO job in jobsDto)
                    {
                        Debug.Print("Job URL: " + job.URL);
                        Console.WriteLine("Job URL: " + job.URL);
                    }

                    //Await user input to keep results on screen
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
