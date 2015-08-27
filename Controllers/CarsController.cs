using System;
using System.Collections.Generic;
using System.Data;
using Bing;
using System.Data.Services.Client;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CarFinder.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;


namespace CarFinder.Controllers
{
    //[Authorize]
    public class CarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Cars
        /// <summary>
        /// Get list of cars according to provided criteria
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        
        public async Task<IHttpActionResult> GetCar(string year, string make, string model, string trim)
        {
            if (trim == null)
            {
                trim = "";
            }
            var carList = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeModelAndTrim @year, @make, @model, @trim", new SqlParameter("year", year), new SqlParameter("make", make), new SqlParameter("model", model), new SqlParameter("trim", trim)).ToList();
            var car = carList[0];
            var carId = car.Id;
            CarViewModel carView = new CarViewModel();
            carView.Car = car;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.nhtsa.gov/");
                try
                {
                    var response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" +
                        car.model_year.ToString() + "/make/" +
                        car.make + "/model/" +
                        car.model_name + "?format=json");
                    //carView.Recalls = await response.Content.ReadAsStringAsync();
                    carView.Recalls = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }


            var bingAccountKey = "h2OeY0azx627HoTtjXgaDKCnUpilrEGIToSKkEb2EWI";
            var image = "";
            string query = car.model_year + " " + car.make + " " + car.model_name + " " + car.model_trim;
            string rootURL = "https://api.datamarket.azure.com/Bing/search/";
            var bingContainer = new Bing.BingSearchContainer(new Uri(rootURL));
            string market = "en-us";
            bingContainer.Credentials = new NetworkCredential(bingAccountKey, bingAccountKey);
                //ConfigurationManager.AppSettings["bing"]);

            var imageQuery = bingContainer.Image(query, null, market, null, null, null, null);
            imageQuery = imageQuery.AddQueryOption("$top", 3);
            var imageResults = imageQuery.Execute().ToList();
            //var marketData = images.Composite(
            //    "image",
            //    car.model_year + " " + car.make + " " + car.model_name + " " + car.model_trim,
            //    null,
            //    null,
            //    null,
            //    null,
            //    null,
            //    null,
            //    null,
            //    null,
            //    null,
            ////    null,
            //    null,
            //    null,
            //    null
            //    ).Execute();


            //carView.Image1= imageResults.ElementAt(0).MediaUrl;
            //carView.Image2 = imageResults.ElementAt(1).MediaUrl;
            //carView.Image3 = imageResults.ElementAt(2).MediaUrl;

            //foreach (var result in imageResults)
            //{
            //    carView.Image1 = result.MediaUrl;
            //}

            carView.Images = imageResults.Select(m => m.MediaUrl).ToList();
            //carView.Image1 = imageResults.ElementAt(0).MediaUrl ?? "";
            //carView.Image2 = imageResults.ElementAt(1).MediaUrl ?? "";

            //carView.Image3 = imageResults.ElementAtOrDefault(0).MediaUrl;

            return Ok(carView);
        }




        //[HttpGet, HttpPost]
        //public async Task<IHttpActionResult> GetCarView(int Id)
        //{
        //    var car = db.Cars.Find(Id);
        //    var recalls ;
        //    var image = "";

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://www.nhtsa.gov/");
        //        try
        //        {
        //            var response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + 
        //                car.model_year.ToString() + "/make/" + 
        //                car.make + "/model/" + 
        //                car.model_name + "?format=json");
        //            recalls = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());  
        //        }
        //        catch (Exception e)
        //        {
        //            return InternalServerError(e);
        //        }
        //    }


        //    return Ok(new { car, image, recalls });
        //}


        // GET: api/Cars           Do not execute this!!!!
        /// <summary>
        /// Get a list of all cars ordered by horsepower
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetCarsOrderByHorsePower()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByHorsePower").ToList();
            return retval;
        }

        // GET: api/CarsMin300HP          Do not execute this!!!!
        /// <summary>
        /// Get a list of all cars, with at least 300 HP, ordered by HP 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetCarsOrderByHorsePowerMin300HP()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByHorsePowerMin300").ToList();
            return retval;
        }


        //[ApiExplorerSettings(IgnoreApi = false)]
        // GET: api/Cars           Do not execute this!!!!
        /// <summary>
        /// Get a list of all cars ordered by weight
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetCarsOrderByWeight()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByWeight").ToList();
            return retval;
        }


        /// <summary>
        /// Get a list of all cars under 2000 Kg, ordered by weight
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Car> GetCarsOrderByWeightUnder2000Kg()
        {
            var retval = db.Database.SqlQuery<Car>("EXEC OrderCarsByWeightUnder2000Kg").ToList();
            return retval;
        }

        /// <summary>
        /// Get a list of all cars under 2000 Kg with at least 300 HP
        /// </summary>
        /// <returns></returns>
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

    public class CarViewModel
    {
        public Car Car { get; set; }
        public dynamic Recalls { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public List<string> Images { get; set;}
    }

}

