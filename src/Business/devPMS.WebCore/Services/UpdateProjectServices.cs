using devPMS.Data;
using devPMS.WebCore.Business;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public class UpdateProjectServices : IService
    {
        private readonly ICommandHandler<UpdateProjectNameCommand> _updateProjectNameHandler;
        private readonly ICommandHandler<UpdateProjectAddressCommand> _updateProjectAddressHandler;
        private readonly ICommandHandler<UpdateProjectCityCommand> _updateProjectCityHandler;
        private readonly ICommandHandler<UpdateProjectStateCommand> _updateProjectStateHandler;
        private readonly ICommandHandler<UpdateProjectZipCommand> _updateProjectZipHandler;
        private readonly ICommandHandler<UpdateDescriptionCommand> _updateDescriptionHandler;
        private readonly ICommandHandler<UpdateProjectEngineerCommand> _updateProjectEngineerHandler;
        private readonly ICommandHandler<UpdateBranchCommand> _updateBranchHandler;
        private readonly ICommandHandler<UpdateStatusCommand> _updateStatusHandler;
        private readonly ICommandHandler<UpdateContactCommand> _updateContactHandler;
        private readonly ICommandHandler<UpdateRequestorCommand> _updateRequestorHandler;
        private readonly ICommandHandler<UpdateReviewerCommand> _updateReviewerHandler;
        private readonly ICommandHandler<UpdateDueDateCommand> _updateDueDateHandler;
        private readonly ICommandHandler<UpdateDatetoVnCommand> _updateDatetoVnHandler;
        private readonly ICommandHandler<UpdateScopeTypeCommand> _updateScopeTypeHandler;
        private readonly ICommandHandler<UpdateEORCommand> _updateEORHandler;
        private readonly ICommandHandler<UpdatePriorityIdCommand> _updatePriorityIdHandler;

        public UpdateProjectServices(
            // Other handlers here
            ICommandHandler<UpdateProjectNameCommand> updateProjectNameHandler,
            ICommandHandler<UpdateScopeTypeCommand> updateScopeTypeHandler,
            ICommandHandler<UpdateDatetoVnCommand> updateDatetoVnHandler,
            ICommandHandler<UpdateDueDateCommand> updateDueDateHandler,
            ICommandHandler<UpdateReviewerCommand> updateReviewerHandler,
            ICommandHandler<UpdateRequestorCommand> updateRequestorHandler,
            ICommandHandler<UpdateContactCommand> updateContactHandler,
            ICommandHandler<UpdateStatusCommand> updateStatusHandler,
            ICommandHandler<UpdateBranchCommand> updateBranchHandler,
            ICommandHandler<UpdateProjectEngineerCommand> updateProjectEngineerHandler,
            ICommandHandler<UpdateProjectZipCommand> updateProjectZipHandler,
            ICommandHandler<UpdateProjectStateCommand> updateProjectStateHandler,
            ICommandHandler<UpdateProjectCityCommand> updateProjectCityHandler,
            ICommandHandler<UpdateProjectAddressCommand> updateProjectAddressHandler,
            ICommandHandler<UpdateEORCommand> updateEORHandler,
            ICommandHandler<UpdateDescriptionCommand> updateDescriptionHandler,
            ICommandHandler<UpdatePriorityIdCommand> updatePriorityIdHandler
            )
        {
            _updateProjectNameHandler = updateProjectNameHandler;
            _updateStatusHandler = updateStatusHandler;
            _updateEORHandler = updateEORHandler;
            _updateProjectAddressHandler = updateProjectAddressHandler;
            _updateProjectCityHandler = updateProjectCityHandler;
            _updateProjectStateHandler = updateProjectStateHandler;
            _updateProjectZipHandler = updateProjectZipHandler;
            _updateProjectEngineerHandler = updateProjectEngineerHandler;
            _updateBranchHandler = updateBranchHandler;
            _updateContactHandler = updateContactHandler;
            _updateRequestorHandler = updateRequestorHandler;
            _updateReviewerHandler = updateReviewerHandler;
            _updateDueDateHandler = updateDueDateHandler;
            _updateDatetoVnHandler = updateDatetoVnHandler;
            _updateScopeTypeHandler = updateScopeTypeHandler;
            _updateDescriptionHandler = updateDescriptionHandler;
            _updatePriorityIdHandler = updatePriorityIdHandler;
        }

        public void UpdateProjectName(UpdateProjectNameCommand updateProjectNameCommand)
        {
            _updateProjectNameHandler.Handle(updateProjectNameCommand);
        }

        public void UpdateScopeType(UpdateScopeTypeCommand updateDescriptionOfServiceCommand)
        {
            _updateScopeTypeHandler.Handle(updateDescriptionOfServiceCommand);
        }

        public void UpdateBranch(UpdateBranchCommand updateBranchCommand)
        {
            _updateBranchHandler.Handle(updateBranchCommand);
        }

        public void UpdateContact(UpdateContactCommand updateContactCommand)
        {
            _updateContactHandler.Handle(updateContactCommand);
        }

        public void UpdateDateToVN(UpdateDatetoVnCommand updateDateToVN)
        {
            _updateDatetoVnHandler.Handle(updateDateToVN);
        }

        public void UpdateDueDate(UpdateDueDateCommand updateDueDate)
        {
            _updateDueDateHandler.Handle(updateDueDate);
        }

        public void UpdateEOR(UpdateEORCommand updateEOR)
        {
            _updateEORHandler.Handle(updateEOR);
        }

        public void UpdateProjectAddress(UpdateProjectAddressCommand updateProjectAddress)
        {
            _updateProjectAddressHandler.Handle(updateProjectAddress);
        }

        public void UpdateProjectCity(UpdateProjectCityCommand updateProjectCity)
        {
            _updateProjectCityHandler.Handle(updateProjectCity);
        }

        public void UpdateProjectState(UpdateProjectStateCommand updateProjectState)
        {
            _updateProjectStateHandler.Handle(updateProjectState);
        }

        public void UpdateProjectZip(UpdateProjectZipCommand updateProjectZip)
        {
            _updateProjectZipHandler.Handle(updateProjectZip);
        }

        public void UpdateProjectEngineer(UpdateProjectEngineerCommand updateProjectEngineer)
        {
            _updateProjectEngineerHandler.Handle(updateProjectEngineer);
        }

        public void UpdateRequestor(UpdateRequestorCommand updateRequestor)
        {
            _updateRequestorHandler.Handle(updateRequestor);
        }

        public void UpdateReviewer(UpdateReviewerCommand updateReviewer)
        {
            _updateReviewerHandler.Handle(updateReviewer);
        }

        public void UpdateStatus(UpdateStatusCommand updateStatus)
        {
            _updateStatusHandler.Handle(updateStatus);
        }

        public void UpdateDescription(UpdateDescriptionCommand updateDescription)
        {
            _updateDescriptionHandler.Handle(updateDescription);
        }

        public void UpdatePriority(UpdatePriorityIdCommand command)
        {
            _updatePriorityIdHandler.Handle(command);
        }
    }
}
