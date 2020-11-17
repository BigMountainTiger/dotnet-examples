using System;
using System.Linq;

using b_01_entity_framework.Example_2.Database;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class DG {
    
    public void Init() {

    }
    public void LoadItems() {
      var items = RecordLoader.Load();

      Console.WriteLine($"{items.Count()}");
    }

  }

}