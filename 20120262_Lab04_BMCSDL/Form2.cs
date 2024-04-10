using _20120262_Lab04_BMCSDL.DAO;
using _20120262_Lab04_BMCSDL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20120262_Lab04_BMCSDL
{
    public partial class Form2 : Form
    {
        private bool TextBoxMatKhauChanged = false;
        private List<DTO_NhanVien> dsNhanVien;
        private int modeGhiLuu = 1;//1-Khong lam gi, 2-Them, 3-Sua
        public Form2()
        {
            InitializeComponent();
            LoadDSNhanVien();
        }
        public void ReloadData()
        {
            dataGridView1.DataSource = null;
            LoadDSNhanVien();
        }
        private void btn_XoaNV_Click(object sender, EventArgs e)
        {
            String maNV = txt_maNV.Text.ToString();

            try
            {
                DAO_NhanVien.get_Instance().DelNhanVien(dsNhanVien[dataGridView1.CurrentCell.RowIndex]);
                ReloadData();
            }
            catch
            {
                MessageBox.Show("Danh sách nhân viên trống");
                ClearAllTextBox();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private String decryptedLuong(byte[] luongNV)
        {
            byte[] decryptluong = AES.Decrypt(luongNV);
            String Decrypted_Luong;
            if (decryptluong == null || luongNV == null) {
                Decrypted_Luong = "Không thể thực hiện Decrypt !!!";
            }
            else
            {
                Decrypted_Luong = BitConverter.ToUInt32(decryptluong, 0).ToString();
            }
            return Decrypted_Luong;
        }
        private DataTable ListToDataTable(List<String> col,List<DTO_NhanVien> dsNhanVien)
        {
            DataTable dt = new DataTable();
            foreach(String s in col)
            {
                dt.Columns.Add(s);
            }

            if(dsNhanVien.Count == 0)
            {
                return dt;
            }
            foreach(DTO_NhanVien nv in dsNhanVien)
            {
                dt.Rows.Add(new object[] {nv.maNV,nv.hotenNV,nv.emailNV,decryptedLuong(nv.Luong),nv.unameNV,Utilities.ByteToItsString(nv.passNV)});
            }
            return dt;
        }
        public void FullRowSelectedClick(int index)
        {
            try
            {

                //Ma nhan vien
                txt_maNV.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();

                //Ho ten
                txt_Hoten.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();

                //Email
                txt_Email.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();

                //Luong
                txt_Luong.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                //Ten DN
                txt_Uname.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();

                //MatKhau
                txt_MatKhau.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();

            }
            catch (Exception)
            {
                //Ma nhan vien
                txt_maNV.Text = "".ToString();

                //Ho ten
                txt_Hoten.Text = "".ToString();

                //Email
                txt_Email.Text = "".ToString();

                //Luong
                txt_Luong.Text = "".ToString();
                //Ten DN
                txt_Uname.Text = "".ToString();

                //MatKhau
                txt_MatKhau.Text = "".ToString();
                throw;
            }
            TextBoxMatKhauChanged = false;

        }
        public void SetTextBoxReadOnly()
        {
            txt_maNV.ReadOnly = true;
            txt_Hoten.ReadOnly = true;
            txt_Email.ReadOnly = true;
            txt_Luong.ReadOnly = true;
            txt_Uname.ReadOnly = true;
            txt_MatKhau.ReadOnly = true;
        }
        public void SetTextBoxEnableEdit()
        {
            txt_maNV.ReadOnly = false;
            txt_Hoten.ReadOnly = false;
            txt_Email.ReadOnly = false;
            txt_Luong.ReadOnly = false;
            txt_Uname.ReadOnly = false;
            txt_MatKhau.ReadOnly = false;
        }
        public void ClearAllTextBox()
        {
            txt_maNV.Clear();
            txt_Hoten.Clear(); 
            txt_Email.Clear(); 
            txt_Luong.Clear(); 
            txt_Uname.Clear(); 
            txt_MatKhau.Clear(); 
        }
        public void LoadDSNhanVien()
        {
            dsNhanVien = DAO_NhanVien.get_Instance().get_List();

            DataTable dt = ListToDataTable(new List<string>() { "MÃ NHÂN VIÊN", "HỌ TÊN", "EMAIL", "LƯƠNG", "TÊN ĐĂNG NHẬP", "MẬT KHẨU" }, dsNhanVien);
            dataGridView1.DataSource = dt;
            dataGridView1.Update();
            dataGridView1.Refresh();
            FullRowSelectedClick(0);
            SetTextBoxReadOnly();

        }
        private void ModifedNhanVien(int mode)
        {
            int index;
            if (dsNhanVien.Count == 0)
            {
                index = 0;
            }
            else
                index = dataGridView1.CurrentCell.RowIndex;

            DTO_NhanVien tmpNV = new DTO_NhanVien();

            tmpNV.maNV = txt_maNV.Text.ToString();
            tmpNV.hotenNV = txt_Hoten.Text.ToString();
            tmpNV.unameNV = txt_Uname.Text.ToString();
            tmpNV.emailNV = txt_Email.Text.ToString();

            //Encrypt Luong
            UInt32 luongtoEnc;
            try
            {
                luongtoEnc = UInt32.Parse((txt_Luong.Text.ToString()));
            }
            catch
            {
                MessageBox.Show("Nhâp đúng định dạng số của lương");
                return;
            }
            tmpNV.Luong = AES.Encrypt(BitConverter.GetBytes(luongtoEnc));

            //Encrypt Mật khẩu nhân viên
            String matkhau = txt_MatKhau.Text.ToString();
            tmpNV.passNV = SHA1.Hash(Encoding.ASCII.GetBytes(matkhau));

            if (modeGhiLuu == 2)
            {
                DAO_NhanVien.get_Instance().AddNhanVien(tmpNV);
            }
            else if (modeGhiLuu == 3)
            {
                if (TextBoxMatKhauChanged)
                {
                    //Xét nếu textbox mật khẩu có bị sửa, sửa với mật khẩu đã hash trước đó
                    DAO_NhanVien.get_Instance().ChangInfoNhanVien(tmpNV);
                }
                else
                {
                    //Xét nếu textbox mật khẩu không bị sửa, gán lại mật khẩu cũ
                    tmpNV.passNV = (byte[])dsNhanVien[index].passNV;
                    DAO_NhanVien.get_Instance().ChangInfoNhanVien(tmpNV);
                }
            }
            else
            {
                return;
            }

        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            SetTextBoxEnableEdit();
            ClearAllTextBox();
            modeGhiLuu = 2;

        }

        private void btn_SuaNV_Click(object sender, EventArgs e)
        {
            SetTextBoxEnableEdit();
            TextBoxMatKhauChanged = false;
            modeGhiLuu = 3;
        }

        private void btn_GhiLuuNV_Click(object sender, EventArgs e)
        {
            
            if (modeGhiLuu != 2 && modeGhiLuu != 3)
            {
                return;
            }

            //Check các trường có bị empty hay không
            if (String.IsNullOrEmpty(txt_maNV.Text) || String.IsNullOrEmpty(txt_Hoten.Text) || String.IsNullOrEmpty(txt_Email.Text) || String.IsNullOrEmpty(txt_Luong.Text) || String.IsNullOrEmpty(txt_Uname.Text) || String.IsNullOrEmpty(txt_MatKhau.Text))
            {
                MessageBox.Show("Các phần không được để trống");
                return;
            }
            try
            {
                ModifedNhanVien(modeGhiLuu);
            }
            catch
            {
                MessageBox.Show("Ghi/Lưu không thành công. Chú ý định dạng và trùng mã nhân viên");
            }
            SetTextBoxReadOnly();
            TextBoxMatKhauChanged = false;
            modeGhiLuu = 1;
            ReloadData();
        }

        private void btn_Khong_Click(object sender, EventArgs e)
        {

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChonNVTrongBang(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            if (dsNhanVien.Count == 0)
            {
                index = 0;
            }
            else
                index = dataGridView1.CurrentCell.RowIndex;

            FullRowSelectedClick(index);
            SetTextBoxReadOnly();
        }

        private void txt_MatKhau_TextChanged(object sender, EventArgs e)
        {
            TextBoxMatKhauChanged = true;
        }

        private void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns.IndexOf(dataGridView1.Columns["Mật khẩu"]))
            {
                if (e.Value != null)
                {
                    string hidden = "HIDDEN";
                    e.Value = hidden;
                }
                else
                    e.Value = "Null";
            }
        }
    }
}
