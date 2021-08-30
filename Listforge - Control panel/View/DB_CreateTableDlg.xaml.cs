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
using BotDLL;

namespace Listforge_Control_panel
{
    /// <summary>
    /// Interaktionslogik für DB_CreateTable.xaml
    /// </summary>
    public partial class DB_CreateTableDlg : Window
    {
        public DB_CreateTableDlg()
        {
            InitializeComponent();
        }
        private void LF_ServerInfoLive_Click(object sender, RoutedEventArgs e)
        {
            LF_ServerInfo.CreateTable_LF_ServerInfoLive(true);
        }
        private void TL_USerdata_Click(object sender, RoutedEventArgs e)
        {
            TL_Userdata.CreateTable_Userdata(true);
        }
        private void LF_API_Uri_Click(object sender, RoutedEventArgs e)
        {
            DB_LF_API_Uri.CreateTable_LF_API_Uri(true);
        }

        private void DC_USerdata_Copy_Click(object sender, RoutedEventArgs e)
        {
            DC_Userdata.CreateTable_Userdata(true);
        }
    }
}
