using TCE.RestClient.Exceptions;
using TCE.RestClient.Extensions;
using TCE.RestClient.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCE.RestClient
{
    public class RestClient : IRestClient
    {
        private const string _mediaType = "application/json";
        public string UrlBase { get; private set; }
        public HttpClient HttpClient { get; private set; }
        private HttpResponseHeaders _responseHeader;

        public RestClient()
        {
            HttpClient = new HttpClient();
        }
        public RestClient(string urlBase)
            : this()
        {
            UrlBase = urlBase;
            HttpClient.BaseAddress = new Uri(UrlBase);
        }

        public void AddHeader(string key, string value) => HttpClient.DefaultRequestHeaders.Add(key, value);

        private static string GetData<TRequest>(TRequest request) => JsonConvert.SerializeObject(request);

        public string GetHeaderResponse(string key)
        {
            var header = _responseHeader.FirstOrDefault(f => f.Key == key);
            return header.Value.First();
        }

        private async Task<TResponse> ResponseAsync<TResponse>(HttpResponseMessage result)
        {
            var resultContent = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                _responseHeader = result.Headers;
                var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
                return response;
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(resultContent);

            throw new Exception(resultContent);
        }

        public string GetUrl(string action)
        {
            return $"{UrlBase}{action}";
        }

        public async Task<Uri> GetUrlAsync<TRequest>(string action, TRequest request)
        {
            var url = GetUrl(action);
            return await url.ToUriAsync(request);
        }

        public Uri GetUrl<TRequest>(string action, TRequest request)
        {
            var url = GetUrl(action);
            return url.ToUri(request);
        }


        #region POST
        
        public virtual TResponse Post<TRequest, TResponse>(TRequest request)
        {
            return Post<TRequest, TResponse>(request, string.Empty);
        }
        public virtual TResponse Post<TRequest, TResponse>(TRequest request, string action)
        {
            var json = GetData(request);
            var content = new StringContent(json, Encoding.UTF8, _mediaType);

            var url = GetUrl(action);
            HttpResponseMessage result = HttpClient.PostAsync(url, content).Result;

            return ResponseAsync<TResponse>(result).Result;
        }
        public virtual async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request)
        {
            return await PostAsync<TRequest, TResponse>(request, string.Empty, CancellationToken.None);
        }
        public virtual async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        {
            return await PostAsync<TRequest, TResponse>(request, string.Empty, cancellationToken);
        }

        public virtual async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string action, CancellationToken cancellationToken)
        {
            var json = GetData(request);
            var content = new StringContent(json, Encoding.UTF8, _mediaType);

            var url = GetUrl(action);
            var result = await HttpClient.PostAsync(url, content, cancellationToken);
            return await ResponseAsync<TResponse>(result);
        }
        #endregion

        #region GETs
        public virtual TResponse Get<TResponse>()
        {
            return Get<TResponse>(string.Empty);
        }
        public virtual TResponse Get<TResponse>(string action)
        {
            var url = GetUrl(action);
            var result = HttpClient.GetAsync(url).Result;
            return ResponseAsync<TResponse>(result).Result;
        }
        public virtual TResponse Get<TRequest, TResponse>(TRequest request)
        {
            return Get<TRequest, TResponse>(request);
        }
        public virtual TResponse Get<TRequest, TResponse>(TRequest request, string action)
        {
            var url = GetUrl(action, request);
            var result = HttpClient.GetAsync(url).Result;
            return ResponseAsync<TResponse>(result).Result;
        }
        public virtual async Task<TResponse> GetAsync<TResponse>()
        {
            return await GetAsync<TResponse>(string.Empty, CancellationToken.None);
        }
        public virtual async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request)
        {
            return await GetAsync<TRequest, TResponse>(request, string.Empty, CancellationToken.None);
        }
        public virtual async Task<TResponse> GetAsync<TResponse>(CancellationToken cancellationToken)
        {
            return await GetAsync<TResponse>(string.Empty, cancellationToken);
        }
        public virtual async Task<TResponse> GetAsync<TResponse>(string action, CancellationToken cancellationToken)
        {
            var url = GetUrl(action);
            var result = await HttpClient.GetAsync(url, cancellationToken);
            return await ResponseAsync<TResponse>(result);
        }
        public virtual async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        {
            return await GetAsync<TRequest, TResponse>(request, string.Empty, cancellationToken);
        }
        public virtual async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, string action, CancellationToken cancellationToken)
        {
            var url = await GetUrlAsync(action, request);
            var result = await HttpClient.GetAsync(url, cancellationToken);
            return await ResponseAsync<TResponse>(result);
        }
        #endregion

        #region PUT
        public virtual TResponse Put<TRequest, TResponse>(TRequest request)
        {
            return Put<TRequest, TResponse>(request, string.Empty);
        }
        public virtual TResponse Put<TRequest, TResponse>(TRequest request, string action)
        {
            var json = GetData(request);
            var content = new StringContent(json, Encoding.UTF8);
            var url = GetUrl(action);

            var result = HttpClient.PutAsync(url, content).Result;
            return ResponseAsync<TResponse>(result).Result;
        }
        public virtual async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        {
            return await PutAsync<TRequest, TResponse>(request, string.Empty, cancellationToken);
        }
        public virtual async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request)
        {
            return await PutAsync<TRequest, TResponse>(request, string.Empty, CancellationToken.None);
        }
        public virtual async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, string action, CancellationToken cancellationToken)
        {
            var json = GetData(request);
            var content = new StringContent(json, Encoding.UTF8);
            var url = GetUrl(action);

            var result = await HttpClient.PutAsync(url, content, cancellationToken);
            return await ResponseAsync<TResponse>(result);
        }
        #endregion

        #region DELETE
        public virtual void Delete<TRequest>(TRequest request)
        {
            Delete(request, string.Empty);
        }

        public virtual void Delete<TRequest>(TRequest request, string action)
        {
            var json = GetData(request);
            using (var content = new StringContent(json, Encoding.UTF8, _mediaType))
            {
                var url = GetUrl(action);
                var result = HttpClient.DeleteAsync(url).Result;
                var resultContent = result.Content.ReadAsStringAsync().Result;
                if (!result.IsSuccessStatusCode)
                    throw new RequestException(resultContent);
            }
        }
        public virtual async Task DeleteAsync<TRequest>(TRequest request)
        {
            await DeleteAsync(request, string.Empty, CancellationToken.None);
        }

        public virtual async Task DeleteAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
        {
            await DeleteAsync(request, string.Empty, cancellationToken);
        }
        #endregion

        public virtual async Task DeleteAsync<TRequest>(TRequest request, string action, CancellationToken cancellationToken)
        {
            var json = GetData(request);

            using (var content = new StringContent(json, Encoding.UTF8, _mediaType))
            {
                var url = GetUrl(action);
                var result = await HttpClient.DeleteAsync(url, cancellationToken);

                var resultContent = await result.Content.ReadAsStringAsync();
                if (!result.IsSuccessStatusCode)
                    throw new RequestException(resultContent);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                HttpClient.Dispose();
                UrlBase = string.Empty;
                _responseHeader = null;
            }
        }
    }
}
