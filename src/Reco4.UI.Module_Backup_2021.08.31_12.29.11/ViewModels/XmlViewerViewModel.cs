using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reco4.UI.Module.ViewModels {
	public class XmlViewerViewModel : ViewModelBase {
		private IEventAggregator _eventAggregator;

		public XmlViewerViewModel(IEventAggregator eventAggregator) {
			_eventAggregator = eventAggregator;


		}
	}
}

