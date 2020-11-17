using System;

namespace b_01_entity_framework.Example_2.Business_Models {

  public class RecordItem {
    public int CorelogicDataID { get; set; }
    public string Property_State { get; set; }
    public int UsageCount { get; set; }
    public DateTime? LastUsedDate { get; set; }
  }

  public class DataRecord {
    public RecordItem Item { get; }

    public DataRecord(RecordItem item) {
      Item = item;
    }
  }
}

// public sealed class DataRecord
//     {
//         public UnusedDataDto Data { get; }
//         public List<OrderDetailSurplus> EligibleOrderDetails { get; } = new List<OrderDetailSurplus>();
//         public List<OrderDetailSurplus> EligibleNonOwnerOrderDetails { get; private set; } = new List<OrderDetailSurplus>();
//         public OrderDetailSurplus Owner { get; private set; }

//         public DataRecord(UnusedDataDto data)
//         {
//             Data = data;
//         }

//         public void SetOwner(OrderDetailSurplus owner)
//         {
//             Owner = owner;
//             EligibleNonOwnerOrderDetails = EligibleOrderDetails.Except(new[] { owner }).ToList();
//         }
//     }

// -----

// public sealed class DataRecordCollection
//     {
//         private readonly Dictionary<int, DataRecord> _records = new Dictionary<int, DataRecord>();

//         public List<DataRecord> GetAllRecords()
//         {
//             return new List<DataRecord>(_records.Select(i => i.Value));
//         }

//         public DataRecord AddToCollection(DataRecord record)
//         {
//             if (_records.ContainsKey(record.Data.CorelogicDataId))
//                 return _records[record.Data.CorelogicDataId];

//             _records.Add(record.Data.CorelogicDataId, record);

//             return record;
//         }
//     }