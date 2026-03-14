namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Screenshot_1;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(117, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(569, 90);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(92, 143);
            label1.Name = "label1";
            label1.Size = new Size(228, 25);
            label1.TabIndex = 1;
            label1.Text = "Введите точность 0<e<1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(547, 143);
            label2.Name = "label2";
            label2.Size = new Size(188, 25);
            label2.TabIndex = 2;
            label2.Text = "Введите аргумент Х";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            textBox1.Location = new Point(117, 187);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(186, 29);
            textBox1.TabIndex = 3;
            textBox1.Text = "0.1";
            textBox1.KeyPress += textBox1_KeyPress;
            textBox1.Validating += textBox1_Validating;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            textBox2.Location = new Point(547, 187);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(171, 29);
            textBox2.TabIndex = 4;
            textBox2.Text = "0";
            textBox2.KeyPress += textBox1_KeyPress;
            textBox2.Validating += textBox1_Validating;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button1.Location = new Point(347, 175);
            button1.Name = "button1";
            button1.Size = new Size(158, 47);
            button1.TabIndex = 5;
            button1.Text = "Вычислить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.Location = new Point(374, 234);
            label3.Name = "label3";
            label3.Size = new Size(111, 30);
            label3.TabIndex = 6;
            label3.Text = "Результат:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(65, 289);
            label4.Name = "label4";
            label4.Size = new Size(0, 30);
            label4.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Label label3;
        private Label label4;
    }
}
