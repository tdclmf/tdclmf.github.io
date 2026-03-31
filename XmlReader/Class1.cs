using System.Xml.Linq;

namespace Task_6
{
    public enum QuestionType
    {
        Anagram,    // Грамматика (вписать слово)
        Choice,     // Перевод (выбор/перетаскивание)
        FillIn      // Пропущенное слово (вставка в предложение)
    }

    // Модель вопроса
    public class Question
    {
        public QuestionType Type { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public int Score { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
        public string RightAnswer { get; set; }
    }

    public class XmlService
    {
        private readonly string _filePath;

        public XmlService(string fileName = "test_data.xml")
        {
            // Путь к файлу в папке с программой
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            // Если файла нет, создадим пустую структуру (для начальной работы)
            if (!File.Exists(_filePath))
            {
                CreateEmptyXml();
            }
        }

        /// <summary>
        /// Получает список случайных вопросов для конкретной темы и уровня
        /// </summary>
        /// <param name="topicName">Название темы</param>
        /// <param name="levelId">Номер уровня (1, 2 или 3)</param>
        /// <param name="count">Сколько случайных вопросов выбрать (по заданию 5 из 10)</param>
        public List<Question> GetQuestions(string topicName, int levelId, int count = 5)
        {
            XDocument doc = XDocument.Load(_filePath);

            var questions = doc.Descendants("Topic")
                .Where(t => t.Attribute("name")?.Value == topicName)
                .Descendants("Level")
                .Where(l => l.Attribute("id")?.Value == levelId.ToString())
                .Descendants("Question")
                .Select(q => new Question
                {
                    Type = (QuestionType)Enum.Parse(typeof(QuestionType), q.Attribute("type").Value),
                    Text = q.Attribute("text").Value,
                    ImagePath = q.Attribute("src")?.Value,
                    Score = int.Parse(q.Attribute("score")?.Value ?? "10"),
                    RightAnswer = q.Elements("Answer")
                                   .FirstOrDefault(a => a.Attribute("right")?.Value == "yes")?.Value,
                    Answers = q.Elements("Answer").Select(a => a.Value).ToList()
                })
                .ToList();
            Random rnd = new Random();
            return questions.OrderBy(x => rnd.Next()).Take(count).ToList();
        }

        /// <summary>
        /// Добавляет новый вопрос в XML (для панели администратора)
        /// </summary>
        public void AddQuestion(string topicName, int levelId, Question q)
        {
            XDocument doc = XDocument.Load(_filePath);

            // Ищем нужный узел уровня в нужной теме
            var levelNode = doc.Descendants("Topic")
                .Where(t => t.Attribute("name")?.Value == topicName)
                .Descendants("Level")
                .FirstOrDefault(l => l.Attribute("id")?.Value == levelId.ToString());

            if (levelNode == null) throw new Exception("Тема или уровень не найдены!");

            // Создаем XML-элемент вопроса
            XElement newQuestion = new XElement("Question",
                new XAttribute("type", q.Type.ToString()),
                new XAttribute("text", q.Text),
                new XAttribute("score", q.Score)
            );

            if (!string.IsNullOrEmpty(q.ImagePath))
                newQuestion.Add(new XAttribute("src", q.ImagePath));
            newQuestion.Add(new XElement("Answer", new XAttribute("right", "yes"), q.RightAnswer));
            foreach (var ans in q.Answers.Where(a => a != q.RightAnswer))
            {
                newQuestion.Add(new XElement("Answer", new XAttribute("right", "no"), ans));
            }

            levelNode.Add(newQuestion);
            doc.Save(_filePath);
        }

        /// <summary>
        /// Получает список всех доступных тем
        /// </summary>
        public List<string> GetTopics()
        {
            XDocument doc = XDocument.Load(_filePath);
            return doc.Descendants("Topic")
                      .Select(t => t.Attribute("name").Value)
                      .ToList();
        }

        private void CreateEmptyXml()
        {
            XDocument doc = new XDocument(
                new XElement("EngTest",
                    new XElement("Topic", new XAttribute("name", "Грамматика"),
                        new XElement("Level", new XAttribute("id", "1"), new XAttribute("minScoreToUnlock", "0")),
                        new XElement("Level", new XAttribute("id", "2"), new XAttribute("minScoreToUnlock", "80")),
                        new XElement("Level", new XAttribute("id", "3"), new XAttribute("minScoreToUnlock", "80"))
                    )
                )
            );
            doc.Save(_filePath);
        }
    }
    public static class GameState
    {
        public static Dictionary<string, int> Scores = new Dictionary<string, int>();

        public static bool IsLevelAvailable(string topic, int level)
        {
            if (level == 1) return true; // Первый уровень всегда открыт
            string key = $"{topic}_{level - 1}";
            return Scores.ContainsKey(key) && Scores[key] >= 80;
        }
    }
}