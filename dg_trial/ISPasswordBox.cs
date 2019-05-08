using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace dg_trial
{
    public class ISPasswordBox : FrameworkElement
    {
        private PasswordBox passwordBox = new PasswordBox();

        public ISPasswordBox()
        {
            this.passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            this.passwordBox.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.Password = passwordBox.Password;
        }



        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(ISPasswordBox), new PropertyMetadata(null));




        protected override IEnumerator LogicalChildren => new[] { passwordBox }.GetEnumerator();

        protected override int VisualChildrenCount => 1;
        protected override Visual GetVisualChild(int index)
        {
            return passwordBox;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            passwordBox.Measure(availableSize);
            return passwordBox.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            passwordBox.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            return finalSize;
        }
    }
}
