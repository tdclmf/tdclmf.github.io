namespace Task_6
{
    partial class AdminForm
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
            cbTopic = new ComboBox();
            cbLevel = new ComboBox();
            cbType = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtQuestion = new TextBox();
            label4 = new Label();
            txtVariants = new TextBox();
            cbCorrectAnswer = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            cbImages = new ComboBox();
            label7 = new Label();
            btnAddImage = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // cbTopic
            // 
            cbTopic.Font = new Font("Segoe UI", 12F);
            cbTopic.FormattingEnabled = true;
            cbTopic.Location = new Point(34, 77);
            cbTopic.Name = "cbTopic";
            cbTopic.Size = new Size(178, 29);
            cbTopic.TabIndex = 0;
            // 
            // cbLevel
            // 
            cbLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLevel.Font = new Font("Segoe UI", 12F);
            cbLevel.FormattingEnabled = true;
            cbLevel.Location = new Point(277, 77);
            cbLevel.Name = "cbLevel";
            cbLevel.Size = new Size(178, 29);
            cbLevel.TabIndex = 1;
            // 
            // cbType
            // 
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.Font = new Font("Segoe UI", 12F);
            cbType.FormattingEnabled = true;
            cbType.Location = new Point(540, 77);
            cbType.Name = "cbType";
            cbType.Size = new Size(178, 29);
            cbType.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 53);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 3;
            label1.Text = "Выбор темы";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(277, 53);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 4;
            label2.Text = "Выбор уровня";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(540, 53);
            label3.Name = "label3";
            label3.Size = new Size(124, 15);
            label3.TabIndex = 5;
            label3.Text = "Выбор типа вопросы";
            // 
            // txtQuestion
            // 
            txtQuestion.Font = new Font("Segoe UI", 12F);
            txtQuestion.Location = new Point(34, 153);
            txtQuestion.Name = "txtQuestion";
            txtQuestion.Size = new Size(684, 29);
            txtQuestion.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 210);
            label4.Name = "label4";
            label4.Size = new Size(144, 15);
            label4.TabIndex = 7;
            label4.Text = "Введите варианты ответа";
            // 
            // txtVariants
            // 
            txtVariants.Location = new Point(36, 228);
            txtVariants.Multiline = true;
            txtVariants.Name = "txtVariants";
            txtVariants.Size = new Size(142, 116);
            txtVariants.TabIndex = 8;
            txtVariants.Leave += TxtVariants_Leave;
            // 
            // cbCorrectAnswer
            // 
            cbCorrectAnswer.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCorrectAnswer.Font = new Font("Segoe UI", 12F);
            cbCorrectAnswer.FormattingEnabled = true;
            cbCorrectAnswer.Location = new Point(212, 228);
            cbCorrectAnswer.Name = "cbCorrectAnswer";
            cbCorrectAnswer.Size = new Size(178, 29);
            cbCorrectAnswer.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 210);
            label5.Name = "label5";
            label5.Size = new Size(135, 15);
            label5.TabIndex = 10;
            label5.Text = "Верный вариант ответа";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(34, 135);
            label6.Name = "label6";
            label6.Size = new Size(130, 15);
            label6.TabIndex = 11;
            label6.Text = "Введите текст вопроса";
            // 
            // cbImages
            // 
            cbImages.DropDownStyle = ComboBoxStyle.DropDownList;
            cbImages.Font = new Font("Segoe UI", 12F);
            cbImages.FormattingEnabled = true;
            cbImages.Location = new Point(540, 228);
            cbImages.Name = "cbImages";
            cbImages.Size = new Size(178, 29);
            cbImages.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(529, 210);
            label7.Name = "label7";
            label7.Size = new Size(114, 15);
            label7.TabIndex = 13;
            label7.Text = "Выберите картинку";
            // 
            // btnAddImage
            // 
            btnAddImage.Font = new Font("Segoe UI", 12F);
            btnAddImage.Location = new Point(540, 280);
            btnAddImage.Name = "btnAddImage";
            btnAddImage.Size = new Size(178, 47);
            btnAddImage.TabIndex = 14;
            btnAddImage.Text = " Открыть картинку";
            btnAddImage.UseVisualStyleBackColor = true;
            btnAddImage.Click += btnAddImage_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 12F);
            btnSave.Location = new Point(36, 365);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(142, 64);
            btnSave.TabIndex = 15;
            btnSave.Text = "Сохранить вопрос";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(btnAddImage);
            Controls.Add(label7);
            Controls.Add(cbImages);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(cbCorrectAnswer);
            Controls.Add(txtVariants);
            Controls.Add(label4);
            Controls.Add(txtQuestion);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbType);
            Controls.Add(cbLevel);
            Controls.Add(cbTopic);
            Name = "AdminForm";
            Text = "AdminForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbTopic;
        private ComboBox cbLevel;
        private ComboBox cbType;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtQuestion;
        private Label label4;
        private TextBox txtVariants;
        private ComboBox cbCorrectAnswer;
        private Label label5;
        private Label label6;
        private ComboBox cbImages;
        private Label label7;
        private Button btnAddImage;
        private Button btnSave;
    }
}