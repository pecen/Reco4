﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Dal {
  public interface IComponentDal {
    bool Exists(string pdNumber);
  }
}
