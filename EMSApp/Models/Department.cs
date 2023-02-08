using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMSApp.Models
{
    public class Department
    {
        [Display(Name = "Department ID")]
        public int DeptID { get; set; }
        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "You must enter a Department Name!")]
        public string DeptName { get; set; }
        [Display(Name = "Department Description")]
        public string DeptDescription { get; set; }
    }
}