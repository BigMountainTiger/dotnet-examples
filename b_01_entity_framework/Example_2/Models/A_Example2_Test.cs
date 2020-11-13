using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace b_01_entity_framework.Example_2 {

  public class A_Example2_Test {
    public static async Task Load_UnusedDataOrder() {
      var orderId = 87746;
      List<UnusedDataOrder> data = null;

      using (var context = new Example2Context())
      {
        data = await context.UnusedDataOrder
        .Where( x => x.OrderId == orderId )
        .Where( x => TestID.Test_IDS.Contains(x.CorelogicDataId))
        .ToListAsync();
      }

      foreach (var d in data) {
        Console.WriteLine($"ID = {d.CorelogicDataId}, Name = {d.ProgramTypeId}");
      }

      Console.WriteLine($"Total count = {data.Count()}");
    }

    public static async Task Load_UnusedData() {
      List<UnusedData> data = null;

      using (var context = new Example2Context())
      {
        data = await context.UnusedData
        .Where( x => TestID.Test_IDS.Contains(x.CorelogicDataID))
        .ToListAsync();
      }

      foreach (var d in data) {
        Console.WriteLine($"ID = {d.CorelogicDataID}, F-Name = {d.Property_Address_Full}");
      }

      Console.WriteLine($"Total count = {data.Count()}");
    }

    public static async Task Load_DataProgramtype() {
      List<DataProgramtype> data = null;

      using (var context = new Example2Context())
      {
        data = await context.DataProgramtype
        .Where(x => TestID.Test_IDS.Contains(x.CorelogicDataID))
        .Where(x => x.ProgramTypeID == 6)
        .ToListAsync();
      }

      foreach (var d in data) {
        Console.WriteLine($"ID = {d.CorelogicDataID}, F-Name = {d.ProgramTypeID}");
      }

      Console.WriteLine($"Total count = {data.Count()}");
    }
  }

}