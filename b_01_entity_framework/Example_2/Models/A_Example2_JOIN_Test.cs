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
          ).GroupJoin(context.UnusedDataOrder
              .Where(i => i.ProgramTypeId == 6)
              .Select(i => new { i.CorelogicDataId, i.DropDate }),
                d => d.CorelogicDataID, o => o.CorelogicDataId,
                (data, dataOrders) => new { data, dataOrders }
          ).SelectMany(i => i.dataOrders.DefaultIfEmpty(),
              (aggregate, dataOrder) => new {
                aggregate.data.CorelogicDataID,
                DropDate = (DateTime?)dataOrder.DropDate
              }
          ).GroupBy(i => new { i.CorelogicDataID },
            i => i.DropDate
          ).Select(i => new {
              i.Key.CorelogicDataID,
              UsageCount = i.Count(),
              LastUsedDate = i.Max()
            }
          );

        Console.WriteLine(query.ToQueryString());
        var data = await query.ToListAsync();

        foreach(var d in data) {
          Console.WriteLine($"{d.CorelogicDataID} - {d.UsageCount} - {d.LastUsedDate}");
        }

        Console.WriteLine($"Count = {data.Count()}");
      }

    }

  }

}