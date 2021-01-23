using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    class ValueProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = Application.Current.Properties["value"].ToString();
            int i = Int32.Parse(val);
            return (double)value / i;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = Application.Current.Properties["value"].ToString();
            int i = Int32.Parse(val);
            return (double)value * i;
        }
    }
}
