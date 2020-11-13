using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace b_01_entity_framework.Example_2 {

  public class A_Example2_JOIN_Test {

    public static async Task Test() {

      using (var context = new Example2Context())
      {

        var query = context.UnusedData
          .Where(x => TestID.Test_IDS.Contains(x.CorelogicDataID))
          .Join(context.DataProgramtype.Where(x => x.ProgramTypeID == 6),
            d => new { d.CorelogicDataID }, p => new { p.CorelogicDataID }, (d, p) => d
          );

        Console.WriteLine(query.ToQueryString());
        var data = await query.ToListAsync();

        foreach(var d in data) {
          Console.WriteLine($"{d.CorelogicDataID}");
        }

        Console.WriteLine($"Count = {data.Count()}");
      }

    }

  }

}