using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Tasks.WpfUi.Helpers;

public class ColorConverterMore : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var stringValue = value as string;

        //Color color = 

        //ColorConverter colodd = new();


        var result = ColorConverter.ConvertFromString(stringValue) as Color?;

        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var color = (Color)value;
        var result = color.ToHexString();
        return result;
    }
}
