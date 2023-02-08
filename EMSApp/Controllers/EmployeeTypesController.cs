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
    public class EmployeeTypesController : Controller
    {
        // GET: EmployeeTypes
        public ActionResult Index(int? page, string sortBy)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<EmployeeType> pagedtypeList;
            IEnumerable<EmployeeType> empTypeList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("EmployeeTypes").Result;
            empTypeList = response.Content.ReadAsAsync<IEnumerable<EmployeeType>>().Result;
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewBag.NameSortParm = sortBy == "Employee Type Name" ? "etname_desc" : "Employee Type Name";

            switch (sortBy)
            {
                case "id_desc":
                    empTypeList = empTypeList.OrderByDescending(e => e.EmployeeTypeID);
                    break;

                case "Employee Type Name":

                    empTypeList = empTypeList.OrderBy(e => e.EmployeeTypeName);

                    break;

                case "etname_desc":

                    empTypeList = empTypeList.OrderByDescending(e => e.EmployeeTypeName);
                    break;

                case "Default":
                    empTypeList = empTypeList.OrderBy(e => e.EmployeeTypeID);
                    break;
            }
            pagedtypeList = empTypeList.ToPagedList(pageIndex, pageSize);
            return View(pagedtypeList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new EmployeeType());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("EmployeeTypes/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EmployeeType>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeType empType)
        {
            if (ModelState.IsValid == true)

                if (empType.EmployeeTypeID == 0)
                {
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("EmployeeTypes", empType).Result;
                }
                else
                {
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("EmployeeTypes/" + empType.EmployeeTypeID, empType).Result;
                }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("EmployeeTypes/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}