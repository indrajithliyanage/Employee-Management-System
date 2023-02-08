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
    public class DepartmentsController : Controller
    {
        // GET: Departments
        public ActionResult Index(int? page, string sortBy)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? System.Convert.ToInt32(page) : 1;
            IPagedList<Department> pageddepList;
            IEnumerable<Department> depList;
            if (TempData["depList"] != null)
            {
                depList = TempData["depList"] as IEnumerable<Department>;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Departments").Result;
                depList = response.Content.ReadAsAsync<IEnumerable<Department>>().Result;
            }
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewBag.DNameSortParm = sortBy == "Department Name" ? "dname_desc" : "Department Name";
            switch (sortBy)
            {
                case "id_desc":
                    depList = depList.OrderByDescending(e => e.DeptID);
                    break;

                case "Department Name":

                    depList = depList.OrderBy(e => e.DeptName);
                    break;

                case "dname_desc":
                    depList = depList.OrderByDescending(e => e.DeptName);
                    break;

                case "Default":
                    depList = depList.OrderBy(e => e.DeptID);
                    break;
            }

            pageddepList = depList.ToPagedList(pageIndex, pageSize);
            return View(pageddepList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Department());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Departments/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Department>().Result);

                // return View();
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Department dep)
        {
            if (ModelState.IsValid == true)

                if (dep.DeptID == 0)
                {
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("Departments", dep).Result;
                }
                else
                {
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("Departments/" + dep.DeptID, dep).Result;
                }
            return RedirectToAction("Index");



        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("Departments/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}