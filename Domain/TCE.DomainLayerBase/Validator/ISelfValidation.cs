
using FluentValidation;
using FluentValidation.Results;
using TCE.DomainLayerBase.Base;
using TCE.Repository.Interfaces;

namespace TCE.DomainLayerBase.Validator
{
    public interface ISelfValidation<T> where T : class
    {
        ValidationResult ValidationResult { get; }

        void SetValidator(IValidator<T> v);

        bool IsValidToAdd(IServiceBase<T> service);

        bool IsValidToUpdade(IServiceBase<T> service);

        bool IsValidToDelete(IServiceBase<T> service);
    }
}
