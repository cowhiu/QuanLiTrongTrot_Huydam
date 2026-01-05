using QuanLiTrongTrot.Model;
using System;
using System.Data;
using System.Windows;

namespace QuanLiTrongTrot.View.VungTrong
{
    public partial class AddVungTrongWindow : Window
    {
        private int? _editId = null;

        public AddVungTrongWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddVungTrongWindow(DataRowView row) : this()
        {
            if (row != null)
            {
                _editId = Convert.ToInt32(row["Id"]);
                txtQuyMo.Text = row["QuyMo"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
                cboBanDo.SelectedValue = Convert.ToInt32(row["BanDoId"]);

                this.Title = "Sửa Vùng Trồng";
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                string queryBanDo = "SELECT Id, BanDoId FROM VungTrong";
                DataTable dtBanDo = DataProvider.ExecuteQuery(queryBanDo);
                cboBanDo.ItemsSource = dtBanDo.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtQuyMo.Text, out double quyMo) || quyMo <= 0)
            {
                MessageBox.Show("Quy mô phải là số dương!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtQuyMo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtDiaChi.Focus();
                return;
            }

            if (cboBanDo.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bản đồ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_editId == null)
                {
                    string[] values = {
                        quyMo.ToString(),
                        txtDiaChi.Text,
                        cboBanDo.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "QuyMo",
                        "DiaChi",
                        "BanDoId"
                    };
                    int result = DataProvider.INSERT_DATA(values, data_names, "VungTrong");
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
                    string[] values = {
                        quyMo.ToString(),
                        txtDiaChi.Text,
                        cboBanDo.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "QuyMo",
                        "DiaChi",
                        "BanDoId"
                    };
                    int result = DataProvider.CHANGE_DATA(values, data_names, "VungTrong");
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
                txtQuyMo.Text = "";
                txtDiaChi.Text = "";
                cboBanDo.SelectedIndex = -1;
                txtQuyMo.Focus();
            }
            else
            {
                var result = MessageBox.Show("Bạn có chức muốn xóa vùng trồng này?", "Xác nhận",
                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int deleteResult = DataProvider.DELETE_DATA(_editId.ToString(), "Id", "VungTrong");
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
