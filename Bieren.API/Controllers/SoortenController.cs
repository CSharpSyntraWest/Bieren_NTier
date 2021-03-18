using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Bieren.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoortenController : ControllerBase
    {
        private readonly BierenDbContext _context;

        public SoortenController(BierenDbContext context)
        {
            _context = context;
        }

        // GET: api/Soorten
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Soort>>> GetSoorten()
        {
            return await _context.Soorten.ToListAsync();
        }

        // GET: api/Soorten/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Soort>> GetSoort(int id)
        {
            var soort = await _context.Soorten.FindAsync(id);

            if (soort == null)
            {
                return NotFound();
            }

            return soort;
        }

        // PUT: api/Soorten/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoort(int id, Soort soort)
        {
            if (id != soort.SoortNr)
            {
                return BadRequest();
            }

            _context.Entry(soort).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoortExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Soorten
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Soort>> PostSoort(Soort soort)
        {
            _context.Soorten.Add(soort);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSoort", new { id = soort.SoortNr }, soort);
        }

        // DELETE: api/Soorten/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Soort>> DeleteSoort(int id)
        {
            var soort = await _context.Soorten.FindAsync(id);
            if (soort == null)
            {
                return NotFound();
            }

            _context.Soorten.Remove(soort);
            await _context.SaveChangesAsync();

            return soort;
        }

        private bool SoortExists(int id)
        {
            return _context.Soorten.Any(e => e.SoortNr == id);
        }
    }
}
