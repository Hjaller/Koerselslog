using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Koerselslog
{
    public partial class Form2 : Form
    {
        int Index = 0;
        private Crud api = new Crud();
        public Form2()
        {
            InitializeComponent();
        }


        public void filldata()
        {
            SqlDataAdapter da = new SqlDataAdapter("select [id], [name], [licensePlate], [date] from [dbo].[users] where [disabled]='false' or [disabled]='0'", Program.connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            filldata();
        }
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.Index = e.RowIndex;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure want to Delete", "confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int id = int.Parse(dataGridView1.Rows[Index].Cells[0].Value.ToString());
                if (api.deleteUser(id) == 1)
                {
                    MessageBox.Show("Record Deleted Successfully");
                    filldata();
                }
                else
                {
                    MessageBox.Show("Record not Deleted....Please try again.");
                }
            }
        }
    }
}
