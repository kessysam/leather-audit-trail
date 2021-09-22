namespace ApplicationServices.Shared.BaseResponse
{
    public class PaginationParameter
    {
        private const int maxPageSize = 100;

        private int _pageSize = maxPageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public int Page { get; set; } = 1;
    }
}