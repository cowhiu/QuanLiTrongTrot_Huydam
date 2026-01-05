using QuanLiTrongTrot.Model;
using System;
using System.Data;
using System.Windows;

namespace QuanLiTrongTrot.View.PhanBon
{
    public partial class AddPhanBonWindow : Window
    {
        private int? _editId = null;

        public AddPhanBonWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddPhanBonWindow(DataRowView row) : this()
        {
            if (row != null)
            {
                _editId = Convert.ToInt32(row["Id"]);
                txtTen.Text = row["Ten"].ToString();
                txtThanhPhan.Text = row["ThanhPhan"].ToString();
                txtPhanLoai.Text = row["PhanLoai"].ToString();
                cboVungTrong.SelectedValue = Convert.ToInt32(row["VTId"]);

                this.Title = "Sửa Phân Bón";
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
                MessageBox.Show("Vui lòng nhập tên phân bón!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtThanhPhan.Text))
            {
                MessageBox.Show("Vui lòng nhập thành phần!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtThanhPhan.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhanLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập phân loại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPhanLoai.Focus();
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
                        txtThanhPhan.Text,
                        txtPhanLoai.Text,
                        cboVungTrong.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "ThanhPhan",
                        "PhanLoai",
                        "VTId"
                    };
                    int result = DataProvider.INSERT_DATA(values, data_names, "PhanBon");
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
                        txtThanhPhan.Text,
                        txtPhanLoai.Text,
                        cboVungTrong.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "ThanhPhan",
                        "PhanLoai",
                        "VTId"
                    };
                    int result = DataProvider.CHANGE_DATA(values, data_names, "PhanBon");
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
                MessageBox.Show("Lỗi thao tác: " + ex.Message, "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (_editId == null)
            {
                txtTen.Text = "";
                txtThanhPhan.Text = "";
                txtPhanLoai.Text = "";
                cboVungTrong.SelectedIndex = -1;
                txtTen.Focus();
            }
            else
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa phân bón này?", "Xác nhận",
                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int deleteResult = DataProvider.DELETE_DATA(_editId.ToString(), "Id", "PhanBon");
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
