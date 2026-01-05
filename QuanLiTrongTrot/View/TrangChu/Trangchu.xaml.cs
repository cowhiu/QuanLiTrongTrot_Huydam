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
using System.Windows.Threading;

namespace QuanLiTrongTrot.View.TrangChu
{
    /// <summary>
    /// Interaction logic for Trangchu.xaml
    /// </summary>
    public partial class TrangChu : UserControl
    {
        public TrangChu()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Update_Timer);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        public void Update_Timer(object sender, EventArgs e)
        {
            DisplayTime.Text = DateTime.Now.ToString();
        }

       
    }
}
