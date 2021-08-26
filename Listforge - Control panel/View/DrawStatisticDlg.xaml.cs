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
    public partial class DrawStatisticDlg : Window
    {
        private MainViewModel mvm = new MainViewModel();
        public DrawStatisticDlg()
        {
            InitializeComponent();

            double caheight = StatisticCanvas.Height;

            double dx = 20;
            double dy = 0;

            mvm = FindResource("mvmodel") as MainViewModel;

            DrawStatistic dss = new DrawStatistic();
            dss = DrawStatistic.BuildStatistic(mvm.SelAPI_URL.Info);

            foreach (var item in dss.DSVal2)
            {
                Ellipse ell = new Ellipse();
                ell.Width = 8;
                ell.Height = 8;
                ell.Fill = Brushes.Purple;
                Canvas.SetLeft(ell, dx);

                int temp = Convert.ToInt32(caheight) - (Convert.ToInt32(caheight) / 10 * item) - 20;
                dy = Convert.ToDouble(temp);

                Canvas.SetTop(ell, dy);

                dx += 20;

                StatisticCanvas.Children.Add(ell);
            }

            dx = 22;

            foreach (var item in dss.DSVal1)
            {
                Ellipse ell = new Ellipse();
                ell.Width = 8;
                ell.Height = 8;
                ell.Fill = Brushes.Green;
                Canvas.SetLeft(ell, dx);

                int temp = Convert.ToInt32(caheight) - (Convert.ToInt32(caheight) / 10 * item) - 22;
                dy = Convert.ToDouble(temp);

                Canvas.SetTop(ell, dy);

                dx += 20;

                StatisticCanvas.Children.Add(ell);
            }
        }
    }
}
