using Azure.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace WebApi.Test
{
    public class StructoClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public StructoClassFixture(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }


        protected async Task<HttpResponseMessage> DoPost(string method,
                                                         object request,
                                                         string culture = "pt_BR") // Implement your POST request logic here
        {
            ChangeRequestCulture(culture);

            return await _httpClient.PostAsJsonAsync(method, request);
        }


        private void ChangeRequestCulture(string culture)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
            }
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
        }





    }
}
