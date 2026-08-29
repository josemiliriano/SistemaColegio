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
            modelBuilder.Entity<ProfessorSubject>()
                .HasOne(ps => ps.Professor)
                .WithMany(p => p.ProfessorSubjects)
                .HasForeignKey(ps => ps.IdProfesor);

            modelBuilder.Entity<CDUser>()
                .HasOne(u => u.Persona)
                .WithOne(p => p.Usuario)
                .HasForeignKey<CDUser>(u => u.IdPersona)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estudent>()
                .HasOne(e => e.Person)
                .WithOne(p => p.Estudiante)
                .HasForeignKey<Estudent>(e => e.IdPersona)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfessorSubject>()
                .HasOne(pm => pm.Professor)
                .WithMany(p => p.ProfessorSubjects)
                .HasForeignKey(pm => pm.IdProfesor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfessorSubject>()
                .HasOne(pm => pm.Professor)
                .WithMany(p => p.ProfessorSubjects)
                .HasForeignKey(pm => pm.IdProfesor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoursePeriod>()
                .HasOne(cp => cp.Course)
                .WithMany(c => c.CursoPeriodos)
                .HasForeignKey(cp => cp.IdCurso)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoursePeriod>()
                .HasOne(cp => cp.Period)
                .WithMany(p => p.CoursePeriods)
                .HasForeignKey(cp => cp.IdPeriodo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfessorSubject>()
                .HasIndex(pm => new
                {
                    pm.IdProfesor,
                    pm.IdMateria
                })
                .IsUnique();

            modelBuilder.Entity<CoursePeriod>()
                .HasIndex(cp => new
                {        
                    cp.IdCurso,
                    cp.IdPeriodo
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

    }
}






