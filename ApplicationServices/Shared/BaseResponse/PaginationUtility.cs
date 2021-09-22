using X.PagedList;

namespace ApplicationServices.Shared.BaseResponse
{
    public class PaginationUtility
    {
        public static PaginationResponse GetPaginationResponse(IPagedList<object> obj)
        {
            return new PaginationResponse()
            {
                PageNumber = obj.PageNumber,
                FirstItemOnPage = obj.FirstItemOnPage,
                HasNextPage = obj.HasNextPage,
                HasPreviousPage = obj.HasPreviousPage,
                LastItemOnPage = obj.LastItemOnPage,
                PageCount = obj.PageCount,
                PageSize = obj.PageSize,
                TotalItemCount = obj.TotalItemCount
            };
        } 
        
    }

    public class PaginationResponse
    {
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public int FirstItemOnPage { get; set; }
        public int LastItemOnPage { get; set; }
        public int TotalItemCount { get; set; }
    }
}