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
    //[Authorize]
    public class MakesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Makes
        /// <summary>
        /// Get a list of all makes
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<string> GetMakes()
        //{
        //    var retval = db.Database.SqlQuery<string>("EXEC GetMakes").ToList();
        //    return retval;
        //}

        // GET: api/Makes/?year=...
        /// <summary>
        /// Get a list of makes by year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public IEnumerable<string>GetMakesByYear(string year)
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetMakesByYear @year", new SqlParameter("year", year)).ToList();
            return retval;
        }

        //// PUT: api/Makes/5
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

        //// POST: api/Makes
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

        //// DELETE: api/Makes/5
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool CarExists(int id)
        //{
        //    return db.Cars.Count(e => e.Id == id) > 0;
        //}
    }
}