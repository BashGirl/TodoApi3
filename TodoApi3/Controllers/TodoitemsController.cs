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
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoitemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoitemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Todoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _context.TodoItems
             .Select(x => ItemToDTO(x))
             .ToListAsync();
        }

        // GET: api/Todoitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoitem(long id)
        {
            var todoitem = await _context.TodoItems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoitem);
        }

        // PUT: api/Todoitems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoitem(long id, TodoItemDTO tidto)
        {
            if (id != tidto.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = tidto.Name;
            todoItem.IsComplete = tidto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoitemExists(id))
            {

                return NotFound();

            }

            return NoContent();
        }

        // POST: api/Todoitems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoitem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new Todoitem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(
            nameof(GetTodoitem),
            new { id = todoItem.Id },
            ItemToDTO(todoItem));

        }

        // DELETE: api/Todoitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoitem(long id)
        {
            var todoitem = await _context.TodoItems.FindAsync(id);
            
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoitemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(Todoitem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
