using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using TCE.CrossCutting.Dto;

namespace TCE.AppLayerBase.Extensions
{
    public static class ExtensionsMethods
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> entities, PaginationDto pagination)
        {
            return entities
                .OrderBy(pagination.Order + " " + pagination.SortOrder)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
    }
}
