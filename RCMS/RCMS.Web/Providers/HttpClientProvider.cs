using System.Net;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RCMS.Shared.Extensions;
using RCMS.Shared.Responses;
using RCMS.Web.Providers.Contracts;

namespace RCMS.Web.Providers;

public class HttpClientProvider(HttpClient httpClient, ILocalStorageService localStorageService,
    NavigationManager navigationManager, ILogger<HttpClientProvider> logger) : IHttpClientProvider
{
    public async Task<TData> GetAsync<TData>(string uri) =>
        await SendRequestAsync<TData>(HttpMethod.Get, uri);

    public async Task<TData> PostAsync<TData>(string uri, object data) =>
        await SendRequestAsync<TData>(HttpMethod.Post, uri, data);

    public async Task<TData> PutAsync<TData>(string uri, object data) =>
        await SendRequestAsync<TData>(HttpMethod.Put, uri, data);

    private async Task SetAuthorizeHeaderAsync()
    {
        // Get auth token if any
        var token = await localStorageService.GetItemAsync<string>("authToken");

        // Add bearer token as a default request header
        if (token != null && !string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private async Task<TData> SendRequestAsync<TData>(HttpMethod method, string uri, object? data = null)
    {
        try
        {
            // Set bearer token if any
            await SetAuthorizeHeaderAsync();

            // Create the HTTP request message
            var request = new HttpRequestMessage(method, uri);

            // Prepare content if the data is not null
            if (data is not null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(data),
                    System.Text.Encoding.UTF8,
                    "application/json");
            }

            // Add headers to the request if necessary
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Send the request and get the response
            var response = await httpClient.SendAsync(request);
            
            return await HandleResponseAsync<TData>(request, response);
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning(ex.Message);

            // Force logout user when it's not authorized to send request on APIs
            navigationManager.NavigateTo("/logout");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError($"Method: {method} | Uri: {uri} | Error Message: {ex.Message}");
            throw;
        }
    }

    private async Task<TData> HandleResponseAsync<TData>(HttpRequestMessage request, HttpResponseMessage response) 
    {
        // Throw unauthorized access when status code is Unauthorized
        if (response.StatusCode is HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException($"User is not authorized to send a request on api {request.Method}: {request.RequestUri}.");
            
        // Read the response content
        var content = await response.Content.ReadAsStringAsync();

        // Convert the content to a result object
        var result = JsonConvert.DeserializeObject<Result<TData>>(content);

        // Check the result of the request
        if (result is not null && result.IsSuccess && response.IsSuccessStatusCode)
            return result.Data!;

        throw new Exception(result?.Error?.GetMessage());
    }
}