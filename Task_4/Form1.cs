using System.Windows.Forms.DataVisualization.Charting;
using static System.Collections.Specialized.BitVector32;

namespace Task_4
{
    public partial class Form1 : Form
    {
        // 1. Объявляем переменную здесь (на уровне класса)
        Chart chart1;
        Election election = new Election();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1 = new Chart();
            chart1.Parent = tabPage3;
            chart1.Dock = DockStyle.Fill;
            // Тест
            ChartArea ca = new ChartArea("MainArea");
            chart1.ChartAreas.Add(ca);
            chart1.BackColor = Color.Yellow;
            ca.BackColor = Color.White;
            Series testSeries = new Series("Test");
            testSeries.ChartType = SeriesChartType.Column;
            chart1.Series.Add(testSeries);
            testSeries.Points.AddXY("Проверка", 50);
            chart1.Legends.Add(new Legend("Default"));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                election.DrawDiagram(chart1);
            }
        }
    }
}
