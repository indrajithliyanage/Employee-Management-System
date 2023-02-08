using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace EMSApp
{
    public class GlobalVariables
    {
        public static HttpClient WebAPIClient = new HttpClient();

        static GlobalVariables()
        {
            WebAPIClient.BaseAddress = new Uri("https://localhost:44322/api/");
            WebAPIClient.DefaultRequestHeaders.Clear();
            WebAPIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}