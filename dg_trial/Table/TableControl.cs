using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace dg_trial.Table
{
    public class TableControl : FrameworkElement
    {
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(TableControl), new PropertyMetadata(Brushes.Transparent));



        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(IEnumerable), typeof(TableControl), new PropertyMetadata(null));
        

        private void GenerateRows(IEnumerable itemSource)
        {
            this.Rows.Clear();
            foreach(var item in itemSource)
            {
                var row = new TableRow();
                foreach(TableColumn column in this.Columns)
                {
                    var cell = new TableCell();
                    if(column.CellStyle!= null)
                    {
                        cell.Style = column.CellStyle;
                    }
                    row.Cells.Add(cell);
                }
                this.Rows.Add(row);
            }
        }

        private UIElementCollection _Columns;
        public UIElementCollection Columns
        {
            get
            {
                if(_Columns == null)
                {
                    _Columns = new UIElementCollection(this, this);
                }
                return _Columns;
            }
        }

        private UIElementCollection _Rows;
        public UIElementCollection Rows
        {
            get
            {
                if(_Rows == null)
                {
                    _Rows = new UIElementCollection(this, this);
                }
                return _Rows;
            }
        }
        

        protected override IEnumerator LogicalChildren
        {
            get
            {
                var children = new List<UIElement>();
                foreach(UIElement ele in Columns)
                {
                    children.Add(ele);
                }
                foreach (UIElement ele in Rows)
                {
                    children.Add(ele);
                }
                return children.GetEnumerator();
            }
        }


        protected override int VisualChildrenCount => this.Columns.Count + this.Rows.Count;
        protected override Visual GetVisualChild(int index)
        {
            if(index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else if(index < this.Columns.Count)
            {
                return this.Columns[index];
            }
            else if(index - this.Columns.Count < this.Rows.Count)
            {
                var row_index = index - this.Columns.Count;
                return Rows[row_index];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            double maxHeaderHeight = 0;
            int shownColumnCount = 0;

            MeasureHeaders(availableSize, out maxHeaderHeight, out shownColumnCount);

            if(availableSize.Height - maxHeaderHeight > 0)
            {
                MeasureRows(new Size(availableSize.Width, availableSize.Height - maxHeaderHeight),
                    shownColumnCount);
            }

            return availableSize;
        }

        private void MeasureHeaders(Size availableSize, out double maxHeight, out int shownCount)
        {
            double leftWidth = availableSize.Width;
            maxHeight = 0;
            shownCount = 0;
            foreach(TableColumn column in this.Columns)
            {
                column.Measure(new Size(leftWidth, availableSize.Height));
                leftWidth -= column.DesiredSize.Width;
                if(leftWidth < 0)
                {
                    leftWidth = 0;
                }
                else
                {
                    shownCount += 1;
                }
                if (maxHeight < column.DesiredSize.Height)
                {
                    maxHeight = column.DesiredSize.Height;
                }
            }
        }

        private void MeasureRows(Size availableSize, int shownColumnCount)
        {

            double leftY = 0;
            foreach(TableRow row in this.Rows)
            {
                if(leftY >= availableSize.Height)
                {
                    break;
                }

                row.ShownColumnCount = shownColumnCount;
                for (var col = 0; col < row.Cells.Count; col++)
                {
                    var cell = row.Cells[col] as TableCell;
                    TableColumn column = null;
                    if (col < this.Columns.Count)
                    {
                        column = this.Columns[col] as TableColumn;
                        cell.ColumnWidth = column.DesiredSize.Width;
                    }
                    else
                    {
                        cell.ColumnWidth = 0;
                    }
                }
                row.Measure(new Size(availableSize.Width, availableSize.Height - leftY));
                

                leftY += row.DesiredSize.Height;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double maxHeaderHeight;
            int shownColumnCount;
            ArrangeHeaders(finalSize, out maxHeaderHeight);
            if (finalSize.Height - maxHeaderHeight > 0)
            {
                ArrangeRows(new Size(finalSize.Width, finalSize.Height - maxHeaderHeight), maxHeaderHeight);
            }
            return finalSize;
        }

        private void ArrangeHeaders(Size finalSize, out double maxHeight)
        {
            maxHeight = 0;
            var leftHeaderArea = new Rect(finalSize);
            var it = this.Columns.GetEnumerator();
            while (leftHeaderArea.X < finalSize.Width)
            {
                if (it.MoveNext())
                {
                    var current = it.Current as UIElement;
                    current.Arrange(new Rect(leftHeaderArea.X, leftHeaderArea.Y, current.DesiredSize.Width, current.DesiredSize.Height));
                    leftHeaderArea.X += current.DesiredSize.Width;
                    leftHeaderArea.Width -= current.DesiredSize.Width;
                    if (maxHeight < current.DesiredSize.Height)
                    {
                        maxHeight = current.DesiredSize.Height;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void ArrangeRows(Size availableSize, double startY)
        {
            double leftY = startY;
            foreach (TableRow row in this.Rows)
            {
                if (leftY >= availableSize.Height)
                {
                    break;
                }
                row.Arrange(new Rect(0, leftY, row.DesiredSize.Width, row.DesiredSize.Height));
                
                leftY += row.DesiredSize.Height;
            }
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Background, new Pen(Brushes.Gray, 1), new Rect(0, 0, RenderSize.Width, RenderSize.Height));
            base.OnRender(drawingContext);
        }
    }
}
