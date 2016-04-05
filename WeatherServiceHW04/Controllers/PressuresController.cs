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
    public class PressuresController : ApiController
    {
        private readonly WeatherServiceContext _db = new WeatherServiceContext();

        /// <summary>
        /// Get all pressure
        /// </summary>
        /// <returns>IQueryable</returns>
        public IQueryable<Pressure> GetPressures()
        {
            return _db.Pressures;
        }

        /// <summary>
        /// Get one pressure by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Pressure))]
        public async Task<IHttpActionResult> GetPressure(string id)
        {
            Pressure pressure = await _db.Pressures.FindAsync(id);
            if (pressure == null)
            {
                return NotFound();
            }

            return Ok(pressure);
        }

        /// <summary>
        /// Update pressure by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pressure"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPressure(string id, Pressure pressure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pressure.Id)
            {
                return BadRequest();
            }

            _db.Entry(pressure).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PressureExists(id))
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
        /// Create pressure 
        /// </summary>
        /// <param name="pressure"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Pressure))]
        public async Task<IHttpActionResult> PostPressure(Pressure pressure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //new id
            string id = Guid.NewGuid().ToString();
            pressure.Id = id;
            //populate year, month, week and day from RecorDateTime
            pressure.Year = pressure.RecorDateTime.Year;
            pressure.Month = pressure.RecorDateTime.Month;
            pressure.Day = pressure.RecorDateTime.DayOfYear;
            var currentCulture = CultureInfo.CurrentCulture;
            pressure.Week = currentCulture.Calendar.GetWeekOfYear(
                            pressure.RecorDateTime,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);

            //add the change to the pressure object
            _db.Pressures.Add(pressure);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PressureExists(pressure.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pressure.Id }, pressure);
        }

        /// <summary>
        /// Delete pressure by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Pressure))]
        public async Task<IHttpActionResult> DeletePressure(string id)
        {
            Pressure pressure = await _db.Pressures.FindAsync(id);
            if (pressure == null)
            {
                return NotFound();
            }

            _db.Pressures.Remove(pressure);
            await _db.SaveChangesAsync();

            return Ok(pressure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PressureExists(string id)
        {
            return _db.Pressures.Count(e => e.Id == id) > 0;
        }
    }
}