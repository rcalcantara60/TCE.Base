using Microsoft.AspNetCore.Mvc;
using TCE.AppLayerBase.Base;

namespace TCE.Web.Api.Base
{
    public abstract class BaseCrudController<TDto> : BaseController<TDto> where TDto : class
    {
        public BaseCrudController(IAppServiceBase<TDto> appService) : base(appService)
        {
        }

        [HttpPost]
        public virtual IActionResult Post([FromBody]TDto entityDto)
        {
            var validation = _appService.Add(entityDto);
            if (!validation.IsValid)
            {
                return BadRequest(validation);
            }
            return new OkObjectResult("OK");
        }

        [HttpPut]
        public virtual IActionResult Put([FromBody]TDto entityDto)
        {
            var validation = _appService.Update(entityDto);

            if (!validation.IsValid)
            {
                return BadRequest(validation);
            }
            return new OkObjectResult("ok");
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            _appService.LogicDelete(id);
            return new OkObjectResult("ok");
        }
    }
}
