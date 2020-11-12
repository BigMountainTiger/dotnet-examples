using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace b_01_entity_framework.Example_2 {
  public class UnusedData {

    [Key]
    public int CorelogicDataID { get; set; }

    [Column("Owner 1 First Name")]
    public string Owner_1_First_Name { get; set; }

    [Column("Owner 1 Last Name")]
    public string Owner_1_Last_Name { get; set; }

    [Column("Owner 2 First Name")]
    public string Owner_2_First_Name { get; set; }

    [Column("Owner 2 Last Name")]
    public string Owner_2_Last_Name { get; set; }

    [Column("Property Address Full")]
    public string Property_Address_Full { get; set; }

    [Column("Property City")]
    public string Property_City { get; set; }

    [Column("Property State")]
    public string Property_State { get; set; }

    [Column("Property Zip Code")]
    public string Property_Zip_Code { get; set; }
  }
}