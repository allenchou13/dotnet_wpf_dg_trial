using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace dg_trial
{
    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = parameter as string;
            if (value.GetType() == typeof(DateTime))
            {
                var time = (DateTime)value;
                if (time == DateTime.MinValue)
                {
                    return null;
                }
                else
                {
                    if (param == "b")
                    {
                        return time.ToString("yyyy-MM-dd\nHH:mm:ss");
                    }
                    else
                    {
                        return time.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            else if (value.GetType() == typeof(DateTimeOffset))
            {
                var time = (DateTimeOffset)value;
                if (time == DateTimeOffset.MinValue)
                {
                    return null;
                }
                else
                {
                    if (param == "b")
                    {
                        return time.ToString("yyyy-MM-dd\nHH:mm:ss");
                    }
                    else
                    {
                        return time.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
