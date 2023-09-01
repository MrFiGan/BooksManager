using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Client.Models;

namespace Client.Api
{
    public static class ApiUtilities
    {
        public static async Task<List<Category>> LoadCategories()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(ApiConfiguration.CategoryPath);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Category>>();
                }
            }

            return new List<Category>();
        }
    }
}