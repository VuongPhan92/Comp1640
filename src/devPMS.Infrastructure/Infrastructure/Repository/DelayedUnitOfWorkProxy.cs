using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSTVN.Core.Repository;
using System.Collections;

namespace Infrastructure.Repository
{
    //public class DelayedUnitOfWorkProxy : IUnitOfWork, IDisposable
    //{
    //    private Lazy<IUnitOfWork> uow;

    //    public DelayedUnitOfWorkProxy(Lazy<IUnitOfWork> uow)
    //    {
    //        this.uow = uow;
    //    }
       
    //    int IUnitOfWork.SubmitChanges()
    //    {
    //        return this.uow.Value.SubmitChanges();
    //    }

    //    // TODO: Implement All other IUnitOfWork methods
    //    private Hashtable _repositories;
    //    public IRepository<T> Repository<T>() where T : class
    //    {
    //        if (_repositories == null)
    //            _repositories = new Hashtable();

    //        var type = typeof(T).Name;

    //        if (!_repositories.ContainsKey(type))
    //        {
    //            var repositoryType = typeof(Repository<>);

    //            var repositoryInstance =
    //                Activator.CreateInstance(repositoryType
    //                        .MakeGenericType(typeof(T)), this);

    //            _repositories.Add(type, repositoryInstance);
    //        }

    //        return (IRepository<T>)_repositories[type];
    //    }

    //    private bool _disposed = false;
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!_disposed)
    //        {
    //            if (disposing)
    //            {
    //                this.Dispose();
    //            }
    //        }
    //        _disposed = true;
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    void IDisposable.Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
