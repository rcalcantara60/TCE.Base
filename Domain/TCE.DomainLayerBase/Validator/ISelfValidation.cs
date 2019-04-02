
using FluentValidation;
using FluentValidation.Results;
using TCE.DomainLayerBase.Base;
using TCE.Repository.Interfaces;

namespace TCE.DomainLayerBase.Validator
{
    public interface ISelfValidation<T> where T : class
    {
        ValidationResult ValidationResult { get; }

        void SetValidator(IValidator<T> validator);

        bool IsValidToAdd();

        bool IsValidToUpdade();

        bool IsValidToDelete();
    }
}
