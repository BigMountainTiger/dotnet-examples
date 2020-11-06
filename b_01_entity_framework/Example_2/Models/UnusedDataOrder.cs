using System;

namespace b_01_entity_framework.Example_2 {
  public class UnusedDataOrder {
    public int? Id { get; set; }
    public int CorelogicDataId { get; set; }
    public int OrderId { get; set; }
    public DateTime DropDate { get; set; }
    public int ProgramTypeId { get; set; }
    public DateTime Created { get; set; }
  }
}