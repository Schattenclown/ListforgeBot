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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BotDLL;

namespace Listforge_Control_panel
{
    public partial class MainWindow : Window
    {
        private MainViewModel mvm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mvm = FindResource("mvmodel") as MainViewModel;
        }
        private void btWeb_Click(object sender, RoutedEventArgs e)
        {   
            System.Diagnostics.Process.Start($"{mvm.SelAPI_URL.Info.LF_Uri}");
        }
        private void btWebStat_Click(object sender, RoutedEventArgs e)
        {
            if(mvm.SelAPI_URL.Info.LF_StatUri != null)
                System.Diagnostics.Process.Start($"{mvm.SelAPI_URL.Info.LF_StatUri}");
        }
        private void btAddServer_Click(object sender, RoutedEventArgs e)
        {
            AddServerDlg addServerDlg = new AddServerDlg();
            addServerDlg.ShowDialog();
            if(addServerDlg.DialogResult == true)
                mvm.RefreshMainViewModel();
        }
        private void btDelServer_Click(object sender, RoutedEventArgs e)
        {
            if(mvm.SelAPI_URL!=null)
            {
                LF_API_Uri.Delete(mvm.SelAPI_URL);
                DB_LF_ServerInfo.Delete(mvm.SelAPI_URL);
                mvm.RefreshMainViewModel();
            }
            else
                MessageBox.Show("Select a server!");
        }
        private void btWebStatQC_Click(object sender, RoutedEventArgs e)
        {
            if (mvm.SelAPI_URL.Info.QC_StatUri != null)
                System.Diagnostics.Process.Start($"{mvm.SelAPI_URL.Info.QC_StatUri}");
        }
        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            mvm.RefreshMainViewModel();
        }
        private void cbHideKeys_Checked(object sender, RoutedEventArgs e)
        {
            Key.Width = 0;
            Key.Visibility = Visibility.Hidden;
        }

        private void cbHideKeys_Unchecked(object sender, RoutedEventArgs e)
        {
            Key.Width = 250;
            Key.Visibility = Visibility.Visible;
        }

        private void cbHideKeys_Copy_Checked(object sender, RoutedEventArgs e)
        {
            mvm.ApiKey = Visibility.Hidden;
        }

        private void cbHideKeys_Copy_Unchecked(object sender, RoutedEventArgs e)
        {
            mvm.ApiKey = Visibility.Visible;
        }
    }
}
