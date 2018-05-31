using System;
using System.Net.Http;
using System.Threading.Tasks;
using Gadget.IService;
using Gadget.IService.Models;
using Newtonsoft.Json;

namespace Gadget.ApiClient
{
    public class AuthorServiceClient : IAuthorService
    {
        private readonly HttpClient _httpClient;

        public AuthorServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string GetAuthorUrl = "api/v1/authors/{0}";

        public async Task<AuthorModel> GetAuthor(long authorId)
        {
            var response = await _httpClient.GetAsync(string.Format(GetAuthorUrl, authorId));

            if (!response.IsSuccessStatusCode)
            {
                return await Task.FromException<AuthorModel>(new Exception(response.ReasonPhrase));
            }

            var authorModel = JsonConvert.DeserializeObject<AuthorModel>(await response.Content.ReadAsStringAsync());

            return authorModel;
        }
    }
}
