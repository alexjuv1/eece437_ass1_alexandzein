using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityApp.Data;
using UniversityApp.Models;
using FluentValidation.AspNetCore;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// EF + Identity
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 🔧 Fix: Disable automatic validation to allow async FluentValidation rules
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// FluentValidation
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// JWT Auth
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtIssuer
    };
});

// Authorization policies
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    opt.AddPolicy("StudentsOnly", p => p.RequireRole("Student"));
});

// Swagger + JWT setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo { Title = "UniversityApp", Version = "v1" });
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Enter: Bearer {token}"
    });
    opts.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="Bearer"}
            }, new string[]{}
        }
    });
    opts.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Seed roles & users
using (var scope = app.Services.CreateScope())
{
    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "Student" };
    foreach (var role in roles)
    {
        if (!await roleMgr.RoleExistsAsync(role))
            await roleMgr.CreateAsync(new IdentityRole(role));
    }

    if (await userMgr.FindByNameAsync("admin") == null)
    {
        var admin = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@uni.com",
            FirstName = "Zein",
            LastName = "Admin",
            EmailConfirmed = true
        };
        var result = await userMgr.CreateAsync(admin, "Admin1!");
        if (result.Succeeded)
        {
            await userMgr.AddToRoleAsync(admin, "Admin");
            Console.WriteLine("✅ Admin created!");
        }
        else
        {
            Console.WriteLine("❌ Admin creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    if (await userMgr.FindByNameAsync("stud") == null)
    {
        var student = new ApplicationUser
        {
            UserName = "stud",
            Email = "stud@uni.com",
            FirstName = "Ali",
            LastName = "Student",
            EmailConfirmed = true
        };
        var result = await userMgr.CreateAsync(student, "Stud1!");
        if (result.Succeeded)
        {
            await userMgr.AddToRoleAsync(student, "Student");
            Console.WriteLine("✅ Student created!");
        }
        else
        {
            Console.WriteLine("❌ Student creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Departments}/{action=Index}/{id?}");
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

// Global error handler
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext ctx) =>
{
    var ex = ctx.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    return Results.Problem(detail: ex?.Message, statusCode: 500);
});

app.Run();
