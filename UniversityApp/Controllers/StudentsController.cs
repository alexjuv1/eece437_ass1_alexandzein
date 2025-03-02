using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
using UniversityApp.ViewModels;

namespace UniversityApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            // Store search and sort inputs for the view
            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            
            var students = _context.Students
                .Include(s => s.Department)
                .AsQueryable();

            // Search logic
            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString) 
                                            || s.Email.Contains(searchString)
                                            || s.Department.Name.Contains(searchString)); 
            }

            // Sorting logic
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "email_asc":
                    students = students.OrderBy(s => s.Email);
                    break;
                case "department_asc":
                    students = students.OrderBy(s => s.Department.Name);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            return View(await students.ToListAsync());
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var model = new StudentViewModel
            {
                Departments = _context.Departments.ToList()
            };

            return View(model);
        }


        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _context.Departments.ToList();
                return View(model);
            }

            var student = new Student
            {
                Name = model.Name,
                Email = model.Email,
                EnrollmentYear = model.EnrollmentYear,
                DepartmentId = model.DepartmentId
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            var model = new StudentViewModel
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Email = student.Email,
                EnrollmentYear = student.EnrollmentYear,
                DepartmentId = student.DepartmentId,
                Departments = _context.Departments.ToList()
            };

            return View(model);
        }


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (id != model.StudentId) return NotFound();

            if (!ModelState.IsValid)
            {
                model.Departments = _context.Departments.ToList();
                return View(model);
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            student.Name = model.Name;
            student.Email = model.Email;
            student.EnrollmentYear = model.EnrollmentYear;
            student.DepartmentId = model.DepartmentId;

            _context.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
