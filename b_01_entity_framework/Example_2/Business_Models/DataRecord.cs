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



// public async Task<OrderDetailSurplusCollection> GetAsync(params DataGenerationDataRetrievalModel[] models)
//         {
//             DataRecordCollection dataRecordCollection = new DataRecordCollection();

//             OrderDetailSurplusCollection orderDetailSurplusCollection = models.Select(i => new OrderDetailSurplus(i)).ToList();

//             using (StopWatchWithLogger.NewLogger("AppendEligibleRecordsToOrderDetailSurplusAsync"))
//                 foreach (OrderDetailSurplus orderDetailSurplus in orderDetailSurplusCollection)
//                 {
//                     await AppendEligibleRecordsToOrderDetailSurplusAsync(orderDetailSurplus, dataRecordCollection);
//                 }

//             PopulateOrderDetailSurpluses(orderDetailSurplusCollection, dataRecordCollection);

//             await ApplyIndividualToleranceIfNeededAsync(orderDetailSurplusCollection, dataRecordCollection);
//             await ApplyToleranceForAlignedJobsIfNeededAsync(orderDetailSurplusCollection, dataRecordCollection);

//             return orderDetailSurplusCollection;
//         }



//  private async Task AppendEligibleRecordsToOrderDetailSurplusAsync(OrderDetailSurplus orderDetailSuplus, DataRecordCollection dataRecordCollection)
//         {
//             List<DataRecord> retrievedDataRecords = (await _unusedDataRepository.GetAsync(orderDetailSuplus.DataRetrievalModel))
//                 .Select(i => new DataRecord(i))
//                 .Select(dataRecordCollection.AddToCollection)
//                 .ToList();

//             orderDetailSuplus.Initialize(retrievedDataRecords);
//         }



// public void Initialize(List<DataRecord> eligibleRecords)
//         {
//             EligibleRecords?.ForEach(i => i.EligibleOrderDetails.Remove(this));

//             eligibleRecords.ForEach(i => i.EligibleOrderDetails.Add(this));
//             EligibleRecords = eligibleRecords;

//             Surplus = eligibleRecords.Count - DataRetrievalModel.ItemCount;
//             InitialSurplus = Surplus;

//             ClearRecords();
//             RefreshLeftoverRecords();
//         }


// private void PopulateOrderDetailSurpluses(OrderDetailSurplusCollection orderDetailSurplusCollection, DataRecordCollection dataRecordCollection)
//         {
//             using (StopWatchWithLogger.NewLogger("PopulateOrderDetailSurpluses"))
//             {
//                 List<DataRecord> dataRecords = dataRecordCollection
//                     .GetAllRecords()
//                     .OrderBy(i => i.EligibleOrderDetails.Count)
//                     .ThenByPriority()
//                     .ToList();

//                 Log.Information($"total available records = {dataRecords.Count}.");

//                 foreach (DataRecord dataRecord in dataRecords)
//                 {
//                     if (orderDetailSurplusCollection.IsFullyPopulated())
//                         break;

//                     OrderDetailSurplus orderDetail = dataRecord
//                         .EligibleOrderDetails
//                         .Where(i => !i.IsFullyPopulated)
//                         .OrderBy(i => i.Surplus)
//                         .ThenBy(i => i.DataRetrievalModel.OrderDetailId)
//                         .FirstOrDefault();

//                     orderDetail?.AddRecord(dataRecord);
//                 }

//                 using (StopWatchWithLogger.NewLogger("ApplyRecursiveRedistribution"))
//                     orderDetailSurplusCollection.ApplyRecursiveRedistribution();
//             }
//         }


// public int Surplus { get; private set; }

// public List<DataRecord> UsedRecords { get; private set; } = new List<DataRecord>();

// public void AddRecord(DataRecord record, bool updateSurplus = true)
//         {
//             AverageUsageCount = !UsedRecords.Any()
//                 ? record.Data.UsageCount
//                 : (AverageUsageCount * UsedRecords.Count + record.Data.UsageCount) / (UsedRecords.Count + 1);

//             AverageLastUsedDateDaysSince2010 = !UsedRecords.Any()
//                 ? record.Data.LastUsedDateDaysSince2010
//                 : (AverageLastUsedDateDaysSince2010 * UsedRecords.Count + record.Data.LastUsedDateDaysSince2010) / (UsedRecords.Count + 1);

//             record.Owner?.RemoveRecord(record, updateSurplus);

//             record.SetOwner(this);
//             UsedRecords.Add(record);

//             if (updateSurplus)
//             {
//                 record.EligibleOrderDetails.Where(i => i != this).ForEachElement(i => i.Surplus--);
//             }
//         }