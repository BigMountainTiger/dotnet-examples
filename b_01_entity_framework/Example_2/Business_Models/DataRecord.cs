using System;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class RecordItem {
    public int CorelogicDataID { get; set; }
    public string Property_State { get; set; }
    public int UsageCount { get; set; }
    public DateTime? LastUsedDate { get; set; }
  }

}