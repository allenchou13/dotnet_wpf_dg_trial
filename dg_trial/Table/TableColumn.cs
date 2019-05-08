using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace dg_trial.Table
{
    public class TableColumn : Control
    {
        public Style CellStyle
        {
            get { return (Style)GetValue(CellStyleProperty); }
            set { SetValue(CellStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CellStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CellStyleProperty =
            DependencyProperty.Register("CellStyle", typeof(Style), typeof(TableColumn), new PropertyMetadata(null));



        #region drag resize 

        private bool isResizing = false;
        private Point lastMousePosition;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var x = Mouse.GetPosition(this).X;
            if (x < this.ActualWidth && x > this.ActualHeight - 20)
            {
                this.CaptureMouse();
                isResizing = true;
                lastMousePosition = e.GetPosition(this);
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (this.isResizing)
            {
                this.isResizing = false;
                this.ReleaseMouseCapture();
            }
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (this.isResizing)
            {
                this.isResizing = false;
                this.ReleaseMouseCapture();
            }
            base.OnMouseLeave(e);
        }

        private void ResizeOnMouseMove(MouseEventArgs e)
        {
            if (isResizing)
            {
                var pos = e.GetPosition(this);
                var shift = pos - lastMousePosition;
                var newWidth = this.ActualWidth + shift.X;
                if (newWidth > 0)
                {
                    this.Width = newWidth;
                }
                this.lastMousePosition = pos;
            }
        }

        #endregion

        #region change cursor type
        private bool isCursorResizeRype = false;

        private void ChangeCursorOnMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseOver)
            {
                var x = e.GetPosition(this).X;
                if (x < this.ActualWidth && x > this.ActualWidth - 5)
                {
                    if (!isCursorResizeRype)
                    {
                        this.Cursor = Cursors.SizeWE;
                        isCursorResizeRype = true;
                    }
                }
                else
                {
                    if (isCursorResizeRype)
                    {
                        this.Cursor = Cursors.Arrow;
                        isCursorResizeRype = false;
                    }
                }
            }
        }

        #endregion


        protected override void OnMouseMove(MouseEventArgs e)
        {
            ResizeOnMouseMove(e);
            ChangeCursorOnMouseMove(e);
            base.OnMouseMove(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.Transparent, null, new System.Windows.Rect(0, 0, this.RenderSize.Width, this.RenderSize.Height));
            drawingContext.DrawLine(new Pen(Brushes.Gray, 1), new Point(RenderSize.Width, 0), new Point(RenderSize.Width, RenderSize.Height));
            base.OnRender(drawingContext);
        }
    }
}
