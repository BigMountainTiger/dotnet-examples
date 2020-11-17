using System;
using System.Linq;

using b_01_entity_framework.Example_2.Business_Models.Database;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class DG {
    
    public void LoadItems() {
      var items = LoadRecordItems.Load();

      Console.WriteLine($"{items.Count()}");
    }

  }

}