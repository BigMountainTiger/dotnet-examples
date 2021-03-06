using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace b_01_entity_framework.Example_1 {
  public class Student
  {
    public int? ID { get; set; }
    public string Name { get; set; }
  }
  public class Example1Context : DbContext {
    public DbSet<Student> Student { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var constr = Environment.GetEnvironmentVariable("Example1-CSTR");
      optionsBuilder.UseSqlServer(constr);
    }
  }

  public static class Example1DB {
    public static async Task<List<Student>> GetStudents() {
      List<Student> students = null;

      using (var context = new Example1Context())
      {
        students = await context.Student.ToListAsync();
      }

      return students;
    }
  }
}