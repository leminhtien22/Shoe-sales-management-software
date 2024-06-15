using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACS2
{
    public class TaiKhoan
    {
        private string tenTaiKhoan;
        private string matKhau;
        private string vaiTro;

        public string TenTaiKhoan
        {
            get => tenTaiKhoan;
            private set => tenTaiKhoan = value;
        }

        public string MatKhau
        {
            get => matKhau;
            private set => matKhau = value;
        }

        public string VaiTro
        {
            get => vaiTro;
            private set => vaiTro = value;
        }

        public TaiKhoan(string tenTaiKhoan, string matKhau, string vaiTro)
        {
            this.tenTaiKhoan = tenTaiKhoan;
            this.matKhau = matKhau;
            this.vaiTro = vaiTro;
        }

    }
}

