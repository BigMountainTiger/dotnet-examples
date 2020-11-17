using System;
using System.Collections.Generic;
using System.Linq;

using b_01_entity_framework.Example_2.Database;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class DG {
    private List<DataGroup> Groups = new List<DataGroup>();
    private DataRecordCollection collection = new DataRecordCollection();

    public void Init() {
      var g = new DataGroup();
      Groups.Add(g);

      g = new DataGroup();
      Groups.Add(g);
    }

    public void LoadItems() {

      foreach(var g in Groups) {

        var items = RecordLoader.Load();
        foreach(var item in items) {

          var record = new DataRecord(item);

          collection.Add(record);
        }
      }

      Console.WriteLine(collection.GetAllRecords().Count());
    }

  }

}