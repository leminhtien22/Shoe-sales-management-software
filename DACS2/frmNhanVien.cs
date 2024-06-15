using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;
using DACS2;

namespace DACS2
{
    public partial class frmNhanvien : Form
    {
        public frmNhanvien()
        {
            InitializeComponent();
        }
        DataTable KHACHHANG;
        DataTable HOADON;
        DataTable CHITIETHD;
        DataTable PHIEUBH;

        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            DACS2.Function.Connect();
            txtMaKH.Enabled = false;
            btnLuuHD.Enabled = false;
            loaddata();
            dgvKH.Columns[0].HeaderText = "Mã Khách Hàng";
            dgvKH.Columns[1].HeaderText = "Tên Khách Hàng";
            dgvKH.Columns[2].HeaderText = "Giới Tính";
            dgvKH.Columns[3].HeaderText = "Địa Chỉ";
            dgvKH.Columns[4].HeaderText = "Số Điện Thoại";    
            dgvKH.Columns[5].HeaderText = "Ngày Mua";
            dgvKH.Columns[6].HeaderText = "Loại Khách Hàng";


            dgvKH.Columns[0].Width = 130;
            dgvKH.Columns[1].Width = 170;
            dgvKH.Columns[2].Width = 100;
            dgvKH.Columns[3].Width = 150;
            dgvKH.Columns[4].Width = 100;
            dgvKH.Columns[5].Width = 100;
            dgvKH.Columns[6].Width = 100;
            //Hóa Đơn
            btnThemHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnInHD.Enabled = false;
            txtSoHD.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenHang.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtDonGia.Text = "0";
            Function.FillCombo("SELECT MAKH, HOTEN FROM KHACHHANG", cboMaKH, "MAKH", "MAKH");
            cboMaKH.SelectedIndex = -1;
            Function.FillCombo("SELECT MANV, HOTEN FROM NHANVIEN", cboMaNV, "MANV", "MANV");
            cboMaNV.SelectedIndex = -1;
            cboMaHang.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboMaHang.AutoCompleteSource = AutoCompleteSource.ListItems;
            Function.FillCombo("SELECT MAHANG, TENSP FROM MATHANG ", cboMaHang, "MAHANG", "MAHANG");
            cboMaHang.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtSoHD.Text != "")
            {
                LoadInfoHoaDon();
                btnXoaHD.Enabled = true;
                btnInHD.Enabled = true;
            }
            loaddataCTHD();
            //Phiếu Bảo Hành
            txtMPBH.Enabled = false;
            btnThemPBH.Enabled = true;
            btnLuuPBH.Enabled = false;
            btnXoaPBH.Enabled = false;
            Function.FillCombo("SELECT SOHD FROM HOADON", cboSHDPBH, "SOHD", "SOHD");
            cboSoHoaDon.SelectedIndex = -1;
            if (txtMPBH.Text != "")
            {
                btnXoaHD.Enabled = true;
                btnLuuHD.Enabled = true;
            }
            loaddata();

        }
        void loaddata()
        {
            string sql, sql1;
            sql = "SELECT *  FROM KHACHHANG";
            KHACHHANG = DACS2.Function.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvKH.DataSource = KHACHHANG; //Nguồn dữ liệu            

            dgvKH.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvKH.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            sql1 = "Select * from PHIEUBAOHANH";
            PHIEUBH = Function.GetDataToTable(sql1);
            dgvPBH.DataSource = PHIEUBH;
            dgvPBH.Columns[0].HeaderText = "Mã Phiếu Bảo Hành";
            dgvPBH.Columns[1].HeaderText = "Mã Hàng";
            dgvPBH.Columns[2].HeaderText = "Thời Gian Bảo Hành";
            dgvPBH.Columns[3].HeaderText = "Số Hóa Đơn";
            dgvPBH.Columns[0].Width = 150;
            dgvPBH.Columns[1].Width = 150;
            dgvPBH.Columns[2].Width = 150;
            dgvPBH.Columns[3].Width = 220;
            dgvPBH.AllowUserToAddRows = false;
            dgvPBH.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        void loaddataCTHD()
        {
            string sql;
            sql = "SELECT a.MAHANG, b.TENSP, a.SL, b.GIA, a.GIAMGIA,a.THANHTIEN FROM CTHD AS a, MATHANG AS b WHERE a.SOHD = N'" + txtSoHD.Text + "' AND a.MAHANG=b.MAHANG";
            CHITIETHD = Function.GetDataToTable(sql);
            dgvCTHD.DataSource = CHITIETHD;
            dgvCTHD.Columns[0].HeaderText = "Mã hàng";
            dgvCTHD.Columns[1].HeaderText = "Tên hàng";
            dgvCTHD.Columns[2].HeaderText = "Số lượng";
            dgvCTHD.Columns[3].HeaderText = "Đơn giá";
            dgvCTHD.Columns[4].HeaderText = "Giảm giá %";
            dgvCTHD.Columns[5].HeaderText = "Thành tiền";
            dgvCTHD.Columns[0].Width = 80;
            dgvCTHD.Columns[1].Width = 130;
            dgvCTHD.Columns[2].Width = 80;
            dgvCTHD.Columns[3].Width = 90;
            dgvCTHD.Columns[4].Width = 90;
            dgvCTHD.Columns[5].Width = 90;
            dgvCTHD.AllowUserToAddRows = false;
            dgvCTHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NGHD FROM HOADON WHERE SOHD = N'" + txtSoHD.Text + "'";
            dtpNgayLapHD.Text = Function.ConvertDateTime(Function.GetFieldValues(str));
            str = "SELECT MANV FROM HOADON WHERE SOHD = N'" + txtSoHD.Text + "'";
            cboMaNV.SelectedValue = Function.GetFieldValues(str);
            str = "SELECT MAKH FROM HOADON WHERE SOHD = N'" + txtSoHD.Text + "'";
            cboMaKH.SelectedValue = Function.GetFieldValues(str);
            str = "SELECT TRIGIA FROM HOADON WHERE SOHD = N'" + txtSoHD.Text + "'";
            txtDonGia.Text = Function.GetFieldValues(str);
            panell.Text = "Bằng chữ: " + Function.ChuyenSoSangChuoi(double.Parse(txtDonGia.Text));
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            btnSuaKH.Enabled = false;
            btnXoaKH.Enabled = false;
            btnLuuKH.Enabled = true;
            btnThemKH.Enabled = false;
            btnDongKH.Enabled = true;
            btnBQKH.Enabled = true;
            ResetValues(); //Xoá trắng các textbox
            txtMaKH.Enabled = true; //cho phép nhập mới
            txtMaKH.Focus();
            loaddata();

        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = false;
            btnThemHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnDongHD.Enabled = false;
            ResetValues();
            txtSoHD.Text = Function.CreateKey("HDB");
            loaddataCTHD();
        }

        private void btnThemPBH_Click(object sender, EventArgs e)
        {
            btnLuuPBH.Enabled = true;
            btnSuaPBH.Enabled = false;
            btnThemPBH.Enabled = false;
            btnBoQuaPBH.Enabled = true;
            btnDongPBH.Enabled = true;
            ResetValues();
            txtMPBH.Text = Function.CreateKey("BH");
            loaddata();
        }
        private void ResetValues()
        {
            //KHACHHANG
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            cboGTKH.Text = "";
            txtDiaChiKH.Text = "";
            txtSDTKH.Text = "";
            dtpNgayMua.Text = "";
            txtLoaiKH.Text = "";
            //HOADON
            txtSoHD.Text = "";
            dtpNgayLapHD.Text = DateTime.Now.ToShortDateString();
            cboMaNV.Text = "";
            txtTenNV.Text = "";
            cboMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChiKH.Text = "";
            txtSDTKH.Text = "";
            cboMaHang.Text = "";
            txtSoLuong.Text = "";
            txtTenHang.Text = "";
            txtGiamGia.Text = "0";
            txtDonGia.Text = "0";
            txtThanhTien.Text = "0";
            panell.Text = "Bằng chữ: ";
            txtTriGia.Text = "0";
            //PHIEUBAOHANH
            txtMPBH.Text = "";
            txtMaHang.Text = "";
            cboSHDPBH.Text = "";
            txtTGBH.Text = "0";
        }

        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaKH.Text.Trim().Length == 0 || txtHTKH.Text.Trim().Length == 0 ||
            cboGTKH.Text.Trim().Length == 0 || txtDiaChiKH.Text.Trim().Length == 0 ||
            txtSDTKH.Text.Trim().Length == 0 || dtpNgayMua.Text.Trim().Length == 0 ||
            txtLoaiKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "Select MAKH From KHACHHANG where MAKH=N'" + txtMaKH.Text.Trim() + "'";
            if (DACS2.Function.CheckKey(sql))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại,Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            sql = "INSERT INTO KHACHHANG VALUES (N'" + txtMaKH.Text + "', N'" + txtHTKH.Text + "',N'" + cboGTKH.Text + "',N'" + txtDiaChiKH.Text + "',N'" + txtSDTKH.Text + "',N'" + dtpNgayMua.Text + "',N'" + txtLoaiKH.Text + "')";
            DACS2.Function.RunSQL(sql); //Thực hiện câu lệnh sql
            loaddata(); //Nạp lại DataGridView
            ResetValues();
            btnXoaKH.Enabled = true;
            btnThemKH.Enabled = true;
            btnSuaKH.Enabled = true;
            btnLuuKH.Enabled = false;
            txtMaKH.Enabled = false;
        }
        //SUA KHACH HANG
        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            string sql;
            if (KHACHHANG.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHTKH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboGTKH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDiaChiKH.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSDTKH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dtpNgayMua.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtLoaiKH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE KHACHHANG SET " +
       "HOTEN=N'" + txtHTKH.Text.Trim().ToString() + "'," +
       "GIOITINHKH=N'" + cboGTKH.Text.Trim().ToString() + "'," +
       "DCHI=N'" + txtDiaChiKH.Text.Trim().ToString() + "'," +
       "SODT=N'" + txtSDTKH.Text.Trim().ToString() + "'," +
       "NGMUA=N'" + dtpNgayMua.Text.Trim().ToString() + "'," +
       "LOAIKH=N'" + txtLoaiKH.Text.Trim().ToString() + "' " + // Đảm bảo dữ liệu nhập là số để tránh lỗi
       "WHERE MAKH=N'" + txtMaKH.Text + "'";
            DACS2.Function.RunSQL(sql);
            loaddata();
            ResetValues();
        }
        //XOA KHACH HANG
        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            string sql;
            if (KHACHHANG.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE KHACHHANG WHERE MAKH=N'" + txtMaKH.Text + "'";
                DACS2.Function.RunSqlDel(sql);
                loaddata();
                ResetValues();
            }
            else
            {
                MessageBox.Show("Mã Khách Hàng Không Tồn Tại!", "Lỗi");
            }
        }
        //KHACH HANG CELL CLICK
        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvKH.CurrentRow.Index;
            txtMaKH.Text = dgvKH.Rows[i].Cells[0].Value.ToString();
            txtHTKH.Text = dgvKH.Rows[i].Cells[1].Value.ToString();
            cboGTKH.Text = dgvKH.Rows[i].Cells[2].Value.ToString();
            txtDiaChiKH.Text = dgvKH.Rows[i].Cells[3].Value.ToString();
            txtSDTKH.Text = dgvKH.Rows[i].Cells[4].Value.ToString();
            dtpNgayMua.Text = dgvKH.Rows[i].Cells[5].Value.ToString();
            txtLoaiKH.Text = dgvKH.Rows[i].Cells[6].Value.ToString();
        }
        //BO QUA KHACH HANG
        private void btnBQKH_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoaKH.Enabled = true;
            btnSuaKH.Enabled = true;
            btnThemKH.Enabled = true;
            btnBQKH.Enabled = false;
            btnLuuKH.Enabled = false;
            btnDongKH.Enabled =true;
            txtMaKH.Enabled = false;
        }

            //thêm reset value hàng
            private void ResetValuesHang()
        {
            cboMaHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void cboMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TENSP FROM MATHANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtTenHang.Text = Function.GetFieldValues(str);
            str = "SELECT GIA FROM MATHANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtDonGia.Text = Function.GetFieldValues(str);
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                txtTenKH.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select HOTEN from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtTenKH.Text = Function.GetFieldValues(str);
            str = "Select DCHI from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtDiaChi.Text = Function.GetFieldValues(str);
            str = "Select SODT from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtSDT.Text = Function.GetFieldValues(str);
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select HOTEN from NHANVIEN where MANV =N'" + cboMaNV.SelectedValue + "'";
            txtTenNV.Text = Function.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void cboSoHoaDon_DropDown(object sender, EventArgs e)
        {
            Function.FillCombo("SELECT SOHD FROM HOADON", cboSoHoaDon, "SOHD", "SOHD");
            cboSoHoaDon.SelectedIndex = -1;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MAHANG,SL FROM CTHD WHERE SOHD = N'" + txtSoHD.Text + "'";
                DataTable tblHang = Function.GetDataToTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(Function.GetFieldValues("SELECT SL FROM MATHANG WHERE MAHANG = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "UPDATE MATHANG SET SL =" + slcon + " WHERE MAHANG= N'" + tblHang.Rows[hang][0].ToString() + "'";
                    Function.RunSQL(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE CTHD WHERE SOHD=N'" + txtSoHD.Text + "'";
                Function.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE HOADON WHERE SOHD=N'" + txtSoHD.Text + "'";
                Function.RunSqlDel(sql);
                ResetValues();
                loaddataCTHD();
                btnXoaHD.Enabled = false;
                btnInHD.Enabled = false;
            }
        }

        private void dgvCTHD_DoubleClick(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (dgvCTHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvCTHD.CurrentRow.Cells["MAHANG"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvCTHD.CurrentRow.Cells["SL"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvCTHD.CurrentRow.Cells["THANHTIEN"].Value.ToString());
                sql = "DELETE CTHD WHERE SOHD=N'" + txtSoHD.Text + "' AND MAHANG = N'" + MaHangxoa + "'";
                Function.RunSQL(sql);
                // Cập nhật lại số lượng cho các mặt hàng
                sl = Convert.ToDouble(Function.GetFieldValues("SELECT SL FROM MATHANG WHERE MAHANG = N'" + MaHangxoa + "'"));
                slcon = sl + SoLuongxoa;
                sql = "UPDATE MATHANG SET SL =" + slcon + " WHERE MAHANG= N'" + MaHangxoa + "'";
                Function.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(Function.GetFieldValues("SELECT TRIGIA FROM HOADON WHERE SOHD = N'" + txtSoHD.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE HOADON SET TRIGIA =" + tongmoi + " WHERE SOHD = N'" + txtSoHD.Text + "'";
                Function.RunSQL(sql);
                txtDonGia.Text = tongmoi.ToString();
                panell.Text = "Bằng chữ: " + Function.ChuyenSoSangChuoi(double.Parse(tongmoi.ToString()));
                loaddataCTHD();
            }
        }



        private void btnDongKH_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        private void txtSDTKH_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            TextBox textBox = (TextBox)sender;
            string soDienThoai = textBox.Text + e.KeyChar; // Lấy chuỗi số điện thoại hiện tại

            // Kiểm tra điều kiện số điện thoại hợp lệ
            if (soDienThoai.Length > 10 || !soDienThoai.All(char.IsDigit) || soDienThoai.Contains("-"))
            {
                e.Handled = true; // Không cho nhập thêm ký tự nữa nếu số điện thoại không hợp lệ
            }
        }

        private void cboMaNV_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select HOTEN from NHANVIEN where MANV =N'" + cboMaNV.SelectedValue + "'";
            txtTenNV.Text = Function.GetFieldValues(str);
        }

        private void cboMaKH_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                txtTenKH.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select HOTEN from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtTenKH.Text = Function.GetFieldValues(str);
            str = "Select DCHI from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtDiaChi.Text = Function.GetFieldValues(str);
            str = "Select SODT from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtSDT.Text = Function.GetFieldValues(str);
        }

        private void cboMaHang_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboMaHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TENSP FROM MATHANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtTenHang.Text = Function.GetFieldValues(str);
            str = "SELECT GIA FROM MATHANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtDonGia.Text = Function.GetFieldValues(str);
        }





        private void cboSoHoaDon_DropDown_1(object sender, EventArgs e)
        {
            Function.FillCombo("SELECT SOHD FROM HOADON", cboSoHoaDon, "SOHD", "SOHD");
            cboSoHoaDon.SelectedIndex = -1;
        }

        private void btnXoaHD_Click_1(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MAHANG,SL FROM CTHD WHERE SOHD = N'" + txtSoHD.Text + "'";
                DataTable tblHang = Function.GetDataToTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(Function.GetFieldValues("SELECT SL FROM MATHANG WHERE MAHANG = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "UPDATE MATHANG SET SL =" + slcon + " WHERE MAHANG= N'" + tblHang.Rows[hang][0].ToString() + "'";
                    Function.RunSQL(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE CTHD WHERE SOHD=N'" + txtSoHD.Text + "'";
                Function.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE HOADON WHERE SOHD=N'" + txtSoHD.Text + "'";
                Function.RunSqlDel(sql);
                ResetValues();
                loaddataCTHD();
                btnXoaHD.Enabled = false;
                btnInHD.Enabled = false;
            }
        }

   



        private void cboMaNV_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select HOTEN from NHANVIEN where MANV =N'" + cboMaNV.SelectedValue + "'";
            txtTenNV.Text = Function.GetFieldValues(str);
        }

        private void cboMaKH_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            string str;
            if (cboMaKH.Text == "")
            {
                txtTenKH.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select HOTEN from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtTenKH.Text = Function.GetFieldValues(str);
            str = "Select DCHI from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtDiaChi.Text = Function.GetFieldValues(str);
            str = "Select SODT from KHACHHANG where MAKH = N'" + cboMaKH.SelectedValue + "'";
            txtSDT.Text = Function.GetFieldValues(str);
        }

        private void cboMaHang_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            string str;
            if (cboMaHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TENSP FROM MATHANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtTenHang.Text = Function.GetFieldValues(str);
            str = "SELECT GIA FROM MATHANG WHERE MAHANG =N'" + cboMaHang.SelectedValue + "'";
            txtDonGia.Text = Function.GetFieldValues(str);
        }

        private void cboSoHoaDon_DropDown_2(object sender, EventArgs e)
        {
            Function.FillCombo("SELECT SOHD FROM HOADON", cboSoHoaDon, "SOHD", "SOHD");
            cboSoHoaDon.SelectedIndex = -1;
        }

        private void btnXoaHD_Click_2(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MAHANG,SL FROM CTHD WHERE SOHD = N'" + txtSoHD.Text + "'";
                DataTable tblHang = Function.GetDataToTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(Function.GetFieldValues("SELECT SL FROM MATHANG WHERE MAHANG = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "UPDATE MATHANG SET SL =" + slcon + " WHERE MAHANG= N'" + tblHang.Rows[hang][0].ToString() + "'";
                    Function.RunSQL(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE CTHD WHERE SOHD=N'" + txtSoHD.Text + "'";
                Function.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE HOADON WHERE SOHD=N'" + txtSoHD.Text + "'";
                Function.RunSqlDel(sql);
                ResetValues();
                loaddataCTHD();
                btnXoaHD.Enabled = false;
                btnInHD.Enabled = false;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Cửa Hàng Máy Ảnh";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Ninh Kiều-Cần Thơ";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 0347268445";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.SOHD, a.NGHD, a.TRIGIA, b.HOTEN, b.DCHI, b.SODT, c.HOTEN FROM HOADON AS a,KHACHHANG AS b, NHANVIEN AS c WHERE a.SOHD = N'" + txtSoHD.Text + "' AND a.MAKH = b.MAKH AND a.MANV = c.MANV";
            tblThongtinHD = Function.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TENSP, a.SL, b.GIA, a.GIAMGIA, a.THANHTIEN " +
                  "FROM CTHD AS a ,MATHANG AS b WHERE a.SOHD = N'" +
                  txtSoHD.Text + "' AND a.MAHANG = b.MAHANG";
            tblThongtinHang = Function.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Function.ChuyenSoSangChuoi(double.Parse(tblThongtinHD.Rows[0][2].ToString()));
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Cần Thơ, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa Đơn Bán Hàng";
            exApp.Visible = true;
        }

        private void btnDongHD_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSoLuong_TextChanged_1(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged_1(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtSoLuong_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(cboSoHoaDon.Text == "")
     {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboSoHoaDon.Focus();
                return;
            }
            txtSoHD.Text = cboSoHoaDon.Text;
            LoadInfoHoaDon();
            loaddataCTHD();
            btnXoaHD.Enabled = true;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = true;
            cboSoHoaDon.SelectedIndex = -1;
        }

        private void btnSuaPBH_Click(object sender, EventArgs e)
        {
            string sql;
            if (PHIEUBH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboSHDPBH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTGBH.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMPBH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn phải nhập mã phiếu bảo hành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE PHIEUBAOHANH SET " +
            "THOIGIANBH=N'" + txtTGBH.Text.Trim().ToString() + "' " +
            "WHERE MAPBH=N'" + txtMPBH.Text + "'";
            DACS2.Function.RunSQL(sql);
            loaddata();
            ResetValues();
        }

        private void btnLuuPBH_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMPBH.Text.Trim().Length == 0 || txtMaHang.Text.Trim().Length == 0 || cboSHDPBH.Text.Trim().Length == 0 || txtTGBH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "Select MAPBH From PHIEUBAOHANH where MAPBH=N'" + txtMPBH.Text.Trim() + "'";
            if (DACS2.Function.CheckKey(sql))
            {
                MessageBox.Show("Mã phiếu bảo hành  này đã tồn tại,Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMPBH.Focus();
                return;
            }

            sql = "INSERT INTO PHIEUBAOHANH VALUES (N'" + txtMPBH.Text + "', N'" + txtMaHang.Text + "',N'" + txtTGBH.Text + "',N'" + cboSHDPBH.Text + "' )";
            DACS2.Function.RunSQL(sql); //Thực hiện câu lệnh sql
            loaddata(); //Nạp lại DataGridView
            ResetValues();
            btnXoaPBH.Enabled = true;
            btnThemPBH.Enabled = true;
            btnSuaPBH.Enabled = true;
            btnLuuPBH.Enabled = false;
            txtMPBH.Enabled = false;
        }

        private void btnXoaPBH_Click(object sender, EventArgs e)
        {
            string sql;
            if (PHIEUBH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMPBH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE PHIEUBAOHANH WHERE MAPBH=N'" + txtMPBH.Text + "'";
                DACS2.Function.RunSqlDel(sql);
                loaddata();
                ResetValues();
            }
            else
            {
                MessageBox.Show("Mã Khách Hàng Không Tồn Tại!", "Lỗi");
            }
        }

        private void txtSDTKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            TextBox textBox = (TextBox)sender;
            string soDienThoai = textBox.Text + e.KeyChar; // Lấy chuỗi số điện thoại hiện tại

            // Kiểm tra điều kiện số điện thoại hợp lệ
            if (soDienThoai.Length > 10 || !soDienThoai.All(char.IsDigit) || soDienThoai.Contains("-"))
            {
                e.Handled = true; // Không cho nhập thêm ký tự nữa nếu số điện thoại không hợp lệ
            }
        }

        private void btnDongPBH_Click(object sender, EventArgs e)
        {
            Close();
            DangNhap DN = new DangNhap();
            DN.ShowDialog();
        }

        private void btnBoQuaPBH_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoaPBH.Enabled = true;
            btnSuaPBH.Enabled = true;
            btnThemPBH.Enabled = true;
            btnBoQuaPBH.Enabled = false;
            btnLuuPBH.Enabled = false;
            txtMPBH.Enabled = false;
        }

        private void btnDongKH_Click_1(object sender, EventArgs e)
        {
            Close();
            DangNhap DN = new DangNhap();
            DN.ShowDialog();
        }

        private void dgvCTHD_DoubleClick_1(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (CHITIETHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvCTHD.CurrentRow.Cells["MAHANG"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvCTHD.CurrentRow.Cells["SL"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvCTHD.CurrentRow.Cells["THANHTIEN"].Value.ToString());
                sql = "DELETE CTHD WHERE SOHD=N'" + txtSoHD.Text + "' AND MAHANG = N'" + MaHangxoa + "'";
                Function.RunSQL(sql);
                // Cập nhật lại số lượng cho các mặt hàng
                sl = Convert.ToDouble(Function.GetFieldValues("SELECT SL FROM MATHANG WHERE MAHANG = N'" + MaHangxoa + "'"));
                slcon = sl + SoLuongxoa;
                sql = "UPDATE MATHANG SET SL =" + slcon + " WHERE MAHANG= N'" + MaHangxoa + "'";
                Function.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(Function.GetFieldValues("SELECT TRIGIA FROM HOADON WHERE SOHD = N'" + txtSoHD.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE HOADON SET TRIGIA =" + tongmoi + " WHERE SOHD = N'" + txtSoHD.Text + "'";
                Function.RunSQL(sql);
                txtTriGia.Text = tongmoi.ToString();
                lblBangChu.Text = "Bằng chữ: " + Function.ChuyenSoSangChuoi(double.Parse(tongmoi.ToString()));
                loaddataCTHD();
            }
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT SOHD FROM HOADON WHERE SOHD=N'" + txtSoHD.Text + "'";
            if (!Function.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                if (dtpNgayLapHD.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpNgayLapHD.Focus();
                    return;
                }
                if (cboMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNV.Focus();
                    return;
                }
                if (cboMaKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKH.Focus();
                    return;
                }
                sql = "INSERT INTO HOADON(SOHD, NGHD, MAKH, MANV, TRIGIA) VALUES (N'" + txtSoHD.Text.Trim() + "','" +
                        Function.ConvertDateTime(dtpNgayLapHD.Text.Trim()) + "',N'" + cboMaKH.SelectedValue + "',N'" +
                        cboMaNV.SelectedValue + "'," + txtTriGia.Text + ")";
                Function.RunSQL(sql);
            }
            // Lưu thông tin của các mặt hàng
            if (cboMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHang.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "SELECT MAHANG FROM CTHD WHERE MAHANG=N'" + cboMaHang.SelectedValue + "' AND SOHD = N'" + txtSoHD.Text.Trim() + "'";
            if (Function.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cboMaHang.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Function.GetFieldValues("SELECT SL FROM MATHANG WHERE MAHANG = N'" + cboMaHang.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "INSERT INTO CTHD(SOHD,MAHANG,SL,DONGIA, GIAMGIA,THANHTIEN) VALUES(N'" + txtSoHD.Text.Trim() + "',N'" + cboMaHang.SelectedValue + "'," + txtSoLuong.Text + "," + txtDonGia.Text + "," + txtGiamGia.Text + "," + txtThanhTien.Text + ")";
            Function.RunSQL(sql);
            loaddataCTHD();
            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE MATHANG SET SL =" + SLcon + " WHERE MAHANG= N'" + cboMaHang.SelectedValue + "'";
            Function.RunSQL(sql);
            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Function.GetFieldValues("SELECT TRIGIA FROM HOADON WHERE SOHD = N'" + txtSoHD.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE HOADON SET TRIGIA =" + Tongmoi + " WHERE SOHD = N'" + txtSoHD.Text + "'";
            Function.RunSQL(sql);
            txtTriGia.Text = Tongmoi.ToString();
            lblBangChu.Text = "Bằng chữ: " + Function.ChuyenSoSangChuoi(double.Parse(Tongmoi.ToString()));
            ResetValuesHang();
            btnXoaHD.Enabled = true;
            btnThemHD.Enabled = true;
            btnInHD.Enabled = true;
        }

        private void cboSHDPBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboSHDPBH.Text == "")
                txtMaHang.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select MAHANG from CTHD where SOHD =N'" + cboSHDPBH.SelectedValue + "'";
            txtMaHang.Text = Function.GetFieldValues(str);
        }

        private void dgvPBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvPBH.CurrentRow.Index;
            txtMPBH.Text = dgvPBH.Rows[i].Cells[0].Value.ToString();
            txtMaHang.Text = dgvPBH.Rows[i].Cells[1].Value.ToString();
            txtTGBH.Text = dgvPBH.Rows[i].Cells[2].Value.ToString();
            cboSHDPBH.Text = dgvPBH.Rows[i].Cells[3].Value.ToString();
            btnXoaPBH.Enabled = true;
        }
    }
}
