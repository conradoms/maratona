using Newtonsoft.Json;
using System;

namespace Maratona.Model
{
    public class GroceryListItem
    {
        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public DateTime DateUtc { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return DateUtc.ToLocalTime().ToString("d"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return DateUtc.ToLocalTime().ToString("t"); } }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Amount")]
        public int Amount { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string GroceryNameAmount => string.Format("{0} {1}", Amount, Type);
    }
}
