using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using System;

namespace OOP_Lab.Helpers
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool boolValue))
                return Visibility.Collapsed;


            bool invert = parameter != null && parameter.ToString() == "true";
            boolValue = invert ? !boolValue : boolValue;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is Visibility visibility))
                return false;

            bool result = visibility == Visibility.Visible;

            bool invert = parameter != null && parameter.ToString() == "true";
            return invert ? !result : result;
        }
    }
}
