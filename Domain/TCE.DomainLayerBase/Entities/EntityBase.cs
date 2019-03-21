using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;
using TCE.DomainLayerBase.Base;
using TCE.DomainLayerBase.Validator;

namespace TCE.DomainLayerBase.Entities
{
    public abstract class EntityBase<TEntity> : IEntityBase<TEntity> where TEntity : class
    {
        public long Id { get; set; }
        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
        protected BaseValidator<TEntity> _validator;

        public EntityBase()
        {

        }
        public abstract bool IsValidToAdd(IServiceBase<TEntity> service);
        public abstract bool IsValidToUpdade(IServiceBase<TEntity> service);
        public abstract bool IsValidToDelete(IServiceBase<TEntity> service);
    }
}