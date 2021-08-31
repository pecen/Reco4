using System.IO;

namespace Reco4.UI.Module.Services {
	public interface IXmlProviderService {
		void XmlViewService(string filePath);
		void XmlViewService(Stream xml);
		string GetXmlStringService(Stream xml);
	}
}
