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

namespace QuanLiTrongTrot.View.CoSoSanXuat
{
    public partial class SanPhamCoSoSanXuatView : UserControl
    {
        private CoSoSanXuatPhanBonModel _coso;
        public SanPhamCoSoSanXuatView(CoSoSanXuatPhanBonModel coSo)
        {
            InitializeComponent();
            _coso = coSo;
        }

    }
}
