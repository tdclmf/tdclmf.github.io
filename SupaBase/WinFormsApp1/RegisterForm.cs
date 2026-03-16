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
    public partial class RegisterForm : Form
    {
        private string selectedAvatarPath = "";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnGoToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChooseAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedAvatarPath = ofd.FileName;
                    pbAvatar.Image = System.Drawing.Image.FromFile(selectedAvatarPath);
                }
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtRegUsername.Text.Trim();
            string password = txtRegPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Логин и пароль обязательны для заполнения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string avatarUrl = "";
                if (!string.IsNullOrEmpty(selectedAvatarPath))
                {
                    byte[] fileBytes = File.ReadAllBytes(selectedAvatarPath);
                    string fileName = $"{username}_avatar{Path.GetExtension(selectedAvatarPath)}";

                    await SupabaseManager.Client.Storage.From("avatars").Upload(fileBytes, fileName);
                    avatarUrl = SupabaseManager.Client.Storage.From("avatars").GetPublicUrl(fileName);
                }

                var newUser = new NewUser { Username = username, Password = password, AvatarUrl = avatarUrl };
                await SupabaseManager.Client.From<NewUser>().Insert(newUser);

                MessageBox.Show("Регистрация успешна! Теперь вы можете войти.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
