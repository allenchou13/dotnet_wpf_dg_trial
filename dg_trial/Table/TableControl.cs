using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace dg_trial.Table
{
    public class TableColumnHeaderControl : Control
    {

    }

    public class TableCellControl: Control
    {

    }

    public class TableControl : FrameworkElement
    {
        public List<TableColumnHeaderControl> Headers { get; set; } = new List<TableColumnHeaderControl>();

        protected override IEnumerator LogicalChildren
        {
            get
            {
                var logicalChildren = new List<FrameworkElement>();

                return logicalChildren.GetEnumerator();
            }
        }
    }
}
