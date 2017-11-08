using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDoCommon.Data;
using DDoCommon.Decorator;
using DDoCommon.EF6;
using devPMS.Data;
using devPMS.Domain.Interfaces.Services;

namespace devPMS.Services.Queries
{
    public class GetProjectAssignmentByIdQueryHandler : IQueryHandler<GetProjectAssignmentByIdQuery, Assignment>
    {
        protected readonly IUnitOfWorkManager _uowManager;
        protected readonly IEFUnitOfWork _uow;
        protected readonly ILoggingService _loggingService;

        public GetProjectAssignmentByIdQueryHandler(IUnitOfWorkManager unitOfWorkManager, ILoggingService loggingService)
        {
            _uowManager = unitOfWorkManager;
            _uow = _uowManager.CurrentUnitOfWork as IEFUnitOfWork;
            _loggingService = loggingService;
        }

        public Assignment Handle(GetProjectAssignmentByIdQuery query)
        {
            try
            {
                var result = _uow.Repository<Assignment>().Get(p => p.ID == query.Id && p.DeteledDateTime == null) ;
                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                _loggingService.LogError(dbEx, $"An assignemt record with id '{query.Id}' has error when retrieve.");
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
