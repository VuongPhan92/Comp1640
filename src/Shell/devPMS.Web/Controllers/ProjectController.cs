using devPMS.Data;
using devPMS.Web.CustomMembership;
using devPMS.Web.Models;
using devPMS.WebCore.Business;
using devPMS.WebCore.Queries;
using devPMS.WebCore.Services;
using SSTVN.Core.Repository.PageResult;
using SSTVN.Core.Repository.Sort;
using SSTVN.DDo.Utility.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devPMS.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private ProjectServices _projectService;
        private ScopeServices _scopeService;
        private SubTaskServices _subTaskService;
        private EmployeeServices _employeeService;
        private CusIdentity _identity;
        private IEnumerable<Employee> _usersByTeamId;
        private ReferenceDataServices _referenceDataService;

        private ProjectReferenceData _referenceData;

        public ProjectController(ProjectServices projectService, ScopeServices scopeService,
            SubTaskServices subTaskService, EmployeeServices employeeService, ReferenceDataServices referenceDataService)
        {
            _projectService = projectService;
            _scopeService = scopeService;
            _subTaskService = subTaskService;
            _employeeService = employeeService;
            _referenceDataService = referenceDataService;
            if (System.Web.HttpContext.Current.User != null)
            {
                _identity = (System.Web.HttpContext.Current.User as CusPrincipal).Identity as CusIdentity;
            }
        }

        public IEnumerable<Employee> UsersByTeamId
        {
            get
            {
                if (_usersByTeamId == null || _usersByTeamId.Count() == 0)
                {
                    _usersByTeamId = _employeeService.GetUserByTeamId(_identity.User.TeamId);
                }
                return _usersByTeamId;
            }
        }

        public PartialViewResult GetEmployeeToolTip(int id, string name)
        {
            var emp = new myEmployeeData();
            emp.Id = id;
            emp.FullName = name;
            emp.Role = "Developer";
            return PartialView("_EmployeeToolTip",emp);
        }

        // GET: Project
        [HttpGet]
        public ActionResult CreateProject()//CreateProjectCommand model)
        {
            //var md = new CreateProjectCommand() { ProjectName = "test" };
            //_projectServices.CreateProject(md);
            return View();
        }

        [HttpGet]
        public ActionResult GetProject()
        {
            var md = new PendingProjectQuery();
            var projects = _projectService.GetPendingProjects(md);
            return View();
        }

        #region Project Table view

        public ActionResult Unassigned()
        {
            ViewBag.ShortCutNav = "Unassigned";
            return View("GetProject");
        }

        public ActionResult ActiveProjects()
        {
            ViewBag.ShortCutNav = "ActiveProjects";
            return View("GetProject");
        }

        public ActionResult AllProjects()
        {
            ViewBag.ShortCutNav = "AllProjects";
            return View("GetProject");
        }

        public ActionResult Table(string filter)
        {
            ViewBag.ShortCutNav = filter;
            return View("GetProject");
        }

        [HttpPost]
        public ActionResult AjaxGetJsonProjectData(jQueryDataTableParamModel param, string ShortCutNav)
        {
            var searchQuery = new SearchQuery<Project_Log>();
            var lstStatusCloseProject = new List<string>() { "Hold", "Completed", "Canceled" };

            switch (ShortCutNav)
            {
                case "INTAKE":
                    searchQuery.AddBaseFilter(p =>
                        p.Status.Equals("INTAKE", StringComparison.OrdinalIgnoreCase)
                        );
                    break;
                case "Unassigned":
                    searchQuery.AddBaseFilter(p =>
                        p.CountTeam == 0 &&
                        string.IsNullOrEmpty(p.Status)
                        );
                    break;
                case "ATS":
                    searchQuery.AddBaseFilter(p =>
                        p.ScopeOfService.ScopeName == "ATS"
                        && string.IsNullOrEmpty(p.Status)
                        );
                    break;
                case "MomentFrame":
                    searchQuery.AddBaseFilter(p =>
                        p.ScopeOfService.ScopeName == "MOMENT FRAME"
                        && string.IsNullOrEmpty(p.Status)
                        );
                    break;
                case "TakeOff":
                    searchQuery.AddBaseFilter(p =>
                        (p.ScopeOfService.ScopeName == "SOFT TAKE-OFF"
                        || p.ScopeOfService.ScopeName == "COLOR TAKE-OFF"
                        || p.ScopeOfService.ScopeName == "TAKE-OFF")
                        && string.IsNullOrEmpty(p.Status)
                        );
                    break;
                case "MyActiveProjects":
                    searchQuery.AddBaseFilter(p =>
                        string.IsNullOrEmpty(p.Status)
                        && p.Assignments.Any(a => a.EmpID == _identity.User.UserId
                            && !a.DeteledDateTime.HasValue
                            && !lstStatusCloseProject.Contains(a.Status))
                        );
                    break;
                case "TeamActiveProjects":
                    var listEmployees = UsersByTeamId.Select(p => p.EmployeeID);
                    searchQuery.AddBaseFilter(p =>
                        string.IsNullOrEmpty(p.Status)
                        && p.Assignments.Any(a => listEmployees.Contains(a.EmpID)
                        && !a.DeteledDateTime.HasValue
                        && !lstStatusCloseProject.Contains(a.Status))
                        );
                    break;
                default:
                    throw new HttpException(404, "Project not found");
            }
            return LoadProjectData(param, searchQuery);
        }

        public JsonResult LoadProjectData(jQueryDataTableParamModel param, SearchQuery<Project_Log> searchQuery)
        {
            //var searchQuery = new SearchQuery<Project_LogView>();

            //Search value for Key, Name, Scope, EOR
            var search = Request.Form.GetValues("search[value]").FirstOrDefault();

            //Find order columns info
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault()
                                    + "][data]").FirstOrDefault();

            //Ordering direction for the column.
            SortDirection sortDir;
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            if (sortColumnDir == "asc")
            {
                sortDir = SortDirection.Ascending;
            }
            else
            {
                sortDir = SortDirection.Descending;
            }

            if (!string.IsNullOrEmpty(search))
            {
                searchQuery.AddFilter(p =>
                p.ProjectNo.Contains(search) ||
                p.Name.Contains(search) ||
                //p.ScopeName.Contains(search) ||
                p.EOR.Contains(search) ||
                p.Status.Contains(search));
            }

            searchQuery.Skip = param.start;
            searchQuery.Take = param.length;
            searchQuery.AddSortCriteria(new SortCriteria<Project_Log>(sortColumn, sortDir));
            searchQuery.IncludeProperties = PropertyHelper<Project_Log>.GetPropertyName(p => p.ScopeOfService);

            var pagedListResut = _projectService.SearchProject(searchQuery);

            // Base count and filter count;
            var TotalProjects = pagedListResut.BaseCount;
            var recordsFilteredCount = pagedListResut.Count;
            // Adjust displaying entities
            var result = from pro in pagedListResut.Entities
                         select new
                         {
                             pro.ID,
                             pro.ProjectNo,
                             pro.Name,
                             ScopeOfService = pro.ScopeOfService.ScopeName,
                             pro.Branch,
                             DatetoVN = pro.DatetoVN.ToShortDateString(),
                             DueDate = pro.DueDate.HasValue ? pro.DueDate.Value.ToShortDateString() : string.Empty,
                             pro.EOR,
                             pro.Status
                         };

            return Json(new
            {
                draw = param.draw,
                recordsTotal = TotalProjects,
                recordsFiltered = recordsFilteredCount,
                data = result
            },
                   JsonRequestBehavior.AllowGet);
        }

        #endregion Project Table view

        #region Project Filter
        [HttpGet]
        public ActionResult GetService2Json()
        {
            return Json(new { obj = GetScopeOfServices() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStatus2Json()
        {
            return Json(new { obj = GetStatus() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUsers2Json()
        {
            return Json(new { obj = GetUsers() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult TestPage()
        {
            return View();
        }

        public ActionResult ComingSoon()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult ProjectWizard(int? id)
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult ProjectService()
        {
            return View();
        }

        [ChildActionOnly]
        [HttpGet]
        public PartialViewResult _ScopeOfService()
        {
            return PartialView(GetScopeOfServices());
        }

        private int CreateProject(ProjectInfoViewModel projectVM)
        {
            var md = new CreateProjectCommand();
            md.Name = projectVM.ProjectName;
            md.ScopeID = projectVM.ScopeId;
            md.ScopeTypeId = projectVM.ScopeTypeId;
            md.PriorityId = projectVM.Priority;
            md.Branch = projectVM.Branch;
            md.DueDate = projectVM.VNDueDate;
            md.ProjectCity = projectVM.ProjectCity;
            md.ProjectAddress = projectVM.ProjectAddress;
            md.ProjectState = projectVM.ProjectState;
            md.ProjectZip = projectVM.ProjectZipCode;
            md.Description = projectVM.TaskDescription;
            md.EOR = projectVM.EOR;
            md.SimpsonDueDate = projectVM.DueDate;
            //people
            md.SimpsonContact = projectVM.SimpsonContact;
            md.RequestedBy = projectVM.RequestedBy;
            md.ReviewedBy = projectVM.ReviewedBy; 
            md.Status = "INTAKE";
            _projectService.CreateProject(md);
            return md.ID;
        }

        [HttpPost]
        public ActionResult _WizardProject(ProjectInfoViewModel projectVM)
        {
            int id = CreateProject(projectVM);
            return RedirectToAction("Detail", new { Id = id });
        }

        //[HttpGet]
        //public ActionResult GetBranches()
        //{
        //    var md = new GetBranchesQuery();
        //    var branchesList = _projectService.GetBranches(md).Select(p=> new {
        //        value = p.Branch1,
        //        text = p.Branch1
        //    }).OrderBy(p=>p.value);
        //    if (branchesList != null)
        //    {
        //        return Json(new { data = branchesList }, JsonRequestBehavior.AllowGet);
        //    }
        //    throw new HttpException(404, "Page not found");
        //}

        [HttpPost]
        public ActionResult DetailUpdate(string name, string value, int pk)
        {
            var success = UpdateProject(name, value, pk);
            return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        }

        private bool UpdateProject(string name, string value, int pk)
        {
            try
            {
                var result = new DateTime();
                switch (name)
                {
                    case "ProjectName":
                        _projectService.UpdateProjectName(new UpdateProjectNameCommand() { ID = pk, Name = value, IncludeScopeofService = true });
                        return true;

                    case "ProjectAddress":
                        _projectService.UpdateProjectAddress(new UpdateProjectAddressCommand() { ID = pk, ProjectAddress = value, IncludeScopeofService = true });
                        return true;

                    case "ProjectCity":
                        _projectService.UpdateProjectCity(new UpdateProjectCityCommand() { ID = pk, ProjectCity = value, IncludeScopeofService = true });
                        return true;

                    case "ProjectState":
                        _projectService.UpdateProjectState(new UpdateProjectStateCommand() { ID = pk, ProjectState = value, IncludeScopeofService = true });
                        return true;

                    case "ProjectZip":
                        _projectService.UpdateProjectZip(new UpdateProjectZipCommand() { ID = pk, ProjectZip = value, IncludeScopeofService = true });
                        return true;

                    case "ProjectEngineer":
                        _projectService.UpdateProjectEngineer(new UpdateProjectEngineerCommand() { ID = pk, ProjectEngineer = value, IncludeScopeofService = true });
                        return true;

                    case "EOR":
                        _projectService.UpdateEOR(new UpdateEORCommand() { ID = pk, EOR = value, IncludeScopeofService = true });
                        return true;

                    case "Description":
                        _projectService.UpdateDescription(new UpdateDescriptionCommand() { ID = pk, Description = value, IncludeScopeofService = true });
                        return true;

                    case "projectSimpsonDuedate":

                        DateTime.TryParse(value, out result);
                        _projectService.UpdateDueDate(new UpdateDueDateCommand() { ID = pk, DueDate = result, IncludeScopeofService = true });
                        return true;

                    case "projectVietNameDuedate":

                        DateTime.TryParse(value, out result);
                        _projectService.UpdateDateToVN(new UpdateDatetoVnCommand() { ID = pk, DatetoVN = result, IncludeScopeofService = true });
                        return true;

                    case "SimpsonContact":
                        _projectService.UpdateContact(new UpdateContactCommand() { ID = pk, Contact = value, IncludeScopeofService = true });
                        return true;

                    case "RequestedBy":
                        _projectService.UpdateRequestor(new UpdateRequestorCommand() { ID = pk, Requestor = value, IncludeScopeofService = true });
                        return true;

                    case "ReviewedBy":
                        _projectService.UpdateReviewer(new UpdateReviewerCommand() { ID = pk, Reviewer = value, IncludeScopeofService = true });
                        return true;

                    case "BranchList":
                        _projectService.UpdateBranch(new UpdateBranchCommand() { ID = pk, Branch = value, IncludeScopeofService = true });
                        return true;

                    case "Status":
                        _projectService.UpdateStatus(new UpdateStatusCommand() { ID = pk, Status = value, IncludeScopeofService = true });
                        return true;
                    case "Priority":
                        int? priority = null;
                        int tempPriorityId = 0;
                        if (int.TryParse(value, out tempPriorityId))
                        {
                            priority = tempPriorityId;
                        }
                        _projectService.UpdatePriority(new UpdatePriorityIdCommand() { ID = pk, PriorityId = priority, IncludePriority = true });
                        return true;
                    case "ScopeTypeId":
                        int? scopeTypeId = null;
                        int tempscopeTypeId = 0;
                        if (int.TryParse(value, out tempscopeTypeId))
                        {
                            scopeTypeId = tempscopeTypeId;
                        }
                        _projectService.UpdateScopeType(new UpdateScopeTypeCommand() { ID = pk, ScopeTypeId = scopeTypeId, IncludeScopeofService = true });
                        return true;

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //[HttpGet]
        //public JsonResult GetSubTask(int id)
        //{
        //    var md = new GetServiceSubTasksQuery();
        //    md.scopeId = id;
        //    var subtaskList = _subTaskService.GetSubTaskServices(md).ToList();
        //    return Json(new { result = subtaskList }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetScopeTypeByScopeId(int scopeId)
        //{
        //    return Json(new { success = true, data = GetScopeType(scopeId) }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public PartialViewResult _ScopeOfService(int id)
        //{
        //    if (id!=null)
        //    {
        //        var md = new GetServiceSubTasksQuery();
        //        md.scopeId = id;
        //        var subTaskServices = _subTaskServices.GetSubTaskServices(md).ToList();
        //        return PartialView("_wizardProject",subTaskServices);
        //    }
        //    else
        //    {
        //        id = 1;
        //        var md = new GetServiceSubTasksQuery();
        //        md.scopeId = id;
        //        var subTaskServices = _subTaskServices.GetSubTaskServices(md).ToList();
        //        return PartialView("_wizardProject", subTaskServices);
        //    }
        //}
        [HttpPost]
        public bool UpdateDescription(myData data)
        {
            try
            {
                var md = new UpdateDescriptionCommand();
                md.ID = data.id;

                var description = data.description;
                md.Description = description;
                md.IncludeScopeofService = true;
                _projectService.UpdateDescription(md);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [ChildActionOnly]
        public PartialViewResult _wizardProject()
        {
            return PartialView();
        }

        public ActionResult Detail(int Id)
        {
            var entity = _projectService.GetProjectById(Id, true, true);
            if (entity != null)
            {
                ViewBag.PriorityListForDemo = _referenceDataService.PriorityList;
                return View(entity);
            }
            throw new HttpException(404, "Project not found");
        }

        [HttpGet]
        public ActionResult ServiceList()
        {
            var md = new myScopeTpye();
            var serviceList = md.Data.Select(p => new
            {
                text = p.ScopeTypeDescription,
                value = p.ScopeTypeDescription
            });
            return Json(new { data = serviceList }, JsonRequestBehavior.AllowGet);

        }


        //public List<ScopeType> GetScopeType(int scopeId)
        //{
        //    return _scopeService.GetScopeTypebyScopeId(scopeId).ToList();
        //}

        [HttpPost]
        public PartialViewResult LoadContent(string step_number, int scopeId)
        {
            var projectData = ReferenceData(scopeId);

            switch (step_number)
            {
                case "0":
                    return PartialView("_WizardLoadContentStep1", projectData);
                case "1":
                    return PartialView("_WizardLoadContentStep2", projectData);
                case "2":
                    return PartialView("_WizardLoadContentStep3", projectData);
                case "3":
                    return PartialView("_WizardLoadContentStep4", projectData);
                case "4":
                    return PartialView("_WizardLoadContentStep5", projectData);
                default:
                    return null;

            }
        }

        // Method
        public ProjectReferenceData ReferenceData(int scopeId)
        {
            if (_referenceData == null || _referenceData.ScopeId != scopeId)
            {
                _referenceData = new ProjectReferenceData();
                //
                _referenceData.ScopeId = scopeId;
                //
                var scopeTypeLst = _referenceDataService
                                    .ScopeTypeListByScopeId(scopeId)
                                    .Select(p => new { value = p.ScopeTypeId, text = p.ScopeTypeDescription });
                _referenceData.ScopeTypeList = new SelectList(scopeTypeLst, "value", "text");
                //
                var priorityLst = _referenceDataService.PriorityList
                                .Select(p => new { value = p.PriorityId, text = p.PriorityDescription });
                _referenceData.PriorityList = new SelectList(priorityLst, "value", "text");
                //
                var branchLst = _referenceDataService.BranchList
                                .Select(p => new { value = p.Branch1, text = p.Branch1 });
                _referenceData.BranchList = new SelectList(branchLst, "value", "text");
            }
            return _referenceData;
        }

        [HttpGet]
        public ActionResult ReferenceDataJson(int scopeId)
        {
            var data = ReferenceData(scopeId);
            if (data != null)
            {
                //
                //var scopeTypeLst = _referenceDataService
                //                    .ScopeTypeListByScopeId(scopeId)
                //                    .Select(p => new { value = p.ScopeTypeId, text = p.ScopeTypeDescription });
                //var priorityLst = _referenceDataService.PriorityList
                //                .Select(p => new { value = p.PriorityId, text = p.PriorityDescription });
                //var branchLst = _referenceDataService.BranchList
                //                .Select(p => new { value = p.Branch1, text = p.Branch1 });
                //return Json(new { data = data, ScopeTypeList = scopeTypeLst, PriorityList= priorityLst, BranchList = branchLst }, JsonRequestBehavior.AllowGet);
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
            throw new HttpException(404, "Page not found");
        }


        [ChildActionOnly]
        public ActionResult AddParticipants()
        {

            //ViewBag.EmployeeList = UsersByTeamId;
            return PartialView("_AddParticipants", UsersByTeamId.Where(p=>p.TerminatedDateTime ==null));
        }


        #region Comments
        [HttpPost]
        public ActionResult AddComment(ProjectCommentViewModel comment)
        {
            var identity = (User as CusPrincipal).Identity as CusIdentity;
            var projectComment = new AddProjectCommentCommand
            {
                ProjectId = comment.ProjectId,
                UserId = identity.User.UserId,
                Body = comment.Body
            };
            _projectService.AddComment(projectComment);

            // reload comments 
            var data = GetComments(comment.ProjectId);
            return PartialView("ShowComment", data);
        }

        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        //[ChildActionOnly]
        //public ActionResult ShowComment()
        //{
        //    throw new NotImplementedException();
        //}

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ChildActionOnly]
        public PartialViewResult ShowComment(int ProjectId)
        {
            var data = GetComments(ProjectId);
            return PartialView(data);
        }

        public List<ProjectComment> GetComments(int ProjectId)
        {
            return _projectService.GetComments(ProjectId).ToList();
        }
        #endregion

        #region helpers
        public IList<ScopeOfService> GetScopeOfServices()
        {
            return _scopeService.GetScopeOfServices().ToList();
        }

        public IList<Status> GetStatus()
        {
            return _scopeService.GetStatus().ToList();
        }

        public IList<Employee> GetUsers()
        {
            return _employeeService.GetUsers();
        }

        
        #endregion
    }
}

public class ProjectReferenceData
{
    public int ScopeId { get; set; }
    public SelectList ScopeTypeList { get; set; }
    public SelectList BranchList { get; set; }
    public SelectList PriorityList { get; set; }
}

public class myData
{
    public int id { get; set; }

    [AllowHtml]
    public string description { get; set; }
}

    public class myScopeTpye
    {
        public List<ScopeType> Data {
            get
            {
                return new List<ScopeType> { new ScopeType { ScopeId = 1, ScopeTypeDescription = "ABC" } };
            }
        }

        public myScopeTpye()
        {
        }
    }



public class myEmployeeData
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
}
//@* @using(Html.BeginForm("_WizardProject","Project", FormMethod.Post))
//    {*@
//@*<form id = "projectForm" > *@
//<div id = "wizard" class="">
//    <ul class="wizard_steps">
//        <li>
//            <a href = "#step-1" >
//                < span class="step_no">1</span>
//                <span class="step_descr">
//                    Service List
//                </span>
//            </a>
//        </li>
//        <li>
//            <a href = "#step-2" >
//                < span class="step_no">2</span>
//                <span class="step_descr">
//                    Informations
//                </span>
//            </a>
//        </li>
//        <li>
//            <a href = "#step-3" >
//                < span class="step_no">3</span>
//                <span class="step_descr">
//                    Project Data
//                </span>
//            </a>
//        </li>
//        <li>
//            <a href = "#step-4" >
//                < span class="step_no">4</span>
//                <span class="step_descr">
//                    People Info
//                </span>
//            </a>
//        </li>
//        <li>
//            <a href = "#step-5" >
//                < span class="step_no">5</span>
//                <span class="step_descr">
//                    Deadline Info
//                </span>
//            </a>
//        </li>
//    </ul>
//    @* Step 1 *@
//    <div class="row setup-content" id="step-1">
//        <!-- step content -->
//    </div>
//    @* End Step 1 *@

//    @* Step 2 *@
//    <div class="row setup-content" id="step-2">
//        <!-- step content -->
//    </div>
//    @* End Step 2 *@

//    @* Step 3 *@
//    <div class="row setup-content " id="step-3">
//        <!-- step content -->
//    </div>
//    @* End Step 3 *@

//    @* Step 4 *@
//    <div class="row setup-content" id="step-4">
//        <!-- step content -->
//    </div>
//    @* End Step 4 *@

//    @* Step 5 *@
//    <div class="row setup-content" id="step-5">
//        <!-- step content -->
//    </div>
//    @* End Step 5 *@
//</div>
//@*</form>*@
//@*}*@
