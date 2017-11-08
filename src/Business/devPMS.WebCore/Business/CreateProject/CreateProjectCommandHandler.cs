using devPMS.Data;
using Infrastructure.Decorator;
using Infrastructure.Repository;
using SSTVN.Core.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace devPMS.WebCore.Business
{
    // Implementation for the "create project" use case.
    public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly IDbContextFactory _factory;

        public CreateProjectCommandHandler(IDbContextFactory factory)
        {
            _factory = factory;
        }

        public void Handle(CreateProjectCommand command)
        {
            using (var uow = _factory.Create(DBContextName.PMSEntities))
            {
                var project = new Project_Log();
                //require
                project.ProjectNo = string.Format("ES-{0}{1}", System.DateTime.Now.Year, System.DateTime.Now.ToString("MMddhhmmss"));
                project.Branch = command.Branch;
                project.ScopeID = command.ScopeID;
                project.CountTeam = 0;
                project.DatetoVN = System.DateTime.Now;
                project.LastModifiedDateTime = System.DateTime.Now;
                //optional
                project.Description = command.Description;
                project.EOR = command.EOR;
                project.DueDate = command.DueDate;
                project.SimpsonDueDate = command.SimpsonDueDate;
                project.SSTQuote = command.SSTQuote;
                project.SubjectEmail = command.SubjectEmail;
                project.ProjectAddress = command.ProjectAddress;
                project.ProjectAwarded = command.ProjectAwarded;
                project.ProjectCity = command.ProjectCity;
                project.ProjectContractor = command.ProjectContractor;
                project.ProjectState = command.ProjectState;
                project.ProjectZip = command.ProjectZip;
                project.RFI = command.RFI;
                project.Parent = command.Parent;
                project.Name = command.Name;
                project.SimpsonContact = command.SimpsonContact;
                project.RequestedBy = command.RequestedBy;
                project.ReviewedBy = command.ReviewedBy;
                project.Status = command.Status;
                project.StatusId = command.StatusId;
                //irrelevant information ?
                project.WindSpeed = command.WindSpeed;
                project.SladThickness = command.SladThickness;
                project.SeismicDesignCategory = command.SeismicDesignCategory;
                project.ProjectDrawingsDate = command.ProjectDrawingsDate;
                project.EstimatedWorkInMI = command.EstimatedWorkInMI;
                project.ExposureCategory = command.ExposureCategory;
                project.ScopeTypeId = command.ScopeTypeId;
                project.PriorityId = command.PriorityId;
                //

                uow.Repository<Project_Log>().Add(project);
                try
                {
                    uow.SubmitChanges();
                   //change later
                    command.ID = project.ID;
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
        }



    }
}