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
        protected PaginationDto Pagination { get; set; }
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
            Pagination = new PaginationDto();
            var pagination = Request.Headers["Pagination"];
            if (!string.IsNullOrEmpty(pagination))
            {
                Pagination = JsonConvert.DeserializeObject<PaginationDto>(pagination);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Response.AddPagination(Pagination);
        }

        /// <summary>
        /// Obtém uma lista de itens.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public virtual IActionResult Get()
        {
            return new OkObjectResult(_appService.AllPaginated(Pagination));
        }

        [HttpGet("{order},{sortOrder}")]
        public virtual IActionResult Get(string order, string sortOrder)
        {
            return new OkObjectResult(_appService.All(order, sortOrder));
        }

        /// <summary>
        /// Obtém uma lista de itens filtrada.
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        [HttpPost("GetFiltered")]
        public virtual IActionResult GetFilterd([FromBody] IEnumerable<DinamicQueryDto> queries)
        {
            return new OkObjectResult(_appService.AllPaginated(Pagination, queries));
        }

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
