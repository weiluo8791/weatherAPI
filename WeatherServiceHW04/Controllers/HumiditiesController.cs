using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeatherServiceHW04.Models;

namespace WeatherServiceHW04.Controllers
{
    /// <summary>
    /// HumiditiesController 
    /// </summary>
    public class HumiditiesController : ApiController
    {
        private WeatherServiceContext _db = new WeatherServiceContext();

        // GET: api/Humidities
        /// <summary>
        /// Get all Humidities
        /// </summary>
        /// <returns>IQueryable</returns>
        public IQueryable<Humidity> GetHumidities()
        {
            return _db.Humidities;
        }


        /// <summary>
        /// Get single Humidity by id
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>GET: api/Humidities/5</remarks>
        /// <returns></returns>
        [ResponseType(typeof(Humidity))]
        public async Task<IHttpActionResult> GetHumidity(string id)
        {
            Humidity humidity = await _db.Humidities.FindAsync(id);
            if (humidity == null)
            {
                return NotFound();
            }

            return Ok(humidity);
        }

        /// <summary>
        /// Update Humidity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="humidity"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHumidity(string id, Humidity humidity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != humidity.Id)
            {
                return BadRequest();
            }

            _db.Entry(humidity).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumidityExists(id))
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

        /// <summary>
        /// Create a new humidity
        /// </summary>
        /// <param name="humidity"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Humidity))]
        public async Task<IHttpActionResult> PostHumidity(Humidity humidity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //new id
            string id = Guid.NewGuid().ToString();
            humidity.Id = id;

            //populate year, month, week and day from RecorDateTime
            humidity.Year = humidity.RecorDateTime.Year;
            humidity.Month = humidity.RecorDateTime.Month;
            humidity.Day = humidity.RecorDateTime.DayOfYear;
            var currentCulture = CultureInfo.CurrentCulture;
            humidity.Week = currentCulture.Calendar.GetWeekOfYear(
                            humidity.RecorDateTime,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);

            //add the change to the humidity object
            _db.Humidities.Add(humidity);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HumidityExists(humidity.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = humidity.Id }, humidity);
        }

        /// <summary>
        /// Delete Humidity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Humidity))]
        public async Task<IHttpActionResult> DeleteHumidity(string id)
        {
            Humidity humidity = await _db.Humidities.FindAsync(id);
            if (humidity == null)
            {
                return NotFound();
            }

            _db.Humidities.Remove(humidity);
            await _db.SaveChangesAsync();

            return Ok(humidity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HumidityExists(string id)
        {
            return _db.Humidities.Count(e => e.Id == id) > 0;
        }
    }
}