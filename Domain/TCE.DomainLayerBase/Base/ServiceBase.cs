using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TCE.CrossCutting.Dto;
using TCE.DomainLayerBase.Validator;
using TCE.Repository.Interfaces;

namespace TCE.DomainLayerBase.Base
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        protected readonly IEFRepositoryBase<TEntity> _repository;
        protected readonly IValidator<TEntity> _validator;
        private readonly IMicroORMBaseRepository<TEntity> _repositoryMicroOrm;
        private readonly ValidationResultDto _validationResult;

        protected IEFRepositoryBase<TEntity> Repository
        {
            get { return _repository; }
        }

        protected IMicroORMBaseRepository<TEntity> RepositoryMicroOrm
        {
            get { return _repositoryMicroOrm; }
        }

        protected ValidationResultDto ValidationResult
        {
            get { return _validationResult; }
        }        

        public ServiceBase(IEFRepositoryBase<TEntity> repository, 
                           IMicroORMBaseRepository<TEntity> repositoryMicroOrm,
                           IValidator<TEntity> v)
        {
            _validator = v;
            _repository = repository;
            _repositoryMicroOrm = repositoryMicroOrm;
            _validationResult = new ValidationResultDto() { IsValid = true };
        }

        //public ServiceBase(IEFRepositoryBase<TEntity> repository)
        //{
        //    _repository = repository;
        //    _validationResult = new ValidationResultDto() { IsValid = true };
        //}

        #region Read Methods
 

        public virtual TEntity GetSingle(int id)
        {
            return _repository.GetSingle(id);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            return _repository.GetSingle(predicate, @readonly);
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            return await _repository.GetSingleAsync(predicate, @readonly);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetSingle(predicate, includeProperties);
        }

        public virtual Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetSingleAsync(predicate, includeProperties);
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetSingle(predicate, @readonly, includeProperties);
        }

        public virtual Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetSingleAsync(predicate, @readonly, includeProperties);
        }

        public virtual IEnumerable<TEntity> All(bool @readonly = true)
        {
            var ret = _repository.All(@readonly);
            var c = ret.ToList();
            return ret;
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            return _repository.Find(predicate, @readonly);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.Find(predicate, @readonly, includeProperties);
        }

        public virtual int Count()
        {
            return _repository.Count();
        }

        public virtual Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Count(predicate);
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.CountAsync(predicate);
        }

        public virtual long LongCount()
        {
            return _repository.LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return _repository.LongCountAsync();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.LongCount(predicate);
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.LongCountAsync(predicate);
        }

        #endregion

        #region CRUD Methods
        
        public virtual ValidationResultDto Add(TEntity entity)
        {
            var selfValidationEntity = entity as ISelfValidation<TEntity>;
            selfValidationEntity.SetValidator(_validator);
            if (selfValidationEntity != null && !selfValidationEntity.IsValidToAdd())
            {
                return AutoMapperHelper.GetValidationResultDto(selfValidationEntity.ValidationResult);
            }
            _repository.Add(entity);
            return _validationResult;
        }

        public virtual async Task<ValidationResultDto> AddAsync(TEntity entity)
        {
            var selfValidationEntity = entity as ISelfValidation<TEntity>;
            selfValidationEntity.SetValidator(_validator);
            if (selfValidationEntity != null && !selfValidationEntity.IsValidToAdd())
            {
                return AutoMapperHelper.GetValidationResultDto(selfValidationEntity.ValidationResult);
            }
            await _repository.AddAsync(entity);
            return _validationResult;
        }

        public virtual ValidationResultDto Update(TEntity entity)
        {
            var selfValidationEntity = entity as ISelfValidation<TEntity>;
            selfValidationEntity.SetValidator(_validator);
            if (selfValidationEntity != null && !selfValidationEntity.IsValidToUpdade())
            {
                return AutoMapperHelper.GetValidationResultDto(selfValidationEntity.ValidationResult);
            }
            _repository.Update(entity);
            return _validationResult;
        }

        public virtual async Task<ValidationResultDto> UpdateAsync(TEntity entity)
        {
            var selfValidationEntity = entity as ISelfValidation<TEntity>;
            selfValidationEntity.SetValidator(_validator);
            if (selfValidationEntity != null && !selfValidationEntity.IsValidToAdd())
            {
                return AutoMapperHelper.GetValidationResultDto(selfValidationEntity.ValidationResult);
            }
            await _repository.UpdateAsync(entity);
            return _validationResult;
        }

        public virtual ValidationResultDto Delete(TEntity entity)
        {
            _repository.Delete(entity);
            return _validationResult;
        }

        public virtual async Task<ValidationResultDto> DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
            return _validationResult;
        }

        public virtual ValidationResultDto DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            _repository.DeleteWhere(predicate);
            return _validationResult;
        }

        public virtual async Task<ValidationResultDto> DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await _repository.DeleteWhereAsync(predicate);
            return _validationResult;
        }
        #endregion
    }
}