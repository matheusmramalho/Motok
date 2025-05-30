﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MatheusR.Motok.E2ETests.Base;
public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _defaultSerializeOptions;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _defaultSerializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<(HttpResponseMessage?, TOutput?)> Post<TOutput>(string route, object payload) where TOutput : class
    {
        var payloadJson = JsonSerializer.Serialize(payload, _defaultSerializeOptions);
        var response = await _httpClient.PostAsync(
            route,
            new StringContent(
                payloadJson,
                Encoding.UTF8,
                "application/json"
            )
        );
        var output = await GetOutput<TOutput>(response);
        return (response, output);
    }

    public async Task<(HttpResponseMessage?, TOutput?)> Get<TOutput>(string route) where TOutput : class
    {
        var response = await _httpClient.GetAsync(route);
        var output = await GetOutput<TOutput>(response);
        return (response, output);
    }

    private async Task<TOutput?> GetOutput<TOutput>(HttpResponseMessage response) where TOutput : class
    {
        var outputString = await response.Content.ReadAsStringAsync();

        if (typeof(TOutput) == typeof(string))
        {
            return outputString as TOutput;
        }

        TOutput? output = null;
        if (!string.IsNullOrWhiteSpace(outputString))
            output = JsonSerializer.Deserialize<TOutput>(
                outputString,
                _defaultSerializeOptions
            );
        return output;
    }

    //public async Task<(HttpResponseMessage?, TResponse?)> Post<TResponse>(string url, object body) where TResponse : class
    //{
    //    var response = await _httpClient.PostAsync(url, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
    //    //response.EnsureSuccessStatusCode();

    //    var outputString = await response.Content.ReadAsStringAsync();
    //    TResponse? output = null;

    //    if (!string.IsNullOrWhiteSpace(outputString))
    //    {
    //        output = JsonSerializer.Deserialize<TResponse>(outputString,
    //          new JsonSerializerOptions
    //          {
    //              PropertyNameCaseInsensitive = true
    //          }
    //      );
    //    }

    //    return (response, output);
    //}
}
