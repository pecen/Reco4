using Prism.Mvvm;

namespace Reco4.Common.Models {
	public class ModelBase : BindableBase {
		private bool _isChecked;
		public bool IsChecked {
			get => _isChecked;
			set {
				SetProperty(ref _isChecked, value);
			}
		}
	}
}
