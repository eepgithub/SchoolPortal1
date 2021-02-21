using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Data;
using SchoolPortal.Entities;

namespace SchoolPortal.Controllers
{
    public class SchoolClassStudentsController : Controller
    {
        private readonly SchoolPortalDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchoolClassStudentsController(SchoolPortalDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolClassStudents
        public async Task<IActionResult> Index()
        {
            var schoolPortalDbContext = _context.SchoolClassStudents.Include(s => s.SchoolClass);
            return View(await schoolPortalDbContext.ToListAsync());
        }

        // GET: SchoolClassStudents/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClassStudent = await _context.SchoolClassStudents
                .Include(s => s.SchoolClass)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (schoolClassStudent == null)
            {
                return NotFound();
            }

            return View(schoolClassStudent);
        }

        // GET: SchoolClassStudents/Create
        public async Task<IActionResult> Create()
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");

            ViewData["StudentId"] = new SelectList(students, "Id", "DisplayName");
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName");
            return View();
        }

        // POST: SchoolClassStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,SchoolClassId")] SchoolClassStudent schoolClassStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolClassStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName", schoolClassStudent.SchoolClassId);
            return View(schoolClassStudent);
        }

        // GET: SchoolClassStudents/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClassStudent = await _context.SchoolClassStudents.FindAsync(id);
            if (schoolClassStudent == null)
            {
                return NotFound();
            }
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName", schoolClassStudent.SchoolClassId);
            return View(schoolClassStudent);
        }

        // POST: SchoolClassStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,SchoolClassId")] SchoolClassStudent schoolClassStudent)
        {
            if (id != schoolClassStudent.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolClassStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassStudentExists(schoolClassStudent.StudentId))
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
            ViewData["SchoolClassId"] = new SelectList(_context.SchoolClasses, "Id", "ClassName", schoolClassStudent.SchoolClassId);
            return View(schoolClassStudent);
        }

        // GET: SchoolClassStudents/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClassStudent = await _context.SchoolClassStudents
                .Include(s => s.SchoolClass)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (schoolClassStudent == null)
            {
                return NotFound();
            }

            return View(schoolClassStudent);
        }

        // POST: SchoolClassStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var schoolClassStudent = await _context.SchoolClassStudents.FindAsync(id);
            _context.SchoolClassStudents.Remove(schoolClassStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassStudentExists(string id)
        {
            return _context.SchoolClassStudents.Any(e => e.StudentId == id);
        }
    }
}
