namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public static int notation = 10;
        public Form1()
        {
            InitializeComponent();
            button2.Click += MyButtonClick;
            button3.Click += MyButtonClick;
            button4.Click += MyButtonClick;
            button5.Click += MyButtonClick;
            button6.Click += MyButtonClick;
            button7.Click += MyButtonClick;
            button8.Click += MyButtonClick;
            button9.Click += MyButtonClick;
            button10.Click += MyButtonClick;
            button11.Click += MyButtonClick;
            button12.Click += MyButtonClick;
            button13.Click += MyButtonClick;
            button14.Click += MyButtonClick;
            button15.Click += MyButtonClick;
            button16.Click += MyButtonClick;
            button17.Click += MyButtonClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String expr = textBox1.Text;
            String ans = Compute(expr);
            textBox2.Text = ans;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private String Compute(String expr)
        {
            if (expr.StartsWith('('))
            {
                expr = GetRidOfBrackets(expr);
            }
            
            bool inBrackets = false;
            int endBrackets = 0;
            int begBrackets = 0;
            int leftPlMi = -1;
            int leftMulDiv = -1;
            for (int i = expr.Length - 1; i >= 0; i--)
            {
                if (expr[i] == ')')
                {
                    endBrackets++;
                }
                if (expr[i] == '(')
                {
                    begBrackets++;
                }
                if (begBrackets != endBrackets)
                {
                    inBrackets = true; 
                } else
                {
                    inBrackets = false;
                }
                if ((expr[i] == '+' || expr[i] == '-') && !inBrackets)
                {
                    if (leftPlMi == -1)
                    {
                        leftPlMi = i;
                    }
                }
                if ((expr[i] == '*' || expr[i] == '/') && !inBrackets)
                {
                    if (leftMulDiv == -1)
                    {
                        leftMulDiv = i;
                    }
                }
            }
            if (leftPlMi == 0)
            {
                leftPlMi = -1;
            }

            if (leftPlMi != -1)
            {
                int i = leftPlMi;
                if (expr[i] == '+')
                {
                    return Sum(Compute(expr.Substring(0, i)), Compute(expr.Substring(i + 1, expr.Length - 1 - i)));
                }
                else
                {
                    return Sub(Compute(expr.Substring(0, i)), Compute(expr.Substring(i + 1, expr.Length - 1 - i)));
                }
            }
            else
            {
                if (leftMulDiv != -1)
                {
                    int i = leftMulDiv;
                    if (expr[i] == '*')
                    {
                        return Mul(Compute(expr.Substring(0, i)), 
                            Compute(expr.Substring(i + 1, expr.Length - 1 - i)));
                    }
                    else
                    {
                        return Div(Compute(expr.Substring(0, i)), 
                            Compute(expr.Substring(i + 1, expr.Length - 1 - i)));
                    }
                }
                else
                {
                    if (expr.StartsWith('-'))
                    {
                        return NormalizeMinusSigns('-' + Compute(expr.Substring(1)));
                    } else
                    {
                        return expr;
                    }     
                }
            }
        }

        public String NormalizeMinusSigns(String expr)
        {
            if (expr.StartsWith('-'))
            {
                int i = 0;
                while (expr[i] == '-')
                {
                    i++;
                }
                if (i % 2 == 1)
                {
                    expr = '-' + expr.Substring(i);
                }
                else
                {
                    expr = expr.Substring(i);
                }
            }
            return expr;
        }
        public String Sum(String st1, String st2)
        {
            if (st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return '-' + SumOfPositive(st1.Substring(1), st2.Substring(1));
            } 
            if (st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return SubOfPositive(st2, st1.Substring(1));
            } 
            if (!st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return SubOfPositive(st1, st2.Substring(1));
            } 
            if (!st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return SumOfPositive(st1, st2);
            }
            return "ERROR";
        }
        public String SumOfPositive(String st1, String st2)
        {
            int razm = notation;
            String ans = "";
            if (st1.Length > st2.Length)
            {
                String st = st1;
                st1 = st2;
                st2 = st;
            }
            st1 = Reverse(st1);
            st2 = Reverse(st2);
            int res = 0;
            int carry = 0;
            for (int i = 0; i < st1.Length; i++)
            {
                res = ((((int)st1[i] - 48) + ((int)st2[i] - 48)) + carry);
                carry = res / razm;
                res %= razm;
                ans += res;
            }
            for (int i = st1.Length; i < st2.Length; i++)
            {
                res = res = (int)st2[i] - 48 + carry;
                carry = res / razm;
                res %= razm;
                ans += res;
            }
            if (carry != 0)
            {
                ans += carry;
                carry = 0;
            }
            ans = Reverse(ans);
            return ans;
        }
        public String Sub(String st1, String st2)
        {
            if (st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return SubOfPositive(st2.Substring(1), st1.Substring(1));
            }
            if (st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return '-' + SumOfPositive(st1.Substring(1), st2);
            }
            if (!st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return SumOfPositive(st1, st2.Substring(1));
            }
            if (!st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return SubOfPositive(st1, st2);
            }
            return "ERROR";
        }
        public String SubOfPositive(String st1, String st2)
        {
            int razm = notation;
            String ans = "";
            bool minus = false;
            if (Bigger(st2, st1))
            {
                minus = true;
                string st = st2;
                st2 = st1;
                st1 = st;
            }

            st1 = Reverse(st1);
            st2 = Reverse(st2);
            int carry = 0;
            int res = 0;
            for (int i = 0; i < st2.Length; i++)
            {
                int big = (int) st1[i] - 48;
                int small = (int) st2[i] - 48;
                res = big - small - carry;
                if (res < 0)
                {
                    carry = 1;
                    res += razm;
                } else
                {
                    carry = 0;
                }
                ans += res;
            }
            for (int i = st2.Length; i < st1.Length; i++)
            {
                int big = (int)st1[i] - 48;
                res = big - carry;
                if (res < 0)
                {
                    carry = 1;
                    res += razm;
                } else
                {
                    carry = 0;
                }
                ans += res;
            }
            if (carry != 0)
            {
                ans += carry;
                carry = 0;
            }
            ans = Reverse(ans);
            while (ans.Length > 1 && ans.StartsWith('0'))
            {
                ans = ans.Substring(1, ans.Length - 1);
            }
            if (minus)
            {
                ans = '-' + ans;
            }
            
            return ans;
        }
        public String Mul(String st1, String st2)
        {
            if (st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return MulOfPositive(st2.Substring(1), st1.Substring(1));
            }
            if (st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return '-' + MulOfPositive(st1.Substring(1), st2);
            }
            if (!st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return '-' + MulOfPositive(st1, st2.Substring(1));
            }
            if (!st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return MulOfPositive(st1, st2);
            }
            return "ERROR";
        }
        public String MulOfPositive(String st1, String st2)
        {
            String ans = "";
            for (int i = st1.Length - 1; i >= 0; i--)
            {
                String sum = "0";
                for (int j = 0; j < (int) st1[i] - 48; j++)
                {
                    sum = SumOfPositive(sum, st2);
                }
                for (int j = 0; j < st1.Length - 1 - i; j++)
                {
                    sum += '0';
                }
                ans = SumOfPositive(ans, sum);
            }
            return ans;
        }
        public String Div(String st1, String st2)
        {
            if (st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return DivOfPositive(st2.Substring(1), st1.Substring(1));
            }
            if (st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return '-' + DivOfPositive(st1.Substring(1), st2);
            }
            if (!st1.StartsWith('-') && st2.StartsWith('-'))
            {
                return '-' + DivOfPositive(st1, st2.Substring(1));
            }
            if (!st1.StartsWith('-') && !st2.StartsWith('-'))
            {
                return DivOfPositive(st1, st2);
            }
            return "ERROR";
        }
        public String DivOfPositive(String st1, String st2)
        {
            int razm = notation;
            String ans = "";
            String dividend;
            int index;
            String remainder;
            // st1 >= st2
            if (!Bigger(st2, st1))
            {
                dividend = st1.Substring(0, st2.Length);
                index = st2.Length;
                if (Bigger(st2, dividend))
                {
                    dividend += st1[st2.Length];
                    index++;
                }
            } else
            {
                dividend = st1;
                index = st1.Length;
            }
            bool active = true;
            bool comma = false;
            int numsAfterComma = 0;
            while (active)
            {
                if (comma)
                {
                    numsAfterComma++;
                    if (numsAfterComma > 5) break;
                }
                String sum = "0";
                int num = 0;
                // dividend >= st2
                if (!Bigger(st2, dividend)) {
                    for (int i = 1; i < razm + 1; i++)
                    {
                        sum = Sum(sum, st2);
                        if (Bigger(sum, dividend))
                        {
                            sum = Sub(sum, st2);
                            num = i - 1;
                            break;
                        }
                    }
                    dividend = Sub(dividend, sum);
                    if (dividend == "0")
                    {
                        dividend = "";
                    }
                    ans += num;
                    if (index >= st1.Length)
                    {
                        if (dividend == "")
                        {
                            break;
                        }
                        else
                        {
                            if (!comma) ans += ',';
                            comma = true;
                            dividend += '0';
                            index++;
                        }
                    } else
                    {
                        dividend += st1[index];
                        index++;
                    }
                } else
                {
                    ans += '0';
                    if (index >= st1.Length)
                    {
                        if (dividend == "0" || dividend == "")
                        {
                            break;
                        }
                        else
                        {
                            if (!comma) ans += ',';
                            comma = true;
                            dividend += '0';
                            index++;
                        }
                    }
                    else
                    {
                        dividend += st1[index];
                        index++;
                    }      
                }
                
            }
            return ans;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static bool Bigger(String st1, String st2)
        {
            bool bigger = false;
            if (st1.Length > st2.Length)
            {
                bigger = true;
            } else if (st1.Length == st2.Length)
            {
                for (int i = 0; i < st1.Length; i++)
                {
                    if (st1[i] > st2[i])
                    {
                        bigger = true;
                        break;
                    }
                    if (st1[i] < st2[i])
                    {
                        break;
                    }
                }
            }
            return bigger;
        }

        public String GetRidOfBrackets(String expr)
        {
            int l = 0;
            int r = 0;
            bool delete = true;
            for (int i = 0; i < expr.Length; i++)
            { 
                if (expr[i] == '(')
                {
                    l++;
                }
                if (expr[i] == ')')
                {
                    r++;
                }
                if (l == r && i != expr.Length - 1)
                {
                    delete = false;
                    break;
                }
            }
            if (delete)
            {
                expr = expr.Substring(1, expr.Length - 2);
            }
            return expr;
        }
        
        private void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String text = button.Text;
            int num;
            if (int.TryParse(text, out num))
            {
                if (num >= notation)
                {
                    throw new NotCorrectNotationException("¬водите числа текущей системы счислени€");
                } else
                {
                    textBox1.Text += num;
                }
            } else
            {
                textBox1.Text += button.Text;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {

        }
        private void button9_Click(object sender, EventArgs e)
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {

        }
        private void button11_Click(object sender, EventArgs e)
        {

        }
        private void button12_Click(object sender, EventArgs e)
        {

        }
        private void button13_Click(object sender, EventArgs e)
        {

        }
        private void button14_Click(object sender, EventArgs e)
        {

        }
        private void button15_Click(object sender, EventArgs e)
        {

        }
        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int num = int.Parse(textBox3.Text);
            notation = num;
        }
    }
}