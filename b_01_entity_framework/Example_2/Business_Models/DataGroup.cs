using System;
using System.Collections.Generic;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class DataGroup {
    public List<DataRecord> EligibleRecords { get; set; }
    public void Initialize(List<DataRecord> records)
    {
        records.ForEach(i => i.EligibleGroups.Add(this));
        EligibleRecords = records;
    }
  }
}