using EMSApp.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EMSApp.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index(int? page, string sortBy)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Employee> pagedempList;
            IEnumerable<Employee> empList;
            if (TempData["empList"] != null)
            {
                empList = TempData["empList"] as IEnumerable<Employee>;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Employees").Result;
                empList = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            }
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewBag.FNameSortParm = sortBy == "First Name" ? "fname_desc" : "First Name";

            switch (sortBy)
            {
                case "id_desc":
                    empList = empList.OrderByDescending(e => e.EmpID);
                    break;

                case "First Name":

                    empList = empList.OrderBy(e => e.FirstName);

                    break;

                case "fname_desc":

                    empList = empList.OrderByDescending(e => e.FirstName);
                    break;

                case "Default":
                    empList = empList.OrderBy(e => e.EmpID);
                    break;
            }
            pagedempList = empList.ToPagedList(pageIndex, pageSize);
            return View(pagedempList);
        }

        public async System.Threading.Tasks.Task<ActionResult> AddOrEdit(int id = 0)
        {
            var responseto = await GlobalVariables.WebAPIClient.GetAsync("Departments").Result.Content.ReadAsAsync<IList<Department>>();

            responseto.Select(d => new SelectListItem { Text = d.DeptName, Value = d.DeptID.ToString() });

            ViewBag.list = responseto.Select(d => new SelectListItem { Text = d.DeptName, Value = d.DeptID.ToString() });

            var response3 = await GlobalVariables.WebAPIClient.GetAsync("EmployeeTypes").Result.Content.ReadAsAsync<IList<Employee>>();

            response3.Select(r => new SelectListItem { Text = r.EmployeeTypeName, Value = r.EmployeeTypeID.ToString() });

            ViewBag.list2 = response3.Select(r => new SelectListItem { Text = r.EmployeeTypeName, Value = r.EmployeeTypeID.ToString() });

            if (id == 0)
                return View(new Employee());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Employees/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Employee>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            if (ModelState.IsValid == true)

                if (emp.EmpID == 0)
                {
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Employees", emp).Result;
                }
                else
                {
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Employees/" + emp.EmpID, emp).Result;
                }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Employees/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}