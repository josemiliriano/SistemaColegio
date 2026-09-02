using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Data
{
    public class MyDataContext:DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ProfessorSubject -> Professor
            modelBuilder.Entity<ProfessorSubject>()
                .HasOne(x => x.Professor)
                .WithMany(x => x.ProfessorSubjects)
                .HasForeignKey(x => x.IdProfesor)
                .OnDelete(DeleteBehavior.Restrict);

            // ProfessorSubject -> Subject
            modelBuilder.Entity<ProfessorSubject>()
                .HasOne(x => x.Subject)
                .WithMany(x => x.ProfessorSubjects)
                .HasForeignKey(x => x.IdMateria)
                .OnDelete(DeleteBehavior.Restrict);

            // Evita que un profesor tenga repetida la misma materia
            modelBuilder.Entity<ProfessorSubject>()
                .HasIndex(x => new
                {
                    x.IdProfesor,
                    x.IdMateria
                })
                .IsUnique();


            // User -> Person
            modelBuilder.Entity<CDUser>()
                .HasOne(x => x.Persona)
                .WithOne(x => x.Usuario)
                .HasForeignKey<CDUser>(x => x.IdPersona)
                .OnDelete(DeleteBehavior.Restrict);


            // Student -> Person
            modelBuilder.Entity<Estudent>()
                .HasOne(x => x.Person)
                .WithOne(x => x.Estudiante)
                .HasForeignKey<Estudent>(x => x.IdPersona)
                .OnDelete(DeleteBehavior.Restrict);


            // Professor -> Person
            modelBuilder.Entity<Professor>()
                .HasOne(x => x.Persona)
                .WithOne(x => x.Profesor)
                .HasForeignKey<Professor>(x => x.IdPersona)
                .OnDelete(DeleteBehavior.Restrict);


            // User -> Role
            modelBuilder.Entity<CDUser>()
                .HasOne(x => x.Rol)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.IdRol)
                .OnDelete(DeleteBehavior.Restrict);


            // CoursePeriod -> Course
            modelBuilder.Entity<CoursePeriod>()
                .HasOne(x => x.Course)
                .WithMany(x => x.CursoPeriodos)
                .HasForeignKey(x => x.IdCurso)
                .OnDelete(DeleteBehavior.Restrict);


            // CoursePeriod -> Period
            modelBuilder.Entity<CoursePeriod>()
                .HasOne(x => x.Period)
                .WithMany(x => x.CoursePeriods)
                .HasForeignKey(x => x.IdPeriodo)
                .OnDelete(DeleteBehavior.Restrict);


            // Evita repetir Course + Period
            modelBuilder.Entity<CoursePeriod>()
                .HasIndex(x => new
                {
                    x.IdCurso,
                    x.IdPeriodo
                })
                .IsUnique();


            // AcademicSubPeriod -> Period
            modelBuilder.Entity<AcademicSubPeriod>()
                .HasOne(x => x.Period)
                .WithMany(x => x.AcademicSubPeriods)
                .HasForeignKey(x => x.IdPeriodo)
                .OnDelete(DeleteBehavior.Restrict);


            // AcademicSubPeriod -> SubPeriod
            modelBuilder.Entity<AcademicSubPeriod>()
                .HasOne(x => x.SubPeriod)
                .WithMany(x => x.AcademicSubPeriods)
                .HasForeignKey(x => x.IdSubPeriodo)
                .OnDelete(DeleteBehavior.Restrict);


            // AcademicMonth -> AcademicSubPeriod
            modelBuilder.Entity<AcademicMonth>()
                .HasOne(x => x.AcademicSubPeriod)
                .WithMany(x => x.AcademicMonths)
                .HasForeignKey(x => x.IdPeriodoAcademico)
                .OnDelete(DeleteBehavior.Restrict);


            // AcademicMonth -> Month
            modelBuilder.Entity<AcademicMonth>()
                .HasOne(x => x.Month)
                .WithMany(x => x.AcademicMonths)
                .HasForeignKey(x => x.IdMes)
                .OnDelete(DeleteBehavior.Restrict);


            // CourseSubject -> Course
            modelBuilder.Entity<CourseSubject>()
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseSubjects)
                .HasForeignKey(x => x.IdCurso)
                .OnDelete(DeleteBehavior.Restrict);


            // CourseSubject -> Subject
            modelBuilder.Entity<CourseSubject>()
                .HasOne(x => x.Subject)
                .WithMany(x => x.CourseSubjects)
                .HasForeignKey(x => x.IdMateria)
                .OnDelete(DeleteBehavior.Restrict);


            // Evita repetir Course + Subject
            modelBuilder.Entity<CourseSubject>()
                .HasIndex(x => new
                {
                    x.IdCurso,
                    x.IdMateria
                })
                .IsUnique();


            // SessionPeriod -> Session
            modelBuilder.Entity<SessionPeriod>()
                .HasOne(x => x.Session)
                .WithMany(x => x.SessionPeriods)
                .HasForeignKey(x => x.IdSeccion)
                .OnDelete(DeleteBehavior.Restrict);


            // SessionPeriod -> Period
            modelBuilder.Entity<SessionPeriod>()
                .HasOne(x => x.Period)
                .WithMany(x => x.SessionPeriods)
                .HasForeignKey(x => x.IdPeriodo)
                .OnDelete(DeleteBehavior.Restrict);


            // SessionPeriod -> Classroom
            modelBuilder.Entity<SessionPeriod>()
                .HasOne(x => x.Classroom)
                .WithMany(x => x.SessionPeriods)
                .HasForeignKey(x => x.IdAula)
                .OnDelete(DeleteBehavior.Restrict);


            // TeachingAssignment -> ProfessorSubject
            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(x => x.ProfessorSubject)
                .WithMany()
                .HasForeignKey(x => x.IdProfesorMateria)
                .OnDelete(DeleteBehavior.Restrict);


            // TeachingAssignment -> SessionPeriod
            modelBuilder.Entity<TeachingAssignment>()
                .HasOne(x => x.SessionPeriod)
                .WithMany()
                .HasForeignKey(x => x.IdSessionPeriod)
                .OnDelete(DeleteBehavior.Restrict);


            // Evita repetir ProfesorMateria + SessionPeriod
            modelBuilder.Entity<TeachingAssignment>()
                .HasIndex(x => new
                {
                    x.IdProfesorMateria,
                    x.IdSessionPeriod
                })
                .IsUnique();
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<CDUser> Users { get; set; }
        public DbSet<Estudent> Estudents { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ProfessorSubject> ProfessorSubjects { get; set; }
        public DbSet<Course> Courses { get; set; }        
        public DbSet<CoursePeriod> CoursePeriods { get; set; }
        public DbSet<SubPeriod> SubPeriods { get; set; }
        public DbSet<AcademicSubPeriod> AcademicSubPeriods { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<AcademicMonth> AcademicMonths { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CourseSubject> CourseSubjects { get; set; }

    }
}






