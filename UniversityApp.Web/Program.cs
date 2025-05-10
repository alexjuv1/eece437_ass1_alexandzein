using Microsoft.EntityFrameworkCore;
using UniversityApp.Infrastructure.Persistence;
using UniversityApp.Core.Interfaces;
using UniversityApp.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Add EF Core + SQLite ──────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Register Repositories ─────────────────────────────────────────────
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// ── MVC setup (you’ll add MediatR, AutoMapper, etc. later) ────────────
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ── Minimal “smoke test” endpoint ────────────────────────────────────
// Fetch all students via your new StudentRepository
app.MapGet("/api/students", async (IStudentRepository repo) =>
    Results.Ok(await repo.GetAllAsync()));

app.Run();
