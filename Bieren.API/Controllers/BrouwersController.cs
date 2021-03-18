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
    public class BrouwersController : ControllerBase
    {
        private readonly BierenDbContext _context;

        public BrouwersController(BierenDbContext context)
        {
            _context = context;
        }

        // GET: api/Brouwers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brouwer>>> GetBrouwers()
        {
            return await _context.Brouwers.ToListAsync();
        }

        // GET: api/Brouwers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brouwer>> GetBrouwer(int id)
        {
            var brouwer = await _context.Brouwers.FindAsync(id);

            if (brouwer == null)
            {
                return NotFound();
            }

            return brouwer;
        }

        // PUT: api/Brouwers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrouwer(int id, Brouwer brouwer)
        {
            if (id != brouwer.BrouwerNr)
            {
                return BadRequest();
            }

            _context.Entry(brouwer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrouwerExists(id))
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

        // POST: api/Brouwers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Brouwer>> PostBrouwer(Brouwer brouwer)
        {
            _context.Brouwers.Add(brouwer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrouwer", new { id = brouwer.BrouwerNr }, brouwer);
        }

        // DELETE: api/Brouwers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brouwer>> DeleteBrouwer(int id)
        {
            var brouwer = await _context.Brouwers.FindAsync(id);
            if (brouwer == null)
            {
                return NotFound();
            }

            _context.Brouwers.Remove(brouwer);
            await _context.SaveChangesAsync();

            return brouwer;
        }

        private bool BrouwerExists(int id)
        {
            return _context.Brouwers.Any(e => e.BrouwerNr == id);
        }
    }
}
