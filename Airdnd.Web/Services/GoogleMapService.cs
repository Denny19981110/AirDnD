using Airdnd.Core.enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;

namespace Airdnd.Web.Services
{
    public class GoogleMapService
    {
        private string _searchUrl = "https://maps.googleapis.com/maps/api/geocode/json?language=zh-TW&address=";
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        public GoogleMapService( IConfiguration configuration )
        {
            _configuration = configuration;
            _apiKey = _configuration.GetSection("GoogleMapsApi").Get<string>();
        }

        /// <summary>
        /// 以住址查詢經緯度
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public GoogleLocation GetLatLngByAddress(string addr)
        {
            string searchUrl = $"{_searchUrl}{addr}&key={_apiKey}&sensor=false";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(searchUrl);

            string GoogleGeoRes = string.Empty;
            using (var response = request.GetResponse())
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                GoogleGeoRes = sr.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(GoogleGeoRes);
            GoogleLocation result = new GoogleLocation();

            if (data.Status == "OK")
            {

                if (data.results[0].address_components.Length <= 4)
                {
                    result.Address = "Error";
                }
                var address = data.results[0].address_components;

                var county = address.FirstOrDefault(a => a.types[0] == "administrative_area_level_2" || a.types[0] == "administrative_area_level_1")?.long_name ?? "";
                var region = address.FirstOrDefault(a => a.types[0] == "administrative_area_level_3")?.long_name ?? "";
                var route = address.FirstOrDefault(a => a.types[0] == "route")?.long_name ?? "";
                var streetNum = address.FirstOrDefault(a => a.types[0] == "street_number")?.long_name ?? "";
                //string subpremise;
                //if (address.FirstOrDefault(a => a.types[0] == "subpremise") != null)
                //{
                //    subpremise = address.FirstOrDefault(a => a.types[0] == "subpremise").long_name;
                //    result.Address = $"{county}-{region}-{route}{streetNum}{subpremise}";
                //}
                //else
                //{
                    result.Address = $"{county}-{region}-{route}{streetNum}";
                //}

                result.Lat = data.results[0].geometry.location.Lat;
                result.Lng = data.results[0].geometry.location.Lng;
            }
            return result;
        }
    }
    /// <summary>
    /// 回傳狀態
    /// </summary>
	public class GoogleGeoCodeResponse
    {
        public string Status { get; set; }
        public results[] results { get; set; }
    }
    /// <summary>
    /// Google回傳結果
    /// </summary>
	public class results
    {
        public string formatted_address { get; set; }
        public geometry geometry { get; set; }
        public string[] types { get; set; }
        public address_component[] address_components { get; set; }
    }
    public class geometry
    {
        public string location_type { get; set; }
        public GoogleLocation location { get; set; }
    }
    /// <summary>
    /// 經緯度 + 地址
    /// </summary>
    public class GoogleLocation
    {
        /// <summary>
        ///  緯度
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public double Lng { get; set; }
        public string Address { get; set; }
    }
    /// <summary>
    /// 地址組成
    /// </summary>
    public class address_component
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
}
