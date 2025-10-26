namespace LibraryManagmentSystem.Shared.Response
{
    public class PaginationInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages =>
        (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
