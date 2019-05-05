using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace dg_trial.ResizableItems
{
    public class Container : FrameworkElement
    {
        public Container()
        {
            Items = new UIElementCollection(this, this);
        }

        public UIElementCollection Items { get; }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                var children = new List<FrameworkElement>();
                foreach(FrameworkElement i in Items)
                {
                    children.Add(i);
                }
                return children.GetEnumerator();
            }
        }

        protected override int VisualChildrenCount => this.Items?.Count ?? 0;

        protected override Visual GetVisualChild(int index)
        {
            return this.Items[index];
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var leftsize = availableSize;
            foreach(UIElement item in Items)
            {
                item.Measure(availableSize);
                if(leftsize.Width > item.DesiredSize.Width)
                {
                    leftsize.Width -= item.DesiredSize.Width;
                }
                else
                {
                    leftsize.Width = 0;
                    break;
                }
            }
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0;
            foreach (UIElement item in Items)
            {
                item.Arrange(new Rect(x, 0, item.DesiredSize.Width, item.DesiredSize.Height));
                x += item.DesiredSize.Width;
                if(x > finalSize.Width)
                {
                    break;
                }
            }
            return finalSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var color = new Color();
            color.A = (int)(255 * 0.7);
            color.R = 0xd6;
            color.G = 0xd6;
            color.B = 0xd6;
            drawingContext.DrawRectangle(new SolidColorBrush(color), null, new Rect(0,0, this.ActualWidth, this.ActualHeight));
        }
        
    }
}
