using System;
using System.Collections.Generic;
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

namespace Bieren.WPF.Views
{
    /// <summary>
    /// Interaction logic for BrouwersView.xaml
    /// </summary>
    public partial class BrouwersView : UserControl
    {
        public BrouwersView()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            if (dg.SelectedItem == null) return;
            dg.ScrollIntoView(dg.SelectedItem);
        }
    }
}
