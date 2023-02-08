using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMSApp.Models
{
    public class LeaveApply
    {
        [Display(Name = "Leave ID")]
        public int LeaveID { get; set; }

        [Required(ErrorMessage = "You must enter an Employee ID!")]
        [Display(Name = "Employee ID")]
        public int EmpID { get; set; }

        [Required(ErrorMessage = "You must enter a Leave Date!")]
        [Display(Name = "Leave From")]
        [DataType(DataType.Date)]
        public System.DateTime LeaveFrom { get; set; }

        [Required(ErrorMessage = "You must enter a Leave Until Date!")]
        [Display(Name = "Leave To")]
        [DataType(DataType.Date)]
        public System.DateTime LeaveTo { get; set; }

        public string Description { get; set; }

        [Display(Name = "Leave Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter a Leave Type!")]
        public int LeaveTypeID { get; set; }

        public string LeaveType1 { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}