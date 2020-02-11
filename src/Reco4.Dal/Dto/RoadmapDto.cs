namespace Reco4.Dal.Dto {
  public class RoadmapDto {
    public int RoadmapId { get; set; }
    public string RoadmapName { get; set; }
    public bool Protected { get; set; }
    public int ValidationStatus { get; set; }
    public int ConvertToVehicleInputStatus { get; set; }
    public int RoadmapGroupId { get; set; }
    public int CurrentYear { get; set; }
    public int ImprovedVehicleCount { get; set; }
  }
}
