namespace WinFormsApp1
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGoToLogin = new Button();
            btnLogin = new Button();
            label2 = new Label();
            label1 = new Label();
            txtRegPassword = new TextBox();
            txtRegUsername = new TextBox();
            pbAvatar = new PictureBox();
            btnChooseAvatar = new Button();
            ((System.ComponentModel.ISupportInitialize)pbAvatar).BeginInit();
            SuspendLayout();
            // 
            // btnGoToLogin
            // 
            btnGoToLogin.Location = new Point(665, 401);
            btnGoToLogin.Name = "btnGoToLogin";
            btnGoToLogin.Size = new Size(111, 23);
            btnGoToLogin.TabIndex = 12;
            btnGoToLogin.Text = "Войти";
            btnGoToLogin.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(323, 305);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(98, 23);
            btnLogin.TabIndex = 11;
            btnLogin.Text = "Регистрация";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(278, 206);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 10;
            label2.Text = "Пароль";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(277, 143);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 9;
            label1.Text = "Логин";
            // 
            // txtRegPassword
            // 
            txtRegPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtRegPassword.Location = new Point(277, 224);
            txtRegPassword.Name = "txtRegPassword";
            txtRegPassword.Size = new Size(208, 29);
            txtRegPassword.TabIndex = 8;
            // 
            // txtRegUsername
            // 
            txtRegUsername.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtRegUsername.Location = new Point(278, 161);
            txtRegUsername.Name = "txtRegUsername";
            txtRegUsername.Size = new Size(208, 29);
            txtRegUsername.TabIndex = 7;
            // 
            // pbAvatar
            // 
            pbAvatar.Location = new Point(278, 28);
            pbAvatar.Name = "pbAvatar";
            pbAvatar.Size = new Size(85, 80);
            pbAvatar.TabIndex = 13;
            pbAvatar.TabStop = false;
            // 
            // btnChooseAvatar
            // 
            btnChooseAvatar.Location = new Point(387, 43);
            btnChooseAvatar.Name = "btnChooseAvatar";
            btnChooseAvatar.Size = new Size(99, 47);
            btnChooseAvatar.TabIndex = 14;
            btnChooseAvatar.Text = "Выбрать фото";
            btnChooseAvatar.UseVisualStyleBackColor = true;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnChooseAvatar);
            Controls.Add(pbAvatar);
            Controls.Add(btnGoToLogin);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtRegPassword);
            Controls.Add(txtRegUsername);
            Name = "RegisterForm";
            Text = "RegisterForm";
            ((System.ComponentModel.ISupportInitialize)pbAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGoToLogin;
        private Button btnLogin;
        private Label label2;
        private Label label1;
        private TextBox txtRegPassword;
        private TextBox txtRegUsername;
        private PictureBox pbAvatar;
        private Button btnChooseAvatar;
    }
}