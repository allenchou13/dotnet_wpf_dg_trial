using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace dg_trial.Table
{
    public class TableCell : Control
    {
        public double ColumnWidth { get; set; } = 0;
        
        protected override void OnRender(DrawingContext drawingContext)
        {
            //drawingContext.DrawRectangle(Brushes.Transparent, null, new System.Windows.Rect(0, 0, this.RenderSize.Width, this.RenderSize.Height));
            drawingContext.DrawLine(new Pen(Brushes.LightGreen, 1), new Point(RenderSize.Width, 0), new Point(RenderSize.Width, RenderSize.Height));
            base.OnRender(drawingContext);
        }
    }
}
