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
    /*To do
     * Tilføj redigering af bruger og kørselslog
     * Fix delay på beskeder når de skal fjernes
     * Tilføj søgefunktion
     */
    public partial class Form2 : Form
    {
        int Index = 0;
        private Crud api = new Crud();
        public Form2()
        {
            InitializeComponent();
        }

        public void updateComboBox(ComboBox combo, List<string> items)
        {
            combo.Items.Clear();
            foreach (string item in items) combo.Items.Add(item);

        }

        public void fillUserData()
        {
            SqlDataAdapter da = new SqlDataAdapter("select [id], [name], [licensePlate], [date] from [dbo].[users] where [disabled]='false' or [disabled]='0'", Program.connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].ReadOnly = true;
        }

        public void fillDrivingLogData()
        {
            SqlDataAdapter da = new SqlDataAdapter("select [driving_logs].[id], [users].[name], [users].[licensePlate], [driving_logs].[assignment], [driving_logs].[date], [driving_logs].[user_id] from [dbo].[driving_logs] inner join [dbo].[users] on [users].[id]=[driving_logs].[user_id] where [driving_logs].[disabled]='false' or [driving_logs].[disabled]='0'", Program.connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[2].ReadOnly = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            fillUserData();
            fillDrivingLogData();
            updateComboBox(comboBox3, api.getNames());
        }
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Index = 0;
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.Index = e.RowIndex;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Er du sikker på du vil slette brugeren", "Slet af brugeren", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int id = int.Parse(dataGridView1.Rows[Index].Cells[0].Value.ToString());
                if (api.deleteUser(id) == 1)
                {
                    fillUserData();
                }
                else
                {
                    MessageBox.Show("Der er sket en fejl. Prøv igen");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (textBox1.TextLength <= 0)
            {
                errorMessage = "Du skal skrive et navn";
            }
            if (textBox2.TextLength <= 0)
            {
                errorMessage = "Du skal skrive en nummerplade";
            }
            if (textBox2.TextLength > 7)
            {
                errorMessage = "Din nummerplade er for lang";
            }

            if (errorMessage != "")
            {
                label4.Visible = true;
                label4.ForeColor = Color.Red;
                label4.Text = errorMessage;
                Task.Delay(3000).Wait();
                label4.Visible = false;
                return;
            }
            label4.Visible = true;
            label4.ForeColor = Color.LightGreen;
            label4.Text = "Bruger oprettet";
            new Crud().saveUser(new User(textBox1.Text, textBox2.Text, DateTime.Now.ToString()));
            textBox1.Clear();
            textBox2.Clear();
            updateComboBox(comboBox3, api.getNames());
            fillUserData();
            Task.Delay(3000).Wait();
            label4.Visible = false;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString().Contains("|"))
            {
                string[] name = comboBox3.SelectedItem.ToString().Split('|');
                int id;

                try
                {
                    int.TryParse(name[1], out id);
                }
                catch
                {
                    return;
                }
                textBox4.Text = new Crud().getLicensePlateFromId(id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (comboBox3.SelectedItem == null)
            {
                errorMessage = "Vælg venligst en bruger!";
            }
            else if (textBox5.TextLength <= 0)
            {
                errorMessage = "Skriv venligst en opgave";
            }

            if (errorMessage != "")
            {
                label11.Visible = true;
                label11.ForeColor = Color.Red;
                label11.Text = errorMessage;
                Task.Delay(3000).Wait();
                label11.Visible = false;
                return;
            }

            label11.Visible = true;
            label11.ForeColor = Color.LightGreen;
            string[] name = comboBox3.SelectedItem.ToString().Split('|');
            int id;

            try
            {
                int.TryParse(name[1], out id);
            }
            catch
            {
                return;
            }

            api.createDrivingLog(id, textBox5.Text, dateTimePicker3.Value.ToString());
            label11.Text = "Opgave oprettet!";
            comboBox3.SelectedItem = null;
            dateTimePicker3.Value = DateTime.Now;
            textBox5.Clear();
            textBox4.Clear();
            fillDrivingLogData();
            Task.Delay(3000).Wait();
            label11.Visible = false;
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                Index = 0;
                this.dataGridView2.Rows[e.RowIndex].Selected = true;
                this.Index = e.RowIndex;
                this.dataGridView2.CurrentCell = this.dataGridView2.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip2.Show(this.dataGridView2, e.Location);
                contextMenuStrip2.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Er du sikker på du vil slette køreloggen", "Slet kørelog", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int id = int.Parse(dataGridView2.Rows[Index].Cells[0].Value.ToString());
                if (api.deleteDrivingLog(id) == 1)
                {
                    fillDrivingLogData();
                }
                else
                {
                    MessageBox.Show("Der er sket en fejl. Prøv igen");
                }
            }
        }

        private void annuller_opret_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker3.Value = DateTime.Now;
            comboBox3.SelectedItem = null;

        }
    }
}
