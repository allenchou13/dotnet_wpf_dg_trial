using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace dg_trial.ResizableItems
{
    public class Item : Control
    {
        private DispatcherTimer timer = new DispatcherTimer();

        public Item()
        {
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.IsEnabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var x = Mouse.GetPosition(this).X;
            if (Mouse.DirectlyOver == this && x >= 0 && x < 5)
            {
                Mouse.SetCursor(Cursors.SizeWE);
            }
            else
            {
                Mouse.SetCursor(Cursors.Arrow);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

    }
}
