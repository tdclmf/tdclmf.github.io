using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Task_6
{
    public partial class AdminForm : Form
    {
        private string _xmlPath;

        public AdminForm()
        {
            InitializeComponent();
            string[] files = Directory.GetFiles(GameState.CurrentFolder, "*.xml");
            if (files.Length > 0)
                _xmlPath = files[0];
            else
                MessageBox.Show("В папке не найден XML файл!");

            SetupForm();
        }

        private void SetupForm()
        {
            cbType.Items.Clear();
            cbType.Items.AddRange(new object[] { "Anagram", "Choice", "FillIn" });
            cbType.SelectedIndex = 0;
            cbLevel.Items.Clear();
            cbLevel.Items.AddRange(new object[] { "1", "2", "3" });
            cbLevel.SelectedIndex = 0;
            RefreshTopics();
            RefreshImages();
            txtVariants.Leave += TxtVariants_Leave;
        }

        private void TxtVariants_Leave(object sender, EventArgs e)
        {
            UpdateCorrectAnswerList();
        }

        private void UpdateCorrectAnswerList()
        {
            string previousSelection = cbCorrectAnswer.SelectedItem?.ToString();
            cbCorrectAnswer.Items.Clear();
            var lines = txtVariants.Lines
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToArray();

            if (lines.Length > 0)
            {
                cbCorrectAnswer.Items.AddRange(lines);
                if (lines.Contains(previousSelection))
                    cbCorrectAnswer.SelectedItem = previousSelection;
                else
                    cbCorrectAnswer.SelectedIndex = 0;
            }
        }

        private void RefreshTopics()
        {
            if (string.IsNullOrEmpty(_xmlPath)) return;
            XDocument doc = XDocument.Load(_xmlPath);
            var topics = doc.Descendants("Topic")
                            .Select(t => t.Attribute("name").Value)
                            .Distinct()
                            .ToArray();
            cbTopic.Items.Clear();
            cbTopic.Items.AddRange(topics);
        }

        private void RefreshImages()
        {
            var images = Directory.GetFiles(GameState.CurrentFolder, "*.*")
                .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") || f.EndsWith(".jpeg"))
                .Select(Path.GetFileName)
                .ToArray();

            cbImages.Items.Clear();
            cbImages.Items.Add("");
            cbImages.Items.AddRange(images);
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Изображения|*.jpg;*.png;*.jpeg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = Path.GetFileName(ofd.FileName);
                    string destPath = Path.Combine(GameState.CurrentFolder, fileName);

                    if (!File.Exists(destPath))
                        File.Copy(ofd.FileName, destPath);

                    RefreshImages();
                    cbImages.SelectedItem = fileName;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateCorrectAnswerList();

            if (string.IsNullOrEmpty(cbTopic.Text) || string.IsNullOrEmpty(txtQuestion.Text) || cbCorrectAnswer.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            try
            {
                XDocument doc = XDocument.Load(_xmlPath);
                int countBefore = doc.Descendants("Question").Count();
                string targetTopic = cbTopic.Text.Trim();
                XElement topicNode = doc.Root.Elements("Topic")
                    .FirstOrDefault(t => t.Attribute("name").Value.Trim().Equals(targetTopic, StringComparison.OrdinalIgnoreCase));

                if (topicNode == null)
                {
                    topicNode = new XElement("Topic", new XAttribute("name", targetTopic));
                    doc.Root.Add(topicNode);
                }
                string targetLevel = cbLevel.Text.Trim();
                XElement levelNode = topicNode.Elements("Level")
                    .FirstOrDefault(l => l.Attribute("id").Value.Trim() == targetLevel);

                if (levelNode == null)
                {
                    levelNode = new XElement("Level",
                        new XAttribute("id", targetLevel),
                        new XAttribute("minScoreToUnlock", "80"));
                    topicNode.Add(levelNode);
                }
                XElement qNode = new XElement("Question",
                    new XAttribute("type", cbType.Text),
                    new XAttribute("text", txtQuestion.Text.Trim()),
                    new XAttribute("score", "20"));

                if (!string.IsNullOrEmpty(cbImages.Text))
                    qNode.Add(new XAttribute("src", cbImages.Text));
                string rightAns = cbCorrectAnswer.SelectedItem.ToString();
                var allVariants = txtVariants.Lines
                    .Select(l => l.Trim())
                    .Where(l => !string.IsNullOrEmpty(l))
                    .ToArray();

                foreach (var v in allVariants)
                {
                    qNode.Add(new XElement("Answer",
                        new XAttribute("right", (v == rightAns ? "yes" : "no")),
                        v));
                }

                levelNode.Add(qNode);
                doc.Save(_xmlPath);
                XDocument checkDoc = XDocument.Load(_xmlPath);
                int countAfter = checkDoc.Descendants("Question").Count();

                MessageBox.Show($"Успешно!\n\n" +
                                $"Путь: {_xmlPath}\n" +
                                $"Вопросов было: {countBefore}\n" +
                                $"Вопросов стало: {countAfter}\n\n");

                txtQuestion.Clear();
                txtVariants.Clear();
                cbCorrectAnswer.Items.Clear();
                RefreshTopics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка: " + ex.Message);
            }
        }
    }
}