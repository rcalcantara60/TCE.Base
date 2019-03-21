using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using TCE.AppLayerBase.Extensions;
using TCE.CrossCutting.Dto;
using TCE.DomainLayerBase.Base;

namespace TCE.AppLayerBase.Base
{
    public abstract class AppServiceBase<TEntity, TDto> : IAppServiceBase<TDto> where TEntity : class where TDto : class
    {
        private readonly IServiceBase<TEntity> _service;

        protected IServiceBase<TEntity> Service
        {
            get { return _service; }
        }
        public AppServiceBase(IServiceBase<TEntity> service)
        {
            _service = service;
            SetDbFunctionsInLinqDynamic();
        }

        private void SetDbFunctionsInLinqDynamic()
        {
            var type = typeof(DynamicQueryable).Assembly.GetType("System.Linq.Dynamic.ExpressionParser");
            FieldInfo field = type.GetField("predefinedTypes", BindingFlags.Static | BindingFlags.NonPublic);
            Type[] predefinedTypes = (Type[])field.GetValue(null);
            if (predefinedTypes.Where(x => x.Name == "DbFunctions").FirstOrDefault() == null)
            {
                Array.Resize(ref predefinedTypes, predefinedTypes.Length + 1);
                predefinedTypes[predefinedTypes.Length - 1] = typeof(DbFunctions);
                field.SetValue(null, predefinedTypes);
            }
        }

        protected virtual TEntity GetTEntity(TDto entityDto)
        {
            return Mapper.Map<TDto, TEntity>(entityDto);
        }

        protected virtual IEnumerable<TEntity> GetListTEntity(IEnumerable<TDto> entityDto)
        {
            return Mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(entityDto);
        }

        protected virtual TDto GetTDto(TEntity entity)
        {
            return Mapper.Map<TEntity, TDto>(entity);
        }

        protected virtual IEnumerable<TDto> GetListTDto(IEnumerable<TEntity> entity)
        {
            return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entity);
        }

        public virtual ValidationResultDto Add(TDto entityDto)
        {
            return _service.Add(GetTEntity(entityDto));
        }

        public virtual ValidationResultDto Delete(TDto entityDto)
        {
            return _service.Delete(GetTEntity(entityDto));
        }

        public virtual TDto GetSingle(int id)
        {
            return GetTDto(_service.GetSingle(id));
        }

        public virtual ValidationResultDto Update(TDto entityDto)
        {
            return _service.Update(GetTEntity(entityDto));
        }

        public virtual ValidationResultDto LogicDelete(int id)
        {
            var entityDto = GetSingle(id);

            throw new NotImplementedException();
        }

        private string GetQuery(IEnumerable<DinamicQueryDto> queries)
        {
            StringBuilder builder = new StringBuilder();
            bool primeiro = true;
            if (queries != null)
                foreach (var item in queries)
                {
                    if (!primeiro)
                        builder.Append("&&");
                    builder.Append(item.ToString());
                    primeiro = false;
                }
            return builder.ToString();
        }

        public virtual int Count()
        {
            return _service.Count();
        }

        public virtual int Count(string query)
        {
            return _service.All(true).Where(query).Count(); ;
        }

        private int GetTotalPages(PaginationDto pagination)
        {
            return (int)Math.Ceiling((double)pagination.TotalItems / pagination.ItemsPerPage); ;
        }

        public  virtual IEnumerable<TDto> All()
        {
            return GetListTDto(_service.All(true));
        }

        public  virtual IEnumerable<TDto> All(string order, string sortOrder)
        {
            return GetListTDto(_service.All(true)).OrderBy(order + " " + sortOrder);
        }

        public virtual IEnumerable<TDto> AllPaginated(PaginationDto pagination)
        {
            return Paginate(_service.All(true), pagination);
        }

        public virtual IEnumerable<TDto> AllPaginated(PaginationDto pagination, IEnumerable<DinamicQueryDto> queries)
        {
            return Paginate(_service.All(true), pagination, queries);
        }

        protected virtual IEnumerable<TDto> Paginate(IEnumerable<TEntity> entities, PaginationDto pagination, IEnumerable<DinamicQueryDto> queries = null)
        {
            if (queries != null && queries.Count() > 0)
            {
                var query = GetQuery(queries);
                pagination.TotalItems = entities.Where(query).Count();
                entities = entities.Where(query).Paginate(pagination);
            }
            else
            {
                pagination.TotalItems = entities.Count();
                entities = entities.Paginate(pagination);
            }
            pagination.TotalPages = GetTotalPages(pagination);
            return GetListTDto(entities.ToList());
        }
    }
}
