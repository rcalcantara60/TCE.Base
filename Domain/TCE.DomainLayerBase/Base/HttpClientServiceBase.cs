using FluentValidation;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using TCE.Repository.Interfaces;

namespace TCE.DomainLayerBase.Base
{
    public abstract class HttpClientServiceBase<TEntity> : ServiceBase<TEntity>, IHttpClientServiceBase<TEntity> where TEntity : class
    {
        protected readonly HttpClient _client;
        public HttpClientServiceBase(string apiName, IEFRepositoryBase<TEntity> repository, IMicroORMBaseRepository<TEntity> repositoryMicroOrm, IValidator<TEntity> v) : base(repository, repositoryMicroOrm, v)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("" + apiName);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
