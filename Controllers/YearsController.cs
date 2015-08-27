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

namespace CarFinder.Controllers
{
    public class YearsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Years
        /// <summary>
        /// Get a list of all car years
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetYears()
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetYears").ToList();
            return retval;
        }


    }
}