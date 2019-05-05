using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace dg_trial
{
    public class RecognizingStatusToStringConverter : IValueConverter
    {
        public static readonly Dictionary<RecognizingStatus, string> StatusTextMapping = new Dictionary<RecognizingStatus, string>();
        static RecognizingStatusToStringConverter()
        {
            StatusTextMapping.Add(RecognizingStatus.Unknown, "未知状态");
            StatusTextMapping.Add(RecognizingStatus.NotRecognized, "等待识别");
            StatusTextMapping.Add(RecognizingStatus.Recognizing, "正在识别");
            StatusTextMapping.Add(RecognizingStatus.Recognized, "识别成功");
            StatusTextMapping.Add(RecognizingStatus.Failed, "识别失败");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(RecognizingStatus))
            {
                var status = (RecognizingStatus)value;
                if (StatusTextMapping.ContainsKey(status))
                {
                    return StatusTextMapping[status];
                }
                else
                {
                    return "未知状态";
                }
            }
            else
            {
                return "未知状态";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
