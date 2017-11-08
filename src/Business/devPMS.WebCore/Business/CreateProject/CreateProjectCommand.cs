using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    // Message for the "Create Project" use case
    public class CreateProjectCommand
    {
        public int ID { get; set; }
        public string ProjectNo { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public int ScopeID { get; set; }
        public string Branch { get; set; }
        public System.DateTime DatetoVN { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string EOR { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CompletedDateTime { get; set; }
        public string SubjectEmail { get; set; }
        public int CountTeam { get; set; }
        public System.DateTime LastModifiedDateTime { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string DescriptionOfService { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> ScopeTypeId { get; set; }
        public Nullable<int> EstimatedWorkInMI { get; set; }
        public Nullable<System.DateTime> SimpsonDueDate { get; set; }
        public string RequestedBy { get; set; }
        public string SimpsonContact { get; set; }
        public string ReviewedBy { get; set; }
        public string ProjectAddress { get; set; }
        public string ProjectCity { get; set; }
        public string ProjectState { get; set; }
        public string ProjectZip { get; set; }
        public string ProjectEngineer { get; set; }
        public string ProjectDrawingsDate { get; set; }
        public string ProjectContractor { get; set; }
        public string RFI { get; set; }
        public Nullable<double> SladThickness { get; set; }
        public string WindSpeed { get; set; }
        public string ExposureCategory { get; set; }
        public string SeismicDesignCategory { get; set; }
        public string ProjectAwarded { get; set; }
        public Nullable<decimal> SSTQuote { get; set; }
        public int PriorityId { get; set; }
    }
}
