using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.DalEF.Entities {
  public class Roadmap {
    public int RoadmapId { get; set; }
    public string RoadmapName { get; set; }
    public bool Protected { get; set; }
    public int Validation_Status { get; set; }
    public int ConvertToVehicleInput_Status { get; set; }
    public int RoadmapGroupId { get; set; }
    public int CurrentYear { get; set; }
    public int ImprovedVehicleCount { get; set; }
  }
}
