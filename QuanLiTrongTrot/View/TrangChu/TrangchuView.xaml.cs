using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiTrongTrot.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

//dgTrangChu
namespace QuanLiTrongTrot.View.TrangChu
{
    public partial class TrangchuView : UserControl
    {
        private string _currentTable = "";
        private DataTable _currentData;
        private bool _isLoading = false;
        
        public TrangchuView(string os)
        {
            InitializeComponent();
            switch (os) {
                case "QuanliLichSu":
                    break;
                case "QuanliHanhChinh":
                    break;
                case "DonViCaphuyen":
                    break;
                case "DonViCapXa":
                    break;
                case "Quanliuser":
                    break;
            }
        }
        
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnThemMoi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
