
using Newtonsoft.Json;

namespace TCE.CrossCutting.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class PaginationDto
    {
        [JsonProperty("page", Required = Required.Always)]
        public int Page { get; set; }
        [JsonProperty("pageSize", Required = Required.Always)]
        public int PageSize { get; set; }
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("order")]
        public string Order { get; set; }
        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }

        public PaginationDto()
        {

        }

        public PaginationDto(int page, int pageSize, int totalItems, int totalPages, string order, string sortOrder)
        {
            this.Page = page;
            this.PageSize = pageSize;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
            this.Order = order;
            this.SortOrder = sortOrder;
        }
    }
}
