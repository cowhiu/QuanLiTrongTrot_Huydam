using QuanLiTrongTrot.Model;
using System;
using System.Data;
using System.Windows;

namespace QuanLiTrongTrot.View.SinhVatGayHai
{
    public partial class AddSinhVatGayHaiWindow : Window
    {
        private int? _editId = null;

        public AddSinhVatGayHaiWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddSinhVatGayHaiWindow(DataRowView row) : this()
        {
            if (row != null)
            {
                _editId = Convert.ToInt32(row["Id"]);
                txtTen.Text = row["Ten"].ToString();
                txtPhanLoai.Text = row["PhanLoai"].ToString();
                this.Title = "S?a Sinh V?t Gây H?i";
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                string queryCay = "SELECT Id, Ten FROM GiongCayDauDong";
                DataTable dtCay = DataProvider.ExecuteQuery(queryCay);
                cboCayAH.ItemsSource = dtCay.DefaultView;

                string queryVungTrong = "SELECT Id, DiaChi FROM VungTrong";
                DataTable dtVungTrong = DataProvider.ExecuteQuery(queryVungTrong);
                cboVungTrong.ItemsSource = dtVungTrong.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i t?i d? li?u: " + ex.Message, "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nh?p tên sinh v?t!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhanLoai.Text))
            {
                MessageBox.Show("Vui lòng nh?p phân lo?i!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPhanLoai.Focus();
                return;
            }

            if (cboCayAH.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng ch?n cây ?nh h??ng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cboVungTrong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng ch?n vùng tr?ng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (_editId == null)
                {
                    string[] values = {
                        txtTen.Text,
                        txtPhanLoai.Text,
                        cboCayAH.SelectedValue.ToString(),
                        cboVungTrong.SelectedValue.ToString(),
                    };
                    string[] data_names = {
                        "Ten",
                        "PhanLoai",
                        "CayAHId",
                        "VungId"
                    };
                    int result = DataProvider.INSERT_DATA(values, data_names, "SinhVatGayHai");
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm m?i thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                else
                {
                    string[] values = {
                        txtTen.Text,
                        txtPhanLoai.Text,
                        cboCayAH.SelectedValue.ToString(),
                        cboVungTrong.SelectedValue.ToString(),
                    };
                    string[] data_names = {
                        "Ten",
                        "PhanLoai",
                        "CayAHId",
                        "VTId",
                    };
                    int result = DataProvider.CHANGE_DATA(values, data_names, "SinhVatGayHai");
                    if (result > 0)
                    {
                        MessageBox.Show("C?p nh?t thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("L?i thao tác: " + ex.Message, "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (_editId == null)
            {
                txtTen.Text = "";
                txtPhanLoai.Text = "";
                cboCayAH.SelectedIndex = -1;
                cboVungTrong.SelectedIndex = -1;
                txtTen.Focus();
            }
            else
            {
                var result = MessageBox.Show("B?n có ch?c mu?n xóa sinh v?t gây h?i này?", "Xác nh?n",
                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int deleteResult = DataProvider.DELETE_DATA(_editId.ToString(), "Id", "SinhVatGayHai");
                        if (deleteResult > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.DialogResult = true;
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("L?i xóa: " + ex.Message, "L?i", MessageBoxButton.OK, MessageBoxImage.Error);
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
