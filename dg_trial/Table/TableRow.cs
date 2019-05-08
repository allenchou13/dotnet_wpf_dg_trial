using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace dg_trial.Table
{
    [ContentProperty(nameof(Cells))]
    public class TableRow : FrameworkElement
    {
        public UIElementCollection _Cells;

        public UIElementCollection Cells
        {
            get
            {
                if(_Cells == null)
                {
                    _Cells = new UIElementCollection(this, this);
                }
                return _Cells;
            }
        }

        public int ShownColumnCount { get; set; }

        protected override IEnumerator LogicalChildren => Cells.GetEnumerator();
        protected override int VisualChildrenCount => Cells.Count;
        protected override Visual GetVisualChild(int index)
        {
            return Cells[index];
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double maxHeight = 0;
            for(int col = 0; col < this.ShownColumnCount; col++)
            {
                if(col >= this.Cells.Count)
                {
                    break;
                }
                TableCell cell = this.Cells[col] as TableCell;

                cell.Measure(new Size(cell.ColumnWidth, availableSize.Height));
                if (maxHeight < cell.DesiredSize.Height)
                {
                    maxHeight = cell.DesiredSize.Height;
                }
            }
            return new Size(availableSize.Width, maxHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double leftX = 0;
            for (int col = 0; col < this.ShownColumnCount; col++)
            {
                if (col >= this.Cells.Count)
                {
                    break;
                }
                TableCell cell = this.Cells[col] as TableCell;

                cell.Arrange(new Rect(leftX, 0, cell.ColumnWidth, cell.DesiredSize.Height));
                leftX += cell.ColumnWidth;
            }
            return finalSize;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
            base.OnRender(drawingContext);
        }
    }
}
