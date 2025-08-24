namespace RCMS.Web.Interfaces.Providers;

public interface IHttpClientProvider
{
    Task<TData> GetAsync<TData>(string uri, object? data = null);
    Task<TData> PostAsync<TData>(string uri, object? data = null);
    Task<TData> PutAsync<TData>(string uri, object? data = null);
    Task<TData> DeleteAsync<TData>(string uri, object? data = null);
}