using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Reco4.UI.Module.Converters {
  public class XmlToFixedLengthConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			var xml = value as string;

	  if (!string.IsNullOrEmpty(xml)) {
				return xml.Substring(0, 40) + "...";
	  }
	  else {
				return string.Empty;
	  }
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
	  throw new NotImplementedException();
	}
  }
}
