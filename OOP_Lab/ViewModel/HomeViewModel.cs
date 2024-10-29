using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using OOP_Lab.Helpers;
using OOP_Lab.Service;
using System;
using OOP_Lab.Entities;

namespace OOP_Lab.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public ApplicationStatistics Statistics { get; } = new ApplicationStatistics();

        public ObservableCollection<ApplicationEntitie> SearchResults { get; } =
           new ObservableCollection<ApplicationEntitie>();

        // Search and filter
        private readonly DelayedAction _delayedSearch;

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                    _delayedSearch.Restart();
                }
            }
        }

        private string _selectedSearchCriterion = "All";
        public string SelectedSearchCriterion
        {
            get => _selectedSearchCriterion;
            set
            {
                if (_selectedSearchCriterion == value )
                    return;

                _selectedSearchCriterion = value;
                OnPropertyChanged();

                if(!string.IsNullOrWhiteSpace(SearchQuery))
                    _delayedSearch.Restart();
            }
        }

        // Delete
        private ApplicationEntitie _selectedItem;

        public ApplicationEntitie SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand DownloadCommand { get; }
        public ICommand UninstallCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand DeleteAllCommand { get; }
        public ICommand UpdateView { get; }

        public HomeViewModel()
        {
            _delayedSearch = new DelayedAction(TimeSpan.FromMilliseconds(500), PerformSearch);

            UninstallCommand = new RelayCommand<ApplicationEntitie>(OnUninstallApplication);
            DownloadCommand = new RelayCommand<ApplicationEntitie>(OnDownloadApplication);
            UpdateCommand = new RelayCommand<ApplicationEntitie>(OnUpdateApplication);
            DeleteCommand = new RelayCommand(OnDeleteApplication);
            DeleteAllCommand = new RelayCommand(OnDeleteAllApplications);
            UpdateView = new RelayCommand(PerformSearch);

            PerformSearch();
        }

        private void PerformSearch()
        {
            SearchResults.Clear();

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                foreach (var item in DataService.ApplicationModels)
                    SearchResults.Add(item);

                return;
            }

            var query = SearchQuery.ToLower();

            Predicate<ApplicationEntitie> nameContains = app => app.Name.ToLower().Contains(query);
            Predicate<ApplicationEntitie> descriptionContains = app => app.Description.ToLower().Contains(query);
            Predicate<ApplicationEntitie> categoryContains = app => app.Category.ToString().ToLower().Contains(query);

            Predicate<ApplicationEntitie> searchPredicate = app =>
                nameContains(app) || descriptionContains(app) || categoryContains(app);

            switch (SelectedSearchCriterion)
            {
                case "Name":
                    searchPredicate = nameContains;
                    break;
                case "Description":
                    searchPredicate = descriptionContains;
                    break;
                case "Category":
                    searchPredicate = categoryContains;
                    break;
            }

            foreach (var item in DataService.ApplicationModels.FindAll(searchPredicate))
                SearchResults.Add(item);
        }


        private void OnUninstallApplication(ApplicationEntitie parameter)
        {
            parameter.Uninstall();
        }

        private void OnDownloadApplication(ApplicationEntitie parameter)
        {
            parameter.Download();
        }

        private void OnUpdateApplication(ApplicationEntitie parameter)
        {
            parameter.Update();
        }

        private void OnDeleteApplication()
        {
            if (SelectedItem == null)
                return;

            SelectedItem.Delete();

            DataService.ApplicationModels.Remove(SelectedItem);
            SearchResults.Remove(SelectedItem);

            SelectedItem = null;
        }

        private void OnDeleteAllApplications()
        {
            var itemsToRemove = SearchResults.ToList();
            SearchResults.Clear();

            foreach (var item in itemsToRemove)
            {
                item.Uninstall();
                DataService.ApplicationModels.Remove(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
