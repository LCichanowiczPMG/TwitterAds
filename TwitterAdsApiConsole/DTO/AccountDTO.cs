using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterAdsApiConsole.DTO
{
    public class AccountDTO
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("business_name")]
        public string BusinessName { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_switch_at")]
        public string TimezoneSwitchAt { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("salt")]
        public string Salt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("business_id")]
        public string BusinessId { get; set; }

        [JsonProperty("approval_status")]
        public string ApprovalStatus { get; set; }

        [JsonProperty("deleted")]
        public string Deleted { get; set; }
    }
}