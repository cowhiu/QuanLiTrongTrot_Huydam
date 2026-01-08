using QuanLiTrongTrot.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace QuanLiTrongTrot.ViewModel
{
    public class CoSoSanXuatPhanBonViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CoSoSanXuatPhanBonModel> DanhSach { get; } =
            new ObservableCollection<CoSoSanXuatPhanBonModel>();

        private CoSoSanXuatPhanBonModel _selected;
        public CoSoSanXuatPhanBonModel Selected
        {
            get => _selected;
            set
            {
                if (_selected == value) return;
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        private int _autoId = 1;

        public CoSoSanXuatPhanBonViewModel()
        {
            LoadDuLieuMau();
        }

        public void LoadDuLieuMau()
        {
            DanhSach.Clear();

            DanhSach.Add(new CoSoSanXuatPhanBonModel
            {
                Id = _autoId++,
                MaCoSo = "CS01",
                TenCoSo = "Nhà máy Phân bón Bình Điền II",
                DiaChi = "KCN Sóng Thần, Bình Dương",
                NguoiDaiDien = "Nguyễn Văn A",
                SoDienThoai = "0901234567",
                Lat = 10.889,
                Lng = 106.755,
                TrangThaiGiayPhep = "Đang hoạt động"
            });

            DanhSach.Add(new CoSoSanXuatPhanBonModel
            {
                Id = _autoId++,
                MaCoSo = "CS02",
                TenCoSo = "Nhà máy Phân bón Miền Tây",
                DiaChi = "Cần Thơ",
                NguoiDaiDien = "Trần Văn B",
                SoDienThoai = "0912345678",
                Lat = 10.045,
                Lng = 105.746,
                TrangThaiGiayPhep = "Đình chỉ"
            });
        }

        public CoSoSanXuatPhanBonModel TaoMoi()
        {
            Selected = null;
            return new CoSoSanXuatPhanBonModel();
        }

        public void Them(CoSoSanXuatPhanBonModel m)
        {
            if (m == null) return;
            m.Id = _autoId++;
            DanhSach.Add(m);
        }

        public void CapNhat(CoSoSanXuatPhanBonModel m)
        {
            if (m == null) return;

            var old = DanhSach.FirstOrDefault(x => x.Id == m.Id);
            if (old == null) return;

            old.MaCoSo = m.MaCoSo;
            old.TenCoSo = m.TenCoSo;
            old.DiaChi = m.DiaChi;
            old.NguoiDaiDien = m.NguoiDaiDien;
            old.SoDienThoai = m.SoDienThoai;
            old.Lat = m.Lat;
            old.Lng = m.Lng;
            old.TrangThaiGiayPhep = m.TrangThaiGiayPhep;
        }

        public void XoaSelected()
        {
            if (Selected == null) return;
            DanhSach.Remove(Selected);
            Selected = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
