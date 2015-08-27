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
    public class EngineNumCylController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EngineNumCyl
        /// <summary>
        /// Get a list of all cars with a selected number of engine cylinders
        /// </summary>
        /// <param name="numcyl"></param>
        /// <returns></returns>
        public IEnumerable<Car> GetCarsByNumCyl(string numcyl)
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByEngineSize @numcyl", new SqlParameter("numcyl", numcyl)).ToList();
            return retval;
        }

        //// GET: api/EngineNumCyl/5
        //[ResponseType(typeof(Car))]
        //public IHttpActionResult GetCar(int id)
        //{
        //    Car car = db.Cars.Find(id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(car);
        //}

        //// PUT: api/EngineNumCyl/5
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

        //// POST: api/EngineNumCyl
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

        //// DELETE: api/EngineNumCyl/5
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