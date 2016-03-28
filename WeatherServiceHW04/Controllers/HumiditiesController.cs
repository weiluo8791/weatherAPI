using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeatherServiceHW04.Models;

namespace WeatherServiceHW04.Controllers
{
    public class HumiditiesController : ApiController
    {
        private WeatherServiceContext db = new WeatherServiceContext();

        // GET: api/Humidities
        public IQueryable<Humidity> GetHumidities()
        {
            return db.Humidities;
        }

        // GET: api/Humidities/5
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

        // PUT: api/Humidities/5
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

        // POST: api/Humidities
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

        // DELETE: api/Humidities/5
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