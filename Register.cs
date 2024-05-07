using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManegmentHotelASystem
{
    public partial class Register : Form
    {

        String query;
        Function fn = new Function();
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text != "" && txtNumber.Text != "" && txtGender.Text != "" && txtEmail.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                DataSet ds = new DataSet();
                String name = txtLogin.Text;

                query = "select username from employee where username = '" + txtLogin.Text + "'";
                ds = fn.GetData(query);

                Int64 mobile = 0;
                String gender = txtGender.Text;
                String email = txtEmail.Text;
                String username = txtUsername.Text;
                String password = HashPassword(txtPassword.Text);

                if (ds.Tables[0].Rows.Count > 0 || !Int64.TryParse(txtNumber.Text, out mobile) || txtNumber.Text.Length < 10)
                {
                    LoginErrorLabel.Visible = true;
                    txtNumber.Clear();
                    txtLogin.Clear();
                    labelErrorNum.Visible = true;
                }
                else if (Int64.TryParse(txtNumber.Text, out mobile) || txtNumber.Text.Length < 10)
                {
                    LoginErrorLabel.Visible = false;
                    labelErrorNum.Visible = false;
                    query = "insert into employee (ename,mobile,gender,emeailid,username,pass) values ('" + name + "' ," + mobile + ",'" + gender + "','" + email + "','" + username + "','" + password + "')";
                    fn.SetData(query, "Employee Registered");
                    clearAll();

                    Dashboard dash = new Dashboard();
                    this.Hide();
                    dash.Show();
                }
            }
            else
            {
                MessageBox.Show("Fill all fields.", "Warning...!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Хешируем пароль
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Преобразуем хеш в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public void clearAll()
        {
            txtLogin.Clear();
            txtNumber.Clear();
            txtGender.SelectedIndex = -1;
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        public string LabelText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public void SetRegisterButtonVisibility(bool isVisible)
        {
            registerBtn.Visible = isVisible;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
