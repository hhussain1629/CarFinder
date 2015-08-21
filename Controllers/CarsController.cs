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
    public class CarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Cars 
        public IEnumerable<Car> GetCars(string year, string make, string model, string trim)
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeModelAndTrim @year, @make, @model, @trim", new SqlParameter("year", year), new SqlParameter("make", make), new SqlParameter("model", model), new SqlParameter("trim", trim)).ToList();
            return retval;
        }




        // GET: api/Cars           Do not execute this!!!!
        public IEnumerable<Car> GetCarsOrderByHorsePower()  
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByHorsePower").ToList();
            return retval;
        }

        // GET: api/CarsMin300HP          Do not execute this!!!!
        public IEnumerable<Car> GetCarsOrderByHorsePowerMin300HP()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByHorsePowerMin300").ToList();
            return retval;
        }

        // GET: api/Cars           Do not execute this!!!!
        public IEnumerable<Car> GetCarsOrderByWeight()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByWeight").ToList();
            return retval;
        }

        public IEnumerable<Car> GetCarsOrderByWeightUnder2000Kg()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByWeightUnder2000Kg").ToList();
            return retval;
        }

        public IEnumerable<Car> GetCarUnder2000KgMin300Hp()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarUnder2000KgMin300Hp").ToList();
            return retval;
        }

        //// GET: api/Cars/5
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

        //// PUT: api/Cars/5
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

        //// POST: api/Cars
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

        //// DELETE: api/Cars/5
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