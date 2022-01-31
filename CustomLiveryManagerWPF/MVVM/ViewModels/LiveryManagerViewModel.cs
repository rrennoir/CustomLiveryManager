using Caliburn.Micro;
using CustomLiveryManagerShared;
using CustomLiveryManagerWPF.Helper;
using CustomLiveryManagerWPF.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLiveryManagerWPF.MVVM.ViewModels
{
    public class LiveryManagerViewModel : Screen
    {
        private BindableCollection<LiveryModel> _liveries;
        private BindableCollection<LiveryModel> _managedLiveries;

        public RelayCommand AddToManagerCmd { get; init; }
        public RelayCommand RemoveFromManagerCmd { get; init; }
        public RelayCommand SelectAllLiveriesCmd { get; init; }
        public RelayCommand SelectAllManagedLiveriesCmd { get; init; }

        public RelayCommand SaveManagedListCmd { get; init; }
        public RelayCommand DeleteEmptyLiveriesCmd { get; init; }
        public RelayCommand DeleteLiveriesCmd { get; init; }
        public RelayCommand ReloadLiveriesCmd { get; init; }

        private LiveryManager _liveryManager;

        public LiveryManagerViewModel()
        {
            AddToManagerCmd = new RelayCommand(AddToManager);
            RemoveFromManagerCmd = new RelayCommand(RemoveFromMananger);
            SelectAllLiveriesCmd = new RelayCommand(SelectAllLiveries);
            SelectAllManagedLiveriesCmd = new RelayCommand(SelectAllManagedLiveries);
            SaveManagedListCmd = new RelayCommand(SaveManagedList);
            DeleteEmptyLiveriesCmd = new RelayCommand(DeleteEmptyLiveries);
            DeleteLiveriesCmd = new RelayCommand(DeleteLiveries);
            ReloadLiveriesCmd = new RelayCommand(ReloadLiveries);

            _liveryManager = new();

            _managedLiveries = new();
            foreach(var livery in _liveryManager.ManagedLiveries)
            {
                _managedLiveries.Add(new LiveryModel(livery));
            }

            _liveries = new();
            foreach (var livery in _liveryManager.Liveries)
            {
                if (!_liveryManager.ManagedLiveries.Contains(livery))
                {
                    _liveries.Add(new LiveryModel(livery));
                }
            }
        }

        private void DeleteLiveries(object obj)
        {
            _liveryManager.DeleteUnamangedLiveries(false);
            ReloadLiveries(null);
        }

        private void ReloadLiveries(object? _)
        {
            _liveryManager.LoadLiveries();
            _liveries.Clear();
            foreach (var livery in _liveryManager.Liveries)
            {
                if (!_liveryManager.ManagedLiveries.Contains(livery))
                {
                    _liveries.Add(new LiveryModel(livery));
                }
            }
        }

        private void DeleteEmptyLiveries(object obj)
        {
            _liveryManager.DeleteUnamangedLiveries(true);
            ReloadLiveries(null);
        }

        private void SaveManagedList(object obj)
        {
            _liveryManager.SaveManagedList();
        }

        private void SelectAllManagedLiveries(object obj)
        {
            foreach(var managedLivery in _managedLiveries)
            {
                managedLivery.IsSelected = !managedLivery.IsSelected;
            }
        }

        private void SelectAllLiveries(object obj)
        {
            foreach(var livery in _liveries)
            {
                livery.IsSelected = !livery.IsSelected;
            }
        }

        private void AddToManager(object obj)
        {
            for (var i = _liveries.Count - 1; i >= 0; i--)
            {
                if (_liveries[i].IsSelected)
                {
                    _liveries[i].IsSelected = false;
                    _liveryManager.AddLivery(_liveries[i].Name);
                    _managedLiveries.Add(_liveries[i]);
                    _liveries.RemoveAt(i);
                }
            }
        }

        private void RemoveFromMananger(object obj)
        {
            for (var i = _managedLiveries.Count - 1; i >= 0; i--)
            {
                if (_managedLiveries[i].IsSelected)
                {
                    _managedLiveries[i].IsSelected = false;
                    _liveryManager.RemoveLivery(_managedLiveries[i].Name);
                    _liveries.Add(_managedLiveries[i]);
                    _managedLiveries.RemoveAt(i);
                }
            }
        }

        public BindableCollection<LiveryModel> Liveries
        {
            get => _liveries;
            set => _liveries = value;
        }

        public BindableCollection<LiveryModel> ManagedLiveries
        {
            get => _managedLiveries;
            set => _managedLiveries = value;
        }
    }
}
