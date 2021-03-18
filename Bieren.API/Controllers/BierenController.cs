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
    public class BierenController : ControllerBase
    {
        private readonly BierenDbContext _context;

        public BierenController(BierenDbContext context)
        {
            _context = context;
        }

        // GET: api/Bieren
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bier>>> GetBieren()
        {
            return await _context.Bieren.ToListAsync();
        }

        // GET: api/Bieren/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bier>> GetBier(int id)
        {
            var bier = await _context.Bieren.FindAsync(id);

            if (bier == null)
            {
                return NotFound();
            }

            return bier;
        }

        // PUT: api/Bieren/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBier(int id, Bier bier)
        {
            if (id != bier.BierNr)
            {
                return BadRequest();
            }

            _context.Entry(bier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BierExists(id))
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

        // POST: api/Bieren
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bier>> PostBier(Bier bier)
        {
            _context.Bieren.Add(bier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBier", new { id = bier.BierNr }, bier);
        }

        // DELETE: api/Bieren/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bier>> DeleteBier(int id)
        {
            var bier = await _context.Bieren.FindAsync(id);
            if (bier == null)
            {
                return NotFound();
            }

            _context.Bieren.Remove(bier);
            await _context.SaveChangesAsync();

            return bier;
        }

        private bool BierExists(int id)
        {
            return _context.Bieren.Any(e => e.BierNr == id);
        }
    }
}
