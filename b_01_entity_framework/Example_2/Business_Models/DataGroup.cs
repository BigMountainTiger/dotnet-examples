using System.Collections.Generic;
using System.Linq;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class DataGroup {
    public int ItemCount { get; set; }
    public int InitialSurplus { get; private set; }
    public int Surplus { get; private set; }
    public List<DataRecord> EligibleRecords { get; set; }
    public List<DataRecord> UsedRecords { get; private set; } = new List<DataRecord>();
    public bool IsFullfilled => ItemCount - UsedRecords.Count == 0;

    public DataGroup(int itemCount) {
      ItemCount = itemCount;
    }
    
    public void Initialize(List<DataRecord> records)
    {
        records.ForEach(i => i.EligibleGroups.Add(this));
        EligibleRecords = records;

        Surplus = records.Count - ItemCount;
        InitialSurplus = Surplus;

    }

    public void AddRecord(DataRecord record) {
      
      record.SetOwner(this);
      UsedRecords.Add(record);

      var groups = record.EligibleGroups.Where(i => i != this).ToList();
      foreach(var g in groups) { g.Surplus --; }
    }
  }
}