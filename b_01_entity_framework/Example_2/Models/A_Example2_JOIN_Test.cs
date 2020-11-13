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
          .Where(x => TestID.Test_IDS.Contains(x.CorelogicDataID));

        // Join(context.DataProgramtype.Where(i => i.ProgramTypeID == model.ProgramTypeId).Select(i => i.CorelogicDataID),
        //             data => data.CorelogicDataID,
        //             dataProgramtype => dataProgramtype,
        //             (data, dataProgramtype) => data)

        // Console.WriteLine(query.ToQueryString());
        var data = await query
          .Select( x => new {
            x.CorelogicDataID
          }).ToListAsync();

        foreach(var d in data) {
          Console.WriteLine($"{d.CorelogicDataID}");
        }

        Console.WriteLine($"Count = {data.Count()}");
      }

    }

  }

}