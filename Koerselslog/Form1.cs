using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            comboBox1.Items.Add("test");
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
            string errorMessage = "";
            if(textBox1.TextLength <= 0)
            {
                errorMessage = "Du skal skrive et navn";
            } 
            if(textBox3.TextLength <= 0)
            {
                errorMessage = "Du skal skrive en nummerplade";
            }
            if (textBox3.TextLength > 7)
            {
                errorMessage = "Din nummerplade er for lang";
            }

            if(errorMessage == "")
            {
                label2.Visible = true;
                label2.ForeColor = Color.Red;
                label2.Text = errorMessage;
                return;
            }
            User user = new User(textBox1.Text, textBox3.Text);
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
    }
}
