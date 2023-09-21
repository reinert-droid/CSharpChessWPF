using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1 
{
    public class HeightToWidth : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Calculate the Width based on the Height
            if (value is double height)
            {
                return height; // Assuming a 1:1 aspect ratio
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

