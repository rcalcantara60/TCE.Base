using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TCE.CrossCutting.Dto;
using TCE.AppLayerBase.Base;
using TCE.Web.Api.Extensions;
using Newtonsoft.Json;

namespace TCE.Web.Api.Base
{
    public abstract class BaseController<TDto> : Controller where TDto : class
    {       
        protected PaginationDto RequestPagination { get; set; }

        protected IAppServiceBase<TDto> _appService { get; set; }

        public BaseController(IAppServiceBase<TDto> appService)
        {
            _appService = appService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            GetPagination();
        }

        private void GetPagination()
        {            
            var pagination = Request.Headers["Pagination"];
            if (!string.IsNullOrEmpty(pagination))
            {   
                RequestPagination = JsonConvert.DeserializeObject<PaginationDto>(pagination, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Response.AddPagination(RequestPagination);
        }

        /// <summary>
        /// Obtém uma lista com TODOS os itens de <typeparamref name="TDto"/>.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public virtual IActionResult Get()
        {
            return new OkObjectResult(_appService.All());
        }

        /// <summary>
        /// Obtém uma lista com TODOS os itens de <typeparamref name="TDto"/> paginado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPaginated")]
        public virtual IActionResult GetPaginated()
        {
            return new OkObjectResult(_appService.AllPaginated(RequestPagination));
        }

        /// <summary>
        /// Obtém uma lista com TODOS os itens de <typeparamref name="TDto"/> ordenada.
        /// </summary>
        /// <param name="order">Campo de orndenação</param>
        /// <param name="sortOrder">Ordem de sort, default asc</param>
        /// <returns></returns>
        [HttpGet("GetSorted/{order}/{sortOrder?}")]
        public virtual IActionResult GetSorted(string order, string sortOrder = "asc")
        {
            return new OkObjectResult(_appService.All(order, sortOrder));
        }

        /// <summary>
        /// Obtém uma lista de itens filtrada. Reader de paginação obrigatório.
        /// </summary>
        /// <param name="queries">Consulta com CAMPO + COMPARADOR + VALOR</param>
        /// <returns></returns>
        [HttpPost("GetFiltered")]
        public virtual IActionResult GetFilterd([FromBody] IEnumerable<DinamicQueryDto> queries)
        {
            return new OkObjectResult(_appService.AllPaginated(RequestPagination, queries));
        }

        /// <summary>
        /// Obterm um item pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual IActionResult Get(int id)
        {
            var entityDto = _appService.GetSingle(id);
            if (entityDto == null)
                return NotFound();

            return new OkObjectResult(entityDto);
        }

    }
}
