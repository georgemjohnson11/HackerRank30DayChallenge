using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;

            if (_context.ToDoItems.Count() == 0)
            {
                // Create a new ToDoItem if collection is empty,
                // which means you can't delete all ToDoItems.
                _context.ToDoItems.Add(new ToDoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<ToDoItem>> GetAll()
        {
            return _context.ToDoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetToDo")]
        public ActionResult<ToDoItem> GetById(long id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetToDo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, ToDoItem item)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.ToDoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
