﻿using EMSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EMSApp.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult Index(String sQuery)
        {
            TempData["sQuery"] = null;
            //Getting Request URLs to change the Search Functions Accrodingly
            Uri uriaddress = new Uri(Request.UrlReferrer.ToString());
            string page = uriaddress.Segments[1].Replace("/", "");
            if (!string.IsNullOrEmpty(sQuery))
            {
                //Keeping search Query for showing in the input box
                TempData["sQuery"] = sQuery;

                if (page == "Employees")
                {
                    IEnumerable<Employee> empList;
                    //Getting the filtered results from the API
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Search/GetEmployee/" + sQuery).Result;
                    empList = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                    TempData["empList"] = empList;
                    return RedirectToAction("Index", "Employees");
                }
                else if (page == "Departments")
                {
                    IEnumerable<Department> depList;
                    HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("Search/GetDepartment/" + sQuery).Result;
                    depList = response.Content.ReadAsAsync<IEnumerable<Department>>().Result;
                    TempData["depList"] = depList;
                    return RedirectToAction("Index", "Departments");
                }
            }
            return RedirectToAction("Index", page);
        }
    }
}