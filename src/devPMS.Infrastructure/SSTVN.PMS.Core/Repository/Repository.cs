using SSTVN.Core.Repository.PageResult;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSTVN.Core.Repository
{
    #region OldCode
    //public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    //{
    //    internal DbContext context;
    //    internal DbSet<T> dbSet;
    //    PagedListResult<T> pagedResult;

    //    public GenericRepository(DbContext context)
    //    {
    //        this.context = context;
    //        context.Configuration.ProxyCreationEnabled = false;
    //        this.dbSet = context.Set<T>();
    //    }

    //    public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
    //    {
    //        return dbSet.SqlQuery(query, parameters).ToList();
    //    }

    //    public virtual PagedListResult<T> Search(SearchQuery<T> searchQuery)
    //    {
    //        IQueryable<T> sequence = dbSet;

    //        pagedResult = new PagedListResult<T>();

    //        //Applying filters
    //        sequence = ManageFilters(searchQuery, sequence);

    //        //Include Properties
    //        sequence = ManageIncludeProperties(searchQuery, sequence);

    //        //Resolving Sort Criteria
    //        //This code applies the sorting criterias sent as the parameter
    //        sequence = ManageSortCriterias(searchQuery, sequence);

    //        return GetTheResult(searchQuery, sequence);
    //    }

    //    /// <summary>
    //    /// Executes the query against the repository (database).
    //    /// </summary>
    //    /// <param name="searchQuery"></param>
    //    /// <param name="sequence"></param>
    //    /// <returns></returns>
    //    protected virtual PagedListResult<T> GetTheResult(SearchQuery<T> searchQuery, IQueryable<T> sequence)
    //    {
    //        //Counting the total number of object.
    //        var resultCount = sequence.Count();

    //        var result = (searchQuery.Take > 0)
    //                            ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
    //                            : (sequence.ToList());

    //        // Setting up the return object.
    //        bool hasNext = (searchQuery.Skip <= 0 && searchQuery.Take <= 0) ? false : (searchQuery.Skip + searchQuery.Take < resultCount);

    //        pagedResult.Entities = result;
    //        pagedResult.HasNext = hasNext;
    //        pagedResult.HasPrevious = (searchQuery.Skip > 0);
    //        pagedResult.Count = resultCount;

    //        return pagedResult;
    //    }

    //    /// <summary>
    //    /// Resolves and applies the sorting criteria of the SearchQuery
    //    /// </summary>
    //    /// <param name="searchQuery"></param>
    //    /// <param name="sequence"></param>
    //    /// <returns></returns>
    //    protected virtual IQueryable<T> ManageSortCriterias(SearchQuery<T> searchQuery, IQueryable<T> sequence)
    //    {
    //        if (searchQuery.SortCriterias != null && searchQuery.SortCriterias.Count > 0)
    //        {
    //            var sortCriteria = searchQuery.SortCriterias[0];
    //            var orderedSequence = sortCriteria.ApplyOrdering(sequence, false);

    //            if (searchQuery.SortCriterias.Count > 1)
    //            {
    //                for (var i = 1; i < searchQuery.SortCriterias.Count; i++)
    //                {
    //                    var sc = searchQuery.SortCriterias[i];
    //                    orderedSequence = sc.ApplyOrdering(orderedSequence, true);
    //                }
    //            }
    //            sequence = orderedSequence;
    //        }
    //        else
    //        {
    //            sequence = ((IOrderedQueryable<T>)sequence).OrderBy(x => (true));
    //        }
    //        return sequence;
    //    }

    //    /// <summary>
    //    /// Chains the where clause to the IQueriable instance.
    //    /// </summary>
    //    /// <param name="searchQuery"></param>
    //    /// <param name="sequence"></param>
    //    /// <returns></returns>
    //    protected virtual IQueryable<T> ManageFilters(SearchQuery<T> searchQuery, IQueryable<T> sequence)
    //    {
    //        //Apply base filter
    //        if (searchQuery.BaseFilters != null && searchQuery.BaseFilters.Count > 0)
    //        {
    //            foreach (var filterClause in searchQuery.BaseFilters)
    //            {
    //                sequence = sequence.Where(filterClause);
    //            }
    //        }

    //        pagedResult.BaseCount = sequence.Count();

    //        if (searchQuery.Filters != null && searchQuery.Filters.Count > 0)
    //        {
    //            foreach (var filterClause in searchQuery.Filters)
    //            {
    //                sequence = sequence.Where(filterClause);
    //            }
    //        }
    //        return sequence;
    //    }

    //    /// <summary>
    //    /// Includes the properties sent as part of the SearchQuery.
    //    /// </summary>
    //    /// <param name="searchQuery"></param>
    //    /// <param name="sequence"></param>
    //    /// <returns></returns>
    //    protected virtual IQueryable<T> ManageIncludeProperties(SearchQuery<T> searchQuery, IQueryable<T> sequence)
    //    {
    //        if (!string.IsNullOrWhiteSpace(searchQuery.IncludeProperties))
    //        {
    //            var properties = searchQuery.IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

    //            foreach (var includeProperty in properties)
    //            {
    //                sequence = sequence.Include(includeProperty);
    //            }
    //        }
    //        return sequence;
    //    }

    //    public virtual int Count(Expression<Func<T, bool>> filter = null)
    //    {
    //        IQueryable<T> query = dbSet;

    //        if (filter != null)
    //        {
    //            return query.Where(filter).Count();
    //        }
    //        else
    //        {
    //            return query.Count();
    //        }
    //    }

    //    #region Existing code
    //    /*
    //    public virtual IEnumerable<TEntity> Get(
    //        Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        string includeProperties = "")
    //    {
    //        IQueryable<TEntity> query = dbSet;

    //        if (filter != null)
    //        {
    //            query = query.Where(filter);
    //        }

    //        foreach(var includeProperty in includeProperties.Split
    //            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //        {
    //            query = query.Include(includeProperties);
    //        }

    //        if(orderBy != null)
    //        {
    //            return orderBy(query).ToList();
    //        }
    //        else
    //        {
    //            return query.ToList();
    //        }
    //    }

    //    // page number. 
    //    // items on page.
    //    // Skip = PageNumber * Items on page
    //    // take = items on page
    //    public virtual IEnumerable<TEntity> Get(
    //        int? pageIndex, int pageSize,
    //        Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        string includeProperties = "")
    //    {
    //        IQueryable<TEntity> query = dbSet;

    //        if (filter != null)
    //        {
    //            query = query.Where(filter);
    //        }

    //        foreach (var includeProperty in includeProperties.Split
    //            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //        {
    //            query = query.Include(includeProperties);
    //        }

    //        var truePageIndex = pageIndex ?? 0;
    //        var itemIndex = truePageIndex * pageIndex;
    //        query.Skip(truePageIndex).Take(pageSize);

    //        if (orderBy != null)
    //        {
    //            return orderBy(query).ToList();
    //        }
    //        else
    //        {
    //            return query.ToList();
    //        }
    //    }

    //    public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
    //    {
    //        IQueryable<TEntity> query = dbSet;

    //        if (filter != null)
    //        {
    //            return query.Where(filter).Count();
    //        }
    //        else
    //        {
    //            return query.Count();
    //        }
    //    }
    //    */
    //    #endregion

    //    public virtual IList<T> GetAll(string IncludeProperties = "")
    //    {
    //        IQueryable<T> sequence = dbSet;
    //        if (!string.IsNullOrWhiteSpace(IncludeProperties))
    //        {
    //            var properties = IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

    //            foreach (var includeProperty in properties)
    //            {
    //                sequence = sequence.Include(includeProperty);
    //            }
    //        }
    //        return sequence.ToList();
    //    }

    //    public virtual T GetById(Expression<Func<T, bool>> filter, string IncludeProperties = "")
    //    {
    //        IQueryable<T> sequence = dbSet;
    //        if (!string.IsNullOrEmpty(IncludeProperties))
    //        {
    //            var properties = IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
    //            foreach (var includeProperty in properties)
    //            {
    //                sequence = sequence.Include(includeProperty);
    //            }
    //        }
    //        return sequence.FirstOrDefault(filter);
    //    }

    //    public virtual T GetById(object id)
    //    {
    //        return dbSet.Find(id);
    //    }

    //    public virtual void Insert(T entity)
    //    {
    //        dbSet.Add(entity);
    //    }

    //    public virtual void Delete(object id)
    //    {
    //        T entityToDelete = dbSet.Find(id);
    //        Delete(entityToDelete);
    //    }

    //    public virtual void Delete(T entityToDelete)
    //    {
    //        if (context.Entry(entityToDelete).State == EntityState.Detached)
    //        {
    //            dbSet.Attach(entityToDelete);
    //        }
    //        dbSet.Remove(entityToDelete);
    //    }

    //    public virtual void Update(T entityToUpdate)
    //    {
    //        dbSet.Attach(entityToUpdate);
    //        context.Entry(entityToUpdate).State = EntityState.Modified;
    //    }

    //    public void Dispose()
    //    {

    //    }
    //}
    #endregion
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        protected DbContext context;
        protected DbSet<TEntity> dbSet;
        PagedListResult<TEntity> pagedResult;

        public Repository(DbContext context)
        {
            this.context = context;
            context.Configuration.ProxyCreationEnabled = false;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).ToList();
        }

        #region Search methods
        public virtual PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery)
        {
            IQueryable<TEntity> sequence = dbSet;

            pagedResult = new PagedListResult<TEntity>();

            //Applying filters
            sequence = ManageFilters(searchQuery, sequence);

            //Include Properties
            sequence = ManageIncludeProperties(searchQuery, sequence);

            //Resolving Sort Criteria
            //This code applies the sorting criterias sent as the parameter
            sequence = ManageSortCriterias(searchQuery, sequence);

            return GetTheResult(searchQuery, sequence);
        }

        /// <summary>
        /// Executes the query against the repository (database).
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        protected virtual PagedListResult<TEntity> GetTheResult(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        {
            //Counting the total number of object.
            var resultCount = sequence.Count();

            var result = (searchQuery.Take > 0)
                                ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
                                : (sequence.ToList());

            // Setting up the return object.
            bool hasNext = (searchQuery.Skip <= 0 && searchQuery.Take <= 0) ? false : (searchQuery.Skip + searchQuery.Take < resultCount);

            pagedResult.Entities = result;
            pagedResult.HasNext = hasNext;
            pagedResult.HasPrevious = (searchQuery.Skip > 0);
            pagedResult.Count = resultCount;

            return pagedResult;
        }

        /// <summary>
        /// Resolves and applies the sorting criteria of the SearchQuery
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ManageSortCriterias(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        {
            if (searchQuery.SortCriterias != null && searchQuery.SortCriterias.Count > 0)
            {
                var sortCriteria = searchQuery.SortCriterias[0];
                var orderedSequence = sortCriteria.ApplyOrdering(sequence, false);

                if (searchQuery.SortCriterias.Count > 1)
                {
                    for (var i = 1; i < searchQuery.SortCriterias.Count; i++)
                    {
                        var sc = searchQuery.SortCriterias[i];
                        orderedSequence = sc.ApplyOrdering(orderedSequence, true);
                    }
                }
                sequence = orderedSequence;
            }
            else
            {
                sequence = ((IOrderedQueryable<TEntity>)sequence).OrderBy(x => (true));
            }
            return sequence;
        }

        /// <summary>
        /// Chains the where clause to the IQueriable instance.
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ManageFilters(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        {
            //Apply base filter
            if (searchQuery.BaseFilters != null && searchQuery.BaseFilters.Count > 0)
            {
                foreach (var filterClause in searchQuery.BaseFilters)
                {
                    sequence = sequence.Where(filterClause);
                }
            }

            pagedResult.BaseCount = sequence.Count();

            if (searchQuery.Filters != null && searchQuery.Filters.Count > 0)
            {
                foreach (var filterClause in searchQuery.Filters)
                {
                    sequence = sequence.Where(filterClause);
                }
            }
            return sequence;
        }

        /// <summary>
        /// Includes the properties sent as part of the SearchQuery.
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ManageIncludeProperties(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery.IncludeProperties))
            {
                var properties = searchQuery.IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProperty in properties)
                {
                    sequence = sequence.Include(includeProperty);
                }
            }
            return sequence;
        }
        #endregion

        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity GetById(Expression<Func<TEntity, bool>> filter, string IncludeProperties = "")
        {
            IQueryable<TEntity> sequence = dbSet;
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                var properties = IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var includeProperty in properties)
                {
                    sequence = sequence.Include(includeProperty);
                }
            }
            return sequence.FirstOrDefault(filter);
        }

        public virtual IEnumerable<TEntity> GetAll(string IncludeProperties = "")
        {
            IQueryable<TEntity> sequence = dbSet;
            if (!string.IsNullOrWhiteSpace(IncludeProperties))
            {
                var properties = IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProperty in properties)
                {
                    sequence = sequence.Include(includeProperty);
                }
            }
            return sequence.ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string IncludeProperties = "")
        {
            IQueryable<TEntity> sequence = dbSet;
            if (!string.IsNullOrWhiteSpace(IncludeProperties))
            {
                var properties = IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProperty in properties)
                {
                    sequence = sequence.Include(includeProperty);
                }
            }
            return sequence.Where(predicate).ToList();
        }

        //
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        //
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public virtual void Remove(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                return query.Where(filter).Count();
            }
            else
            {
                return query.Count();
            }
        }

        public virtual void Dispose()
        {

        }
    }
}
