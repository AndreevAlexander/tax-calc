using System.Text;
using Newtonsoft.Json;
using TaxCalculator.UI.Data.Contracts;

namespace TaxCalculator.UI.Data
{
    public class WebApi : IWebApi
    {
        private readonly HttpClient _client;

        public WebApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<TResult?> GetOneAsync<TResult>(string route) where TResult : class
        {
            TResult? result = null;

            var response = await _client.GetAsync(route);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TResult>(json);
            }

            return result;
        }

        public async Task<IEnumerable<TResult>> GetManyAsync<TResult>(string route) where TResult : class
        {
            var result = await GetOneAsync<IEnumerable<TResult>>(route);
            return result ?? Enumerable.Empty<TResult>();
        }

        public async Task<Guid?> PostAsync(string route, object body)
        {
            Guid? id = null;

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(route, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<BodyResponse>(json);
                id = responseModel?.RecordId;
            }

            return id;
        }

        public async Task<bool> PutAsync(string route, object body)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(route, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string route)
        {
            var result = false;

            var response = await _client.DeleteAsync(route);
            if (response.IsSuccessStatusCode)
            {
                result = true;
            }

            return result;
        }
    }
}
