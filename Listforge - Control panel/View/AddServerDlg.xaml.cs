using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Listforge_Control_panel
{
    /// <summary>
    /// Interaktionslogik für AddServerDlg.xaml
    /// </summary>
    public partial class AddServerDlg : Window
    {
        public AddServerDlg()
        {
            InitializeComponent();
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            AddServerViewModel addServerViewModel = FindResource("addmodel") as AddServerViewModel;
            if(addServerViewModel.API_URL.Key != null)
                addServerViewModel.API_URL.Write(true);
            DialogResult = true;
            this.Close();
        }
    }
}
