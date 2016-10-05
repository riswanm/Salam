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
    public class EntitiesController : ApiController
    {
        private SLMEntities db = new SLMEntities();

        // GET: api/Entities
        public IQueryable<Entity> GetEntities(int type, string searchText, int pageSize, int currentPage)
        {
            var retVal = new List<Entity>();
           
            
            if (string.IsNullOrEmpty(searchText))
            {
                retVal = (from entity in db.Entities.OrderBy(i => i.CreatedDate).Skip((currentPage - 1) * pageSize)
                                where entity.EntityTypeID == type
                                select new Entity() { EntityID = entity.EntityID, Data = entity.Data, EntityName = entity.EntityName  }).ToList();
            }
            else
            {
                retVal = (from entity in db.Entities.OrderBy(i => i.CreatedDate).Skip((currentPage - 1) * pageSize)
                          where entity.EntityTypeID == type && entity.Data.Contains(searchText)
                          select new Entity() { EntityID = entity.EntityID, Data = entity.Data, EntityName = entity.EntityName }).ToList();
            }

            return retVal.AsQueryable();
        }

        // GET: api/Entities/5
        [ResponseType(typeof(Entity))]
        public IHttpActionResult GetEntity(int id)
        {
            Entity entity = db.Entities.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // PUT: api/Entities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntity(int id, Entity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entity.EntityID)
            {
                return BadRequest();
            }

            db.Entry(entity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
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

        // POST: api/Entities
        [ResponseType(typeof(Entity))]
        public IHttpActionResult PostEntity(Entity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entities.Add(entity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = entity.EntityID }, entity);
        }

        // DELETE: api/Entities/5
        [ResponseType(typeof(Entity))]
        public IHttpActionResult DeleteEntity(int id)
        {
            Entity entity = db.Entities.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            db.Entities.Remove(entity);
            db.SaveChanges();

            return Ok(entity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntityExists(int id)
        {
            return db.Entities.Count(e => e.EntityID == id) > 0;
        }
    }
}