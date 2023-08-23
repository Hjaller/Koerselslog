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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
