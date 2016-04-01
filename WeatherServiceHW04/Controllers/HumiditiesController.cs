using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        private WeatherServiceContext db = new WeatherServiceContext();

        // GET: api/Humidities
        /// <summary>
        /// Get all Humidities
        /// </summary>
        /// <returns>IQueryable</returns>
        public IQueryable<Humidity> GetHumidities()
        {
            return db.Humidities;
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
            Humidity humidity = await db.Humidities.FindAsync(id);
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

            db.Entry(humidity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

            db.Humidities.Add(humidity);

            try
            {
                await db.SaveChangesAsync();
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
            Humidity humidity = await db.Humidities.FindAsync(id);
            if (humidity == null)
            {
                return NotFound();
            }

            db.Humidities.Remove(humidity);
            await db.SaveChangesAsync();

            return Ok(humidity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HumidityExists(string id)
        {
            return db.Humidities.Count(e => e.Id == id) > 0;
        }
    }
}