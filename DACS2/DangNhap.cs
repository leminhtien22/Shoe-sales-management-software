using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACS2
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTenDN.Text;
            string matKhau = txtMatKhau.Text;

            // Kiểm tra xem radio button nào được chọn
            string vaiTro = radAdmin.Checked ? "Admin" : "Nhân viên";

            if (KiemTraDangNhap(taiKhoan, matKhau, vaiTro))
            {
                if (vaiTro == "Admin")
                {
                    // Chuyển hướng đến form Admin
                    frmAdmin formAdmin = new frmAdmin();
                    formAdmin.Show();
                    this.Hide(); // Ẩn form hiện tại nếu cần
                }
                else
                {
                    // Chuyển hướng đến form Nhân viên
                    frmNhanvien formNhanVien = new frmNhanvien();
                    formNhanVien.Show();
                    this.Hide(); // Ẩn form hiện tại nếu cần
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }
        private bool KiemTraDangNhap(string taiKhoan, string matKhau, string vaiTro)
        {
            // Lấy danh sách tài khoản từ lớp DsTk
            List<TaiKhoan> listTaiKhoan = DanhSachTaiKhoan.Instance.ListTaiKhoan;

            // Kiểm tra xem tài khoản và mật khẩu có tồn tại trong danh sách không
            foreach (TaiKhoan tk in listTaiKhoan)
            {
                if (tk.TenTaiKhoan == taiKhoan && tk.MatKhau == matKhau)
                {
                    if ((vaiTro == "Admin" && tk.VaiTro == "Admin") || (vaiTro == "Nhân viên" && tk.VaiTro == "Nhân viên"))
                    {
                        return true;
                    }
                    else
                    {
                        return false; // Nếu tài khoản tồn tại nhưng vai trò không phù hợp
                    }
                }
            }

            return false; // Nếu không tìm thấy tài khoản trong danh sách
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show("Bạn có muốn thoát không?", "Thông Báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.Close();
            }    
        }

        private void txtTenDN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
