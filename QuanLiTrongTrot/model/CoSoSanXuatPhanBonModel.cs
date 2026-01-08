using System;

namespace QuanLiTrongTrot.Model
{
    public class CoSoSanXuatPhanBonModel
    {
        public int Id { get; set; }                 // khóa chính giả (tăng dần)
        public string MaCoSo { get; set; }          // CS01...
        public string TenCoSo { get; set; }
        public string DiaChi { get; set; }
        public string NguoiDaiDien { get; set; }
        public string SoDienThoai { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string TrangThaiGiayPhep { get; set; }
    }
}
