namespace Task_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                List<Point2D> pointsList = new List<Point2D>();
                foreach (DataGridViewRow row in dgvPoints.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        if (double.TryParse(row.Cells[0].Value.ToString(), out double x) &&
                            double.TryParse(row.Cells[1].Value.ToString(), out double y))
                        {
                            pointsList.Add(new Point2D(x, y));
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ввод координат. Пожалуйста, вводите только числа.");
                            return;
                        }
                    }
                }

                List<int> targetQuartersList = new List<int>();
                for (int i = 0; i < clbQuarters.Items.Count; i++)
                {
                    if (clbQuarters.GetItemChecked(i))
                    {
                        targetQuartersList.Add(i + 1);
                    }
                }

                if (targetQuartersList.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы одну координатную четверть для поиска.");
                    return;
                }

                GeometryHelper.FindClosestPairsInQuarters(
                    pointsList.ToArray(),
                    targetQuartersList.ToArray(),
                    out double minDistance,
                    out List<PointPair> closestPairs);

                txtResult.Clear();
                txtResult.AppendText($"Минимальное расстояние: {Math.Round(minDistance, 4)}\n       ");
                txtResult.AppendText("Найденные пары точек:\n");
                foreach (var pair in closestPairs)
                {
                    txtResult.AppendText($"- {pair.P1} и {pair.P2}\n");
                }
            }
            catch (ArgumentException ex)
            {
                txtResult.Text = $"Ошибка расчета: {ex.Message}";
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
