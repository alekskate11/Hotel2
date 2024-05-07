using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManegmentHotelASystem.All_Users_Control
{
    public partial class UC_Employee : UserControl
    {

        Function fn = new Function();
        String query;
        public UC_Employee()
        {
            InitializeComponent();
        }

        private void UC_Employee_Load(object sender, EventArgs e)
        {
            getMAXId();
        }

        public  void getMAXId()
        {
            query = "select max(eid) from employee";
            DataSet ds = fn.GetData(query);

            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                Int64 num = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                labelToSet.Text = (num + 1).ToString();

            }
        }

        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtNumber.Text != "" && txtGender.Text != "" && txtEmail.Text!="" && txtUsername.Text != "" && txtPassword.Text != "")
            {
                String name = txtName.Text;
                Int64 mobile = Int64.Parse(txtNumber.Text);
                String gender = txtGender.Text;
                String email = txtEmail.Text;
                String username = txtUsername.Text;
                String password = HashPassword(txtPassword.Text);

                query = "insert into employee (ename,mobile,gender,emeailid,username,pass) values ('"+name+"' ,"+mobile+",'"+gender+"','"+email+"','"+username+"','"+password+"')";
                fn.SetData(query, "Employee Registered");
                clearAll();


            }
            else
            {
                MessageBox.Show("Fill all fields.","Warning...!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
            txtName.Clear();
            txtNumber.Clear();
            txtGender.SelectedIndex = -1;
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void TabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                setEmployee(guna2DataGridView1);
            }
            else if(tabControl1.SelectedIndex==2){
                setEmployee(guna2DataGridView2);
            }
        }
        public void setEmployee(DataGridView dgv)
        {
            query = "select * from employee";
            DataSet ds = fn.GetData(query);
            dgv.DataSource = ds.Tables[0];

        }

        private void Guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                if(MessageBox.Show("Are you Sure?","Confirmatioin...",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
 query = "delete from employee where eid ="+txtId.Text+"";
            fn.SetData(query,"Record Deleted.");
            TabControl2_SelectedIndexChanged(this, null);
            }
           
        }

        private void UC_Employee_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void TabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
