using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAdsApiConsole.DTO
{
    class jobDTO
    {
        [JsonProperty("account_id")]
        public string AccountID { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

    }
}
