using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devPMS.Web.Models
{
    public class ProjectInfoViewModel
    {
        public int ID { get; set; }
        public int ScopeId { get; set; }
        public int ScopeTypeId { get; set; }
        [Required(ErrorMessage = "Please input project name!")]
        public string ProjectName { get; set; }
        public string ProjectNo { get; set; }
        public string Branch { get; set; }
        public int Priority { get; set; }
        public string Quote { get; set; }
        public string CP { get; set; }
        public string ProjectAddress { get; set; }
        public string ProjectCity { get; set; }
        public string ProjectState { get; set; }
        public string ProjectZipCode { get; set; }
        public string ProjectDrawingDate { get; set; }
        public string ProjectContractor { get; set; }
        [AllowHtml]
        public string TaskDescription { get; set; }
        public string EOR { get; set; }
        public string RFI { get; set; }
        public Nullable<double> SladThickness { get; set; }
        public string WindSpeed { get; set; }
        public string ExposureCategory { get; set; }
        public string SeismicDesignCategory { get; set; }
        public string ProjectAwarded { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? VNDueDate { get; set; }
        public string SimpsonContact { get; set; }
        public string RequestedBy { get; set; }
        public string ReviewedBy { get; set; }
    }
}