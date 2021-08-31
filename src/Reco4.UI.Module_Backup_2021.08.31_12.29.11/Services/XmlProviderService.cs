using Reco4.UI.Module.Controls;
using Reco4.UI.Module.Views;
using System.IO;
using System.Windows.Forms.Integration;

namespace Reco4.UI.Module.Services {
	public class XmlProviderService : IXmlProviderService {
		public void XmlViewService(string filePath) {
			// Create the interop host control.
			var host = new WindowsFormsHost();

			// Create the Windows Forms UserControl
			var xmlGridView = new XmlGridView {
				DataFilePath = filePath
			};

			// Assign the XmlGridView control as the host control's child.
			host.Child = xmlGridView;

			// Create the Wpf View
			var window = new XmlViewer();

			// Add the interop host control to the Grid
			// control's collection of child controls.
			window.xmlGrid.Children.Add(host);

			// Show the window
			window.Show();
		}

		public void XmlViewService(Stream xml) {
			// Create the interop host control.
			var host = new WindowsFormsHost();

			// Create the Windows Forms UserControl
			var xmlGridView = new XmlGridView {
				Xml = xml
			};

			// Assign the XmlGridView control as the host control's child.
			host.Child = xmlGridView;

			// Create the Wpf View
			var window = new XmlViewer();

			// Add the interop host control to the Grid
			// control's collection of child controls.
			window.xmlGrid.Children.Add(host);

			// Show the window
			window.Show();
		}

		public string GetXmlStringService(Stream xml) {
			using (StreamReader reader = new StreamReader(xml)) {
				return reader.ReadToEnd();
			}
		}
	}
}
