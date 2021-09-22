using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ApplicationServices.Shared.BaseResponse
{
    public class PagedResponse<T>
    {

        public PaginationResponse PageData { get; set; } = new PaginationResponse();
        public IPagedList<T> Record { get; set;  }
        public PagedResponse(IPagedList<T> items)
        {
            PageData.PageNumber = items.PageNumber;
            PageData.FirstItemOnPage = items.FirstItemOnPage;
            PageData.HasNextPage = items.HasNextPage;
            PageData.HasPreviousPage = items.HasPreviousPage;
            PageData.LastItemOnPage = items.LastItemOnPage;
            PageData.PageCount = items.PageCount;
            PageData.PageSize = items.PageSize;
            PageData.TotalItemCount = items.TotalItemCount;
            Record = items;

        } 

        public static async Task<PagedResponse<T>> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var items = await source.ToPagedListAsync(pageNumber, pageSize); 
            return new PagedResponse<T>(items);
        }
       
       
    }
}