using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Task_4
{
    public partial class Form1 : Form
    {
        Chart chart1;
        Election election = new Election();

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
            comboBox1.Items.AddRange(new string[] { "Иванов", "Петров", "Сидоров" });
            comboBox1.SelectedIndex = 0;
        }


        private void SetupDataGridView()
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Кандидат";
            dataGridView1.Columns[1].Name = "Голоса";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            election.AddOrUpdate(comboBox1.Text, 1);
            UpdateTable();
            MessageBox.Show("Данные учтены");
        }

        private void UpdateTable()
        {
            dataGridView1.Rows.Clear();
            foreach (var c in election.GetAll())
            {
                dataGridView1.Rows.Add(c.Name, c.Votes);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Text|*.txt" };
            if (sfd.ShowDialog() == DialogResult.OK)
                election.SaveToFile(sfd.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Text Files|*.txt" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                election.LoadFromFile(ofd.FileName);
                UpdateTable();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                election.DrawDiagram(chart1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1 = new Chart();
            chart1.Parent = tabPage3;
            chart1.Dock = DockStyle.Fill;
            ChartArea ca = new ChartArea("MainArea");
            chart1.ChartAreas.Add(ca);
            ca.Area3DStyle.Enable3D = true;
            Legend leg = new Legend("Default");
            chart1.Legends.Add(leg);
            chart1.BackColor = Color.Gray;
            chart1.BackSecondaryColor = Color.WhiteSmoke;
            chart1.BackGradientStyle = GradientStyle.DiagonalRight;
        }
    }
}
