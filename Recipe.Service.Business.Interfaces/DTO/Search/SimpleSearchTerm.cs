using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service.Business.Interfaces.DTO.Search;

public class SimpleTermSearch
{
    public int PageSize { get; set; } = 25;
    public int PageOffset { get; set; }
    public string SearchString { get; set; }
    public string SortField { get; set; }
    public bool SortAsc { get; set; } = true;

}
