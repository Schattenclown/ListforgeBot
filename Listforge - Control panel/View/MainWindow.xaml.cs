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
using Listforge_Control_panel;

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
            try
            {
                System.Diagnostics.Process.Start($"{mvm.SelAPI_URL.Info.LF_Uri}");
            }
            catch (Exception)
            {
                MessageBox.Show("Can´t open the Void", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btWebStat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($"{mvm.SelAPI_URL.Info.LF_StatUri}");
            }
            catch (Exception)
            {
                MessageBox.Show("Can´t open the Void", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btWebStatQC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start($"{mvm.SelAPI_URL.Info.QC_StatUri}");
            }
            catch (Exception)
            {
                MessageBox.Show("Can´t open the Void", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            try
            {
                LF_API_Uri.Delete(mvm.SelAPI_URL, true);
                DB_LF_ServerInfo.Delete(mvm.SelAPI_URL, true);
                mvm.RefreshMainViewModel();
            }
            catch (Exception)
            {
                MessageBox.Show("Select a server!");
            }
        }
        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            mvm.RefreshMainViewModel();
        }
        private void cbHideKeys_Checked(object sender, RoutedEventArgs e)
        {
            Key.Width = 0; Key.Visibility = Visibility.Hidden;
        }

        private void cbHideKeys_Unchecked(object sender, RoutedEventArgs e)
        {
            Key.Width = 250; Key.Visibility = Visibility.Visible;
        }
        private void btDB_CreateTable_Click(object sender, RoutedEventArgs e)
        {
            DB_CreateTableDlg DBCreateDlg = new DB_CreateTableDlg();
            DBCreateDlg.ShowDialog();
        }
    }
}
