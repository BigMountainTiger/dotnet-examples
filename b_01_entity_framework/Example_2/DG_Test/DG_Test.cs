using System.Threading.Tasks;

using b_01_entity_framework.Example_2.Business_Models;

namespace b_01_entity_framework.Example_2.DG_Test {

  public class DG_Test {
    public static async Task Test() {
      await Task.Run(() => {
        var operation = new DG();

        operation.Init();
        operation.Test();
      });
    }

  }

}