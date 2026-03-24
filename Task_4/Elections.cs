using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Task_4
{
    public class Candidate
    {
        public string Name { get; set; }
        public int Votes { get; set; }

        public Candidate(string name, int votes)
        {
            this.Name = name;
            this.Votes = votes;
        }
    }

    // Класс для управления списком кандидатов (аналог ArrayBrower)
    public class Election
    {
        private List<Candidate> candidates;
        public string Question { get; set; } // Текст вопроса/название диаграммы

        public Election()
        {
            candidates = new List<Candidate>();
            Question = "Результаты голосования";
        }

        public void Add(Candidate c)
        {
            candidates.Add(c);
        }

        public void Clear()
        {
            candidates.Clear();
        }

        public List<Candidate> GetAll() => candidates;

        // Сохранение в текстовый файл
        public void SaveToFile(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(Question);
                sw.WriteLine(candidates.Count);
                foreach (var c in candidates)
                {
                    sw.WriteLine($"{c.Name};{c.Votes}");
                }
            }
        }

        // Загрузка из текстового файла
        public void LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return;

            candidates.Clear();
            using (StreamReader sr = new StreamReader(fileName))
            {
                Question = sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    string[] line = sr.ReadLine().Split(';');
                    candidates.Add(new Candidate(line[0], int.Parse(line[1])));
                }
            }
        }

        // Отрисовка диаграммы
        public void DrawDiagram(Chart chart)
        {
            chart.Series.Clear();
            chart.Titles.Clear();

            // Настройка внешнего вида (как в примере)
            chart.BackColor = Color.Gray;
            chart.BackSecondaryColor = Color.WhiteSmoke;
            chart.BackGradientStyle = GradientStyle.DiagonalRight;
            chart.ChartAreas[0].Area3DStyle.Enable3D = true;

            chart.Titles.Add(Question);
            chart.Titles[0].Font = new Font("Utopia", 16);

            Series series = new Series("VotesSeries")
            {
                ChartType = SeriesChartType.Pie,
                Label = "#PERCENT" // Показывать проценты на самой диаграмме
            };
            chart.Series.Add(series);

            foreach (var c in candidates)
            {
                series.Points.AddXY(c.Name, c.Votes);
            }

            // Легенда для имен
            chart.Series["VotesSeries"].LegendText = "#VALX";
        }
    }
}
