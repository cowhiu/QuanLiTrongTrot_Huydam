using QuanLiTrongTrot.Model;
using System;
using System.Windows;

namespace QuanLiTrongTrot.View.TrangChu
{
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            var username = (txtUsername.Text ?? string.Empty).Trim();
            var password = pbPassword.Password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (DataProvider.CHECK_DATA_EXISTS(username, "Ten", "TaiKhoan"))
                {
                    MessageBox.Show("Tài khoản đã tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string[] values = { username, password };
                string[] data_names = { "Ten", "MatKhau" };

                int result = DataProvider.INSERT_DATA(values, data_names, "TaiKhoan");
                if (result > 0)
                {
                    MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                    return;
                }

                MessageBox.Show("Thao tác không thành công!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thao tác: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
