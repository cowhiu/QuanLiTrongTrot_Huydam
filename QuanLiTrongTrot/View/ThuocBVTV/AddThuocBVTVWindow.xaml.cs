using QuanLiTrongTrot.Model;
using System;
using System.Data;
using System.Windows;

namespace QuanLiTrongTrot.View.ThuocBVTV
{
    public partial class AddThuocBVTVWindow : Window
    {
        private int? _editId = null;

        public AddThuocBVTVWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddThuocBVTVWindow(DataRowView row) : this()
        {
            if (row != null)
            {
                _editId = Convert.ToInt32(row["Id"]);
                txtTen.Text = row["Ten"].ToString();
                dpNgaySX.SelectedDate = Convert.ToDateTime(row["NgaySX"]);
                dpHanSD.SelectedDate = Convert.ToDateTime(row["HanSD"]);
                cboVungTrong.SelectedValue = Convert.ToInt32(row["VTId"]);

                this.Title = "Sửa Thuốc BVTV";
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
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
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thuốc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTen.Focus();
                return;
            }

            if (dpNgaySX.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sản xuất!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dpHanSD.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn hạn sử dụng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cboVungTrong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vùng trồng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_editId == null)
                {
                    string[] values = {
                        txtTen.Text,
                        dpNgaySX.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        dpHanSD.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        cboVungTrong.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "NgaySX",
                        "HanSD",
                        "VTId"
                    };
                    int result = DataProvider.INSERT_DATA(values, data_names, "ThuocBVTV");
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
                        txtTen.Text,
                        dpNgaySX.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        dpHanSD.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        cboVungTrong.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "NgaySX",
                        "HanSD",
                        "VTId"
                    };
                    int result = DataProvider.CHANGE_DATA(values, data_names, "ThuocBVTV");
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Thao tác không thành công!", "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
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
                txtTen.Text = "";
                dpNgaySX.SelectedDate = null;
                dpHanSD.SelectedDate = null;
                cboVungTrong.SelectedIndex = -1;
                txtTen.Focus();
            }
            else
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa thuốc BVTV này?", "Xác nhận",
                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int deleteResult = DataProvider.DELETE_DATA(_editId.ToString(), "Id", "ThuocBVTV");
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
