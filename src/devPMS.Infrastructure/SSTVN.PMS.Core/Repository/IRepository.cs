using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SSTVN.Core.Repository.PageResult;

namespace SSTVN.Core.Repository
{
    #region OldCode
    //public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    //{
    //    IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);

    //    PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery);
    //    int Count(Expression<Func<TEntity, bool>> filter = null);

    //    /* I would like to replace these methods below with single method as Search: 
    //     * for more information, go to here: http://www.agile-code.com/blog/entity-framework-code-first-filtering-and-sorting-with-paging-1/
    //    IEnumerable<TEntity> Get(
    //        Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        string includeProperties = "");

    //    IEnumerable<TEntity> Get(
    //        int? pageIndex, int pageSize,
    //        Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        string includeProperties = "");
       
    //    int Count(Expression<Func<TEntity, bool>> filter = null);
    //     */

    //    IList<TEntity> GetAll(string IncludeProperties = "");

    //    TEntity GetById(object id);
    //    TEntity GetById(Expression<Func<TEntity, bool>> filter, string IncludeProperties = "");

    //    void Insert(TEntity entity);

    //    void Delete(object id);
    //    void Delete(TEntity entityToDelete);

    //    void Update(TEntity entityToUpdate);
    //}
    #endregion

    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        //
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        //
        PagedListResult<TEntity> Search(SearchQuery<TEntity> searchQuery);
        int Count(Expression<Func<TEntity, bool>> filter = null);
        //
        TEntity Get(object id);
        TEntity GetById(Expression<Func<TEntity, bool>> filter, string IncludeProperties = "");
        //
        IEnumerable<TEntity> GetAll(string IncludeProperties = "");
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string IncludeProperties = "");
        //
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        //
        bool Any(Expression<Func<TEntity, bool>> predicate);
        //
        void Remove(object id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        // no need Update or Save method in repository
        void Update(TEntity entityToUpdate);

    }
}
