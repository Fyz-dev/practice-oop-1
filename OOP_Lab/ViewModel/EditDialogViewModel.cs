using OOP_Lab.Entities;
using OOP_Lab.Enums;
using OOP_Lab.Helpers;
using OOP_Lab.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OOP_Lab.ViewModel
{
    internal class EditDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> Categories { get; set; } =
            new List<string>(EnumHelper.EnumToStringArray<ApplicationCategory>());

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private double _rating;
        public double Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }

        private int _version;
        public int Version
        {
            get => _version;
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged(nameof(Version));
                }
            }
        }

        private int _selectedCategoryIndex;
        public int SelectedCategoryIndex
        {
            get => _selectedCategoryIndex;
            set
            {
                if (_selectedCategoryIndex != value)
                {
                    _selectedCategoryIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                if (_error != value)
                {
                    _error = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLimitReached { get; set; }

        public int SelectedConstructorIndex { get; set; }

        private ApplicationEntitie _application;

        public EditDialogViewModel(ApplicationEntitie application)
        {
            if (DataService.isLimitReached())
                IsLimitReached = true;

            _application = application;

            Name = application.Name;
            Description = application.Description;
            SelectedCategoryIndex = (int)application.Category;
            Rating = application.Rating;
            Version = application.Version;
        }

        public bool UpdateApplication()
        {
            try
            {
                _application.Name = Name;
                _application.Description = Description;
                _application.Category = (ApplicationCategory)SelectedCategoryIndex;
                _application.Rating = Rating;
                _application.Update(Version);

                return true;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

