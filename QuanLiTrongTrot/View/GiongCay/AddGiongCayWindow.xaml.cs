using QuanLiTrongTrot.Model;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Windows;

namespace QuanLiTrongTrot.View.GiongCay
{
    public partial class AddGiongCayWindow : Window
    {
        private int? _editId = null; // null = thêm mới, có giá trị = sửa

        public AddGiongCayWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        // Constructor để sửa dữ liệu
        public AddGiongCayWindow(DataRowView row) : this()
        {
            if (row != null)
            {
                _editId = Convert.ToInt32(row["Id"]);
                txtTen.Text = row["Ten"].ToString();
                txtNguonGoc.Text = row["NguonGoc"].ToString();
                txtDacTinh.Text = row["DacTinh"].ToString();
                txtThoiGianThuHoach.Text = row["ThoiGianThuHoach"].ToString();
                cboGiong.SelectedValue = Convert.ToInt32(row["GiongId"]);
                cboVungTrong.SelectedValue = Convert.ToInt32(row["VTId"]);

                this.Title = "Sửa Giống Cây";
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load danh sách Giống Cây Chính
                string queryGiong = "SELECT Id, Ten FROM GiongCayChinh";
                DataTable dtGiong = DataProvider.ExecuteQuery(queryGiong);
                cboGiong.ItemsSource = dtGiong.DefaultView;

                // Load danh sách Vùng Trồng
                string queryVungTrong = "SELECT Id, DiaChi FROM VungTrong";
                DataTable dtVungTrong = DataProvider.ExecuteQuery(queryVungTrong);
                cboVungTrong.ItemsSource = dtVungTrong.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên giống cây!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNguonGoc.Text))
            {
                MessageBox.Show("Vui lòng nhập nguồn gốc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNguonGoc.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDacTinh.Text))
            {
                MessageBox.Show("Vui lòng nhập đặc tính!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDacTinh.Focus();
                return;
            }

            if (!int.TryParse(txtThoiGianThuHoach.Text, out int thoiGian) || thoiGian <= 0)
            {
                MessageBox.Show("Thời gian thu hoạch phải là số nguyên dương!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtThoiGianThuHoach.Focus();
                return;
            }

            if (cboGiong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn giống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cboVungTrong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vùng trồng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string query;
                if (_editId == null)
                {
                    // Thêm mới
                    string[] values = {
                        txtTen.Text,
                        txtNguonGoc.Text,
                        txtDacTinh.Text,
                        thoiGian.ToString(),
                        cboGiong.SelectedValue.ToString(),
                        cboVungTrong.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "NguonGoc",
                        "DacTinh",
                        "ThoiGianThuHoach",
                        "GiongId",
                        "VTId"
                    };
                    int result = DataProvider.INSERT_DATA(values, data_names, "GiongCayDauDong");
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Thao tác không thành công!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    // Cập nhật
                    string[] values = {
                        txtTen.Text,
                        txtNguonGoc.Text,
                        txtDacTinh.Text,
                        thoiGian.ToString(),
                        cboGiong.SelectedValue.ToString(),
                        cboVungTrong.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "NguonGoc",
                        "DacTinh",
                        "ThoiGianThuHoach",
                        "GiongId",
                        "VTId"
                    };
                    int result = DataProvider.CHANGE_DATA(values, data_names, "GiongCayDauDong");
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Thao tác không thành công!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thao tác: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (_editId == null)
            {
                // Clear form nếu đang thêm mới
                txtTen.Text = "";
                txtNguonGoc.Text = "";
                txtDacTinh.Text = "";
                txtThoiGianThuHoach.Text = "";
                cboGiong.SelectedIndex = -1;
                cboVungTrong.SelectedIndex = -1;
                txtTen.Focus();
            }
            else
            {
                // Xóa record nếu đang sửa
                var result = MessageBox.Show("Bạn có chắc muốn xóa giống cây này?", "Xác nhận", 
                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        string query = $"DELETE FROM GiongCayDauDong WHERE Id = {_editId}";
                        int deleteResult = DataProvider.DELETE_DATA(_editId.ToString(), "Id", "GiongCayDauDong");
                        if (deleteResult > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.DialogResult = true;
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}