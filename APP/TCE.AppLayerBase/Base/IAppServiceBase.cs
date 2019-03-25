using System.Collections.Generic;
using System.Threading.Tasks;
using TCE.CrossCutting.Dto;

namespace TCE.AppLayerBase.Base
{
    public interface IAppServiceBase<TDto> where TDto : class
    {
        IEnumerable<TDto> All();
        IEnumerable<TDto> All(string order, string sortOrder);
        IEnumerable<TDto> AllPaginated(PaginationDto pagination);
        IEnumerable<TDto> AllPaginated(PaginationDto pagination, IEnumerable<DinamicQueryDto> queries);
        TDto GetSingle(int id);
        ValidationResultDto Add(TDto entityDto);
        ValidationResultDto Update(TDto entityDto);
        ValidationResultDto Delete(TDto entityDto);
        ValidationResultDto LogicDelete(int id);
        int Count();
    }
}
