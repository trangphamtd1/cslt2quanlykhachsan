using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace QuanLyKhachSan
{

    public partial class QuanLyKhachSan : Form
    {
        SqlConnection con = new SqlConnection();
        public QuanLyKhachSan()
        {
            InitializeComponent();
        }    
        private void QuanLyKhachSan_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            con.ConnectionString = connectionString;
            con.Open();
            loaddatatogridview();

        }
        private void loaddatatogridview()
        {
            string sql = "select*from QLKhachsan";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tableQLKhachsan = new DataTable();
            adp.Fill(tableQLKhachsan);
            dataGridView_qlks.DataSource = tableQLKhachsan;
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            txtmaphong.Enabled = true;
            txtmaphong.Text = "";
            txtdongia.Text = "";
            txttenphong.Text = "";
        }

        private void dataGridView_qlks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtdongia.Text = dataGridView_qlks.CurrentRow.Cells["Dongia"].Value.ToString();
            txtmaphong.Text = dataGridView_qlks.CurrentRow.Cells["Maphong"].Value.ToString();
            txttenphong.Text = dataGridView_qlks.CurrentRow.Cells["Tenphong"].Value.ToString();
            txtmaphong.Enabled = false;
        }
        private void btnluu_Click(object sender, EventArgs e)
        {
            if (txtmaphong.Text == "")
            {
                MessageBox.Show("bạn cần nhập mã phòng");
                txtmaphong.Focus();
            }
            if (txttenphong.Text == "")
            {
                MessageBox.Show("bạn cần nhập tên phòng");
                txttenphong.Focus();
            }
            if(txtdongia.Text=="")
            {
                MessageBox.Show("bạn cần nhập đơn giá");
                txtdongia.Focus();
            }    
            else
            {
                string sql = " insert into QLKhachsan values ('" + txtmaphong.Text + "','" + txttenphong.Text + "'";
                if (txtdongia.Text != "")
                    sql = sql + "," + txtdongia.Text.Trim();
                sql = sql + ")";
                // MessageBox.Show(sql);
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    loaddatatogridview();
                }
                 catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }                               
                   return;
            }
        }
        private void btnxoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from QLKhachsan where Maphong='" + txtmaphong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loaddatatogridview();
        }
        private void txtdongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) ||
                (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void btnhuy_Click(object sender, EventArgs e)
        {       
            txtmaphong.Text = "";
            txtdongia.Text = "";
            txttenphong.Text = "";
        } 
        private void btnthoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string sql = " update QLKhachsan set Tenphong= '" + txttenphong.Text + "',Dongia='" + txtdongia.Text + "' where Maphong='" + txtmaphong.Text + "'";
            if (txtdongia.Text == "")
            {
                MessageBox.Show("Bạn cần nhập đơn giá");
                txtdongia.Focus();
                return;
            }
            if (txttenphong.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên phòng");
                txtdongia.Focus();
                return;
            } 
            
            txtmaphong.Enabled = false;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loaddatatogridview();
        }
    }
}

