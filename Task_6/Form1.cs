namespace Task_6
{
    public partial class Form1 : Form
    {
        XmlService xml = new XmlService();
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(xml.GetTopics().ToArray());
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            RefreshLevels();
        }
        private void RefreshLevels()
        {
            string topic = comboBox1.SelectedItem.ToString();
            btnLevel2.Enabled = GameState.IsLevelAvailable(topic, 2);
            btnLevel3.Enabled = GameState.IsLevelAvailable(topic, 3);
        }

        private void btnLevel_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int level = int.Parse(btn.Tag.ToString()); // Достаем свойство Tag у кнопок
            string topic = comboBox1.SelectedItem.ToString();
            if (topic != null)
            {
                TestForm tf = new TestForm(topic, level);
                tf.ShowDialog(); // Ждем завершения теста
                RefreshLevels(); // Обновляем доступность уровней после теста
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => RefreshLevels();
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshLevels();
        }
    }
}
