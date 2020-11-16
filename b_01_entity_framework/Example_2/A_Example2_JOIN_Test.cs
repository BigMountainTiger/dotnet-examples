using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using b_01_entity_framework.Example_2.Business_Models;

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
                aggregate.data.Property_State,
                DropDate = (DateTime?)dataOrder.DropDate
              }
          ).GroupBy(i => new { i.CorelogicDataID, i.Property_State },
            i => i.DropDate
          ).Select(i => new RecordItem() {
              CorelogicDataID= i.Key.CorelogicDataID,
              Property_State = i.Key.Property_State,
              UsageCount = i.Count(),
              LastUsedDate = i.Max()
            }
          ).OrderBy(d => d.UsageCount);

        Console.WriteLine(query.ToQueryString());
        var data = await query.ToListAsync();

        foreach(var d in data) {
          Console.WriteLine($"{d.CorelogicDataID} - {d.Property_State} - {d.UsageCount} - {d.LastUsedDate}");
        }

        Console.WriteLine("\n" + $"Count = {data.Count()}");
      }

    }

  }

}