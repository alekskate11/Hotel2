using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ManegmentHotelASystem.All_Users_Control
{
    public partial class UC_CustomerRegistration : UserControl
    {
        Function fn = new Function();
        String query;
        public UC_CustomerRegistration()
        {
            InitializeComponent();
        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomType.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }
        public void setComboBox(string query, ComboBox combo)
        {
            using (SqlConnection con = fn.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        combo.Items.Add(sdr.GetString(0));
                    }
                    sdr.Close();
                }
            }
        }
        private void Guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
            query = "select roomNo from rooms where bed = '" + txtBed.Text + "' and roomType='" + txtRoomType.Text + "' and booked='NO' ";
            setComboBox(query, txtRoomNo);
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }
        int rid;
        private void Guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price,roomid from rooms  where roomNo='" + txtRoomNo.Text + "'  ";
            DataSet ds = fn.GetData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());
        }

        private void BtnAlloteRoom_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtNumber.Text != "" && txtNationality.Text != "" && txtGender.Text != "" && txtBirthDate.Text != "" && txtIdProof.Text != "" && txtAddress.Text != "" && txtCheckIn.Text != "" && txtPrice.Text != "")
            {
                String name = txtName.Text;
                Int64 mobile = Int64.Parse(txtNumber.Text);
                String national = txtNationality.Text;
                String gender = txtGender.Text;
                String date = txtBirthDate.Text;
                String idproof = txtIdProof.Text;
                String address = txtAddress.Text;
                String checkin = txtCheckIn.Text;

                query = "INSERT INTO customer (cname, mobile, nationality, gender, dob, idproof, addres, checkin, roomid) VALUES ('" + name + "', " + mobile + ", '" + national + "', '" + gender + "', '" + date + "', '" + idproof + "', '" + address + "', '" + checkin + "', " + rid + "); UPDATE rooms SET booked = 'YES' WHERE roomNo = '" + txtRoomNo.Text + "'";


                fn.SetData(query, "Room  Number  " + txtRoomNo.Text + "Allocation Successful.");
                clearAll();
            }
            else
            {
                MessageBox.Show("All fields are medetory", "Information !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearAll()
        {
            txtName.Clear();
            txtNumber.Clear();
            txtNationality.Clear();
            txtGender.SelectedIndex = -1;
            txtBirthDate.ResetText();
            txtIdProof.Clear();
            txtAddress.Clear();
            txtCheckIn.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtRoomType.SelectedIndex = -1;
            txtPrice.Clear();

        }

        private void UC_CustomerRegistration_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
