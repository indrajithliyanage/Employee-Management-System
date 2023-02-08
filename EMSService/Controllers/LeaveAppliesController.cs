using EMSDataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace EMSService.Controllers
{
    public class LeaveAppliesController : ApiController
    {
        private EMSDBEntities db = new EMSDBEntities();

        // GET: api/LeaveApplies
        public IQueryable<LeaveApply> GetLeaveApplies()
        {
            return db.LeaveApplies;
        }

        // GET: api/LeaveApplies/5
        [ResponseType(typeof(LeaveApply))]
        public IHttpActionResult GetLeaveApply(int id)
        {
            LeaveApply leaveApply = db.LeaveApplies.Find(id);
            if (leaveApply == null)
            {
                return NotFound();
            }

            return Ok(leaveApply);
        }

        // PUT: api/LeaveApplies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLeaveApply(int id, LeaveApply leaveApply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leaveApply.LeaveID)
            {
                return BadRequest();
            }

            db.Entry(leaveApply).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveApplyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/LeaveApplies
        [ResponseType(typeof(LeaveApply))]
        public IHttpActionResult PostLeaveApply(LeaveApply leaveApply)
        {
            bool leaveAlreadyExsists = db.LeaveTypes.Any(x => x.LeaveType1 == leaveApply.LeaveTypeID.ToString());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LeaveApplies.Add(leaveApply);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = leaveApply.LeaveID }, leaveApply);
        }

        // DELETE: api/LeaveApplies/5
        [ResponseType(typeof(LeaveApply))]
        public IHttpActionResult DeleteLeaveApply(int id)
        {
            LeaveApply leaveApply = db.LeaveApplies.Find(id);
            if (leaveApply == null)
            {
                return NotFound();
            }

            db.LeaveApplies.Remove(leaveApply);
            db.SaveChanges();

            return Ok(leaveApply);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaveApplyExists(int id)
        {
            return db.LeaveApplies.Count(e => e.LeaveID == id) > 0;
        }
    }
}