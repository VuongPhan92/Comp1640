using University;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore
{
    public class DbContextFactory : IDbContextFactory
    {
        public IUnitOfWork Create(string contextType)
        {
            switch (contextType)
            {
              
                case DBContextName.UniversityEntities:
                    return new UnitOfWork<UniversityEntities>();
                default:
                    throw new ArgumentException("Unknown contextType...");
            }
        }

        // see comment in IDbContextFactory inteface...
        //public void Dispose()
        //{
        //    if (_context != null)
        //    {
        //        _context.Dispose();
        //        GC.SuppressFinalize(this);
        //    }
        //}
    }
}
