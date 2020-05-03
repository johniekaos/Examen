using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace PuntoVenta
{
    public static class GlobalVariables
    {
        public static HttpClient webClient = new HttpClient();
        static GlobalVariables()
        {
            var url = ConfigurationManager.AppSettings["API.URL"].ToString();
            webClient.BaseAddress = new Uri(url);
            webClient.DefaultRequestHeaders.Clear();
            webClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}