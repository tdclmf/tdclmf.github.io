using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_5
{
    public partial class Form1 : Form
    {
        Slovar mySlovar;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string defaultPath = "dictionary.txt";
            if (File.Exists(defaultPath))
            {
                mySlovar = new Slovar(defaultPath);
                RefreshListBox1();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                mySlovar.AddWord(textBox1.Text.Trim());
                RefreshListBox1();
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                mySlovar.DeleteWord(textBox1.Text.Trim());
                RefreshListBox1();
                textBox1.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string target = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(target)) return;

            this.Cursor = Cursors.WaitCursor;

            int index = mySlovar.FindFuzzyIndex(target);

            if (index != -1)
            {
                listBox1.SelectedIndex = index;
                listBox1.TopIndex = Math.Max(0, index - 5);
                string foundWord = listBox1.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Слово не найдено даже с учетом опечаток.", "Поиск");
            }

            this.Cursor = Cursors.Default;
        }

        private void RefreshListBox1()
        {
            listBox1.Items.Clear();
            foreach (var word in mySlovar.GetAllWords())
            {
                listBox1.Items.Add(word);
            }
            toolStripStatusLabel1.Text = $"Слов в словаре: {mySlovar.Count}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HashSet<char> forbiddenLetters = new HashSet<char>(textBox2.Text.ToLower());
            int targetLength = (int)numericUpDown1.Value;
            List<string> filteredWords = mySlovar.GetWords(forbiddenLetters, targetLength);
            listBox2.Items.Clear();
            foreach (var w in filteredWords)
            {
                listBox2.Items.Add(w);
            }
            if (filteredWords.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Текстовые файлы|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    mySlovar.SaveResults(sfd.FileName, filteredWords);
                    MessageBox.Show("Результаты успешно сохранены!");
                }
            }
            else
            {
                MessageBox.Show("Слова по заданным критериям не найдены.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string forbiddenInput = textBox2.Text.Trim();
            HashSet<char> forbiddenLetters = new HashSet<char>(forbiddenInput.ToLower());
            int targetLength = (int)numericUpDown1.Value;
            listBox2.Items.Clear();
            try
            {
                List<string> results = mySlovar.GetWords(forbiddenLetters, targetLength);
                if (results.Count == 0)
                {
                    MessageBox.Show("Слова с такими параметрами не найдены.", "Результат поиска");
                    toolStripStatusLabel1.Text = "Поиск не дал результатов.";
                    return;
                }
                listBox2.BeginUpdate();
                foreach (string s in results)
                {
                    listBox2.Items.Add(s);
                }
                listBox2.EndUpdate();

                toolStripStatusLabel1.Text = $"Найдено слов: {results.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при поиске: " + ex.Message);
            }

        }
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            ofd.Title = "Выберите файл словаря";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mySlovar = new Slovar(ofd.FileName);
                    RefreshListBox1();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mySlovar != null)
            {
                try
                {
                    mySlovar.SaveToFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить словарь перед выходом: " + ex.Message);
                }
            }
        }
    }
}
