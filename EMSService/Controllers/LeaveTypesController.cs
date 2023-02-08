using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EMSDataAccess;

namespace EMSService.Controllers
{
    public class LeaveTypesController : ApiController
    {
        private EMSDBEntities db = new EMSDBEntities();

        // GET: api/LeaveTypes
        public IQueryable<LeaveType> GetLeaveTypes()
        {
            return db.LeaveTypes;
        }

        // GET: api/LeaveTypes/5
        [ResponseType(typeof(LeaveType))]
        public IHttpActionResult GetLeaveType(int id)
        {
            LeaveType leaveType = db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return Ok(leaveType);
        }

        // PUT: api/LeaveTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLeaveType(int id, LeaveType leaveType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leaveType.LeaveTypeID)
            {
                return BadRequest();
            }

            db.Entry(leaveType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(id))
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

        // POST: api/LeaveTypes
        [ResponseType(typeof(LeaveType))]
        public IHttpActionResult PostLeaveType(LeaveType leaveType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LeaveTypes.Add(leaveType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = leaveType.LeaveTypeID }, leaveType);
        }

        // DELETE: api/LeaveTypes/5
        [ResponseType(typeof(LeaveType))]
        public IHttpActionResult DeleteLeaveType(int id)
        {
            LeaveType leaveType = db.LeaveTypes.Find(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            db.LeaveTypes.Remove(leaveType);
            db.SaveChanges();

            return Ok(leaveType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaveTypeExists(int id)
        {
            return db.LeaveTypes.Count(e => e.LeaveTypeID == id) > 0;
        }
    }
}