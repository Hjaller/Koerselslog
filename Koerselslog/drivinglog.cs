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
    public partial class drivinglog : Form
    {
        int Index = 0;
        private DataManager dataManager = new DataManager();
        DataTable drivinglogDT = new DataTable();
        DataTable usersDT = new DataTable();

        /* To do
         * input validering
         */

        public drivinglog()
        {
            InitializeComponent();
        }

        // Update the items in a ComboBox with a given list of items
        public void updateComboBox(ComboBox combo, List<string> items)
        {
            combo.Items.Clear();
            foreach (string item in items) combo.Items.Add(item);
        }

        private async void ShowAndHideMessage(Label label, string message, Color color, int duration)
        {

            label.Text = message;
            label.ForeColor = color;
            label.Visible = true;


            await Task.Delay(duration);

            /*await Task.Run(async () =>
            {
                await Task.Delay(duration);
            });*/

            label.Visible = false;
        }


        // Load user data from the database and populate dataGridView1
        public void fillUserData()
        {
            usersDT.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select [id], [name], [licensePlate], [date] from [dbo].[users] where [disabled]='false' or [disabled]='0'", dataManager.connectionString);
            da.Fill(usersDT);
            dataGridView1.DataSource = usersDT;

            dataGridView1.Columns[0].ReadOnly = true;
        }

        // Load driving log data from the database and populate dataGridView2
        public void fillDrivingLogData()
        {
            drivinglogDT.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select [driving_logs].[id], [users].[name], [users].[licensePlate], [driving_logs].[assignment], [driving_logs].[distance], [driving_logs].[date], [driving_logs].[user_id] from [dbo].[driving_logs] inner join [dbo].[users] on [users].[id]=[driving_logs].[user_id] where [driving_logs].[disabled]='false' or [driving_logs].[disabled]='0'", dataManager.connectionString);
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

        // Initialize the form by loading user and driving log data
        private void Form2_Load(object sender, EventArgs e)
        {
            fillUserData();
            fillDrivingLogData();

            panel1.AutoScroll = true;
            comboBox3.Parent = panel1;
            updateComboBox(comboBox3, dataManager.getNames());
        }

        // Handle right-click context menu for dataGridView1
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                Index = 0;
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.Index = e.RowIndex;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        // Handle context menu click to delete a user
        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Er du sikker på du vil slette brugeren", "Slet af brugeren", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                int id = int.Parse(dataGridView1.Rows[Index].Cells[0].Value.ToString());

                if (dataManager.deleteUser(id) == 1)
                {
                    fillUserData();
                    updateComboBox(comboBox3, dataManager.getNames());
                }
                else
                {
                    MessageBox.Show("Der er sket en fejl. Prøv igen");
                }
            }
        }

        // Handle the creation of a new user upon button click
        private void button2_Click(object sender, EventArgs e)
        {
            // Check if text fields meet requirements
            if (textBox1.TextLength <= 0)
            {
                ShowAndHideMessage(label4, "Du skal skrive et navn", Color.Red, 3000);
                return;
            }
            if (textBox2.TextLength <= 0)
            {
                ShowAndHideMessage(label4, "Du skal skrive en nummerplade", Color.Red, 3000);
                return;
            }
            if (textBox2.TextLength > 7)
            {
                ShowAndHideMessage(label4, "Nummerpladen er for lang", Color.Red, 3000);
                return;
            }

            // Display success message in label4
            ShowAndHideMessage(label4, "Bruger oprettet", Color.LightGreen, 3000); // Viser i 3 sekund


            // Save user to the database
            dataManager.saveUser(new User(textBox1.Text, textBox2.Text, DateTime.Now.ToString()));

            // Clear text fields
            textBox1.Clear();
            textBox2.Clear();

            // Update combobox and datagridview1
            updateComboBox(comboBox3, dataManager.getNames());
            fillUserData();
        }
        // Handle the ComboBox's selected index change event
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString().Contains("|"))
            {
                // Split the selected item to extract ID
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

                // Get and display the license plate associated with the ID
                textBox4.Text = dataManager.getLicensePlateFromId(id);
            }
        }

        // Handle the "Create Log" button click event
        private void button1_Click(object sender, EventArgs e)
        {
            int distance = 0;

            if (comboBox3.SelectedItem == null)
            {
                ShowAndHideMessage(label11, "Vælg venligst en bruger", Color.Red, 3000);
                return;

            }
            if (textBox5.TextLength <= 0)
            {
                ShowAndHideMessage(label11, "Skriv venligst en opgave", Color.Red, 3000);
                return;
            }
            if (textBox6.TextLength > 0)
            {
                if (!int.TryParse(textBox6.Text, out distance))
                {
                    ShowAndHideMessage(label11, "Km kan kun være tal", Color.Red, 3000);
                    return;
                }
                    
                
            }



            // Extract user ID from the selected ComboBox item
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

            // Create a new driving log entry
            dataManager.createDrivingLog(id, textBox5.Text, distance, dateTimePicker3.Value.ToString());
            ShowAndHideMessage(label11, "Opgave oprettet", Color.LightGreen, 3000);

            // Clear fields and update DataGridView
            comboBox3.SelectedItem = null;
            dateTimePicker3.Value = DateTime.Now;
            textBox6.Clear();
            textBox5.Clear();
            textBox4.Clear();
            fillDrivingLogData();
        }

        // Handle right-click context menu for dataGridView2
        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
            {
                Index = 0;
                this.dataGridView2.Rows[e.RowIndex].Selected = true;
                this.Index = e.RowIndex;
                this.dataGridView2.CurrentCell = this.dataGridView2.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip2.Show(this.dataGridView2, e.Location);
                contextMenuStrip2.Show(Cursor.Position);
            }
        }

        // Handle context menu click to delete a driving log
        private void contextMenuStrip2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Er du sikker på du vil slette køreloggen", "Slet kørelog", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                int id = int.Parse(dataGridView2.Rows[Index].Cells[0].Value.ToString());

                if (dataManager.deleteDrivingLog(id) == 1)
                {
                    fillDrivingLogData();
                }
                else
                {
                    MessageBox.Show("Der er sket en fejl. Prøv igen");
                }
            }
        }

        // Handle cancel button click to clear user creation fields
        private void annuller_opret_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        // Handle cancel button click to clear driving log creation fields
        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            dateTimePicker3.Value = DateTime.Now;
            comboBox3.SelectedItem = null;
        }

        // Search for data in both DataGridViews based on entered text
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView users_search = usersDT.DefaultView;
            users_search.RowFilter = "name Like '%" + textBox3.Text + "%' OR licensePlate Like '%" + textBox3.Text + "%' or id ="+getSearchId(textBox3.Text);

            DataView drivinglog_search = drivinglogDT.DefaultView;
            drivinglog_search.RowFilter = "name Like '%" + textBox3.Text + "%' OR licensePlate Like '%" + textBox3.Text + "%' or assignment Like '%" + textBox3.Text + "%' OR date Like '%" + textBox3.Text + "%' or id ="+getSearchId(textBox3.Text);
        }

        private string getSearchId(string searchText)
        {
            if (int.TryParse(searchText, out int id))
            { 
                return id.ToString();
            }
            return "-1";
        }

        // Handle cell value change in dataGridView1 to update database
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            string queryString = $"update [dbo].[users] set {columnName}=@updatedData where id=@id;";

            using (SqlConnection connection = new SqlConnection(dataManager.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@updatedData", dataGridView1.CurrentCell.Value);
                command.Parameters.AddWithValue("@id", dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                command.ExecuteNonQuery();
                connection.Close();
                fillUserData();
            }
        }

        // Handle cell value change in dataGridView2 to update database
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridView2.Columns[e.ColumnIndex].Name;
            string queryString = $"update [dbo].[driving_logs] set {columnName}=@updatedData where id=@id;";

            using (SqlConnection connection = new SqlConnection(dataManager.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@updatedData", dataGridView2.CurrentCell.Value);
                command.Parameters.AddWithValue("@id", dataGridView2.Rows[e.RowIndex].Cells[0].Value);

                command.ExecuteNonQuery();
                connection.Close();
                fillDrivingLogData();
            }
        }
    }
}
 // End of drivinglog class
