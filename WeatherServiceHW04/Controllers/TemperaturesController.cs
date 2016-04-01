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
    /// 
    /// </summary>
    public class TemperaturesController : ApiController
    {
        private readonly WeatherServiceContext _db = new WeatherServiceContext();

        // GET: api/Temperatures
        /// <summary>
        /// Get all temperature
        /// </summary>
        /// <returns>IQueryable</returns>
        public IQueryable<Temperature> GetAllTemperatures()
        {
            return _db.Temperatures;
        }


        /// <summary>
        /// Get temperature by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Temperature))]
        public async Task<IHttpActionResult> GetOneTemperature(string id)
        {
            Temperature temperature = await _db.Temperatures.FindAsync(id);
            if (temperature == null)
            {
                return NotFound();
            }

            return Ok(temperature);
        }


        /// <summary>
        /// Get low temperature by time 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="period"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Temperature))]
        [Route("api/temperature/{type}/{period}/low")]
        public async Task<IHttpActionResult> GetLowTemperature(string type, int period)
        {
            IQueryable<Temperature> temperature;

            switch (type)
            {
                case "year":
                    temperature = from a in _db.Temperatures
                                  where a.Year == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "month":
                    temperature = from a in _db.Temperatures
                                  where a.Month == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "week":
                    temperature = from a in _db.Temperatures
                                  where a.Week == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "day":
                    temperature = from a in _db.Temperatures
                                  where a.Day == period
                                  where a.Degree !=-99
                                  select a;
                    break;
                default:
                    return NotFound();
            }
            var lowestDegree = await temperature.Select(a => a.Degree).MinAsync();

            if (lowestDegree == null)
            {
                return NotFound();
            }

            return Ok(new {type,period, lowTemperature = lowestDegree });
        }

        /// <summary>
        /// Get high temperature by time 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="period"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Temperature))]
        [Route("api/temperature/{type}/{period}/high")]
        public async Task<IHttpActionResult> GetHighTemperature(string type, int period)
        {
            IQueryable<Temperature> temperature;

            switch (type)
            {
                case "year":
                    temperature = from a in _db.Temperatures
                                  where a.Year == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "month":
                    temperature = from a in _db.Temperatures
                                  where a.Month == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "week":
                    temperature = from a in _db.Temperatures
                                  where a.Week == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "day":
                    temperature = from a in _db.Temperatures
                                  where a.Day == period
                                  where a.Degree != -99
                                  select a;
                    break;
                default:
                    return NotFound();
            }
            var highestDegree = await temperature.Select(a => a.Degree).MaxAsync();

            if (highestDegree == null)
            {
                return NotFound();
            }

            return Ok(new { type, period, highTemperature = highestDegree });
        }

        /// <summary>
        /// Get average temperature by time 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="period"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Temperature))]
        [Route("api/temperature/{type}/{period}/avg")]
        public async Task<IHttpActionResult> GetAverageTemperature(string type, int period)
        {
            IQueryable<Temperature> temperature;

            switch (type)
            {
                case "year":
                    temperature = from a in _db.Temperatures
                                  where a.Year == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "month":
                    temperature = from a in _db.Temperatures
                                  where a.Month == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "week":
                    temperature = from a in _db.Temperatures
                                  where a.Week == period
                                  where a.Degree != -99
                                  select a;
                    break;
                case "day":
                    temperature = from a in _db.Temperatures
                                  where a.Day == period
                                  where a.Degree != -99
                                  select a;
                    break;
                default:
                    return NotFound();
            }
            var averageDegree = await temperature.Select(a => a.Degree).AverageAsync();

            if (averageDegree == null)
            {
                return NotFound();
            }

            return Ok(new { type, period, averageDegree });
        }
        // PUT: api/Temperatures/5
        /// <summary>
        /// Update temperature by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="temperature"></param>
        /// <returns>IHttpActionResult interface</returns>
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


        /// <summary>
        /// Create temperature 
        /// </summary>
        /// <param name="temperature"></param>
        /// <returns>IHttpActionResult interface</returns>
        [ResponseType(typeof(Temperature))]
        public async Task<IHttpActionResult> PostTemperature(Temperature temperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //new id
            string id = Guid.NewGuid().ToString();
            temperature.Id = id;
            //populate year, month, week and day from RecorDateTime
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


        /// <summary>
        /// Delete temperature by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IHttpActionResult interface</returns>
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