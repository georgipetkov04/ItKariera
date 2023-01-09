using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_7.Data;

namespace Project_7.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToDoItem.Include(t => t.Assignee).Include(t => t.ToDoList);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToDoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToDoItem == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem
                .Include(t => t.Assignee)
                .Include(t => t.ToDoList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        

        public IActionResult Create()
        {
            ViewData["AssigneeId"] = new SelectList(_context.Set<AppUser>(), "Id", "Email");
            ViewData["ToDoListId"] = new SelectList(_context.ToDoList, "Id", "Title");
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssigneeId,Title,DeadLine,Description,ToDoListId")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssigneeId"] = new SelectList(_context.Set<AppUser>(), "Id", "Email", toDoItem.AssigneeId);
            ViewData["ToDoListId"] = new SelectList(_context.ToDoList, "Id", "Email", toDoItem.ToDoListId);
            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToDoItem == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            ViewData["AssigneeId"] = new SelectList(_context.Set<AppUser>(), "Id", "Email", toDoItem.AssigneeId);
            ViewData["ToDoListId"] = new SelectList(_context.ToDoList, "Id", "Email", toDoItem.ToDoListId);
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssigneeId,Title,DeadLine,Description,ToDoListId")] ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssigneeId"] = new SelectList(_context.Set<AppUser>(), "Id", "Email", toDoItem.AssigneeId);
            ViewData["ToDoListId"] = new SelectList(_context.ToDoList, "Id", "Email", toDoItem.ToDoListId);
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToDoItem == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem
                .Include(t => t.Assignee)
                .Include(t => t.ToDoList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToDoItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ToDoItem'  is null.");
            }
            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem != null)
            {
                _context.ToDoItem.Remove(toDoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemExists(int id)
        {
          return _context.ToDoItem.Any(e => e.Id == id);
        }
    }
}
