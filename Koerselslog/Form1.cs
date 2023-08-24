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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dateTimePicker2.Enabled = false;
            dateTimePicker1.Enabled = false;
            //Add users to dropdown menu 

            updateComboBox(comboBox1, new Crud().getNames());
            updateComboBox(comboBox2, new Crud().getNames());
        }

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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                dateTimePicker1.Enabled = false;

            }
            else
            {
                dateTimePicker1.Enabled = true;
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
            User user = new User(textBox1.Text, textBox3.Text, dateTimePicker2.Value.ToString());
            new Crud().saveUser(user);
            label2.Text = "Bruger oprettet";
            updateComboBox(comboBox1, new Crud().getNames());
            updateComboBox(comboBox2, new Crud().getNames());

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

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
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
        //Opdater bruger knap
        private void button3_Click(object sender, EventArgs e)
        {

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
    }
}
