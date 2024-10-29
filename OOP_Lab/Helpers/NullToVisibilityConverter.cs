using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using System;

namespace OOP_Lab.Helpers
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isNull = value == null;

            bool invert = parameter != null && parameter.ToString() == "true";
            isNull = invert ? !isNull : isNull;

            return isNull ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
            {
                bool invert = parameter != null && parameter.ToString() == "true";

                bool isVisible = visibility == Visibility.Visible;

                isVisible = invert ? !isVisible : isVisible;

                return isVisible ? new object() : null;
            }

            throw new ArgumentException("Invalid visibility value");
        }
    }
}
