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
    public class ProfessorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Professors.Include(p => p.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/Create
        public IActionResult Create()
        {
            var model = new ProfessorViewModel
            {
                Departments = _context.Departments.ToList()
            };

            return View(model);
        }


        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfessorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _context.Departments.ToList();
                return View(model);
            }

            var professor = new Professor
            {
                Name = model.Name,
                Email = model.Email,
                DepartmentId = model.DepartmentId
            };

            _context.Professors.Add(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Professors/Edit/5
       public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var professor = await _context.Professors.FindAsync(id);
            if (professor == null) return NotFound();

            var model = new ProfessorViewModel
            {
                ProfessorId = professor.ProfessorId,
                Name = professor.Name,
                Email = professor.Email,
                DepartmentId = professor.DepartmentId,
                Departments = _context.Departments.ToList()
            };

            return View(model);
        }


        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfessorViewModel model)
        {
            if (id != model.ProfessorId) return NotFound();

            if (!ModelState.IsValid)
            {
                model.Departments = _context.Departments.ToList();
                return View(model);
            }

            var professor = await _context.Professors.FindAsync(id);
            if (professor == null) return NotFound();

            professor.Name = model.Name;
            professor.Email = model.Email;
            professor.DepartmentId = model.DepartmentId;

            _context.Update(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Professors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professors.FindAsync(id);
            if (professor != null)
            {
                _context.Professors.Remove(professor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professors.Any(e => e.ProfessorId == id);
        }
    }
}
