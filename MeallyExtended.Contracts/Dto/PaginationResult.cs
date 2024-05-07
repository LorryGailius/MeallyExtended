namespace MeallyExtended.Contracts.Dto
{
    internal class PaginationResult<T>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public required List<T> Data { get; set; }
    }
 }
