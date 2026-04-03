namespace Task_6
{
    public partial class Form1 : Form
    {
        XmlService xml;
        public Form1()
        {
            InitializeComponent();
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
                tf.ShowDialog();
                RefreshLevels();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => RefreshLevels();
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Выберите папку с тестом";
                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        GameState.CurrentFolder = fbd.SelectedPath;

                        string[] xmlFiles = Directory.GetFiles(GameState.CurrentFolder, "*.xml");
                        if (xmlFiles.Length == 0)
                        {
                            MessageBox.Show("В выбранной папке нет XML файлов!");
                            this.Close();
                            return;
                        }

                        InitApp();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критическая ошибка при запуске: " + ex.Message);
                this.Close();
            }
        }
        private void InitApp()
        {
            XmlService xml = new XmlService();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(xml.GetTopics().ToArray());
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            RefreshLevels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.ShowDialog();
            InitApp();
        }
    }
}
