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
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using devPMS.WebCore.Services;
using System.Web.Mvc;

namespace devPMS.Web.Controllers
{
	[RoutePrefix("TimeTracking")]
	public class TimeTrackingController : Controller
	{
		private readonly IDbContextFactory _factory;
		private TeamServices _teamServices;
		private EmployeeServices _empServices;

		public TimeTrackingController(TeamServices teamServices, EmployeeServices empServices, IDbContextFactory factory)
		{
			_teamServices = teamServices;
			_empServices = empServices;
			_factory = factory;
		}
		

		// GET: TimeTracking
		public ActionResult TransactionView()
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
					foreach(var e in searchresult.Entities)
					{
						if (e.UserID.HasValue && e.FirstIn.HasValue)
						{
							var timeTracking = new TimeTrackingModel();
							timeTracking.UserId = e.UserID.Value;
							timeTracking.Fullname = e.Name;
							timeTracking.Date = e.FirstIn.Value.Date;
							timeTracking.FirstIn = e.FirstIn.HasValue ? e.FirstIn.ToString() : string.Empty;
							timeTracking.LastOut = e.LastOut.HasValue ? e.LastOut.ToString() : string.Empty;
							timeTracking.EventNote = e.EventName;
							timeTracking.TAWeekID = e.TAWeekID;
							timeTracking.ValueVacation = GetValueVacation((int)e.UserID.Value, e.FirstIn.Value.Date);
							model.Add(timeTracking);
						}
					}
				}
				catch (Exception e)
				{

				}
			}
			return View(model);
		}

		public ActionResult TransactionTimeXML()
		{
			List<TimeTrackingModel> model = new List<TimeTrackingModel>();
			using (var uow = _factory.Create(DBContextName.SmartTAS2012Entities))
			{
				try
				{
					var query = new SearchQuery<TransactionView>();
					var dtquery = new DateTime(2017, 02, 01);
					query.AddBaseFilter(p => p.FirstIn > dtquery);
					query.Take = 10;
					var searchresult = uow.Repository<TransactionView>().Search(query);
					foreach (var e in searchresult.Entities)
					{
						if (e.UserID.HasValue && e.FirstIn.HasValue)
						{
							var timeTracking = new TimeTrackingModel();
							timeTracking.UserId = e.UserID.Value;
							timeTracking.Fullname = e.Name;
							timeTracking.Date = e.FirstIn.Value.Date;
							timeTracking.FirstIn = e.FirstIn.HasValue ? e.FirstIn.ToString() : string.Empty;
							timeTracking.LastOut = e.LastOut.HasValue ? e.LastOut.ToString() : string.Empty;
							timeTracking.EventNote = e.EventName;
							timeTracking.TAWeekID = e.TAWeekID;
							timeTracking.ValueVacation = GetValueVacation((int)e.UserID.Value, e.FirstIn.Value.Date);
							model.Add(timeTracking);
						}
					}
				}
				catch (Exception e)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			if (model.Count == 0) return HttpNotFound();
			
			return new XmlResult<List<TimeTrackingModel>>()
			{
				Data = model
			};
		}

		[Route("TimeLogXML/{teamName}/{fromDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}/{toDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
		public ActionResult TimeLogXML(string teamName, DateTime fromDate, DateTime toDate)
		{
			List<TimeLogModel> model;
			using (var uow = _factory.Create(DBContextName.PMSEntities))
			{
				try
				{
					var query = new SearchQuery<ViewTimeLogPerDate>();
					query.AddBaseFilter(p => p.Team.Equals(teamName));
					query.AddBaseFilter(p => p.WorkedDate >= fromDate
							&& p.WorkedDate <= toDate);

					var searchResult = uow.Repository<ViewTimeLogPerDate>().Search(query);
					model = searchResult.Entities.Select(p => new TimeLogModel
					{
						FullName = p.FullName,
						WorkdedDate = p.WorkedDate,
						Hours = p.TotalHours,
						Team = p.Team
					}).ToList();
				}
				catch (Exception e)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			if (model.Count == 0) return HttpNotFound();
			
			return new XmlResult<List<TimeLogModel>>()
			{
				Data = model,
				FileName = "TimeLog" + DateTime.Now.ToString("ddmmyyyyhhss")
		};
		}

		double GetValueVacation(int userid, DateTime date)
		{
			try
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
			}
			catch { }
			return 0;
		}

		// GET: TimeTracking
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

	   
		public ActionResult LoadTeamList()
		{
			var teamList = _teamServices.GetAllTeams();
			var result = new JsonResult();
			if (teamList != null)
			{
				result = Json(new
				{
					success = "true",
					teamList = teamList
				}, JsonRequestBehavior.AllowGet);
			}
			else
			{
				result = Json(new { success = "false" });
			}
			return result;
		}
		[HttpPost]
		public ActionResult LoadEmpByTeamId(string Id)
		{
			int intId;
			int.TryParse(Id, out intId);
			var empList = _empServices.GetUserById(intId);
			var result = new JsonResult();
			if (empList != null)
			{
				result = Json(new
				{
					success = "true",
					empList = empList
				}, JsonRequestBehavior.AllowGet);
			}
			else
			{
				result = Json(new { success = "false" });
			}
			return result;
		}
	}
}