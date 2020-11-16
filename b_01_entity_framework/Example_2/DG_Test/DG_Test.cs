using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using b_01_entity_framework.Example_2.Business_Models;

namespace b_01_entity_framework.Example_2.DG_Test {

  public class DG_Test {

    private static IQueryable<RecordItem> GetQuery_1(Example2Context context) {
      int programTypeId = 6;

      var query = context.UnusedData
          .Where(x => TestID.Test_IDS.Contains(x.CorelogicDataID))
          .Join(context.DataProgramtype.Where(x => x.ProgramTypeID == programTypeId),
            d => new { d.CorelogicDataID }, p => new { p.CorelogicDataID }, (d, p) => d
          ).GroupJoin(context.UnusedDataOrder
              .Where(i => i.ProgramTypeId == programTypeId)
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

      return query;
    }
    private static IQueryable<RecordItem> GetQuery_2(Example2Context context) {
      int programTypeId = 6;

      var query = context.UnusedData
          .Where(x => TestID.Test_IDS.Contains(x.CorelogicDataID))
          .Join(context.DataProgramtype,
            d => new { CorelogicDataID = d.CorelogicDataID, ProgramTypeID = programTypeId },
            p => new { CorelogicDataID = p.CorelogicDataID, ProgramTypeID = p.ProgramTypeID }, (d, p) => d
          ).GroupJoin(context.UnusedDataOrder.Where(i => i.ProgramTypeId == programTypeId),
                d => d.CorelogicDataID , o => o.CorelogicDataId,
                (d, o) => new { data = d, dataOrders = o }
          ).SelectMany(i => i.dataOrders.DefaultIfEmpty(),
              (a, o) => new {
                CorelogicDataID = a.data.CorelogicDataID,
                Property_State = a.data.Property_State,
                DropDate = (DateTime?)o.DropDate
              }
          ).GroupBy(i => new { i.CorelogicDataID, i.Property_State }, i => i.DropDate
          ).Select(i => new RecordItem() {
              CorelogicDataID= i.Key.CorelogicDataID,
              Property_State = i.Key.Property_State,
              UsageCount = i.Count(),
              LastUsedDate = i.Max()
            }
          ).OrderBy(d => d.UsageCount);

      return query;
    }
    public static async Task Test() {

      using (var context = new Example2Context())
      {

        var query = GetQuery_2(context);

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