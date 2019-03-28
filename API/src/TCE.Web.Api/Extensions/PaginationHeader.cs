
namespace TCE.Web.Api.Extensions
{
    public class PaginationHeader_
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string Order { get; set; }
        public string SortOrder { get; set; }

        public PaginationHeader_(int currentPage, int itemsPerPage, int totalItems, int totalPages, string order, string sortOrder)
        {
            this.CurrentPage = currentPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
            this.Order = order;
            this.SortOrder = sortOrder;
        }
    }
}
