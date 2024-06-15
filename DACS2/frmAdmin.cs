using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DACS2
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            dtpTu.Value = DateTime.Today;
            dtpDen.Value = DateTime.Today;
            UpdateReport();
        }
        DataTable NHANVIEN;
        DataTable SANPHAM;
        DataTable NHACC;
        DataTable LOAISP;

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            DACS2.Function.Connect();
            txtMNV.Enabled = false;
            txtMH.Enabled = false;
            txtMNCC.Enabled = false;
            txtML.Enabled = false;
            txtMaLoai.Enabled = false;
            btnLuu.Enabled = false;
            btnLuuSP.Enabled = false;
            btnLuuNCC.Enabled = false;
            loaddata();
            dgvNV.Columns[0].HeaderText = "Mã Nhân Viên";
            dgvNV.Columns[1].HeaderText = "Tên Nhân Viên";
            dgvNV.Columns[2].HeaderText = "Giới Tính";
            dgvNV.Columns[3].HeaderText = "Số Điện Thoại";
            dgvNV.Columns[4].HeaderText = "Ngày vào Làm";
            dgvNV.Columns[5].HeaderText = "Loại Nhân Viên";
            dgvNV.Columns[6].HeaderText = "Lương";


            dgvNV.Columns[0].Width = 130;
            dgvNV.Columns[1].Width = 130;
            dgvNV.Columns[2].Width = 110;
            dgvNV.Columns[3].Width = 100;
            dgvNV.Columns[4].Width = 140;
            dgvNV.Columns[5].Width = 150;
            dgvNV.Columns[6].Width = 100;
            //dgvSP
            dgvSP.Columns[0].HeaderText = "Mã Hàng";
            dgvSP.Columns[1].HeaderText = "Mã Loại";
            dgvSP.Columns[2].HeaderText = "Mã NCC";
            dgvSP.Columns[3].HeaderText = "Tên Sản Phẩm";
            dgvSP.Columns[4].HeaderText = "Nước Sản Xuất";
            dgvSP.Columns[5].HeaderText = "Giá";
            dgvSP.Columns[6].HeaderText = "Ảnh";
            dgvSP.Columns[7].HeaderText = "Số Lượng";
            dgvSP.Columns[8].HeaderText = "Ghi Chú";

            dgvSP.Columns[0].Width = 130;
            dgvSP.Columns[1].Width = 130;
            dgvSP.Columns[2].Width = 110;
            dgvSP.Columns[3].Width = 110;
            dgvSP.Columns[4].Width = 140;
            dgvSP.Columns[5].Width = 70;
            dgvSP.Columns[6].Width = 70;
            dgvSP.Columns[5].Width = 70;
            dgvSP.Columns[6].Width = 70;

            dgvLSP.Columns[0].HeaderText = "Mã Loại";
            dgvLSP.Columns[1].HeaderText = "Tên Loại";


            dgvLSP.Columns[0].Width = 300;
            dgvLSP.Columns[1].Width = 300;
            //dgvNhaCC
            dgvNCC.Columns[0].HeaderText = "Mã Nhà CC";
            dgvNCC.Columns[1].HeaderText = "Tên Nhà CC";
            dgvNCC.Columns[2].HeaderText = "Tên Giao Dịch";
            dgvNCC.Columns[3].HeaderText = "Địa Chỉ";
            dgvNCC.Columns[4].HeaderText = "SDT";
            dgvNCC.Columns[5].HeaderText = "Email";

            dgvNCC.Columns[0].Width = 130;
            dgvNCC.Columns[1].Width = 130;
            dgvNCC.Columns[2].Width = 110;
            dgvNCC.Columns[3].Width = 110;
            dgvNCC.Columns[4].Width = 170;
            dgvNCC.Columns[5].Width = 150;

            Function.FillCombo("SELECT MANV, HOTEN FROM NHANVIEN", cboMANV, "MANV", "HOTEN");
            cboMANV.SelectedIndex = -1;
            Function.FillCombo("SELECT MAHANG, TENSP FROM MATHANG", cboMSP, "MAHANG", "TENSP");
            cboMSP.SelectedIndex = -1;
        }

        void loaddata()
        {
            string sql, sql1, sql2, sql3;
            sql = "SELECT *  FROM NHANVIEN";
            NHANVIEN = DACS2.Function.GetDataToTable(sql);
            dgvNV.DataSource = NHANVIEN;
            dgvNV.AllowUserToAddRows = false;
            dgvNV.EditMode = DataGridViewEditMode.EditProgrammatically;
            sql1 = "SELECT *  FROM MATHANG";
            SANPHAM = DACS2.Function.GetDataToTable(sql1);
            dgvSP.DataSource = SANPHAM;
            dgvSP.AllowUserToAddRows = false;
            dgvSP.EditMode = DataGridViewEditMode.EditProgrammatically;
            sql2 = "SELECT *  FROM NHACC";
            NHACC = DACS2.Function.GetDataToTable(sql2);
            dgvNCC.DataSource = NHACC;
            dgvNCC.AllowUserToAddRows = false;
            dgvNCC.EditMode = DataGridViewEditMode.EditProgrammatically;
            sql3 = "SELECT *  FROM LOAIHANG";
            LOAISP = DACS2.Function.GetDataToTable(sql3);
            dgvLSP.DataSource = LOAISP;
            dgvLSP.AllowUserToAddRows = false;
            dgvLSP.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnBoQuaSP.Enabled = true;
            ResetValue(); //Xoá trắng các textbox
            txtMNV.Enabled = true; //cho phép nhập mới
            txtMNV.Focus();
            loaddata();
        }
        private void ResetValue()
        {
            txtMNV.Text = "";
            txtHT.Text = "";
            cboGT.Text = "";
            txtSDT.Text = "";
            dtpNVL.Text = "";
            txtLNV.Text = "";
            txtLuong.Text = "";

            txtMH.Text = "";
            txtML.Text = "";
            txtMNCC.Text = "";
            txtTSP.Text = "";
            txtNSX.Text = "";
            txtGia.Text = "";
            txtSL.Text = "";
            txtAnh.Text = "";
            txtGhiChu.Text = "";

            txtMaLoai.Text = "";
            txtTenLoai.Text = "";

            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtTenGD.Text = "";
            txtDiaChiNCC.Text = "";
            txtSDTNCC.Text = "";
            txtEmailNCC.Text = "";
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            btnSuaSP.Enabled = false;
            btnXoaSP.Enabled = false;
            btnLuuSP.Enabled = true;
            btnThemSP.Enabled = false;
            btnBoQuaSP.Enabled = true;
            ResetValue(); //Xoá trắng các textbox
            txtMH.Enabled = true; //cho phép nhập mới
            txtML.Enabled = true;
            txtMaLoai.Enabled = true;
            txtMNCC.Enabled = true;
            txtML.Focus();
            txtMaLoai.Focus();
            txtMH.Focus();
            loaddata();

        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            btnSuaNCC.Enabled = false;
            btnXoaNCC.Enabled = false;
            btnLuuNCC.Enabled = true;
            btnThemNCC.Enabled = false;
            btnBoQuaNCC.Enabled = true;
            ResetValue(); //Xoá trắng các textbox
            txtMaNCC.Enabled = true; //cho phép nhập mới
            txtMaNCC.Focus();
            loaddata();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (NHANVIEN.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMNV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE NHANVIEN WHERE MANV=N'" + txtMNV.Text + "'";
                DACS2.Function.RunSqlDel(sql);
                loaddata();
                ResetValue();
            }
            else
            {
                MessageBox.Show("Mã Nhân Viên Không Tồn Tại!", "Lỗi");
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            string sql;
            if (ConditionCheck())
            {
                if (SANPHAM.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtMH.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "DELETE MATHANG WHERE MAHANG=N'" + txtMH.Text + "'";
                    DACS2.Function.RunSqlDel(sql);
                    loaddata();
                    ResetValue();
                }
                else
                {
                    MessageBox.Show("Mã Sản Phẩm Không Tồn Tại!", "Lỗi");
                }
            }
            else
            {
                if (LOAISP.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtMaLoai.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "DELETE LOAIHANG WHERE MALOAI=N'" + txtMaLoai.Text + "'";
                    DACS2.Function.RunSqlDel(sql);
                    loaddata();
                    ResetValue();
                }
                else
                {
                    MessageBox.Show("Mã Loại Hàng Không Tồn Tại!", "Lỗi");
                }
            }
        }
        private bool ConditionCheck()
        {
            if (!string.IsNullOrEmpty(txtMH.Text))
            {
                return true; // Trả về true nếu điều kiện được thoả mãn để thêm dữ liệu vào bảng MATHANG
            }
            else
            {
                return false; // Trả về false nếu không thoả mãn điều kiện để thêm vào bảng MATHANG
            }
        }

        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            string sql;
            if (NHACC.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNCC.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE NHACC WHERE MANCC=N'" + txtMaNCC.Text + "'";
                DACS2.Function.RunSqlDel(sql);
                loaddata();
                ResetValue();
            }
            else
            {
                MessageBox.Show("Mã Nhà Cung Cấp Không Tồn Tại!", "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (NHANVIEN.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMNV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHT.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboGT.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSDT.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dtpNVL.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtLNV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtLuong.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE NHANVIEN SET " +
       "HOTEN=N'" + txtHT.Text.Trim().ToString() + "'," +
       "GIOITINHNV=N'" + cboGT.Text.Trim().ToString() + "'," +
       "SODT=N'" + txtSDT.Text.Trim().ToString() + "'," +
       "NGVL=N'" + dtpNVL.Value+ "'," +
       "LOAINV=N'" + txtLNV.Text.Trim().ToString() + "', " + // Đảm bảo dữ liệu nhập là số để tránh lỗi
       "LUONG=N'" + txtLuong.Text.Trim().ToString() + "' " +
       "WHERE MANV=N'" + txtMNV.Text + "'";
            DACS2.Function.RunSQL(sql);
            loaddata();
            ResetValue();
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (ConditionCheck())
            {
                if (SANPHAM.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtMH.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtML.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtMNCC.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtTSP.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
                {
                    MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtNSX.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtGia.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtAnh.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAnh.Focus();
                    return;
                }
                if (txtGhiChu.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGhiChu.Focus();
                    return;
                }
                if (txtSL.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập vào số lượng cho mặt hàng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSL.Focus();
                    return;
                }

                sql = "UPDATE MATHANG SET " +
        "MALOAI=N'" + txtML.Text.Trim().ToString() + "'," +
        "MANCC=N'" + txtMNCC.Text.Trim().ToString() + "'," +
        "TENSP=N'" + txtTSP.Text.Trim().ToString() + "'," +
        "NUOCSX=N'" + txtNSX.Text.Trim().ToString() + "'," +
        "GIA=N'" + txtGia.Text.Trim().ToString() + "'," +
        "ANH=N'" + txtAnh.Text.Trim().ToString() + "'," +
        "GHICHU=N'" + txtGhiChu.Text.Trim().ToString() + "'," +
        "SL=N'" + txtSL.Text.Trim().ToString() + "'" +// Đảm bảo dữ liệu nhập là số để tránh lỗi
        "WHERE MAHANG=N'" + txtMH.Text + "'";
                DACS2.Function.RunSQL(sql);
                loaddata();
                ResetValue();
            }
            else
            {
                if (LOAISP.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtMaLoai.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtTenLoai.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                sql = "UPDATE LOAIHANG SET TENLOAI=N'" +
             txtTenLoai.Text.ToString() +
              "' WHERE MALOAI=N'" + txtMaLoai.Text + "'";
                DACS2.Function.RunSQL(sql);
                loaddata();
                ResetValue();
                btnBoQuaSP.Enabled = false;
            }
        }

        private void btnSuaNCC_Click(object sender, EventArgs e)
        {
            string sql;
            if (NHACC.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNCC.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNCC.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenGD.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDiaChiNCC.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSDTNCC.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtEmailNCC.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE NHACC SET " +
      "TENNCC=N'" + txtTenNCC.Text.Trim().ToString() + "'," +
      "TENGIAODICH=N'" + txtTenGD.Text.Trim().ToString() + "'," +
      "DIACHI=N'" + txtDiaChiNCC.Text.Trim().ToString() + "'," +
      "DIENTHOAI=N'" + txtSDTNCC.Text.Trim().ToString() + "'," +
      "EMAIL=N'" + txtEmailNCC.Text.Trim().ToString() + "' " + // Đảm bảo dữ liệu nhập là số để tránh lỗi
      "WHERE MANCC=N'" + txtMaNCC.Text + "'";
            DACS2.Function.RunSQL(sql);
            loaddata();
            ResetValue();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMNV.Text.Trim().Length == 0 || txtHT.Text.Trim().Length == 0 ||
            cboGT.Text.Trim().Length == 0 || txtSDT.Text.Trim().Length == 0 ||
            dtpNVL.Text.Trim().Length == 0 || txtLNV.Text.Trim().Length == 0 || txtLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "Select MANV From NHANVIEN where MANV=N'" + txtMNV.Text.Trim() + "'";
            if (DACS2.Function.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại,Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMNV.Focus();
                return;
            }
            sql = "INSERT INTO NHANVIEN VALUES (N'" + txtMNV.Text + "',N'" + txtHT.Text + "',N'" + cboGT.Text + "',N'" + txtSDT.Text + "',N'" + dtpNVL.Value + "',N'" + txtLNV.Text + "',N'" + txtLuong.Text + "')";
            DACS2.Function.RunSQL(sql); //Thực hiện câu lệnh sql
            loaddata(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMNV.Enabled = false;
        }

        private void btnLuuSP_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (ConditionCheck())
            {
                if (txtMH.Text.Trim().Length == 0
            || txtML.Text.Trim().Length == 0
            || txtMNCC.Text.Trim().Length == 0
            || txtTSP.Text.Trim().Length == 0
            || txtGia.Text.Trim().Length == 0 || txtAnh.Text.Trim().Length == 0 || txtGhiChu.Text.Trim().Length == 0 || txtSL.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                sql = "Select MAHANG From MATHANG where MAHANG=N'" + txtMH.Text.Trim() + "'";
                if (DACS2.Function.CheckKey(sql))
                {
                    MessageBox.Show("Mã hàng này đã tồn tại,Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMH.Focus();
                    return;
                }

                sql = "INSERT INTO MATHANG VALUES (N'" + txtMH.Text +
                    "', N'" + txtML.Text +
                    "',N'" + txtMNCC.Text +
                    "',N'" + txtTSP.Text +
                    "',N'" + txtNSX.Text +
                    "',N'" + txtGia.Text +
                    "',N'" + txtAnh.Text +
                    "',N'" + txtGhiChu.Text + 
                    "',N'" + txtSL.Text + "')";
                DACS2.Function.RunSQL(sql); //Thực hiện câu lệnh sql
                loaddata(); //Nạp lại DataGridView
                ResetValue();
            }
            else
            {
                sql = "Select MALOAI from LOAIHANG where MALOAI=N'" + txtMaLoai.Text.Trim() + "'";
                if (DACS2.Function.CheckKey(sql))
                {
                    MessageBox.Show("Mã loại này đã tồn tại,Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaLoai.Focus();
                    return;
                }
                sql = "INSERT INTO LOAIHANG VALUES(N'" + txtMaLoai.Text + "',N'" + txtTenLoai.Text + "')";
                DACS2.Function.RunSQL(sql);
                loaddata();
                ResetValue();
            }
            btnXoaSP.Enabled = true;
            btnThemSP.Enabled = true;
            btnSuaSP.Enabled = true;
            btnLuuSP.Enabled = false;
            txtMH.Enabled = false;
            btnLuuSP.Enabled = false;
        }

        private void btnLuuNCC_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNCC.Text.Trim().Length == 0 || txtTenNCC.Text.Trim().Length == 0 ||
            txtTenGD.Text.Trim().Length == 0 || txtDiaChiNCC.Text.Trim().Length == 0 ||
            txtSDTNCC.Text.Trim().Length == 0 || txtEmailNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "Select MANCC From NHACC where MANCC=N'" + txtMaNCC.Text.Trim() + "'";
            if (DACS2.Function.CheckKey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này đã tồn tại,Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCC.Focus();
                return;
            }

            sql = "INSERT INTO NHACC VALUES (N'" + txtMaNCC.Text + "', N'" + txtTenNCC.Text + "',N'" + txtTenGD.Text + "',N'" + txtDiaChiNCC.Text + "',N'" + txtSDTNCC.Text + "',N'" + txtEmailNCC.Text + "')";
            DACS2.Function.RunSQL(sql); //Thực hiện câu lệnh sql
            loaddata(); //Nạp lại DataGridView
            ResetValue();
            btnXoaNCC.Enabled = true;
            btnThemNCC.Enabled = true;
            btnSuaNCC.Enabled = true;
            btnLuuNCC.Enabled = false;
            txtMaNCC.Enabled = false;
            
        }

        private void dgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMNV.Focus();
                return;
            }
            if (NHANVIEN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int i;
            i = dgvNV.CurrentRow.Index;
            txtMNV.Text = dgvNV.Rows[i].Cells[0].Value.ToString();
            txtHT.Text = dgvNV.Rows[i].Cells[1].Value.ToString();
            cboGT.Text = dgvNV.Rows[i].Cells[2].Value.ToString();
            txtSDT.Text = dgvNV.Rows[i].Cells[3].Value.ToString();
            dtpNVL.Text = dgvNV.Rows[i].Cells[4].Value.ToString();
            txtLNV.Text = dgvNV.Rows[i].Cells[5].Value.ToString();
            txtLuong.Text= dgvNV.Rows[i].Cells[6].Value.ToString();
        }


        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMNCC.Focus();
                return;
            }
            if (NHACC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int j;
            j = dgvNCC.CurrentRow.Index;

            int i;
            i = dgvNCC.CurrentRow.Index;
            txtMaNCC.Text = dgvNCC.Rows[i].Cells[0].Value.ToString();
            txtTenNCC.Text = dgvNCC.Rows[i].Cells[1].Value.ToString();
            txtTenGD.Text = dgvNCC.Rows[i].Cells[2].Value.ToString();
            txtDiaChiNCC.Text = dgvNCC.Rows[i].Cells[3].Value.ToString();
            txtSDTNCC.Text = dgvNCC.Rows[i].Cells[4].Value.ToString();
            txtEmailNCC.Text = dgvNCC.Rows[i].Cells[5].Value.ToString();
        }


        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
                txtGhiChu.Text = dlgOpen.FileName;
            }
        }
        private void btnBoQuaNCC_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnXoaNCC.Enabled = true;
            btnSuaNCC.Enabled = true;
            btnThemNCC.Enabled = true;
            btnBoQuaNCC.Enabled = false;
            btnLuuNCC.Enabled = false;
            txtMH.Enabled = false;
        }

        private void btnBoQuaSP_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnXoaSP.Enabled = true;
            btnSuaSP.Enabled = true;
            btnThemSP.Enabled = true;
            btnBoQuaSP.Enabled = false;
            btnLuuSP.Enabled = false;
            txtMH.Enabled = false;
        }

        private void btnBQ_Click_1(object sender, EventArgs e)
        {
            ResetValue();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBQ.Enabled = false;
            btnLuu.Enabled = false;
            txtMNV.Enabled = false;
        }

        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMH.Focus();
                return;
            }
            if (SANPHAM.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int i;
            i = dgvSP.CurrentRow.Index;
            txtMH.Text = dgvSP.Rows[i].Cells[0].Value.ToString();
            txtML.Text = dgvSP.Rows[i].Cells[1].Value.ToString();
            txtMNCC.Text = dgvSP.Rows[i].Cells[2].Value.ToString();
            txtTSP.Text = dgvSP.Rows[i].Cells[3].Value.ToString();
            txtNSX.Text = dgvSP.Rows[i].Cells[4].Value.ToString();
            txtGia.Text = dgvSP.Rows[i].Cells[5].Value.ToString();
            txtAnh.Text = dgvSP.Rows[i].Cells[6].Value.ToString();
            txtGhiChu.Text = dgvSP.Rows[i].Cells[7].Value.ToString();
            txtSL.Text = dgvSP.Rows[i].Cells[8].Value.ToString();
            sql = "SELECT ANH FROM MATHANG WHERE MAHANG=N'" + txtMH.Text + "'";
            txtAnh.Text = DACS2.Function.GetFieldValues(sql);
            if (!string.IsNullOrEmpty(txtAnh.Text) && File.Exists(txtAnh.Text))
            {
                pictureBox1.Image = Image.FromFile(txtAnh.Text);
            }
            string sqlnote;
            sqlnote = "SELECT GHICHU FROM MATHANG WHERE MAHANG = N'" + txtMH.Text + "'";
            txtGhiChu.Text = DACS2.Function.GetFieldValues(sqlnote);
            btnSuaSP.Enabled = true;
            btnXoaSP.Enabled = true;
            btnBoQuaSP.Enabled = true;
        }

        private void btnDongSP_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDongNCC_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvLSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {  
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoai.Focus();
                return;
            }
            if (LOAISP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int j;
            j = dgvLSP.CurrentRow.Index;
            txtMaLoai.Text = dgvLSP.Rows[j].Cells[0].Value.ToString();
            txtTenLoai.Text = dgvLSP.Rows[j].Cells[1].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=MSI\\MSI;Initial Catalog=DA_QLBMT;Integrated Security=True";
            string searchText = txtTuKhoa.Text.Trim();
            string query = "SELECT * FROM MATHANG WHERE MAHANG LIKE @SearchText OR MALOAI LIKE @SearchText OR MANCC LIKE @SearchText";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgvLSP.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSDTNCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            TextBox textBox = (TextBox)sender;
            string soDienThoai = textBox.Text + e.KeyChar; // Lấy chuỗi số điện thoại hiện tại

            // Kiểm tra điều kiện số điện thoại hợp lệ
            if (soDienThoai.Length > 10 || !soDienThoai.All(char.IsDigit) || soDienThoai.Contains("-"))
            {
                e.Handled = true; // Không cho nhập thêm ký tự nữa nếu số điện thoại không hợp lệ
            }
        }
        private void UpdateReport()
        {
            Function.Connect();

            string query = @"
SELECT HD.SOHD, HD.NGHD, NV.HOTEN, MH.TENSP, CTHD.SL, CTHD.thanhtien
FROM HOADON AS HD
JOIN CTHD AS CTHD ON HD.SOHD = CTHD.SOHD
JOIN NHANVIEN AS NV ON HD.MANV = NV.MANV
JOIN MATHANG AS MH ON CTHD.MAHANG = MH.MAHANG
WHERE HD.NGHD BETWEEN @FromDate AND @ToDate";

            if (cboMANV.SelectedValue != null && cboMANV.SelectedValue.ToString() != "")
            {
                query += " AND HD.MANV = @EmployeeID";
            }

            if (cboMSP.SelectedValue != null && cboMSP.SelectedValue.ToString() != "")
            {
                query += " AND CTHD.MAHANG = @ProductID";
            }

            SqlCommand cmd = new SqlCommand(query, Function.Con);
            cmd.Parameters.AddWithValue("@FromDate", dtpTu.Value);
            cmd.Parameters.AddWithValue("@ToDate", dtpDen.Value);

            if (cboMANV.SelectedValue != null && cboMANV.SelectedValue.ToString() != "")
            {
                cmd.Parameters.AddWithValue("@EmployeeID", cboMANV.SelectedValue);
            }

            if (cboMSP.SelectedValue != null && cboMSP.SelectedValue.ToString() != "")
            {
                cmd.Parameters.AddWithValue("@ProductID", cboMSP.SelectedValue);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dgvDH.DataSource = dataTable;

            // Tính tổng số lượng và tổng tiền
            var totalQuantity = dataTable.AsEnumerable().Sum(row => row.Field<int>("SL"));
            var totalAmount = dataTable.AsEnumerable().Sum(row =>
            {
                if (row["thanhtien"] != DBNull.Value)
                {
                    return Convert.ToDecimal(row["thanhtien"]);
                }
                else
                {
                    return 0m;
                }
            });

            txtSL1.Text = totalQuantity.ToString();
            txtTT.Text = totalAmount.ToString("N0");

            Function.Disconnect();
        }

        private void dtpTu_ValueChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void dtpDen_ValueChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void cboMANV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMANV.SelectedItem != null)
                {
                    string str = "SELECT HOTEN FROM NHANVIEN WHERE MANV = N'" + cboMANV.SelectedValue + "'";
                    string employeeName = Function.GetFieldValues1(str);
                    if (!string.IsNullOrEmpty(employeeName))
                    {
                        cboMANV.Text = employeeName;
                    }
                    else
                    {
                        cboMANV.Text = "Tên nhân viên không tìm thấy";
                    }
                }
                else
                {
                    cboMANV.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy tên nhân viên: " + ex.Message);
            }
        }

        private void cboMSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMSP.SelectedItem != null)
                {
                    string str = "SELECT TENSP FROM MATHANG WHERE MAHANG = N'" + cboMSP.SelectedValue + "'";
                    string productName = Function.GetFieldValues1(str);
                    if (!string.IsNullOrEmpty(productName))
                    {
                        cboMSP.Text = productName;
                    }
                    else
                    {
                        cboMSP.Text = "Tên sản phẩm không tìm thấy";
                    }
                }
                else
                {
                    cboMSP.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy tên sản phẩm: " + ex.Message);
            }
        }

        private void btnDongDoanhThu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}    