using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace QuanLiTrongTrot.ViewModel
{
    // ===================== VIEWMODEL =====================
    // NOTE: This file previously caused CS0101 because there is ANOTHER type with the same name
    // `SanPhamCoSoSanXuatViewModel` in the same namespace.
    // Fix: rename this class to a different name and update the view to use it.
    public class SanPhamCoSoSanXuatViewModel2 : INotifyPropertyChanged
    {
        // Hiển thị tên cơ sở (header)
        private string _tenCoSo = "Cơ sở mẫu A";
        public string TenCoSo
        {
            get { return _tenCoSo; }
            set { _tenCoSo = value; OnPropertyChanged(nameof(TenCoSo)); }
        }

        // Danh sách gốc
        public ObservableCollection<SanPhamCoSoModel> SanPhams { get; } = new ObservableCollection<SanPhamCoSoModel>();

        // Danh sách đã lọc (Binding trong XAML)
        public ICollectionView SanPhamView { get; }

        // Tìm kiếm
        private string _keywordSanPham = "";
        public string KeywordSanPham
        {
            get { return _keywordSanPham; }
            set
            {
                _keywordSanPham = value ?? "";
                OnPropertyChanged(nameof(KeywordSanPham));
                SanPhamView.Refresh();
            }
        }

        // Lọc phân loại: "VoCo" / "HuuCo" / null
        private string _filterPhanLoai = null;
        public string FilterPhanLoai
        {
            get { return _filterPhanLoai; }
            set
            {
                _filterPhanLoai = string.IsNullOrWhiteSpace(value) ? null : value;
                OnPropertyChanged(nameof(FilterPhanLoai));
                SanPhamView.Refresh();
            }
        }

        // Sản phẩm đang chọn (DataGrid)
        private SanPhamCoSoModel _selectedSanPham;
        public SanPhamCoSoModel SelectedSanPham
        {
            get { return _selectedSanPham; }
            set
            {
                _selectedSanPham = value;
                OnPropertyChanged(nameof(SelectedSanPham));

                // update form bindings
                OnPropertyChanged(nameof(TenSanPham));
                OnPropertyChanged(nameof(ThanhPhan));
                OnPropertyChanged(nameof(PhanLoai));
                OnPropertyChanged(nameof(SanLuongTanNam));

                CommandManager.InvalidateRequerySuggested();
            }
        }

        // ===== Form bindings (XAML đang Binding trực tiếp) =====
        public string TenSanPham
        {
            get { return SelectedSanPham != null ? (SelectedSanPham.TenSanPham ?? "") : ""; }
            set
            {
                EnsureSelected();
                SelectedSanPham.TenSanPham = value ?? "";
                OnPropertyChanged(nameof(TenSanPham));
            }
        }

        public string ThanhPhan
        {
            get { return SelectedSanPham != null ? (SelectedSanPham.ThanhPhan ?? "") : ""; }
            set
            {
                EnsureSelected();
                SelectedSanPham.ThanhPhan = value ?? "";
                OnPropertyChanged(nameof(ThanhPhan));
            }
        }

        // ComboBox Tag: "VoCo"/"HuuCo"
        public string PhanLoai
        {
            get
            {
                if (SelectedSanPham == null) return "VoCo";
                return string.IsNullOrWhiteSpace(SelectedSanPham.PhanLoai) ? "VoCo" : SelectedSanPham.PhanLoai;
            }
            set
            {
                EnsureSelected();
                SelectedSanPham.PhanLoai = string.IsNullOrWhiteSpace(value) ? "VoCo" : value;
                OnPropertyChanged(nameof(PhanLoai));
            }
        }

        // TextBox nhận string nhưng lưu double
        public string SanLuongTanNam
        {
            get { return SelectedSanPham == null ? "" : SelectedSanPham.SanLuongTanNam.ToString(); }
            set
            {
                EnsureSelected();
                double x;
                SelectedSanPham.SanLuongTanNam = double.TryParse(value, out x) ? x : 0;
                OnPropertyChanged(nameof(SanLuongTanNam));
            }
        }

        // ===== Commands =====
        public ICommand NewSanPhamCommand { get; }
        public ICommand SaveSanPhamCommand { get; }
        public ICommand DeleteSanPhamCommand { get; }

        private int _autoId = 1;

        public SanPhamCoSoSanXuatViewModel2()
        {
            // tạo view lọc
            SanPhamView = CollectionViewSource.GetDefaultView(SanPhams);
            SanPhamView.Filter = FilterRow;

            // commands
            NewSanPhamCommand = new RelayCommand(_ => NewSanPham());
            SaveSanPhamCommand = new RelayCommand(_ => SaveSanPham());
            DeleteSanPhamCommand = new RelayCommand(_ => DeleteSanPham(), _ => SelectedSanPham != null);

            // dữ liệu mẫu
            Seed();
        }

        private void EnsureSelected()
        {
            if (SelectedSanPham == null)
                SelectedSanPham = new SanPhamCoSoModel { PhanLoai = "VoCo" };
        }

        private bool FilterRow(object obj)
        {
            // Fix CS8370 (C# 7.3): don't use "is not" pattern
            var sp = obj as SanPhamCoSoModel;
            if (sp == null) return false;

            // filter phân loại
            if (!string.IsNullOrWhiteSpace(FilterPhanLoai) && sp.PhanLoai != FilterPhanLoai)
                return false;

            // search keyword
            var kw = (KeywordSanPham ?? "").Trim().ToLower();
            if (kw.Length == 0) return true;

            var hay = string.Format("{0} {1} {2}", sp.TenSanPham, sp.ThanhPhan, sp.PhanLoai).ToLower();
            return hay.Contains(kw);
        }

        private void Seed()
        {
            SanPhams.Clear();

            SanPhams.Add(new SanPhamCoSoModel
            {
                Id = _autoId++,
                TenSanPham = "NPK 20-20-15",
                ThanhPhan = "Đạm - Lân - Kali",
                PhanLoai = "VoCo",
                SanLuongTanNam = 5000
            });

            SanPhams.Add(new SanPhamCoSoModel
            {
                Id = _autoId++,
                TenSanPham = "Phân hữu cơ vi sinh",
                ThanhPhan = "Hữu cơ + vi sinh",
                PhanLoai = "HuuCo",
                SanLuongTanNam = 1200
            });
        }

        private void NewSanPham()
        {
            SelectedSanPham = new SanPhamCoSoModel
            {
                Id = 0,
                PhanLoai = "VoCo",
                SanLuongTanNam = 0
            };
        }

        private void SaveSanPham()
        {
            if (SelectedSanPham == null) return;
            if (string.IsNullOrWhiteSpace(SelectedSanPham.TenSanPham)) return;

            // thêm mới
            if (SelectedSanPham.Id == 0)
            {
                SelectedSanPham.Id = _autoId++;
                SanPhams.Add(SelectedSanPham);
            }
            else
            {
                // cập nhật theo Id
                var old = SanPhams.FirstOrDefault(x => x.Id == SelectedSanPham.Id);
                if (old != null)
                {
                    old.TenSanPham = SelectedSanPham.TenSanPham;
                    old.ThanhPhan = SelectedSanPham.ThanhPhan;
                    old.PhanLoai = SelectedSanPham.PhanLoai;
                    old.SanLuongTanNam = SelectedSanPham.SanLuongTanNam;
                }
            }

            SanPhamView.Refresh();
        }

        private void DeleteSanPham()
        {
            if (SelectedSanPham == null) return;

            var old = SanPhams.FirstOrDefault(x => x.Id == SelectedSanPham.Id);
            if (old != null) SanPhams.Remove(old);

            SelectedSanPham = null;
            SanPhamView.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // ===================== MODEL (gộp trong file để bạn khỏi tạo thêm) =====================
    public class SanPhamCoSoModel
    {
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public string ThanhPhan { get; set; }
        public string PhanLoai { get; set; } // "VoCo" hoặc "HuuCo"
        public double SanLuongTanNam { get; set; }
    }

    // ===================== RELAY COMMAND (gộp trong file để khỏi tạo thêm) =====================
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) { return _canExecute == null || _canExecute(parameter); }
        public void Execute(object parameter) { _execute?.Invoke(parameter); }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}