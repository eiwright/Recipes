namespace Recipe.Service.Domain.Models.Search;

public class PagedSearch
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string SearchString { get; set; }
    public string SortField { get; set; }
    public bool SortAsc { get; set; } = true;
}
