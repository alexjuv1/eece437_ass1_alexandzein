using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityApp.Data;
using UniversityApp.Models;
using FluentValidation.AspNetCore;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Identity;
using UniversityApp.Models;


var builder = WebApplication.CreateBuilder(args);

// EF + Identity
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(opt=> {
    opt.Password.RequiredLength=6;
    opt.Password.RequireNonAlphanumeric=false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv=>fv.RegisterValidatorsFromAssemblyContaining<Program>());

// JWT Auth
var jwtKey    = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;
builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts=>{
    opts.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer           = true,
        ValidIssuer              = jwtIssuer,
        ValidateAudience         = true,
        ValidAudience            = jwtIssuer
    };
});

// Authorization policies
builder.Services.AddAuthorization(opt=>{
    opt.AddPolicy("AdminOnly",    p=>p.RequireRole("Admin"));
    opt.AddPolicy("StudentsOnly", p=>p.RequireRole("Student"));
});

// Swagger + JWT setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts=>{
    opts.SwaggerDoc("v1", new OpenApiInfo{Title="UniversityApp",Version="v1"});
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        Name="Authorization",In=ParameterLocation.Header,Type=SecuritySchemeType.ApiKey,
        Scheme="Bearer",BearerFormat="JWT",
        Description="Enter: Bearer {token}"
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
using(var scope = app.Services.CreateScope()){
    var rm = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var um = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    foreach(var role in new[]{"Admin","Student"})
        if(!await rm.RoleExistsAsync(role)) await rm.CreateAsync(new IdentityRole(role));
    if(await um.FindByNameAsync("admin")==null){
        var u=new ApplicationUser{UserName="admin",Email="admin@uni.com",EmailConfirmed=true};
        await um.CreateAsync(u,"Admin1!"); await um.AddToRoleAsync(u,"Admin");
    }
    if(await um.FindByNameAsync("stud")==null){
        var u=new ApplicationUser{UserName="stud",Email="stud@uni.com",EmailConfirmed=true};
        await um.CreateAsync(u,"Stud1!"); await um.AddToRoleAsync(u,"Student");
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","v1"));

// Global error handler
app.UseExceptionHandler("/error");
app.Map("/error",(HttpContext ctx)=>{
    var ex = ctx.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    return Results.Problem(detail:ex?.Message,statusCode:500);
});

app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // 1) Ensure roles exist
    foreach(var roleName in new[] { "Admin", "Student" })
    {
        if (!await roleMgr.RoleExistsAsync(roleName))
            await roleMgr.CreateAsync(new IdentityRole(roleName));
    }

    // 2) Seed Admin user
    string adminUser = "admin";
    string adminPwd  = "Admin1!";
    if (await userMgr.FindByNameAsync(adminUser) == null)
    {
        var usr = new ApplicationUser
        {
            UserName       = adminUser,
            Email          = "admin@uni.com",
            EmailConfirmed = true,
            FirstName      = "Super",
            LastName       = "Admin"
        };
        var res = await userMgr.CreateAsync(usr, adminPwd);
        if (res.Succeeded)
            await userMgr.AddToRoleAsync(usr, "Admin");
        else
            Console.WriteLine("❌ Admin creation failed: " + string.Join(", ", res.Errors.Select(e => e.Description)));
    }

    // 3) Seed Student user
    string studUser = "student";
    string studPwd  = "Stud1!";
    if (await userMgr.FindByNameAsync(studUser) == null)
    {
        var usr = new ApplicationUser
        {
            UserName       = studUser,
            Email          = "student@uni.com",
            EmailConfirmed = true,
            FirstName      = "Normal",
            LastName       = "Student"
        };
        var res = await userMgr.CreateAsync(usr, studPwd);
        if (res.Succeeded)
            await userMgr.AddToRoleAsync(usr, "Student");
        else
            Console.WriteLine("❌ Student creation failed: " + string.Join(", ", res.Errors.Select(e => e.Description)));
    }
}

app.Run();
