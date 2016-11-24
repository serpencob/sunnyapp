using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAApp.DataModels
{
    public class Results
    {
 
        [JsonProperty("sunrise")]
        public DateTime Sunrise { get; set; }

        [JsonProperty("sunset")]
        public DateTime Sunset { get; set; }

        [JsonProperty("solar_noon")]
        public DateTime solar { get; set; }

        [JsonProperty("day_length")]
        public DateTime daylength { get; set; }

        [JsonProperty("civil_twilight_begin")]
        public string tb { get; set; }

        [JsonProperty("civil_twilight_end")]
        public string te { get; set; }

        [JsonProperty("nautical_twilight_begin")]
        public string ntb { get; set; }

        [JsonProperty("nautical_twilight_end")]
        public string nte { get; set; }

        [JsonProperty("astronomical_twilight_begin")]
        public string atb { get; set; }

        [JsonProperty("astronomical_twilight_end")]
        public string ate { get; set; }
    }

    public class Times
    {
        [JsonProperty("results")]
        public Results result { get; set; }

        [JsonProperty("status")]
        public string status { get; set;}
    }

    public class Output
    {
        public string title { get; set; }
        public string time { get; set; }
        public string imageSource { get; set; }
    }
}
