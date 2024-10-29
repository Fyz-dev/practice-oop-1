using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OOP_Lab.Entities;
using OOP_Lab.Enums;
using OOP_Lab.Helpers;
using OOP_Lab.Service;

namespace OOP_Lab.ViewModel
{
    public class AddDialogViewModel : INotifyPropertyChanged
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

        public string ParseInput { get; set; }

        public AddDialogViewModel()
        {
            if (DataService.isLimitReached())
                IsLimitReached = true;

            Version = 1;
            Rating = 1;
            SelectedCategoryIndex = (int)ApplicationCategory.Other;
        }

        public bool AddApplication()
        {
            try
            {
                switch (SelectedConstructorIndex)
                {
                    case 0:
                        DataService.ApplicationModels.Add(
                            new ApplicationEntitie(
                                Name,
                                Description,
                                (ApplicationCategory)SelectedCategoryIndex,
                                Rating,
                                Version
                            )
                        );
                        break;

                    case 1:
                        DataService.ApplicationModels.Add(
                            new ApplicationEntitie(Name, Description)
                        );
                        break;

                    case 2:
                        DataService.ApplicationModels.Add(
                            new ApplicationEntitie(
                                Name,
                                Description,
                                (ApplicationCategory)SelectedCategoryIndex
                            )
                        );
                        break;

                    case 3:
                        DataService.ApplicationModels.Add(
                            new ApplicationEntitie { Name = Name, Description = Description }
                        );
                        break;

                    case 4:
                        DataService.ApplicationModels.Add(ApplicationEntitie.Parse(ParseInput));

                        break;

                    default:
                        break;
                }

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

//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Text.RegularExpressions;
//using System.Windows.Input;
//using CommunityToolkit.Mvvm.Input;
//using Microsoft.UI.Xaml.Controls;
//using OOP_Lab.Enums;
//using OOP_Lab.Helpers;
//using OOP_Lab.Service;

//namespace OOP_Lab.ViewModel
//{
//    public class ValidationProperty<T> : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        public bool HasError => !string.IsNullOrEmpty(Error);

//        private T _value;
//        private string _error;

//        public T Value
//        {
//            get => _value;
//            set
//            {
//                if (!Equals(_value, value))
//                {
//                    _value = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        public string Error
//        {
//            get => _error;
//            set
//            {
//                if (_error != value)
//                {
//                    _error = value;
//                    OnPropertyChanged();
//                    OnPropertyChanged(nameof(HasError));
//                }
//            }
//        }

//        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }

//    public class AddDialogViewModel : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        public List<string> Categories { get; set; } =
//            new List<string>(EnumHelper.EnumToStringArray<ApplicationCategory>());

//        public ValidationProperty<string> Name { get; } = new ValidationProperty<string>();
//        public ValidationProperty<string> Description { get; } = new ValidationProperty<string>();
//        public ValidationProperty<double> Rating { get; } = new ValidationProperty<double>();
//        public ValidationProperty<int> Version { get; } = new ValidationProperty<int>();

//        private int _selectedCategoryIndex;
//        public int SelectedCategoryIndex
//        {
//            get => _selectedCategoryIndex;
//            set
//            {
//                if (_selectedCategoryIndex != value)
//                {
//                    _selectedCategoryIndex = value;
//                    OnPropertyChanged();
//                }
//            }
//        }

//        public bool IsLimitReached { get; set; }

//        public AddDialogViewModel()
//        {
//            if (DataService.isLimitReached())
//                IsLimitReached = true;

//            Version.Value = 1;

//            Name.PropertyChanged += (s, e) => ValidateName();
//            Description.PropertyChanged += (s, e) => ValidateDescription();
//            Version.PropertyChanged += (s, e) => ValidateVersion();
//        }

//        public bool HasErrors()
//        {
//            ValidateName();
//            ValidateDescription();
//            ValidateVersion();

//            return Name.HasError || Description.HasError || Version.HasError || IsLimitReached;
//        }

//        private void ValidateName() => Name.Error = Validator.ValidateName(Name.Value);

//        private void ValidateDescription() =>
//            Description.Error = Validator.ValidateDescription(Description.Value);

//        private void ValidateVersion() =>
//            Version.Error = Validator.ValidateVersion(Version.Value.ToString());

//        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
