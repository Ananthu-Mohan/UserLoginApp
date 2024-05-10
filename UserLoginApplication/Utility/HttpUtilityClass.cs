using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UserLoginApplication.Models;
using UserLoginApplication.Abstraction;

namespace UserLoginApplication.Utility
{
    public class HttpUtilityClass : IHttpUtility
    {
        private readonly dynamic url;
        HttpClient client = new HttpClient();
        public HttpUtilityClass()
        {

        }
        public HttpUtilityClass(string url)
        {
            this.url = url;
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public HttpUtilityClass(string url , string jwtToken)
        {
            this.url = url;
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
        public async Task<string> IsUserAuthenticated(IdentityModel userDetails)
        {
            string serializedContent = JsonConvert.SerializeObject(userDetails);
            HttpResponseMessage res = await client.PostAsync(url, new StringContent(serializedContent, Encoding.UTF8, "application/json"));
            var response = res.Content.ReadAsStringAsync().Result;
            return response;
        }

        public async Task<List<IdentityModel>> UserDetails()
        {
            List<IdentityModel> userDetails = new List<IdentityModel>();
            HttpResponseMessage res = await client.GetAsync(url);
            if(res.IsSuccessStatusCode)
            {
                var responseContent = res.Content.ReadAsStringAsync().Result;
                userDetails = JsonConvert.DeserializeObject<List<IdentityModel>>(responseContent);
            }
            else
            {
                userDetails = null;
            }
            return userDetails;
        }
    }
}