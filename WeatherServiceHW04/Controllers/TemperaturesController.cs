using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeatherServiceHW04.Models;

namespace WeatherServiceHW04.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TemperaturesController : ApiController
    {
        private readonly WeatherServiceContext _db = new WeatherServiceContext();

        // GET: api/Temperatures
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Temperature> GetTemperatures()
        {
            return _db.Temperatures;
        }

        // GET: api/Temperatures/5
        [ResponseType(typeof(Temperature))]
        public async Task<IHttpActionResult> GetTemperature(string id)
        {
            Temperature temperature = await _db.Temperatures.FindAsync(id);
            if (temperature == null)
            {
                return NotFound();
            }

            return Ok(temperature);
        }

        // PUT: api/Temperatures/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTemperature(string id, Temperature temperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temperature.Id)
            {
                return BadRequest();
            }

            _db.Entry(temperature).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureExists(id))
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

        // POST: api/Temperatures
        [ResponseType(typeof(Temperature))]
        public async Task<IHttpActionResult> PostTemperature(Temperature temperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string id = Guid.NewGuid().ToString();
            temperature.Id = id;
            temperature.Year = temperature.RecorDateTime.Year;
            temperature.Month = temperature.RecorDateTime.Month;
            temperature.Day = temperature.RecorDateTime.DayOfYear;

            var currentCulture = CultureInfo.CurrentCulture;
            temperature.Week = currentCulture.Calendar.GetWeekOfYear(
                            temperature.RecorDateTime,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);

            _db.Temperatures.Add(temperature);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TemperatureExists(temperature.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = temperature.Id }, temperature);
        }

        // DELETE: api/Temperatures/5
        [ResponseType(typeof(Temperature))]
        public async Task<IHttpActionResult> DeleteTemperature(string id)
        {
            Temperature temperature = await _db.Temperatures.FindAsync(id);
            if (temperature == null)
            {
                return NotFound();
            }

            _db.Temperatures.Remove(temperature);
            await _db.SaveChangesAsync();

            return Ok(temperature);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemperatureExists(string id)
        {
            return _db.Temperatures.Count(e => e.Id == id) > 0;
        }
    }
}