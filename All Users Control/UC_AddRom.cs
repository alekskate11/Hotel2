using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ManegmentHotelASystem.All_Users_Control
{
    public partial class UC_AddRom : UserControl
    {
        Function fn = new Function();
        String query;

        public UC_AddRom()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void UC_AddRom_Load(object sender, EventArgs e)
        {
            query = "select * from rooms";
            DataSet ds = fn.GetData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }
        public void clearAll()
        {
            txtRoomNo.Clear();
            txtRoomType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();

        }
        private void UC_AddRom_Leave(object sender, EventArgs e)
        {
            clearAll();

        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRom_Load(this, null);
        }
      
        private void BtnAddRoom_Click_1(object sender, EventArgs e)
        {
            if (txtRoomNo.Text != "" && txtRoomType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                String roomno = txtRoomNo.Text;
                String type = txtRoomType.Text;
                String bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                query = "insert into  rooms(roomNo,roomType,bed,price) values('" + roomno + "','" + type + "', '" + bed + "'," + price + ") ";
                fn.SetData(query, "Room Added");
                UC_AddRom_Load(this, null);
                clearAll();
            }
            else
            {
                MessageBox.Show("Fill all Fields.", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

     
    }
}
