using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aim_Tracker
{
    public partial class Form1 : Form
    {
        Form2 f2 = null;
        Form3 f3 = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (f2 == null) 
                f2 = new Form2();
            f2.Show();
        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            if (f3 == null)
                f3 = new Form3();
            f3.Show();
        }
    }
}
