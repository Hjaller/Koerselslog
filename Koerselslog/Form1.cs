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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dateTimePicker2.Enabled = false;
            //Add users to dropdown menu 
            updateComboBox(comboBox1, new Crud().getNames());
            updateComboBox(comboBox2, new Crud().getNames());
            updateComboBox(comboBox3, new Crud().getNames());

        }

        //Update Combobox
        public void updateComboBox(ComboBox combo, List<string> items)
        {
            combo.Items.Clear();
            foreach(string item in items) combo.Items.Add(item); 

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox3.Clear();

        }
        //navn - textbox i opret
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.TextLength <= 0)
            {
                dateTimePicker2.Enabled = false;

            } else
            {
                dateTimePicker2.Enabled = true;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
        //combobox navne opdater bruger
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Debug.WriteLine(comboBox1.SelectedItem.ToString());
            if (!new Crud().getNames().Contains(comboBox1.SelectedItem.ToString())) {
                comboBox1.SelectedItem = null;
            } 
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        //datopicker opret bruger
        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {

        }
        //Annuller knappen på opret bruger
        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox3.Clear();
        }
        //Annuller knappen på rediger bruger
        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            comboBox1.SelectedItem = null;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
        //Opret bruger knap
        private void button2_Click_1(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (textBox1.TextLength <= 0)
            {
                errorMessage = "Du skal skrive et navn";
            }
            if (textBox3.TextLength <= 0)
            {
                errorMessage = "Du skal skrive en nummerplade";
            }
            if (textBox3.TextLength > 7)
            {
                errorMessage = "Din nummerplade er for lang";
            }

            if (errorMessage != "")
            {
                label2.Visible = true;
                label2.ForeColor = Color.Red;
                label2.Text = errorMessage;
                return;
            }
            label2.Visible = true;
            label2.ForeColor = Color.LightGreen;
            label2.Text = "Bruger oprettet";
            new Crud().saveUser(new User(textBox1.Text, textBox3.Text, dateTimePicker2.Value.ToString()));
            updateComboBox(comboBox1, new Crud().getNames());
            updateComboBox(comboBox2, new Crud().getNames());
            updateComboBox(comboBox3, new Crud().getNames());
            textBox1.Clear();
            textBox3.Clear();
            dateTimePicker2.Value = DateTime.Now;



        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string errorMessage = "";
            if(comboBox3.SelectedItem == null)
            {
                errorMessage = "Vælg venligst en bruger!";
            } 
            else if(textBox5.TextLength <= 0)
            {
                errorMessage = "Skriv venligst en opgave";
            }

            if (errorMessage != "")
            {
                label11.Visible = true;
                label11.ForeColor = Color.Red;
                label11.Text = errorMessage;
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
            
            new Crud().createDrivingLog(id, textBox5.Text, dateTimePicker3.Value.ToString());
            label11.Text = "Opgave oprettet!";
            comboBox3.SelectedItem = null;
            dateTimePicker3.Value = DateTime.Now;
            textBox5.Clear();
            textBox4.Clear();
        }
        //tekst opret bruger
        private void label2_Click_1(object sender, EventArgs e)
        {

        }
        //Navn tekstboks bruger opret
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.TextLength <= 0)
            {
                dateTimePicker2.Enabled = false;

            }
            else
            {
                dateTimePicker2.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (comboBox2.SelectedItem == null)
            {
                errorMessage = "Vælg venligst en bruger";
            }
            if (errorMessage != "")
            {
                label19.Visible = true;
                label19.ForeColor = Color.Red;
                label19.Text = errorMessage;
                return;
            }
            label19.Visible = true;
            label19.ForeColor = Color.LightGreen;
            User user = new User(textBox1.Text, textBox3.Text, dateTimePicker2.Value.ToString());
            new Crud().deleteUser(comboBox2.SelectedItem.ToString());
            label19.Text = "Bruger slettet";
            comboBox2.SelectedItem = null;
            updateComboBox(comboBox1, new Crud().getNames());
            updateComboBox(comboBox2, new Crud().getNames());
            updateComboBox(comboBox3, new Crud().getNames());
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
        //Opdater bruger knap
        private void button3_Click(object sender, EventArgs e)
        {

            string errorMessage = "";
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
                label6.Visible = true;
                label6.ForeColor = Color.Red;
                label6.Text = errorMessage;
                return;
            }
            label6.Visible = true;
            label6.ForeColor = Color.LightGreen;
            label6.Text = "Bruger opdateret";
            
            string[] name = comboBox1.SelectedItem.ToString().Split('|');
            int id;

            try
            {
                int.TryParse(name[1], out id);
            } catch {
                return;
            }
            new Crud().updateUser(id, textBox2.Text);
            
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
        //nr plade tekstboks opret bruger
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString().Contains("|"))
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



        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
