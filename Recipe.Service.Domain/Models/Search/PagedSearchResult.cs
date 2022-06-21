using System.Collections.Generic;

namespace Recipe.Service.Domain.Models.Search;

public class PagedSearchResult<TEntity>
{
    public IList<TEntity> Records { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public long TotalRecords { get; set; }
}
