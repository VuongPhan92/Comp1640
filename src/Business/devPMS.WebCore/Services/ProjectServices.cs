using devPMS.Data;
using devPMS.WebCore.Business;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using SSTVN.Core.Repository.PageResult;
using System.Collections;
using System.Collections.Generic;

namespace devPMS.WebCore.Services
{
    // This proxy forwards calls to the corresponding command handlers
    public class ProjectServices : IService
    {
        private readonly ICommandHandler<CreateProjectCommand> _createProjectHandler;
        private readonly ICommandHandler<AddProjectCommentCommand> _addCommentHandler;
        private readonly PendingProjectQueryHandler _pendingHandler;
        private readonly SearchProjectQueryHandler _searchProjectHandler;
        private readonly GetProjectByIdQueryHandler _getProjectByIdHandler;
        private readonly GetBranchesQueryHandler _getBranchesHandler;
        private readonly UpdateProjectService _updateProjectHandlers;
        private readonly GetProjectCommentsQueryHandler _getProjectCommentsHandler;

        public ProjectServices(
            // Other handlers here
            ICommandHandler<CreateProjectCommand> createProjectHandler,
            ICommandHandler<AddProjectCommentCommand> addCommentHandler,
            PendingProjectQueryHandler pendingHandler,
            SearchProjectQueryHandler searchProjectHandler,
            GetProjectByIdQueryHandler getProjectByIdHandler,
            GetBranchesQueryHandler getBranchesHandler,
            UpdateProjectService updateProjectHandlers,
            GetProjectCommentsQueryHandler getProjectCommentsHandler
            )
        {
            _createProjectHandler = createProjectHandler;
            _addCommentHandler = addCommentHandler;
            _pendingHandler = pendingHandler;
            _searchProjectHandler = searchProjectHandler;
            _getProjectByIdHandler = getProjectByIdHandler;
            _getBranchesHandler = getBranchesHandler;
            _updateProjectHandlers = updateProjectHandlers;
            _getProjectCommentsHandler = getProjectCommentsHandler;
        }

        public void CreateProject(CreateProjectCommand createProjectCommand)
        {
            //var handler =
            //            new DeadlockRetryCommandHandlerDecorator<CreateProjectCommand>(
            //                new TransactionCommandHandlerDecorator<CreateProjectCommand>(
            //                    _createProjectHandler
            //                )
            //            );
            //handler.Handle(createProjectCommand);
            _createProjectHandler.Handle(createProjectCommand);

            //_abc.Handle(createProjectCommand);
        }

        public Project_Log[] GetPendingProjects(PendingProjectQuery query)
        {
            return _pendingHandler.Handle(query);
        }

        public PagedListResult<Project_Log> SearchProject(SearchQuery<Project_Log> searchQuery)
        {
            return _searchProjectHandler.Handle(new SearchProjectQuery() { SearchQuery = searchQuery });
        }

        public Project_Log GetProjectById(int id, bool IsIncludeScopeOfService, bool IsIncludeScopeType)
        {
            return _getProjectByIdHandler.Handle(new GetProjectByIdQuery() { Id = id, IncludeScopeofService = IsIncludeScopeOfService, IncludeScopeType = IsIncludeScopeType });
        }

        public IEnumerable<Branch> GetBranches(GetBranchesQuery query)
        {
           return _getBranchesHandler.Handle(query);
        }

        #region Update Project
        public void UpdateProjectName(UpdateProjectNameCommand updateProjectNameCommand)
        {
            _updateProjectHandlers.UpdateProjectName(updateProjectNameCommand);
        }

        public void UpdateScopeType(UpdateScopeTypeCommand updateDescriptionOfServiceCommand)
        {
            _updateProjectHandlers.UpdateScopeType(updateDescriptionOfServiceCommand);
        }

        public void UpdateBranch(UpdateBranchCommand updateBranchCommand)
        {
            _updateProjectHandlers.UpdateBranch(updateBranchCommand);
        }

        public void UpdateContact(UpdateContactCommand updateContactCommand)
        {
            _updateProjectHandlers.UpdateContact(updateContactCommand);
        }

        public void UpdateDateToVN(UpdateDatetoVnCommand updateDateToVN)
        {
            _updateProjectHandlers.UpdateDateToVN(updateDateToVN);
        }

        public void UpdateDueDate(UpdateDueDateCommand updateDueDate)
        {
            _updateProjectHandlers.UpdateDueDate(updateDueDate);
        }

        public void UpdateEOR(UpdateEORCommand updateEOR)
        {
            _updateProjectHandlers.UpdateEOR(updateEOR);
        }

        public void UpdateProjectAddress(UpdateProjectAddressCommand updateProjectAddress)
        {
            _updateProjectHandlers.UpdateProjectAddress(updateProjectAddress);
        }

        public void UpdateProjectCity(UpdateProjectCityCommand updateProjectCity)
        {
            _updateProjectHandlers.UpdateProjectCity(updateProjectCity);
        }

        public void UpdateProjectState(UpdateProjectStateCommand updateProjectState)
        {
            _updateProjectHandlers.UpdateProjectState(updateProjectState);
        }

        public void UpdateProjectZip(UpdateProjectZipCommand updateProjectZip)
        {
            _updateProjectHandlers.UpdateProjectZip(updateProjectZip);
        }

        public void UpdateProjectEngineer(UpdateProjectEngineerCommand updateProjectEngineer)
        {
            _updateProjectHandlers.UpdateProjectEngineer(updateProjectEngineer);
        }

        public void UpdateRequestor(UpdateRequestorCommand updateRequestor)
        {
            _updateProjectHandlers.UpdateRequestor(updateRequestor);
        }

        public void UpdateReviewer(UpdateReviewerCommand updateReviewer)
        {
            _updateProjectHandlers.UpdateReviewer(updateReviewer);
        }

        public void UpdateStatus(UpdateStatusCommand updateStatus)
        {
            _updateProjectHandlers.UpdateStatus(updateStatus);
        }

        public void UpdateDescription(UpdateDescriptionCommand updateDescription)
        {
            _updateProjectHandlers.UpdateDescription(updateDescription);
        }

        public void UpdatePriority(UpdatePriorityIdCommand command)
        {
            _updateProjectHandlers.UpdatePriority(command);
        }
        #endregion

        #region Comment
        public void AddComment(AddProjectCommentCommand cm)
        {
            _addCommentHandler.Handle(cm);
        }

        public IEnumerable<ProjectComment> GetComments(int projectId)
        {
            return _getProjectCommentsHandler.Handle(new GetProjectCommentsQuery { ProjectId = projectId, IncludeEmployee = true });
        }
        #endregion

        public void unkown()
        {
            //var handler =
            //            new DeadlockRetryCommandHandlerDecorator<CreateProjectCommand>(
            //                new TransactionCommandHandlerDecorator<CreateProjectCommand>(
            //                    new CreateProjectCommandHandler(

            //                    )
            //                )
            //            );

            //var controller = new CustomerController(handler);
        }
    }
}