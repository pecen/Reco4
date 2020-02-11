using System;

namespace Reco4.Dal.Dto {
  public class RoadmapGroupDto {
    public int RoadmapGroupId { get; set; }
    public string OwnerSss { get; set; }
    public string RoadmapName { get; set; }
    public bool Protected { get; set; }
    public DateTime CreationTime { get; set; }
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string Xml { get; set; }
    public int ValidationStatusValue { get; set; }
    public int ConvertToVehicleInputStatusValue { get; set; }
  }
}
