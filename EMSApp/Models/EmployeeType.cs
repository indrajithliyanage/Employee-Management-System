using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMSApp.Models
{
    public class EmployeeType
    {
        [Display(Name = "Employee Type ID")]
        public int EmployeeTypeID { get; set; }
        [Display(Name = "Employee Type Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Employee Type Name!")]
        public string EmployeeTypeName { get; set; }
    }
}