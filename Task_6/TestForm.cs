using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_6
{
    public partial class TestForm : Form
    {
        private List<Question> _questions;
        private int _currentIdx = 0;
        private int _score = 0;
        private int _timeLeft = 60;
        private string _topic;
        private int _level;
        public TestForm(string topic, int level)
        {
            InitializeComponent();
            _topic = topic;
            _level = level;
            _questions = new XmlService().GetQuestions(topic, level, 5);
        }
        private void ShowQuestion()
        {
            if (_currentIdx >= _questions.Count)
            {
                FinishTest();
                return;
            }

            var q = _questions[_currentIdx];
            label1.Text = $"Вопрос {_currentIdx + 1}: {q.Text}";
            panel1.Controls.Clear();

            if (!string.IsNullOrEmpty(q.ImagePath))
            {
                string fullPath = Path.Combine(GameState.CurrentFolder, q.ImagePath);
                if (File.Exists(fullPath))
                {
                    pictureBox1.Image = Image.FromFile(fullPath);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
            if (q.Type == QuestionType.Anagram)
            {
                TextBox txt = new TextBox { Width = 200, Location = new Point(10, 10) };
                Button btn = new Button { Text = "Ответить", Location = new Point(10, 40) };
                btn.Click += (s, e) => ProcessAnswer(txt.Text);
                panel1.Controls.Add(txt);
                panel1.Controls.Add(btn);
            }
            else
            {
                int y = 10;
                foreach (var ans in q.Answers)
                {
                    Button btn = new Button { Text = ans, Width = 200, Location = new Point(10, y) };
                    btn.Click += (s, e) => ProcessAnswer(ans);
                    panel1.Controls.Add(btn);
                    y += 35;
                }
            }
        }
        private void ProcessAnswer(string userAnswer)
        {
            if (userAnswer.Trim().ToUpper() == _questions[_currentIdx].RightAnswer.ToUpper())
            {
                _score += 20;
            }

            _currentIdx++;
            ShowQuestion();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            labelTimer.Text = $"Осталось времени: {_timeLeft} сек";
            if (_timeLeft <= 0) FinishTest();
        }

        private void FinishTest()
        {
            timer1.Stop();
            string key = $"{_topic}_{_level}";
            if (!GameState.Scores.ContainsKey(key) || GameState.Scores[key] < _score)
                GameState.Scores[key] = _score;

            MessageBox.Show($"Тест завершен! Набрано баллов: {_score}");
            this.Close();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            if (_questions == null || _questions.Count == 0)
            {
                MessageBox.Show("Вопросы для этой темы не найдены!");
                this.DialogResult = DialogResult.Cancel;
                this.BeginInvoke(new MethodInvoker(Close));
                return;
            }

            timer1.Start();
            ShowQuestion();
        }
    }
}
