using SSTVN.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int SubmitChanges();
        IRepository<T> Repository<T>() where T : class;
    }
}
