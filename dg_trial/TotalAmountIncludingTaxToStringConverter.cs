using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace dg_trial
{
    /// <summary>
    /// 价税合计转字符串
    /// </summary>
    public class TotalAmountIncludingTaxToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (double)value;
            if (val == 0)
            {
                return "";
            }
            else
            {
                return val.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
