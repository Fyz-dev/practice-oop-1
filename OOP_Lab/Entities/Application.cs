using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OOP_Lab.Enums;
using OOP_Lab.Helpers;

namespace OOP_Lab.Entities
{
    public class ApplicationEntitie : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private double _rating;
        private DateTime _lastUpdate;
        private int _downloadCount;

        private static int _applicationCount;
        private static double _totalRating;

        public static int ApplicationCount
        {
            get => _applicationCount;
            set
            {
                _applicationCount = value;
                OnStaticPropertyChanged();
            }
        }
        public static double TotalRating
        {
            get => _totalRating;
            set
            {
                _totalRating = value;
                OnStaticPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                string validationError = Validator.ValidateName(value);
                if (validationError != null)
                    throw new ArgumentException(validationError);

                _name = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                string validationError = Validator.ValidateDescription(value);
                if (validationError != null)
                    throw new ArgumentException(validationError);

                _description = value;
                OnPropertyChanged();
            }
        }
        public double Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                    TotalRating += Math.Max(value, 0) - Math.Max(_rating, 0);

                _rating = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PopularityScore));
            }
        }
        public DateTime LastUpdate
        {
            get => _lastUpdate;
            set
            {
                _lastUpdate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PopularityScore));
            }
        }
        public int DownloadCount
        {
            get => _downloadCount;
            set
            {
                _downloadCount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PopularityScore));
            }
        }

        public ApplicationCategory Category { get; set; }
        public bool IsInstalled { get; private set; } = false;
        public int Version { get; private set; }

        public double PopularityScore
        {
            get
            {
                double daysSinceLastUpdate = (DateTime.Now - LastUpdate).TotalDays;
                double freshnessFactor = daysSinceLastUpdate > 30 ? 0.5 : 1.0;
                double downloadFactor = Math.Log(DownloadCount + 1);
                double ratingFactor = Math.Max(Rating, 0) / 5.0;

                return Math.Round(freshnessFactor * (downloadFactor * 0.7 + ratingFactor * 0.3), 1);
            }
        }

        public ApplicationEntitie(
            string name,
            string description,
            ApplicationCategory category,
            double rating,
            int version
        )
        {
            Name = name;
            Description = description;
            Category = category;
            Version = version;

            ApplicationCount++;

            Rating = rating == 0 ? -1 : rating;
            LastUpdate = DateTime.Now;
        }

        public ApplicationEntitie()
            : this("Unknow", "Unknow", ApplicationCategory.Other, 1, 1) { }

        public ApplicationEntitie(string name, string description)
            : this(name, description, ApplicationCategory.Other, 1, 1) { }

        public ApplicationEntitie(string name, string description, ApplicationCategory category)
            : this(name, description, category, 1, 1) { }

        public void Update()
        {
            if (!IsInstalled)
                return;

            Version++;
            LastUpdate = DateTime.Now;

            OnPropertyChanged(nameof(LastUpdate));
        }

        public void Update(int version)
        {
            if (version < Version)
                throw new InvalidOperationException(
                    "It is not possible to install an older version."
                );

            Version = version;
            LastUpdate = DateTime.Now;

            OnPropertyChanged(nameof(LastUpdate));
        }

        public void Download()
        {
            if (IsInstalled)
                return;

            DownloadCount++;
            IsInstalled = true;

            OnPropertyChanged(nameof(IsInstalled));
        }

        public void Uninstall()
        {
            if (!IsInstalled)
                return;

            IsInstalled = false;

            OnPropertyChanged(nameof(IsInstalled));
        }

        public void Delete()
        {
            ApplicationCount--;

            TotalRating -= Math.Max(Rating, 0);

            OnPropertyChanged(nameof(ApplicationCount));
            OnPropertyChanged(nameof(TotalRating));
        }

        public static double AverageRating()
        {
            if (ApplicationCount == 0)
                return 0;

            return (int)(_totalRating / ApplicationCount * 10) / 10.0;
        }

        public static ApplicationEntitie Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("Input string cannot be null or empty.");

            string[] parts = s.Split(';');

            if (parts.Length != 5)
                throw new FormatException("Input string must have exactly 5 parts.");

            string name = parts[0];
            string description = parts[1];
            string categoryString = parts[2];
            string ratingString = parts[3];
            string versionString = parts[4];

            if (!Enum.TryParse<ApplicationCategory>(categoryString, out ApplicationCategory category))
                throw new ArgumentException($"Invalid category: {categoryString}");

            if (!double.TryParse(ratingString, out double rating) || rating < 0 || rating > 5)
                throw new ArgumentException($"Invalid rating: {ratingString}.");

            string versionValidationError = Validator.ValidateVersion(versionString);
            if (versionValidationError != null)
                throw new ArgumentException(versionValidationError);

            return new ApplicationEntitie(name, description, category, rating, int.Parse(versionString));
        }

        public static bool TryParse(string s, out ApplicationEntitie obj)
        {
            try
            {
                obj = Parse(s);
                return true;
            }
            catch
            {
                obj = null;
                return false;
            }
        }

        public override string ToString()
        {
            return $"{Name};{Description};{Category};{Rating};{Version}";
        }


        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnStaticPropertyChanged([CallerMemberName] string propertyName = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
