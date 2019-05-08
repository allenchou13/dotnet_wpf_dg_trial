using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dg_trial
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnShowDetailsButtonClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = this.pb.Password;
            var vm = this.DataContext as MainViewModel;
            vm.Password = password;
            if(!string.IsNullOrEmpty(password) || this.pb.IsKeyboardFocusWithin)
            {
                this.pbHint.Visibility = Visibility.Hidden;
            }
            else
            {
                this.pbHint.Visibility = Visibility.Visible;
            }
        }

        private void OnPBFocusChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.pb.Password) || this.pb.IsKeyboardFocusWithin)
            {
                this.pbHint.Visibility = Visibility.Hidden;
            }
            else
            {
                this.pbHint.Visibility = Visibility.Visible;
            }
        }
    }
}
