namespace Calculator
{
    public partial class Form1 : Form
    {


        double num1 = 0, num2 = 0, num3=0, result = 0;
        string operation = "";

        public Form1()
        {

            //for (int i = 0; i < 10; i++)
            //{
            //    Button button = new Button();
            //    button.Text = i.ToString();
            //    //button.Click += new EventHandler(button_Click);
            //    button.Width = 40;
            //    button.Height = 40;
            //    button.Left = 10 + (i % 3) * 45;
            //    button.Top = 10 + (i / 3) * 45;
            //    this.Controls.Add(button);
            //}

            //// Создание кнопок для операций
            //Button buttonAdd = new Button();
            //buttonAdd.Text = "+";
            //// buttonAdd.Click += new EventHandler(operator_Click);
            //buttonAdd.Width = 45;
            //buttonAdd.Height = 45;
            //buttonAdd.Left = 10 + 3 * 45;
            //buttonAdd.Top = 10;
            //this.Controls.Add(buttonAdd);

            //Button buttonSubtract = new Button();
            //buttonSubtract.Text = "-";
            //// buttonSubtract.Click += new EventHandler(operator_Click);
            //buttonSubtract.Width = 45;
            //buttonSubtract.Height = 45;
            //buttonSubtract.Left = 10 + 3 * 45;
            //buttonSubtract.Top = 60;
            //this.Controls.Add(buttonSubtract);

            //Button buttonMultiply = new Button();
            //buttonMultiply.Text = "*";
            //// buttonMultiply.Click += new EventHandler(operator_Click);
            //buttonMultiply.Width = 45;
            //buttonMultiply.Height = 45;
            //buttonMultiply.Left = 10 + 3 * 45;
            //buttonMultiply.Top = 110;
            //this.Controls.Add(buttonMultiply);

            //Button buttonDivide = new Button();
            //buttonDivide.Text = "/";
            ////buttonDivide.Click += new EventHandler(operator_Click);
            //buttonDivide.Width = 45;
            //buttonDivide.Height = 45;
            //buttonDivide.Left = 10 + 3 * 45;
            //buttonDivide.Top = 160;
            //this.Controls.Add(buttonDivide);

            //// Создание кнопки равно
            //Button buttonEqual = new Button();
            //buttonEqual.Text = "=";
            //// buttonEqual.Click += new EventHandler(buttonEqual_Click);
            //buttonEqual.Width = 90;
            //buttonEqual.Height = 45;
            //buttonEqual.Left = 10;
            //buttonEqual.Top = 210;
            //this.Controls.Add(buttonEqual);

            //// Создание кнопки очистки
            //Button buttonClear = new Button();
            //buttonClear.Text = "C";
            //// buttonClear.Click += new EventHandler(buttonClear_Click);
            //buttonClear.Width = 90;
            //buttonClear.Height = 45;
            //buttonClear.Left = 10 + 3 * 45;
            //buttonClear.Top = 210;
            //this.Controls.Add(buttonClear);

            //// Создание текстового поля
            //TextBox textBox1 = new TextBox();
            //textBox1.Width = 230;
            //textBox1.Name = "textBox1";
            //textBox1.Height = 45;
            //textBox1.Left = 10;
            //textBox1.Top = 10;
            //textBox1.TextAlign = HorizontalAlignment.Right;
            //this.Controls.Add(textBox1);

            InitializeComponent();
           
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

   


        //private void numberButton_Click(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    if (textBox1.Text == "0" && button.Text != ",")
        //    {
        //        textBox1.Clear();
        //    }
        //    textBox1.Text = textBox1.Text + button.Text;
        //}

        private void operationButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;
            num1 = Convert.ToDouble(textBox1.Text);
            textBox1.Clear();
        }

        private void Geer_Click(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (textBox1.Text == "0" && button.Text != ",")
            {
                textBox1.Clear();
            }
            textBox1.Text = textBox1.Text + button.Text;
        }

        //ВЫполнение последовательных операций
        private void FullCalculate_Click(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Calculate(textBox1.Text));
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            num2 = Convert.ToDouble(textBox1.Text);

            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
            }

            textBox1.Text = result.ToString();
            operation = "";
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            num1 = 0;
            num2 = 0;
            result = 0;
            operation = "";
        }




        public static double Calculate(string expression)
        {
            List<string> tokens = new List<string>();
            string number = "";

            foreach (char c in expression)
            {
                if (char.IsDigit(c) || c == '.')
                {
                    number += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        tokens.Add(number);
                        number = "";
                    }
                    tokens.Add(c.ToString());
                }
            }

            if (!string.IsNullOrEmpty(number))
            {
                tokens.Add(number);
            }

            Stack<double> operands = new Stack<double>();
            Stack<string> operators = new Stack<string>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double operand))
                {
                    operands.Push(operand);
                }
                else
                {
                    while (operators.Count > 0 && GetPrecedence(token) <= GetPrecedence(operators.Peek()))
                    {
                        string op = operators.Pop();
                        double right = operands.Pop();
                        double left = operands.Pop();
                        double result = Calculate(left, right, op);
                        operands.Push(result);
                    }
                    operators.Push(token);
                }
            }

            while (operators.Count > 0)
            {
                string op = operators.Pop();
                double right = operands.Pop();
                double left = operands.Pop();
                double result = Calculate(left, right, op);
                operands.Push(result);
            }

            return operands.Pop();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = num3.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;


        }

        private void button18_Click(object sender, EventArgs e)
        {
            num3 = Convert.ToDouble(textBox1.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != '*' && e.KeyChar != '/')
                {
                    e.Handled = true;
                }
            
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;

        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Window;
           
        }

        public static double Calculate(double left, double right, string op)
        {
            switch (op)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    if (right != 0)
                        return left / right;
                    else
                        throw new DivideByZeroException();
                default:
                    throw new ArgumentException("Неизвестная операция");
            }
        }

        private static int GetPrecedence(string op)
        {
            if (op == "+" || op == "-")
                return 1;
            else if (op == "*" || op == "/")
                return 2;
            else
                throw new ArgumentException("Неизвестная операция");
        }


        public bool Check_wrong()
        {
            bool w = true;
            //textBox1.Text

         


            return false;
        }


    }



    }
