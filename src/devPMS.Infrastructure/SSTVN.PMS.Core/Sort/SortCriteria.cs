﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTVN.Core.Repository.Sort
{
    //-----------------------------------------------------------------------
    /// <summary>
    /// Represents a sort expression where a property of the sequence item is sorted upon.
    /// Useful to avoid case statement when doing "simple" sorts by "simple" property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortCriteria<T> : ISortCriteria<T> where T : class
    {
        //-----------------------------------------------------------------------
        public String Name { get; set; }

        //-----------------------------------------------------------------------
        public SortDirection Direction { get; set; }

        //-----------------------------------------------------------------------
        public SortCriteria()
        {
            this.Direction = SortDirection.Ascending;
        }

        //-----------------------------------------------------------------------
        public SortCriteria(String name, SortDirection direction)
            : base()
        {
            Name = name;
            Direction = direction;
        }

        //-----------------------------------------------------------------------
        public IOrderedQueryable<T> ApplyOrdering(IQueryable<T> qry, Boolean useThenBy)
        {
            IOrderedQueryable<T> result = null;
            var descending = this.Direction == SortDirection.Descending;
            result = !useThenBy ? qry.OrderBy(Name, descending) : qry.ThenBy(Name, descending);
            return result;
        }
    }
}
