using FluentValidation;
using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using TCE.DomainLayerBase.Base;
using TCE.DomainLayerBase.Validator;

namespace TCE.DomainLayerBase.Entities
{
    public abstract class EntityBase<TEntity> : IEntityBase<TEntity> where TEntity : class
    {
        //public long Id { get; set; }
        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
        protected BaseValidator<TEntity> _validator;

        public EntityBase()
        {
            
        }
        public abstract void SetValidator(IValidator<TEntity> v);
        public abstract bool IsValidToAdd(IServiceBase<TEntity> service);
        public abstract bool IsValidToUpdade(IServiceBase<TEntity> service);
        public abstract bool IsValidToDelete(IServiceBase<TEntity> service);
    }
}