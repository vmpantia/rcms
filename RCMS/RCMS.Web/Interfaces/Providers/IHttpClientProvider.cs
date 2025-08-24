namespace RCMS.Web.Interfaces.Providers;

public interface IHttpClientProvider
{
    Task<TData> GetAsync<TData>(string uri);
    Task<TData> PostAsync<TData>(string uri, object data);
    Task<TData> PutAsync<TData>(string uri, object data);
}