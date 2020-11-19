using System;
using System.Collections.Generic;
using System.Linq;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class RecordItem {
    public int CorelogicDataID { get; set; }
    public string Property_State { get; set; }
    public int UsageCount { get; set; }
    public DateTime? LastUsedDate { get; set; }
  }

  public class DataRecord {
    public RecordItem Item { get; }
    public DataGroup Owner { get; private set; }
    public List<DataGroup> EligibleGroups = new List<DataGroup>();

    public DataRecord(RecordItem item) { Item = item; }
    public void SetOwner(DataGroup owner) { Owner = owner; }
  }

  public class DataRecordCollection {
    private readonly Dictionary<int, DataRecord> records = new Dictionary<int, DataRecord>();

    public List<DataRecord> GetAllRecords()
    {
        return new List<DataRecord>(records.Select(i => i.Value));
    }
    public DataRecord Add(DataRecord rd)
    {
      var id = rd.Item.CorelogicDataID;

      if (records.ContainsKey(id)) {
        return records[id];
      }

      records.Add(id, rd);
      return rd;
    }
  }
}
