using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;
using TCE.CrossCutting.Dto;

namespace TCE.Web.Api.Extensions
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, PaginationDto pagination)
        {
            var paginationHeader = new PaginationHeader(pagination.Page, pagination.ItemsPerPage, pagination.TotalItems, pagination.TotalPages, pagination.Order, pagination.SortOrder);

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            // CORS
            response.Headers.Add("access-control-expose-headers", "Pagination");
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            message = Regex.Replace(message, @"[^\u001F-\u007F]+", string.Empty);

            response.Headers.Add("Application-Error", message);
            // CORS
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
}
