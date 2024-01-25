using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;

namespace AutoWarden.Api.IntegrationTests.Utilities;

public static class Extensions
{
    public static async Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri)
    {
        var response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var deserializedObject = JsonConvert.DeserializeObject<T>(responseBody);
        deserializedObject.Should().NotBeNull();

        return deserializedObject!;
    }

    public static async Task<TResponse> PostAsJsonAsyncAndDeserialize<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest value)
    {
        var response = await client.PostAsJsonAsync(requestUri, value);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var deserializedObject = JsonConvert.DeserializeObject<TResponse>(responseBody);
        deserializedObject.Should().NotBeNull();

        return deserializedObject!;
    }

    public static async Task<TResponse> PatchAsJsonAsyncAndDeserialize<TRequest, TResponse>(this HttpClient client, string requestUri, TRequest value)
    {
        var response = await client.PatchAsJsonAsync(requestUri, value);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var deserializedObject = JsonConvert.DeserializeObject<TResponse>(responseBody);
        deserializedObject.Should().NotBeNull();

        return deserializedObject!;
    }
}
