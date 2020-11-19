using System;
using System.Collections.Generic;
using System.Linq;

using b_01_entity_framework.Example_2.Database;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class DG {
    private DataGroups Groups = new DataGroups();
    private DataRecordCollection Collection = new DataRecordCollection();

    public void Init() {
      for (var i = 0; i <= 1; i++) {
        var g = new DataGroup(25);
        Groups.Add(g);
      }
    }

    public void LoadItems() {

      foreach(var g in Groups) {

        var items = RecordLoader.Load()
          .Select(i => new DataRecord(i))
          .Select(Collection.Add)
          .ToList();
        
        g.Initialize(items);
      }

    }

    public void FulFill() {

      var records = Collection
        .GetAllRecords()
        .OrderBy(i => i.EligibleGroups.Count)
        .ToList();

      foreach (var record in records)
      {
          if (Groups.IsFullyPopulated) { break; }

          var g = record.EligibleGroups
            .Where(i => !i.IsFullfilled)
            .OrderBy(i => i.Surplus)
            .FirstOrDefault();

          g?.AddRecord(record);
      }
    }

    public void Test() {
      Init();
      LoadItems();
      FulFill();
      
      foreach(var g in Groups) {
        
        Console.WriteLine($"Need ItemCount - {g.ItemCount}");
        Console.WriteLine($"EligibleRecords - {g.EligibleRecords.Count}");
        Console.WriteLine($"Surplus - {g.Surplus}");
        Console.WriteLine($"UsedRecords - {g.UsedRecords.Count}");
      }

    }

  }

}