using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace b_01_entity_framework.Example_2 {
  public class Example2Context : DbContext {
    //public DbSet<Student> Student { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var constr = Environment.GetEnvironmentVariable("Example2-CSTR");
      optionsBuilder.UseSqlServer(constr);
    }
  }

  public static class Example2DB {
  }
}