using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTVN.Core.Repository.Sort
{
    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1
    }

    public interface ISortCriteria<T>
    {
        SortDirection Direction { get; set; }

        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, Boolean useThenBy);
    }
}
