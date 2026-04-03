namespace Task_6
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
            btnLevel1 = new Button();
            btnLevel2 = new Button();
            btnLevel3 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnLevel1
            // 
            btnLevel1.Font = new Font("Segoe UI", 12F);
            btnLevel1.Location = new Point(178, 300);
            btnLevel1.Name = "btnLevel1";
            btnLevel1.Size = new Size(122, 47);
            btnLevel1.TabIndex = 0;
            btnLevel1.Tag = "1";
            btnLevel1.Text = "Уровень 1";
            btnLevel1.UseVisualStyleBackColor = true;
            btnLevel1.Click += btnLevel_Click;
            // 
            // btnLevel2
            // 
            btnLevel2.Font = new Font("Segoe UI", 12F);
            btnLevel2.Location = new Point(365, 300);
            btnLevel2.Name = "btnLevel2";
            btnLevel2.Size = new Size(122, 47);
            btnLevel2.TabIndex = 2;
            btnLevel2.Tag = "2";
            btnLevel2.Text = "Уровень 2";
            btnLevel2.UseVisualStyleBackColor = true;
            btnLevel2.Click += btnLevel_Click;
            // 
            // btnLevel3
            // 
            btnLevel3.Font = new Font("Segoe UI", 12F);
            btnLevel3.Location = new Point(560, 300);
            btnLevel3.Name = "btnLevel3";
            btnLevel3.Size = new Size(122, 47);
            btnLevel3.TabIndex = 3;
            btnLevel3.Tag = "3";
            btnLevel3.Text = "Уровень 3";
            btnLevel3.UseVisualStyleBackColor = true;
            btnLevel3.Click += btnLevel_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 12F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(315, 138);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(212, 29);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(312, 112);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 5;
            label1.Text = "Выберите тему";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(297, 240);
            label2.Name = "label2";
            label2.Size = new Size(263, 21);
            label2.TabIndex = 6;
            label2.Text = "Нужно сначала набрать 80 баллов!";
            label2.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(737, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 28);
            button1.TabIndex = 7;
            button1.Text = "admin";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 459);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(btnLevel3);
            Controls.Add(btnLevel2);
            Controls.Add(btnLevel1);
            Name = "Form1";
            Text = "Form1";
            Shown += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLevel1;
        private Button btnLevel2;
        private Button btnLevel3;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Button button1;
    }
}
