using FluentValidation;
using TCE.Repository.Interfaces;

namespace TCE.DomainLayerBase.Validator
{
    public abstract class BaseValidator<TEntity> : AbstractValidator<TEntity> where TEntity : class
    {
        protected readonly IEFRepositoryBase<TEntity> _repositoryBase;
        public BaseValidator(IEFRepositoryBase<TEntity> repository)
        {
            _repositoryBase = repository;
        }

        public abstract void SetValidators();
        public abstract void SetCommonValidators();
    }
}
