using System;
using System.Reflection.Emit;
using System.Text.Json;
using System.Windows.Forms;
using WinFormsApp1;

namespace WinFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            await SupabaseManager.InitializeAsync();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            label3.Visible = false;

            string username = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                label3.Text = "Пожалуйста, заполните все поля!";
                label3.Visible = true;
                return;
            }

            try
            {
                var response = await SupabaseManager.Client.From<NewUser>()
                .Filter("username", Supabase.Postgrest.Constants.Operator.Equals, username)
                .Filter("password", Supabase.Postgrest.Constants.Operator.Equals, password)
                .Get();

                var user = response.Models.FirstOrDefault();

                if (user != null)
                {
                    MainForm mainForm = new MainForm(user.Username, user.AvatarUrl);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    label3.Text = "Неверный логин или пароль!";
                    label3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                label3.Text = "Ошибка подключения к БД!";
                label3.Visible = true;
            }
        }

        private void btnGoToRegister_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();

            this.Hide();
            regForm.ShowDialog();
            this.Show();
        }
    }
}