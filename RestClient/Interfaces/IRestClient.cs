using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCE.RestClient.Interfaces
{
    public interface IRestClient : IDisposable
    {
        void AddHeader(string chave, string valor);
        string GetHeaderResponse(string key);

        TResponse Post<TRequest, TResponse>(TRequest request);
        TResponse Post<TRequest, TResponse>(TRequest request, string action);        
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request);
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken);
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string action, CancellationToken cancellationToken);

        TResponse Get<TResponse>();
        TResponse Get<TRequest, TResponse>(TRequest request);
        TResponse Get<TRequest, TResponse>(TRequest request, string action);
        Task<TResponse> GetAsync<TResponse>();
        Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request);
        Task<TResponse> GetAsync<TResponse>(CancellationToken cancellationToken);
        Task<TResponse> GetAsync<TResponse>(string action, CancellationToken cancellationToken);
        Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken);
        Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, string action, CancellationToken cancellationToken);

        TResponse Put<TRequest, TResponse>(TRequest request);
        TResponse Put<TRequest, TResponse>(TRequest request, string action);
        Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request);
        Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken);
        Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, string action, CancellationToken cancellationToken);

        void Delete<TRequest>(TRequest request);
        void Delete<TRequest>(TRequest request, string action);
        Task DeleteAsync<TRequest>(TRequest request);
        Task DeleteAsync<TRequest>(TRequest request, CancellationToken cancellationToken);
        Task DeleteAsync<TRequest>(TRequest request, string action, CancellationToken cancellationToken);
    }
}

