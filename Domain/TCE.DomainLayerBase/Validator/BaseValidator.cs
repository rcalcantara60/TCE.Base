using FluentValidation;
using TCE.DomainLayerBase.Base;
using TCE.Repository.Interfaces;

namespace TCE.DomainLayerBase.Validator
{
    public abstract class BaseValidator<TEntity> : AbstractValidator<TEntity> where TEntity : class
    {
        protected IServiceBase<TEntity> Service { get; set; }

        public BaseValidator()
        {

        }

        public virtual void SetService(IServiceBase<TEntity> service)
        {
            Service = service;
        }

        public abstract void SetValidators();
        public abstract void SetCommonValidators();

    }
}
