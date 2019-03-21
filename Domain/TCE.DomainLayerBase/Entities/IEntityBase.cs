
using TCE.DomainLayerBase.Validator;

namespace TCE.DomainLayerBase.Entities
{
    public interface IEntityBase<TEntity> : ISelfValidation<TEntity> where TEntity : class
    {
    }
}