﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Tasks.WpfUi.Helpers;

/// <summary>
/// Convert a color to string and vice versa
/// </summary>
public class ColorConverterMore : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var stringValue = value as string;
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
