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
using Salam.lk.Model;

namespace Salam.lk.api
{
    public class DataFieldsController : ApiController
    {
        private SLMEntities db = new SLMEntities();

        // GET: api/DataFields
        public IQueryable<DataField> GetDataFields(int type)
        {
            var retVal = new List<DataField>();
            retVal = (from field in db.DataFields
                      where field.EntityTypeID == type
                      select new DataField() { DataFieldID = field.DataFieldID, DataFieldName = field.DataFieldName }).ToList();
            return retVal.AsQueryable();
        }

        // GET: api/DataFields/5
        [ResponseType(typeof(DataField))]
        public IHttpActionResult GetDataField(int id)
        {
            DataField dataField = db.DataFields.Find(id);
            if (dataField == null)
            {
                return NotFound();
            }

            return Ok(dataField);
        }

        // PUT: api/DataFields/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDataField(int id, DataField dataField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dataField.DataFieldID)
            {
                return BadRequest();
            }

            db.Entry(dataField).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataFieldExists(id))
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

        // POST: api/DataFields
        [ResponseType(typeof(DataField))]
        public IHttpActionResult PostDataField(DataField dataField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DataFields.Add(dataField);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DataFieldExists(dataField.DataFieldID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dataField.DataFieldID }, dataField);
        }

        // DELETE: api/DataFields/5
        [ResponseType(typeof(DataField))]
        public IHttpActionResult DeleteDataField(int id)
        {
            DataField dataField = db.DataFields.Find(id);
            if (dataField == null)
            {
                return NotFound();
            }

            db.DataFields.Remove(dataField);
            db.SaveChanges();

            return Ok(dataField);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DataFieldExists(int id)
        {
            return db.DataFields.Count(e => e.DataFieldID == id) > 0;
        }
    }
}