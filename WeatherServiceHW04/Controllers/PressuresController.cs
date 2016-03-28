﻿using System;
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
    public class PressuresController : ApiController
    {
        private WeatherServiceContext db = new WeatherServiceContext();

        // GET: api/Pressures
        public IQueryable<Pressure> GetPressures()
        {
            return db.Pressures;
        }

        // GET: api/Pressures/5
        [ResponseType(typeof(Pressure))]
        public async Task<IHttpActionResult> GetPressure(string id)
        {
            Pressure pressure = await db.Pressures.FindAsync(id);
            if (pressure == null)
            {
                return NotFound();
            }

            return Ok(pressure);
        }

        // PUT: api/Pressures/5
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

            db.Entry(pressure).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

        // POST: api/Pressures
        [ResponseType(typeof(Pressure))]
        public async Task<IHttpActionResult> PostPressure(Pressure pressure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pressures.Add(pressure);

            try
            {
                await db.SaveChangesAsync();
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

        // DELETE: api/Pressures/5
        [ResponseType(typeof(Pressure))]
        public async Task<IHttpActionResult> DeletePressure(string id)
        {
            Pressure pressure = await db.Pressures.FindAsync(id);
            if (pressure == null)
            {
                return NotFound();
            }

            db.Pressures.Remove(pressure);
            await db.SaveChangesAsync();

            return Ok(pressure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PressureExists(string id)
        {
            return db.Pressures.Count(e => e.Id == id) > 0;
        }
    }
}