using System.Xml.Linq;

namespace XmlReader
{
    public class Question
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public List<string> Answers { get; set; }
        public string RightAnswer { get; set; }

        public List<Question> LoadQuestions(string topicName, int levelId)
        {
            XDocument doc = XDocument.Load("data.xml");
            var qList = doc.Descendants("Topic")
                .Where(t => t.Attribute("name").Value == topicName)
                .Descendants("Level")
                .Where(l => l.Attribute("id").Value == levelId.ToString())
                .Descendants("Question")
                .Select(q => new Question
                {
                    Type = q.Attribute("type").Value,
                    Text = q.Attribute("text").Value,
                    ImagePath = q.Attribute("src")?.Value,
                    Answers = q.Elements("Answer").Select(a => a.Value).ToList(),
                    RightAnswer = q.Elements("Answer").First(a => a.Attribute("right")?.Value == "yes").Value
                }).ToList();

            Random rnd = new Random();
            return qList.OrderBy(x => rnd.Next()).Take(5).ToList();
        }
        public void AddQuestion(string topicName, int levelId, Question newQ)
        {
            XDocument doc = XDocument.Load("data.xml");
            var levelElement = doc.Descendants("Topic")
                .First(t => t.Attribute("name").Value == topicName)
                .Descendants("Level")
                .First(l => l.Attribute("id").Value == levelId.ToString());

            XElement qNode = new XElement("Question",
                new XAttribute("type", newQ.Type),
                new XAttribute("text", newQ.Text),
                new XAttribute("src", newQ.ImagePath ?? ""),
                new XElement("Answer", new XAttribute("right", "yes"), newQ.RightAnswer)
            );


            foreach (var alt in newQ.Answers.Where(a => a != newQ.RightAnswer))
            {
                qNode.Add(new XElement("Answer", new XAttribute("right", "no"), alt));
            }

            levelElement.Add(qNode);
            doc.Save("data.xml");
        }
    }


}
