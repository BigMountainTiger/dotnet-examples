using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace b_01_entity_framework.Example_2 {

  public class A_Example2_JOIN_Test {

    public static async Task Test() {
      List<DataProgramtype> data = null;

      using (var context = new Example2Context())
      {
        var query = context.DataProgramtype
        .Where(x => TestID.Test_IDS.Contains(x.CorelogicDataID))
        .Where(x => x.ProgramTypeID == 6);

        // Console.WriteLine(query.ToQueryString());
        data = await query.ToListAsync();
      }

      foreach (var d in data) {
        Console.WriteLine($"ID = {d.CorelogicDataID}, F-Name = {d.ProgramTypeID}");
      }

      Console.WriteLine($"Total count = {data.Count()}");
    }

  }

}