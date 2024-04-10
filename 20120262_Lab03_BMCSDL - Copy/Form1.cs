using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _20120262_Lab04_BMCSDL
{
    public partial class Form1 : Form
    {
        private static bool first_time = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String uname = txtUsername.Text.ToString();
            String pword = txtPassword.Text.ToString();
            String message;
            object[] hash = new object[3] { uname, MD5.Hash(Encoding.ASCII.GetBytes(pword)), SHA1.Hash(Encoding.ASCII.GetBytes(pword)) };
            
            try
            {
                DBConnect dp = new DBConnect();
                DataTable dt = dp.ExecuteQuery("EXEC SP_LOG_IN @uname , @pwordMD5 , @pwordSHA1", hash);
                String isPassed = dt.Rows[0][0].ToString();
                if (isPassed.Equals("1"))
                {
                    if (dt.Rows[0][1].ToString().Equals("0"))
                    {
                        
                        message = "Sinh viên đăng nhập thành công";
                        MessageBox.Show(message);
                    }
                    else
                    {
                        
                        message = "Giáo viên đăng nhập thành công";
                        MessageBox.Show(message);
                    }
                    if (Form1.first_time == true)
                    {
                        Form1.first_time = false;
                        Form2 quanli = new Form2();
                        this.Hide();
                        quanli.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    message = "Tên đăng nhập hay mật khẩu sai, vui lòng kiểm tra lại";
                    MessageBox.Show(message);
                    txtPassword.Text = " ";
                    txtUsername.Text = " ";
                }
               
            }
            catch (Exception login)
            {
                MessageBox.Show(login.Message);
                MessageBox.Show("Tên đăng nhập và mật khẩu không hợp lệ");
                txtPassword.Text = " ";
                txtUsername.Text = " ";
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void cb_Show_CheckedChanged(object sender, EventArgs e)
        {
            if(!cb_Show.Checked) {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar= false;
            }
        }
    }
}
