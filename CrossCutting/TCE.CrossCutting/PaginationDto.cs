
namespace TCE.CrossCutting.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string Order { get; set; } = "";
        public string SortOrder { get; set; } = "";
    }
}
