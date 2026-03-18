namespace Task_2
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
            dgvPoints = new DataGridView();
            X = new DataGridViewTextBoxColumn();
            Y = new DataGridViewTextBoxColumn();
            clbQuarters = new CheckedListBox();
            btnCalculate = new Button();
            txtResult = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvPoints).BeginInit();
            SuspendLayout();
            // 
            // dgvPoints
            // 
            dgvPoints.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPoints.Columns.AddRange(new DataGridViewColumn[] { X, Y });
            dgvPoints.Location = new Point(344, 98);
            dgvPoints.Name = "dgvPoints";
            dgvPoints.Size = new Size(243, 285);
            dgvPoints.TabIndex = 0;
            // 
            // X
            // 
            X.HeaderText = "X";
            X.Name = "X";
            // 
            // Y
            // 
            Y.HeaderText = "Y";
            Y.Name = "Y";
            // 
            // clbQuarters
            // 
            clbQuarters.FormattingEnabled = true;
            clbQuarters.Items.AddRange(new object[] { "1 четверть", "2 четверть", "3 четверть", "4 четверть" });
            clbQuarters.Location = new Point(630, 98);
            clbQuarters.Name = "clbQuarters";
            clbQuarters.Size = new Size(120, 94);
            clbQuarters.TabIndex = 1;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(630, 207);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(120, 41);
            btnCalculate.TabIndex = 2;
            btnCalculate.Text = "Рассчитать";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(12, 98);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.Size = new Size(303, 285);
            txtResult.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtResult);
            Controls.Add(btnCalculate);
            Controls.Add(clbQuarters);
            Controls.Add(dgvPoints);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvPoints).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPoints;
        private DataGridViewTextBoxColumn X;
        private DataGridViewTextBoxColumn Y;
        private CheckedListBox clbQuarters;
        private Button btnCalculate;
        private TextBox txtResult;
    }
}
