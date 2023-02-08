using EMSApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace EMSApp.Controllers
{
    public class LeaveAppliesController : Controller
    {
        // GET: LeaveApplies
        public ActionResult Index()
        {
            IEnumerable<LeaveApply> empLeaveList;
            HttpResponseMessage response = GlobalVariables.WebAPIClient.GetAsync("LeaveApplies").Result;
            empLeaveList = response.Content.ReadAsAsync<IEnumerable<LeaveApply>>().Result;

            return View(empLeaveList);
        }

        public async System.Threading.Tasks.Task<ActionResult> AddOrEdit(int id = 0)
        {
            var responseto = await GlobalVariables.WebAPIClient.GetAsync("LeaveTypes").Result.Content.ReadAsAsync<IList<LeaveType>>();

            responseto.Select(d => new SelectListItem { Text = d.LeaveType1, Value = d.LeaveTypeID.ToString() });

            ViewBag.list = responseto.Select(d => new SelectListItem { Text = d.LeaveType1, Value = d.LeaveTypeID.ToString() });

            if (id == 0)
            {
                return View(new LeaveApply());
            }
            else
            {
                var response = await GlobalVariables.WebAPIClient.GetAsync("LeaveApplies/" + id.ToString()).Result.Content.ReadAsAsync<LeaveApply>();
                return View(response);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(LeaveApply leave)
        {
            if (leave.LeaveID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PostAsJsonAsync("LeaveApplies", leave).Result;
                HttpResponseMessage mailresponse = GlobalVariables.WebAPIClient.PostAsJsonAsync("Email/PostLeaveApplyMail", leave).Result;
                TempData["SuccessMessage"] = "Leave Requested Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebAPIClient.PutAsJsonAsync("LeaveApplies/" + leave.LeaveID, leave).Result;
                TempData["SuccessMessage"] = "Update Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebAPIClient.DeleteAsync("LeaveApplies/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successFully";
            return RedirectToAction("Index");
        }
    }
}