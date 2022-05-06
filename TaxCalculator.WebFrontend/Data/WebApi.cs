using System.Net;
using System.Text;
using Newtonsoft.Json;
using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Data;

public class WebApi
{
    private readonly HttpClient _client;
    
    private const string ContentType = "application/json";

    public WebApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<TModel>> GetManyAsync<TModel>(string route)
    {
        var response = await _client.GetAsync($"api/{route}");

        if (response.IsSuccessStatusCode)
        {
            var dataString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<IEnumerable<TModel>>(dataString);
            if (data == null)
            {
                throw new Exception("Can not deserialize data");
            }

            return data;
        }

        throw new Exception("Server error");
    }

    public async Task<TModel> GetOneAsync<TModel>(string route)
    {
        var response = await _client.GetAsync($"api/{route}");

        if (response.IsSuccessStatusCode)
        {
            var dataString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<TModel>(dataString);
            if (data == null)
            {
                throw new Exception("Can not deserialize data");
            }

            return data;
        }

        throw new Exception("Server error");
    }

    public async Task<ValidatedCommandResult> Create<TModel>(TModel body, string route)
    {
        var json = JsonConvert.SerializeObject(body);

        var response = await _client.PostAsync($"api/{route}", 
            new StringContent(json, Encoding.Default, ContentType));

        if (response.IsSuccessStatusCode)
        {
            var dataString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ValidatedCommandResult>(dataString);
            if (data == null)
            {
                throw new Exception("Can not deserialize data");
            }

            return data;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var dataString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ValidatedCommandResult>(dataString);
            return data;
        }

        throw new Exception("Server error");
    }
    
    public async Task<ValidatedCommandResult> Update<TModel>(TModel body, string route)
    {
        var json = JsonConvert.SerializeObject(body);

        var response = await _client.PutAsync($"api/{route}", 
            new StringContent(json, Encoding.Default, ContentType));

        if (response.IsSuccessStatusCode)
        {
            var dataString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ValidatedCommandResult>(dataString);
            if (data == null)
            {
                throw new Exception("Can not deserialize data");
            }

            return data;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var dataString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ValidatedCommandResult>(dataString);
            return data;
        }

        throw new Exception("Server error");
    }
}