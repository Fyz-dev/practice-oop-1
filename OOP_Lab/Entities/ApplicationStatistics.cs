using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OOP_Lab.Entities
{
    public class ApplicationStatistics : INotifyPropertyChanged
    {
        public ApplicationStatistics()
        {
            ApplicationEntitie.StaticPropertyChanged += OnStaticPropertyChanged;
        }

        public int ApplicationCount => ApplicationEntitie.ApplicationCount;
        public double TotalRating => ApplicationEntitie.AverageRating();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnStaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
