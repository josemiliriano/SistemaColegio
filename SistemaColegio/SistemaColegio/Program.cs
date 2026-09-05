using Application.AsignacionDocente;
using Application.Aula;
using Application.Autenticacion;
using Application.Curso;
using Application.CursoMateria;
using Application.CursoPeriodo;
using Application.CursoPeriodo.Application.CursoPeriodo;
using Application.Estudiante;
using Application.Materia;
using Application.Periodo;
using Application.PeriodoSesion;
using Application.PeriodoSesion.Application.SeccionPeriodo;
using Application.Persona;
using Application.Profesor;
using Application.ProfesorMateria;
using Application.Seccion;
using Application.Usuario;
using Infraestructure.Data;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Entity Framework
builder.Services.AddDbContext<MyDataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// MVC
builder.Services.AddControllersWithViews();

// Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!
                )
            )
        };
    });

// Repository
builder.Services.AddScoped(typeof(GeneralRepository<>));

// Services
builder.Services.AddScoped<IPersonAppService, PersonAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IProfesorAppService, ProfessorAppService>();
builder.Services.AddScoped<IEstudentAppService, EstudentAppService>();

builder.Services.AddScoped<ISubjectAppService, SubjectAppService>();
builder.Services.AddScoped<ICourseAppService, CourseAppService>();
builder.Services.AddScoped<ISessionAppService, SessionAppService>();
builder.Services.AddScoped<IClassroomAppService, ClassroomAppService>();
builder.Services.AddScoped<IPeriodAppService, PeriodAppService>();

builder.Services.AddScoped<ICoursePeriodAppService, CoursePeriodAppService>();
builder.Services.AddScoped<ICourseSubjectAppService, CourseSubjectAppService>();
builder.Services.AddScoped<IProfessorSubjectAppService, ProfessorSubjectAppService>();
builder.Services.AddScoped<ISessionPeriodAppService, SessionPeriodAppService>();
builder.Services.AddScoped<ITeachingAssignmentAppService, TeachingAssignmentAppService>();

// Autenticación
builder.Services.AddScoped<IAuthAppService, AuthAppService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Archivos estáticos: HTML, CSS, JS, imágenes, etc.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();