using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Task_3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.Owner;

            trackBar1.Value = mainForm.MySpeed;

            if (mainForm.MyDirection == Form1.MoveDirection.Horizontal)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
            linkLabel1.LinkColor = mainForm.MyColorForward;
            linkLabel2.LinkColor = mainForm.MyColorBackward;
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.Owner;
            mainForm.MySpeed = trackBar1.Value;
        }

        private void linkColorFwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 mainForm = (Form1)this.Owner;
            colorDialog1.Color = mainForm.MyColorForward;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                mainForm.MyColorForward = colorDialog1.Color;
                linkLabel1.LinkColor = colorDialog1.Color;
            }
        }

        private void linkColorBck_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 mainForm = (Form1)this.Owner;
            colorDialog1.Color = mainForm.MyColorBackward;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                mainForm.MyColorBackward = colorDialog1.Color;
                linkLabel2.LinkColor = colorDialog1.Color;
            }
        }

        private void rbDirection_CheckedChanged(object sender, EventArgs e)
        {
            Form1 mainForm = (Form1)this.Owner;
            if (radioButton1.Checked)
                mainForm.MyDirection = Form1.MoveDirection.Horizontal;
            else if (radioButton2.Checked)
                mainForm.MyDirection = Form1.MoveDirection.Vertical;
        }
    }
}
