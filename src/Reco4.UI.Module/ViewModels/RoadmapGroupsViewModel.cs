using Prism.Commands;
using Prism.Events;
using Reco4.Common.Extensions;
using Reco4.Common.Models;
using Reco4.Library;
using Reco4.UI.Module.Commands;
using Reco4.UI.Module.Enums;
using Reco4.UI.Module.Models;
using Reco4.UI.Module.Services;
using Reco4.Utilities.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Reco4.UI.Module.ViewModels {
  public class RoadmapGroupsViewModel : ViewModelBase {
    private readonly EventAggregator _eventAggregator;
        private readonly IFilteredListService _filteredListService;

    public DelegateCommand DeleteRoadmapGroupsCommand { get; set; }
    public DelegateCommand CopyGridRowCommand { get; set; }

    #region Properties

    //private bool _isChecked;
    //public bool IsChecked {
    //  get => _isChecked;
    //  set {
    //    //if (value == _isChecked) return;
    //    //_isChecked = value;
    //    //OnPropertyChanged(nameof(IsChecked));
    //    SetProperty(ref _isChecked, value);
    //  }
    //}

    private bool _hasCheckedItem;
    private bool HasCheckedItem {
      get => _hasCheckedItem;
      set { SetProperty(ref _hasCheckedItem, value); }
    }

    private bool? _allSelected;
    public bool? AllSelected {
      get => _allSelected;
      set {
        SetProperty(ref _allSelected, value);

        // Set all other CheckBoxes
        AllSelectedChanged();
      }
    }

    //private RoadmapGroupList _roadmapGroups;
    //public RoadmapGroupList RoadmapGroups {
    //  get { return _roadmapGroups; }
    //  set { SetProperty(ref _roadmapGroups, value); }
    //}

    private ObservableCollection<RoadmapGroup> _roadmapGroups;
    public ObservableCollection<RoadmapGroup> RoadmapGroups {
      get { return _roadmapGroups; }
      set { SetProperty(ref _roadmapGroups, value); }
    }

    private ObservableCollection<string> _columns;
    public ObservableCollection<string> Columns {
      get { return _columns; }
      set { SetProperty(ref _columns, value); }
    }

    private int _selectedColumns;
    public int SelectedColumn {
      get { return _selectedColumns; }
      set { SetProperty(ref _selectedColumns, value); }
    }

    private string _searchText;
    public string SearchText {
      get { return _searchText; }
      set { SetProperty(ref _searchText, value); }
    }

    private ObservableCollection<string> _validationStatus;
    public ObservableCollection<string> ValidationStatusList {
      get { return _validationStatus; }
      set { SetProperty(ref _validationStatus, value); }
    }

    private int _selectedValidationStatus;
    public int SelectedValidationStatus {
      get { return _selectedValidationStatus; }
      set { SetProperty(ref _selectedValidationStatus, value); }
    }

    private ObservableCollection<string> _convertToVehicleStatus;
    public ObservableCollection<string> ConvertToVehicleStatusList {
      get { return _convertToVehicleStatus; }
      set { SetProperty(ref _convertToVehicleStatus, value); }
    }

    private int _selectedConvertToVehicleStatus;
    public int SelectedConvertToVehicleStatus {
      get { return _selectedConvertToVehicleStatus; }
      set { SetProperty(ref _selectedConvertToVehicleStatus, value); }
    }

    #endregion

    public RoadmapGroupsViewModel(EventAggregator eventAggregator, IFilteredListService filteredListService) {
      _eventAggregator = eventAggregator;
      _filteredListService = filteredListService;

      Title = Titles.RoadmapGroups.GetDescription();

      Columns = new ObservableCollection<string>();
      ValidationStatusList = new ObservableCollection<string>();
      ConvertToVehicleStatusList = new ObservableCollection<string>();

      Columns.GetEnumValues<FilterableRoadmapGroupColumns>();
      ValidationStatusList.GetEnumValues<ValidationStatus>();
      ConvertToVehicleStatusList.GetEnumValues<ConvertToVehicleStatus>();

      SelectedColumn = 0;
      SelectedValidationStatus = -1;
      SelectedConvertToVehicleStatus = -1;
      SearchText = string.Empty;
      HasCheckedItem = false;

      _allSelected = false;

      DeleteRoadmapGroupsCommand = new DelegateCommand(Execute, CanExecute)
        .ObservesProperty(() => HasCheckedItem);
      CopyGridRowCommand = new DelegateCommand(CopyGridRow);

      _eventAggregator.GetEvent<GetRoadmapGroupsCommand>().Subscribe(RoadmapGroupsReceived);
      _eventAggregator.GetEvent<GetRoadmapGroupsCommand>().Publish(RoadmapGroupList.GetRoadmapGroups());
    }

    private void CopyGridRow() {
      throw new NotImplementedException();
    }

    private void RoadmapGroupsReceived(RoadmapGroupList obj) {
      RoadmapGroups = new ObservableCollection<RoadmapGroup>();

      foreach (var item in obj) {
        item.PropertyChanged += RoadmapGroupOnPropertyChanged;

        RoadmapGroups.Add(new RoadmapGroup {
          IsChecked = false,
          RoadmapGroupInfo = item
        });

        RoadmapGroups.Last().PropertyChanged += RoadmapGroupOnPropertyChanged;
      }

      RaisePropertyChanged(nameof(RoadmapGroups));
    }

    private bool CanExecute() {
      return RoadmapGroups != null
          && RoadmapGroups.Any(c => c.IsChecked);
    }

    private void Execute() {
      var count = RoadmapGroups.Where(c => c.IsChecked).Count();

      if (MessageBox.Show($"Are you sure you want to delete {(count > 1 ? "these Roadmap Groups" : "this Roadmap Group")}?",
        "Delete Component?",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning) == MessageBoxResult.Yes) {

        foreach (var item in RoadmapGroups) {
          if (item.IsChecked) {
            RoadmapGroupEdit.DeleteRoadmapGroup(item.RoadmapGroupInfo.RoadmapGroupId);
          }
        }

        ClearFields();
      }
    }

    #region Row Select Checkbox handling

    private void RoadmapGroupOnPropertyChanged(object sender, PropertyChangedEventArgs args) {
      // Only re-check if the IsChecked property changed
      if (args.PropertyName == nameof(RoadmapGroup.IsChecked)) {
        RecheckAllSelected();
      }
    }

    private void RecheckAllSelected() {
      // Has this change been caused by some other change?
      // return so we don't mess things up
      if (_allSelectedChanging) return;

      try {
        _allSelectedChanging = true;

        if (RoadmapGroups.All(e => e.IsChecked)) {
          AllSelected = true;
          HasCheckedItem = true;
        }
        else if (RoadmapGroups.All(e => !e.IsChecked)) {
          AllSelected = false;
          HasCheckedItem = false;
        }
        else {
          AllSelected = null;
          HasCheckedItem = true;
        }
      }
      finally {
        _allSelectedChanging = false;
      }
    }

    private bool _allSelectedChanging;
    private void AllSelectedChanged() {
      // Has this change been caused by some other change?
      // return so we don't mess things up
      if (_allSelectedChanging) return;

      try {
        _allSelectedChanging = true;

        // this can of course be simplified
        if (AllSelected == true) {
          foreach (var component in RoadmapGroups) {
            component.IsChecked = true;
          }
          HasCheckedItem = true;
        }
        else if (AllSelected == false) {
          foreach (var component in RoadmapGroups) {
            component.IsChecked = false;
          }
          HasCheckedItem = false;
        }
      }
      finally {
        _allSelectedChanging = false;
      }
    }

    #endregion

    private void ClearFields() {
      AllSelected = false;
      //SearchText = string.Empty;
      //SelectedValidationStatus = SelectedConvertToVehicleStatus = -1;
    }
  }
}
