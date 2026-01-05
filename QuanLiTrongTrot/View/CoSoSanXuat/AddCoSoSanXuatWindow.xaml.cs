using QuanLiTrongTrot.Model;
using System;
using System.Data;
using System.Windows;

namespace QuanLiTrongTrot.View.CoSoSanXuat
{
    public partial class AddCoSoSanXuatWindow : Window
    {
        private int? _editId = null;

        public AddCoSoSanXuatWindow()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public AddCoSoSanXuatWindow(DataRowView row) : this()
        {
            if (row != null)
            {
                _editId = Convert.ToInt32(row["Id"]);
                txtTen.Text = row["Ten"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
                cboBanDo.SelectedValue = Convert.ToInt32(row["BanDoId"]);

                this.Title = "Thêm cơ sở sản xuất";
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
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên cơ sở!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTen.Focus();
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
                MessageBox.Show("Vui lòng chọn bản ??!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_editId == null)
                {
                    string[] values = {
                        txtTen.Text,
                        txtDiaChi.Text,
                        cboBanDo.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "DiaChi",
                        "BanDoId"
                    };
                    int result = DataProvider.INSERT_DATA(values, data_names, "CS_VG");
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
                        txtDiaChi.Text,
                        cboBanDo.SelectedValue.ToString()
                    };
                    string[] data_names = {
                        "Ten",
                        "DiaChi",
                        "BanDoId"
                    };
                    int result = DataProvider.CHANGE_DATA(values, data_names, "CS_VG");
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
                txtTen.Text = "";
                txtDiaChi.Text = "";
                cboBanDo.SelectedIndex = -1;
                txtTen.Focus();
            }
            else
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa cơ sở này?", "Xác nhận",
                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        int deleteResult = DataProvider.DELETE_DATA(_editId.ToString(), "Id", "CS_VG");
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
