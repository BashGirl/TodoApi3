using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi3.Models;

namespace TodoApi3.Controllers
{
  
    [Route("api/PizzaItems")]
    [ApiController]
    public class PizzaitemsController : ControllerBase
    {
        private readonly PizzaContext _context;

        public PizzaitemsController(PizzaContext context)
        {
            _context = context;
        }

        // GET: api/Pizzaitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaItemDTO>>> GetPizzaItems()
        {
            return await _context.PizzaItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/Pizzaitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaItemDTO>> GetPizzaitem(long id)
        {
            var pizzaitem = await _context.PizzaItems.FindAsync(id);

            if (pizzaitem == null)
            {
                return NotFound();
            }

            return ItemToDTO(pizzaitem);
        }

        // PUT: api/Pizzaitems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaitem(long id, PizzaItemDTO tidto)
        {
            if (id != tidto.Id)
            {
                return BadRequest();
            }

            var pizzaItem = await _context.PizzaItems.FindAsync(id);
            if (pizzaItem == null)
            {
                return NotFound();
            }

            pizzaItem.Name = tidto.Name;
            pizzaItem.IsComplete = tidto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PizzaitemExists(id))
            {

                return NotFound();

            }

            return NoContent();
        }

        // POST: api/Pizzaitems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PizzaItemDTO>> CreatePizzaitem(PizzaItemDTO pizzaItemDTO)
        {
            var pizzaItem = new Pizzaitem
            {
                IsComplete = pizzaItemDTO.IsComplete,
                Name = pizzaItemDTO.Name
            };

            _context.PizzaItems.Add(pizzaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetPizzaitem),
            new { id = pizzaItem.Id },
            ItemToDTO(pizzaItem));

        }

        // DELETE: api/Pizzaitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaitem(long id)
        {
            var pizzaitem = await _context.PizzaItems.FindAsync(id);

            if (pizzaitem == null)
            {
                return NotFound();
            }

            _context.PizzaItems.Remove(pizzaitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaitemExists(long id)
        {
            return _context.PizzaItems.Any(e => e.Id == id);
        }

        private static PizzaItemDTO ItemToDTO(Pizzaitem pizzaItem) =>
            new PizzaItemDTO
            {
                Id = pizzaItem.Id,
                Name = pizzaItem.Name,
                IsComplete = pizzaItem.IsComplete
            };
    }

}