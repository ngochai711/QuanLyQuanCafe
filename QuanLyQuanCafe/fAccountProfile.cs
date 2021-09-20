using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;

namespace QuanLyQuanCafe
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get
            {
                return loginAccount;
            }

            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }
        }
        public fAccountProfile(Account acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }


        private event EventHandler updateAccount;
        public event EventHandler UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        void UpdateAccount()
        {
            string displayName = txbDisPlayName.Text;
            string password = txbPassWord.Text;
            string newpass = txbNewPass.Text;
            string reenterpass = txbReEnterPass.Text;
            string username = txbUserName.Text;

            if(newpass.Equals(reenterpass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới !!!");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(username, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật mật khẩu thành công");
                }
                else
                    MessageBox.Show("Vui lòng nhập lại mật khẩu");
            }
        }
        void ChangeAccount(Account acc)
        {
            txbUserName.Text = LoginAccount.UserName;
            txbDisPlayName.Text = LoginAccount.DisplayName;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
    }
}
