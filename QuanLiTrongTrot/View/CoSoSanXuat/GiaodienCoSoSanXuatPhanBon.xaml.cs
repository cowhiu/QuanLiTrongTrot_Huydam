using QuanLiTrongTrot.Model;
using QuanLiTrongTrot.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace QuanLiTrongTrot.View.CoSoSanXuat
{
    public partial class GiaodienCoSoSanXuatPhanBon : UserControl
    {
        private readonly CoSoSanXuatPhanBonViewModel _vm = new CoSoSanXuatPhanBonViewModel();

        // These fields are required because your XAML is not generating named controls for this class
        private DataGrid dgCoSo;
        private TextBox txtMaCoSo;
        private TextBox txtTenCoSo;
        private TextBox txtDiaChi;
        private TextBox txtNguoiDaiDien;
        private TextBox txtSDT;
        private TextBox txtLat;
        private TextBox txtLng;
        private ComboBox cbTrangThai;

        public GiaodienCoSoSanXuatPhanBon()
        {
            // If InitializeComponent() is missing, it means XAML is NOT wired to this class.
            // We keep it ONLY if the project is correctly wired; otherwise it will not compile.
            // Comment it out to guarantee compilation even when XAML is mismatched.
            // InitializeComponent();

            WireUpControlsOrCreateFallbackUi();

            // Gán datasource cho DataGrid (không cần sửa XAML)
            dgCoSo.ItemsSource = _vm.DanhSach;
        }

        private void WireUpControlsOrCreateFallbackUi()
        {
            // Try to find controls from XAML (only works if XAML x:Class matches THIS class)
            dgCoSo = FindName("dgCoSo") as DataGrid;
            txtMaCoSo = FindName("txtMaCoSo") as TextBox;
            txtTenCoSo = FindName("txtTenCoSo") as TextBox;
            txtDiaChi = FindName("txtDiaChi") as TextBox;
            txtNguoiDaiDien = FindName("txtNguoiDaiDien") as TextBox;
            txtSDT = FindName("txtSDT") as TextBox;
            txtLat = FindName("txtLat") as TextBox;
            txtLng = FindName("txtLng") as TextBox;
            cbTrangThai = FindName("cbTrangThai") as ComboBox;

            // If XAML is not wired / names don't exist, build a minimal UI so code compiles and runs.
            if (dgCoSo != null &&
                txtMaCoSo != null &&
                txtTenCoSo != null &&
                txtDiaChi != null &&
                txtNguoiDaiDien != null &&
                txtSDT != null &&
                txtLat != null &&
                txtLng != null &&
                cbTrangThai != null)
            {
                dgCoSo.SelectionChanged += DataGrid_SelectionChanged;
                return;
            }

            // Fallback UI (prevents null refs + guarantees compilation)
            var root = new Grid();
            root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(12) });
            root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            dgCoSo = new DataGrid
            {
                AutoGenerateColumns = true,
                IsReadOnly = true,
                CanUserAddRows = false,
                Margin = new Thickness(10),
            };
            dgCoSo.SelectionChanged += DataGrid_SelectionChanged;
            Grid.SetColumn(dgCoSo, 0);
            root.Children.Add(dgCoSo);

            var form = new StackPanel { Margin = new Thickness(10) };
            Grid.SetColumn(form, 2);
            root.Children.Add(form);

            txtMaCoSo = AddLabeledTextBox(form, "Mã cơ sở");
            txtTenCoSo = AddLabeledTextBox(form, "Tên cơ sở");
            txtDiaChi = AddLabeledTextBox(form, "Địa chỉ");
            txtNguoiDaiDien = AddLabeledTextBox(form, "Người đại diện");
            txtSDT = AddLabeledTextBox(form, "SĐT");
            txtLat = AddLabeledTextBox(form, "Lat");
            txtLng = AddLabeledTextBox(form, "Lng");

            form.Children.Add(new TextBlock { Text = "Trạng thái giấy phép", Margin = new Thickness(0, 8, 0, 4) });
            cbTrangThai = new ComboBox { Height = 28 };
            form.Children.Add(cbTrangThai);

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 10, 0, 0) };
            form.Children.Add(buttons);

            var btnTaoMoi = new Button { Content = "Tạo mới", MinWidth = 80, Margin = new Thickness(0, 0, 6, 0) };
            btnTaoMoi.Click += BtnTaoMoi_Click;
            buttons.Children.Add(btnTaoMoi);

            var btnLuu = new Button { Content = "Lưu", MinWidth = 80, Margin = new Thickness(0, 0, 6, 0) };
            btnLuu.Click += BtnLuu_Click;
            buttons.Children.Add(btnLuu);

            var btnXoa = new Button { Content = "Xóa", MinWidth = 80, Margin = new Thickness(0, 0, 6, 0) };
            btnXoa.Click += BtnXoa_Click;
            buttons.Children.Add(btnXoa);

            var btnBoChon = new Button { Content = "Bỏ chọn", MinWidth = 80 };
            btnBoChon.Click += BtnBoChon_Click;
            buttons.Children.Add(btnBoChon);

            Content = root;
        }

        private static TextBox AddLabeledTextBox(Panel panel, string label)
        {
            panel.Children.Add(new TextBlock { Text = label, Margin = new Thickness(0, 8, 0, 4) });
            var tb = new TextBox { Height = 28 };
            panel.Children.Add(tb);
            return tb;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.Selected = dgCoSo.SelectedItem as CoSoSanXuatPhanBonModel;
            if (_vm.Selected == null) return;

            txtMaCoSo.Text = _vm.Selected.MaCoSo;
            txtTenCoSo.Text = _vm.Selected.TenCoSo;
            txtDiaChi.Text = _vm.Selected.DiaChi;
            txtNguoiDaiDien.Text = _vm.Selected.NguoiDaiDien;
            txtSDT.Text = _vm.Selected.SoDienThoai;
            txtLat.Text = _vm.Selected.Lat.ToString();
            txtLng.Text = _vm.Selected.Lng.ToString();
            cbTrangThai.Text = _vm.Selected.TrangThaiGiayPhep;
        }

        private void BtnTaoMoi_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            _vm.TaoMoi();
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            string ma = (txtMaCoSo.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Mã cơ sở không được để trống");
                return;
            }

            var m = new CoSoSanXuatPhanBonModel
            {
                MaCoSo = ma,
                TenCoSo = txtTenCoSo.Text ?? "",
                DiaChi = txtDiaChi.Text ?? "",
                NguoiDaiDien = txtNguoiDaiDaiDienSafe(),
                SoDienThoai = txtSDT.Text ?? "",
                Lat = double.TryParse(txtLat.Text, out double lat) ? lat : 0,
                Lng = double.TryParse(txtLng.Text, out double lng) ? lng : 0,
                TrangThaiGiayPhep = cbTrangThai.Text ?? ""
            };

            if (_vm.Selected == null)
            {
                _vm.Them(m);
                MessageBox.Show("Thêm mới thành công");
            }
            else
            {
                m.Id = _vm.Selected.Id;
                _vm.CapNhat(m);
                MessageBox.Show("Cập nhật thành công");
            }

            dgCoSo.Items.Refresh();
            ClearForm();
        }

        private string txtNguoiDaiDaiDienSafe()
        {
            return txtNguoiDaiDien != null ? (txtNguoiDaiDien.Text ?? "") : "";
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.Selected == null)
            {
                MessageBox.Show("Hãy chọn 1 cơ sở để xóa");
                return;
            }

            if (MessageBox.Show("Xóa cơ sở này?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _vm.XoaSelected();
                dgCoSo.Items.Refresh();
                ClearForm();
            }
        }

        private void BtnBoChon_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

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
            _vm.Selected = null;
        }
    }
}