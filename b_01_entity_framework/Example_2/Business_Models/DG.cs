using System;
using System.Collections.Generic;
using System.Linq;

using b_01_entity_framework.Example_2.Database;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class DG {
    private List<DataGroup> Groups = new List<DataGroup>();
    private DataRecordCollection collection = new DataRecordCollection();

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
          .Select(collection.Add)
          .ToList();
        
        g.Initialize(items);
      }


    }

    public void Test() {
      Init();
      LoadItems();
      
      foreach(var g in Groups) {
        Console.WriteLine(g.EligibleRecords.Count.ToString());
      }
    }

  }

}