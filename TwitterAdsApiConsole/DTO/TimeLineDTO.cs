using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAdsApiConsole.DTO
{
    class TimeLineDTO
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("id_str")]
        public string IdString { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public string InReplyToUserId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        //[JsonProperty("user")]
        //public string User { get; set; }

        [JsonProperty("retweet_count")]
        public string RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public string FavoriteCount { get; set; }
    }
}
