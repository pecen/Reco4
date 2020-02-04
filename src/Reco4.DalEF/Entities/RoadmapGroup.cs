﻿using Reco4.Dal.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public ValidationStatus Validation_Status { get; set; }
    public ConvertToVehicleInputStatus ConvertToVehicleInput_Status { get; set; }
  }
}