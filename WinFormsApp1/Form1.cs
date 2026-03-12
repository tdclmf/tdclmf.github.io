namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isControl = char.IsControl(e.KeyChar);
            bool isSeparator = (e.KeyChar == ',' || e.KeyChar == '.');
            if (tb.Name == "textBox2" && e.KeyChar == '-' && tb.SelectionStart == 0 && !tb.Text.Contains("-"))
            {
                return;
            }
            if (!isDigit && !isControl && !isSeparator)
            {
                e.Handled = true;
            }
            if (isSeparator && (tb.Text.Contains(",") || tb.Text.Contains(".")))
            {
                e.Handled = true;
            }

        }
        public static long Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }

        private void textBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.Name == "textBox1")
            {

                string input = tb.Text.Replace('.', ',');

                if (double.TryParse(input, out double value))
                {
                    if (value <= 0 || value >= 1)
                    {
                        tb.Text = "0,1";
                    }
                    else
                    {
                        tb.Text = input;
                    }
                }
                else
                {
                    tb.Text = "0,1";
                }
            }
            else
            {
                string input = tb.Text.Replace('.', ',');
                if (!double.TryParse(input, out double value))
                {
                    tb.Text = "0";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double precision = double.Parse(textBox1.Text.Replace('.', ','));
                double x = double.Parse(textBox2.Text.Replace('.', ','));
                double sum = 0;
                double term = 1;
                double etalon = Math.Exp(-x);
                int n = 1;
                double temp = 0;
                while (Math.Abs(term - temp) >= precision)
                {
                    sum += term;
                    temp = term;
                    term = (Math.Pow(-1, n) * Math.Pow(x, n)) / Factorial(n);
                    n++;
                }
                label4.Text = "e^-x = " + etalon.ToString("F15") + "\n" +
                      "Сумма ряда: " + sum.ToString("F15") + "\n" +
                      "Количество членов ряда: " + n.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения (используйте запятую).");
            }
        }
    }
}
