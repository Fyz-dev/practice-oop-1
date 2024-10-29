using OOP_Lab.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OOP_Lab.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private int _applicationsLimit;

        public int ApplicationsLimit
        {
            get => _applicationsLimit;
            set
            {
                if (_applicationsLimit == value)
                    return;

                _applicationsLimit = value > 0 ? value : -1;
                DataService.ApplicationsLimit = _applicationsLimit;
                OnPropertyChanged();
            }
        }

        public int CurrentApplicationsCount => DataService.ApplicationModels.Count == 0 ? 1 : DataService.ApplicationModels.Count;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            ApplicationsLimit = DataService.ApplicationsLimit;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
