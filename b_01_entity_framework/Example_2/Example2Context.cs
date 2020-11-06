using Microsoft.EntityFrameworkCore;
using System;

namespace b_01_entity_framework.Example_2 {
  public class Example2Context : DbContext {
    public DbSet<UnusedDataOrder> UnusedDataOrder { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var constr = Environment.GetEnvironmentVariable("Example2-CSTR");
      optionsBuilder.UseSqlServer(constr);
    }
  }
}