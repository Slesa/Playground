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
using System.Windows.Shapes;

namespace TestIt
{
    /// <summary>
    /// Interaktionslogik für Preview.xaml
    /// </summary>
    public partial class Preview : Window
    {
        public Preview(FrameworkElement element)
        {
            InitializeComponent();
            Host.Content = element;
        }
    }
}
