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
using System.Diagnostics;

namespace Koerselslog
{
    /*To do
     * Fix delay på beskeder når de skal fjernes
     * Tilføje så man også kan søge efter id'er i søgefunktionen
     * tilføje så man kan se flere navne i dropdown
     * tilføje kommentare
     * fejl når man klikker på coloumn
     * automatisk oprettelse af database
     * database informationer gemt i konfigurations fil
     */
    public partial class drivinglog : Form
    {
        int Index = 0;
        private Crud api = new Crud();
        DataTable drivinglogDT = new DataTable();
        DataTable usersDT = new DataTable();
        public drivinglog()
        {
            InitializeComponent();
        }

        public void updateComboBox(ComboBox combo, List<string> items)
        {
            combo.Items.Clear();
            foreach (string item in items) combo.Items.Add(item);

        }
        //Fylder datagridview1 ud med data fra databasen
        public void fillUserData()
        {
            usersDT.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select [id], [name], [licensePlate], [date] from [dbo].[users] where [disabled]='false' or [disabled]='0'", api.connectionString);
            da.Fill(usersDT);
            dataGridView1.DataSource = usersDT;

            dataGridView1.Columns[0].ReadOnly = true;
        }
        //Fylder datagridview2 ud med data fra databasen
        public void fillDrivingLogData()
        {
            drivinglogDT.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select [driving_logs].[id], [users].[name], [users].[licensePlate], [driving_logs].[assignment], [driving_logs].[distance], [driving_logs].[date], [driving_logs].[user_id] from [dbo].[driving_logs] inner join [dbo].[users] on [users].[id]=[driving_logs].[user_id] where [driving_logs].[disabled]='false' or [driving_logs].[disabled]='0'", api.connectionString);
            da.Fill(drivinglogDT);
            dataGridView2.DataSource = drivinglogDT;

            for (int i = 0; i < 7; i++)
            {
                if (i != 3 && i != 4)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
            }

        }
        //Loader form2
        private void Form2_Load(object sender, EventArgs e)
        {
            fillUserData();
            fillDrivingLogData();
            updateComboBox(comboBox3, api.getNames());

        }
        //Viser contextmenu ved højreklik
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Tjekker om det er højreklik
            if (e.Button == MouseButtons.Right)
            {
                //Viser contextmenu og sætter index nummeret
                Index = 0;
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.Index = e.RowIndex;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
        //Sletter brugeren ved klik på contextmenu
        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            //Viser dialogboksen
            DialogResult dr = MessageBox.Show("Er du sikker på du vil slette brugeren", "Slet af brugeren", MessageBoxButtons.YesNo);
            //Tjekker om resultatet er ja
            if (dr == DialogResult.Yes)
            {
                //Parser id til int
                int id = int.Parse(dataGridView1.Rows[Index].Cells[0].Value.ToString());
                //Tjekker om brugeren blev slettet og opddatere datagridview1
                if (api.deleteUser(id) == 1)
                {
                    fillUserData();
                    updateComboBox(comboBox3, api.getNames());
                }
                //Brugeren blev ikke fundet i databasen
                else
                {
                    MessageBox.Show("Der er sket en fejl. Prøv igen");
                }
            }
        }
        //Opret bruger knappen ved klik
        private void button2_Click(object sender, EventArgs e)
        {
            //Initialisere variabler
            string errorMessage = "";
            //Tjek om tekst felterne opfylder kravene
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
            //Fejl besked - Gør label4 synlig, sætter farven til rød og skriver tekst
            if (errorMessage != "")
            {
                label4.Visible = true;
                label4.ForeColor = Color.Red;
                label4.Text = errorMessage;
                Task.Delay(3000).Wait();
                label4.Visible = false;
                return;
            }
            //Success besked - Gøre label4 synlig, sætter farven til grøn og skriver tekst
            label4.Visible = true;
            label4.ForeColor = Color.LightGreen;
            label4.Text = "Bruger oprettet";
            //Gemme brugeren i databasen
            api.saveUser(new User(textBox1.Text, textBox2.Text, DateTime.Now.ToString()));
            //Rydde tekst felterne
            textBox1.Clear();
            textBox2.Clear();
            //Opdatere combobox til det nyeste data
            updateComboBox(comboBox3, api.getNames());
            //Opdatere datagridview1 med nyeste data
            fillUserData();
            //Delay for at gøre beskeden usynlig igen
            Task.Delay(1000).Wait();
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
            int distance = 0;
            if (comboBox3.SelectedItem == null) errorMessage = "Vælg venligst en bruger!";
            else if (textBox5.TextLength <= 0) errorMessage = "Skriv venligst en opgave"; 
            if (textBox6.TextLength > 0)
            {
                if (!int.TryParse(textBox6.Text, out distance)) errorMessage = "Skriv veligst kun tal i km";

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

            api.createDrivingLog(id, textBox5.Text, distance, dateTimePicker3.Value.ToString());
            label11.Text = "Opgave oprettet!";
            comboBox3.SelectedItem = null;
            dateTimePicker3.Value = DateTime.Now;
            textBox6.Clear();
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
        //Contextmenu til sletning af kørelog i datagridview2
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
        //Annullere oprettelsen af bruger og rydder tekstfelterne
        private void annuller_opret_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        //Annullere oprettelse af kørelog og rydder tekstfelterne
        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            dateTimePicker3.Value = DateTime.Now;
            comboBox3.SelectedItem = null;

        }
        //Søger efter det indtastede i både datagridview1 og datagridview2
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView users_search = usersDT.DefaultView;
            users_search.RowFilter = "name Like '%" + textBox3.Text + "%' OR licensePlate Like '%" + textBox3.Text + "%'";

            DataView drivinglog_search = drivinglogDT.DefaultView;
            drivinglog_search.RowFilter = "name Like '%" + textBox3.Text + "%' OR licensePlate Like '%" + textBox3.Text + "%' or assignment Like '%" + textBox3.Text + "%' OR date Like '%" + textBox3.Text + "%'";
        }
        //Opdatere databasen ved ændring i datagridview1
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Initialisere variabler
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            string queryString = $"update [dbo].[users] set {columnName}=@updatedData where id=@id;";
            //Starter sql connection
            using (SqlConnection connection = new SqlConnection(
                      api.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@updatedData", dataGridView1.CurrentCell.Value);
                command.Parameters.AddWithValue("@id", dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                //Executer sql command
                command.ExecuteNonQuery();
                //Lukker connection
                connection.Close();
                //Opdatere datagridview2 med nyeste data
                fillUserData();
            }
        }
        //Opdatere databasen ved ændring i datagridview2
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Initialisere variabler
            string columnName = dataGridView2.Columns[e.ColumnIndex].Name;
            string queryString = $"update [dbo].[driving_logs] set {columnName}=@updatedData where id=@id;";
            //Starter sql connection
            using (SqlConnection connection = new SqlConnection(
                      api.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@updatedData", dataGridView2.CurrentCell.Value);
                command.Parameters.AddWithValue("@id", dataGridView2.Rows[e.RowIndex].Cells[0].Value);

                //Executer sql command
                command.ExecuteNonQuery();
                //Lukker connection
                connection.Close();
                //Opdatere datagridview2 med nyeste data
                fillDrivingLogData();
            }
        }
    }
}
