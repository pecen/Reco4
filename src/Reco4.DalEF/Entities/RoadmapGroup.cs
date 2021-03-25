using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reco4.DalEF.Entities {
  public class RoadmapGroup {
    public int RoadmapGroupId { get; set; }
    public string OwnerSss { get; set; }
    public string RoadmapName { get; set; }
    public bool Protected { get; set; }
    public DateTime CreationTime { get; set; }
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string Xml { get; set; }
		[Column("Validation_Status")]
    public int ValidationStatus { get; set; }
		[Column("ConvertToVehicleInput_Status")]
    public int ConvertToVehicleInputStatus { get; set; }
  }
}
