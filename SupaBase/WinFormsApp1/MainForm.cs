using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm(string username, string avatarUrl)
        {
            InitializeComponent();
            label1.Text = $"Привет, {username}!";
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                try
                {
                    pictureBox1.LoadAsync(avatarUrl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось загрузить аватарку.", "Внимание");
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}