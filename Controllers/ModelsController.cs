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
using CarFinder.Models;
using System.Data.SqlClient;

namespace CarFinder.Controllers
{
   
    public class ModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Models
        /// <summary>
        /// Get a list of all models
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetModels()
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetModel").ToList();
            return retval;
        }

        // GET: api/Models/
        /// <summary>
        /// Get a list of models by the selected criteria
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        public IEnumerable<string> GetModelsByYearAndMake(string year, string make)
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetModelsByYearAndMake @year, @make", new SqlParameter("year", year), new SqlParameter("make", make)).ToList();
            return retval;
        }

        //// PUT: api/Models/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutCar(int id, Car car)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != car.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(car).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CarExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Models
        //[ResponseType(typeof(Car))]
        //public IHttpActionResult PostCar(Car car)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Cars.Add(car);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = car.Id }, car);
        //}

        //// DELETE: api/Models/5
        //[ResponseType(typeof(Car))]
        //public IHttpActionResult DeleteCar(int id)
        //{
        //    Car car = db.Cars.Find(id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Cars.Remove(car);
        //    db.SaveChanges();

        //    return Ok(car);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.Id == id) > 0;
        }
    }
}