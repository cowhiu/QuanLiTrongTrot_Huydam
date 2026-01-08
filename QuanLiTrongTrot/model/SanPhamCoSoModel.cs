namespace QuanLiTrongTrot.Model
{
    public class SanPhamCoSoModel
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public string ThanhPhan { get; set; }
        // Lưu theo mã: "VoCo" hoặc "HuuCo" để khớp Tag trong XAML
        public string PhanLoai { get; set; }
        public double SanLuongTanNam { get; set; }
    }
}
