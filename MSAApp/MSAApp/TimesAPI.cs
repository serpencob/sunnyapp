using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSAApp.DataModels;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using ModernHttpClient;
using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;

namespace MSAApp
{
    class TimesAPI 
    {
        private static TimesAPI instance;
        HttpClient client = new HttpClient();
        Position position = new Position();
        
        //public TimesAPI()
        //{
        //    GetLocation();
        //}

        //public async void GetLocation()
        //{
        //    position = await CrossGeolocator.Current.GetPositionAsync();
        //}

        public static TimesAPI timesAPIinstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TimesAPI();
                }

                return instance;
            }
        }

        public async Task<Times> RefreshDataAsync(double lat, double lon)
        {
            Times times = new Times();
            
            client.MaxResponseContentBufferSize = 256000;

            var uri = new Uri(string.Format("http://api.sunrise-sunset.org/json?lat=" + lat + "&lng="+ lon, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                times=JsonConvert.DeserializeObject<Times>(content);
            }

            return times;

        }
    }
}
