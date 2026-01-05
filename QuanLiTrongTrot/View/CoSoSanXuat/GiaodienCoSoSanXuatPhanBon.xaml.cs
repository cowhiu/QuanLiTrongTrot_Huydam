using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QuanLiTrongTrot.View.CoSoSanXuat
{
    public partial class GiaodienCoSoSanXuatPhanBon : UserControl
    {
        // “Database” giả
        private List<CoSoSanXuatPhanBonModel> _dsCoSo =
            new List<CoSoSanXuatPhanBonModel>();

        private CoSoSanXuatPhanBonModel _coSoDangChon = null;

        public GiaodienCoSoSanXuatPhanBon()
        {
            InitializeComponent();
            LoadDuLieuMau();
            dgCoSo.ItemsSource = _dsCoSo;
        }

        // ================== DỮ LIỆU MẪU ==================
        private void LoadDuLieuMau()
        {
            _dsCoSo.Add(new CoSoSanXuatPhanBonModel
            {
                MaCoSo = "CS01",
                TenCoSo = "Nhà máy Phân bón Bình Điền II",
                DiaChi = "KCN Sóng Thần, Bình Dương",
                NguoiDaiDien = "Nguyễn Văn A",
                SoDienThoai = "0901234567",
                Lat = 10.889,
                Lng = 106.755,
                TrangThaiGiayPhep = "Đang hoạt động"
            });

            _dsCoSo.Add(new CoSoSanXuatPhanBonModel
            {
                MaCoSo = "CS02",
                TenCoSo = "Nhà máy Phân bón Miền Tây",
                DiaChi = "Cần Thơ",
                NguoiDaiDien = "Trần Văn B",
                SoDienThoai = "0912345678",
                Lat = 10.045,
                Lng = 105.746,
                TrangThaiGiayPhep = "Đình chỉ"
            });
        }

        // ================== DATAGRID CLICK ==================
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _coSoDangChon = dgCoSo.SelectedItem as CoSoSanXuatPhanBonModel;
            if (_coSoDangChon == null) return;

            txtMaCoSo.Text = _coSoDangChon.MaCoSo;
            txtTenCoSo.Text = _coSoDangChon.TenCoSo;
            txtDiaChi.Text = _coSoDangChon.DiaChi;
            txtNguoiDaiDien.Text = _coSoDangChon.NguoiDaiDien;
            txtSDT.Text = _coSoDangChon.SoDienThoai;
            txtLat.Text = _coSoDangChon.Lat.ToString();
            txtLng.Text = _coSoDangChon.Lng.ToString();
            cbTrangThai.Text = _coSoDangChon.TrangThaiGiayPhep;
        }

        // ================== TẠO MỚI ==================
        private void BtnTaoMoi_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            _coSoDangChon = null;
        }

        // ================== LƯU (THÊM / SỬA) ==================
        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaCoSo.Text))
            {
                MessageBox.Show("Mã cơ sở không được để trống");
                return;
            }

            if (_coSoDangChon == null)
            {
                // Thêm mới
                CoSoSanXuatPhanBonModel cs = new CoSoSanXuatPhanBonModel
                {
                    MaCoSo = txtMaCoSo.Text,
                    TenCoSo = txtTenCoSo.Text,
                    DiaChi = txtDiaChi.Text,
                    NguoiDaiDien = txtNguoiDaiDien.Text,
                    SoDienThoai = txtSDT.Text,
                    Lat = double.TryParse(txtLat.Text, out double lat) ? lat : 0,
                    Lng = double.TryParse(txtLng.Text, out double lng) ? lng : 0,
                    TrangThaiGiayPhep = cbTrangThai.Text
                };

                _dsCoSo.Add(cs);
            }
            else
            {
                // Sửa
                _coSoDangChon.MaCoSo = txtMaCoSo.Text;
                _coSoDangChon.TenCoSo = txtTenCoSo.Text;
                _coSoDangChon.DiaChi = txtDiaChi.Text;
                _coSoDangChon.NguoiDaiDien = txtNguoiDaiDien.Text;
                _coSoDangChon.SoDienThoai = txtSDT.Text;
                _coSoDangChon.Lat = double.TryParse(txtLat.Text, out double lat) ? lat : 0;
                _coSoDangChon.Lng = double.TryParse(txtLng.Text, out double lng) ? lng : 0;
                _coSoDangChon.TrangThaiGiayPhep = cbTrangThai.Text;
            }

            dgCoSo.Items.Refresh();
            MessageBox.Show("Lưu thành công");
            ClearForm();
        }

        // ================== XÓA ==================
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (_coSoDangChon == null) return;

            if (MessageBox.Show("Xóa cơ sở này?", "Xác nhận",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _dsCoSo.Remove(_coSoDangChon);
                dgCoSo.Items.Refresh();
                ClearForm();
            }
        }

        // ================== BỎ CHỌN ==================
        private void BtnBoChon_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        // ================== HÀM PHỤ ==================
        private void ClearForm()
        {
            txtMaCoSo.Text = "";
            txtTenCoSo.Text = "";
            txtDiaChi.Text = "";
            txtNguoiDaiDien.Text = "";
            txtSDT.Text = "";
            txtLat.Text = "";
            txtLng.Text = "";
            cbTrangThai.SelectedIndex = -1;

            dgCoSo.SelectedItem = null;
            _coSoDangChon = null;
        }
    }
}
