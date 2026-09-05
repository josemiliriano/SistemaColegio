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
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Entity Framework
builder.Services.AddDbContext<MyDataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// MVC
builder.Services.AddControllersWithViews();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Archivos estáticos: HTML, CSS, JS, imágenes, etc.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();