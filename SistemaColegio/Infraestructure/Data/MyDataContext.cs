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
        public DbSet<Person> Persons { get; set; }
        public DbSet<CDUser> Users { get; set; }
        public DbSet<Estudent> Estudents { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Period> Periods { get; set; }
    }
}
