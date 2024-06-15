using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACS2
{
    public class DanhSachTaiKhoan
    {
        private static readonly Lazy<DanhSachTaiKhoan> lazyInstance = new Lazy<DanhSachTaiKhoan>(() => new DanhSachTaiKhoan());

        public static DanhSachTaiKhoan Instance => lazyInstance.Value;

        private List<TaiKhoan> listTaiKhoan;

        public List<TaiKhoan> ListTaiKhoan
        {
            get => listTaiKhoan;
            set => listTaiKhoan = value;
        }

        private DanhSachTaiKhoan()
        {
            listTaiKhoan = new List<TaiKhoan>();
            listTaiKhoan.Add(new TaiKhoan("admin", "123", "Admin"));
            listTaiKhoan.Add(new TaiKhoan("user", "456", "Nhân viên"));
        }

    }
}
