using devPMS.Data;
using devPMS.Web.Models;
using devPMS.WebCore;
using devSmartTAS.Data;
using Infrastructure.Repository;
using SSTVN.Core.Repository.PageResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace devPMS.Web.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        private readonly IDbContextFactory _factory;

        public TestController(IDbContextFactory factory)
        {
            _factory = factory;
        }

        [Route("TransactionView")]
        [ResponseType(typeof(IList<TimeTrackingModel>))]
        public IHttpActionResult TransactionView()
        {
            IList<TimeTrackingModel> model = new List<TimeTrackingModel>();
            using (var uow = _factory.Create(DBContextName.SmartTAS2012Entities))
            {
                try
                {
                    var query = new SearchQuery<TransactionView>();
                    query.AddBaseFilter(p => p.FirstIn > new DateTime(2017, 01, 01));
                    query.Take = 10;
                    var searchresult = uow.Repository<TransactionView>().Search(query);
                    foreach (var e in searchresult.Entities)
                    {
                        if (e.UserID.HasValue && e.FirstIn.HasValue)
                        {
                            var timeTracking = new TimeTrackingModel();
                            timeTracking.UserId = e.UserID.Value;
                            timeTracking.Fullname = e.Name;
                            timeTracking.FirstIn = e.FirstIn.HasValue ? e.FirstIn.ToString() : string.Empty;
                            timeTracking.LastOut = e.LastOut.HasValue ? e.LastOut.ToString() : string.Empty;
                            timeTracking.EventNote = e.EventName;
                            timeTracking.TAWeekID = e.TAWeekID;
                            timeTracking.ValueVacation = GetValueVacation(e.UserID.Value, e.FirstIn.Value);
                            model.Add(timeTracking);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
            return Ok(model);
        }

        double GetValueVacation(long userid, DateTime date)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {
                var repo = uow.Repository<ViewApprovedVacationPerDate>();
                var entity = repo.GetById(p => p.EmpId == userid && p.OrderDateTime == date);
                if (entity != null)
                {
                    return entity.TotalValue.GetValueOrDefault();
                }
            }
            return 0;
        }
    }
}
