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
                    LoadLichSu();
                    break;
                case "QuanliHanhChinh":
                    LoadHanhChinh();
                    break;
                case "DonViCaphuyen":
                    LoadHuyen();
                    break;
                case "DonViCapXa":
                    LoadXa();
                    break;
                case "Quanliuser":
                    LoadUser();
                    break;
            }
            if (_currentTable != "TaiKhoan")
            {
                btnXoa.Visibility = Visibility.Collapsed;
            }
        }
        #region Load data
        public void LoadLichSu()
        {
            try
            {
                _isLoading = true;
                _currentTable = "LichSuDangNhap";
                txtTitle.Text = "Lịch sử đăng nhập";
                txtSearch.Text = "";

                dgTrangChu.ItemsSource = null;

                dgTrangChu.Columns.Clear();
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Tài Khoản", Binding = new System.Windows.Data.Binding("Ten"), Width = 250 });
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Thời gian", Binding = new System.Windows.Data.Binding("ThoiGian"), Width = 300 });

                string query = $"SELECT * FROM {_currentTable}";
                _currentData = DataProvider.ExecuteQuery(query);

                dgTrangChu.ItemsSource = _currentData.DefaultView;
                txtTongSo.Text = _currentData.Rows.Count.ToString();
                _isLoading = false;
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadHanhChinh()
        {
            try
            {
                _isLoading = true;
                _currentTable = "HanhChinh";
                txtTitle.Text = "Đơn vị Hành Chính";
                txtSearch.Text = "";

                dgTrangChu.ItemsSource = null;

                dgTrangChu.Columns.Clear();
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Tỉnh/Thành", Binding = new System.Windows.Data.Binding("Ten"), Width = 250 });
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Trực thuộc", Binding = new System.Windows.Data.Binding("TrucThuocId"), Width = 300 });

                string query = $"SELECT * FROM {_currentTable}";
                _currentData = DataProvider.ExecuteQuery(query);

                dgTrangChu.ItemsSource = _currentData.DefaultView;
                txtTongSo.Text = _currentData.Rows.Count.ToString();
                _isLoading = false;
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadHuyen()
        {
            try
            {
                _isLoading = true;
                _currentTable = "Huyen";
                txtTitle.Text = "Danh sách các Huyện trong khu vực của bạn";
                txtSearch.Text = "";

                dgTrangChu.ItemsSource = null;

                dgTrangChu.Columns.Clear();
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Tài Khoản", Binding = new System.Windows.Data.Binding("Ten"), Width = 250 });
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Thời gian", Binding = new System.Windows.Data.Binding("ThoiGian"), Width = 300 });

                string query = $"SELECT * FROM {_currentTable}";
                _currentData = DataProvider.ExecuteQuery(query);

                dgTrangChu.ItemsSource = _currentData.DefaultView;
                txtTongSo.Text = _currentData.Rows.Count.ToString();
                _isLoading = false;
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadXa()
        {
            try
            {
                _isLoading = true;
                _currentTable = "Xa";
                txtTitle.Text = "Danh sách các Xã trong khu vực của bạn";
                txtSearch.Text = "";

                dgTrangChu.ItemsSource = null;

                dgTrangChu.Columns.Clear();
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Tài Khoản", Binding = new System.Windows.Data.Binding("Ten"), Width = 250 });
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Thời gian", Binding = new System.Windows.Data.Binding("ThoiGian"), Width = 300 });

                string query = $"SELECT * FROM {_currentTable}";
                _currentData = DataProvider.ExecuteQuery(query);

                dgTrangChu.ItemsSource = _currentData.DefaultView;
                txtTongSo.Text = _currentData.Rows.Count.ToString();
                _isLoading = false;
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadUser()
        {
            try
            {
                _isLoading = true;
                _currentTable = "TaiKhoan";
                txtTitle.Text = "Danh sách Tài khoản";
                txtSearch.Text = "";

                dgTrangChu.ItemsSource = null;

                dgTrangChu.Columns.Clear();
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Tên", Binding = new System.Windows.Data.Binding("Ten"), Width = 250 });
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Hạng quyền hạn", Binding = new System.Windows.Data.Binding("QuyenID"), Width = 150 });
                dgTrangChu.Columns.Add(new DataGridTextColumn { Header = "Hồ sơ Id", Binding = new System.Windows.Data.Binding("HoSoId"), Width = 100 });
                string query = $"SELECT * FROM {_currentTable}";
                _currentData = DataProvider.ExecuteQuery(query);

                dgTrangChu.ItemsSource = _currentData.DefaultView;
                txtTongSo.Text = _currentData.Rows.Count.ToString();
                _isLoading = false;
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ReloadCurrentData()
        {
            switch (_currentTable)
            {
                case "LichSuDangNhap":
                    LoadLichSu();
                    break;
                case "QuanliHanhChinh":
                    LoadHanhChinh();
                    break;
                case "Huyen":
                    LoadHuyen();
                    break;
                case "Xa":
                    LoadXa();
                    break;
                case "TaiKhoan":
                    LoadUser();
                    break;
            }
            if (_currentTable != "TaiKhoan")
            {
                btnXoa.Visibility = Visibility.Collapsed;
            }
        }

        #endregion
        #region Nút và Thanh công cụ
        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgTrangChu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedRow = dgTrangChu.SelectedItem as DataRowView;
            if (selectedRow == null) return;

            string tenHienThi = "";

            // Lấy tên hiển thị tùy theo bảng
            switch (_currentTable)
            {
                case "CS_VG":
                    tenHienThi = selectedRow["Ten"].ToString();
                    break;
            }

            // Xác nhận xóa
            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa \"{tenHienThi}\"\nThao tác này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int deleteResult = DataProvider.DELETE_DATA(tenHienThi, "Ten", _currentTable);

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                        ReloadCurrentData(); // Reload lại dữ liệu
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa dữ liệu!", "Lỗi",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnThemMoi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isLoading || _currentData == null) return;

            string keyword = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                _currentData.DefaultView.RowFilter = "";
            }
            else
            {
                string filter = BuildRowFilter(keyword);
                try
                {
                    _currentData.DefaultView.RowFilter = filter;
                }
                catch
                {
                    _currentData.DefaultView.RowFilter = "";
                }
            }

            txtTongSo.Text = _currentData.DefaultView.Count.ToString();
        }

        private string BuildRowFilter(string keyword)
        {
            List<string> conditions = new List<string>();

            foreach (DataColumn col in _currentData.Columns)
            {
                if (col.DataType == typeof(string))
                {
                    conditions.Add($"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{keyword}%'");
                }
                else if (col.DataType == typeof(int) || col.DataType == typeof(double))
                {
                    conditions.Add($"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{keyword}%'");
                }
            }

            return string.Join(" OR ", conditions);
        }
        #endregion
    }
}
