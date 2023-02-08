using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMSApp.Models
{
    public class Employee
    {
        [Display(Name = "Employee ID")]
        public int EmpID { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a First Name!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a Last Name!")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a NIC!")]
        public string NIC { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter a Mobile Number!")]
        public string MobileNum { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must enter an Address!")]
        public string Address { get; set; }

        public int EmployeeTypeID { get; set; }
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string EmployeeTypeName { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee EmployeeType { get; set; }
    }
}