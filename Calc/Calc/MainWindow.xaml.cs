using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RunButton.IsEnabled = false;
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "1";
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "2";
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "3";
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "4";
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "5";
        }
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "6";
        }
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "7";
        }
        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "8";
        }
        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "9";
        }
        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "0";
        }
        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "+";
        }
        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "-";
        }
        private void ButtonDelit_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "/";
        }
        private void ButtonUmnozhit_Click(object sender, RoutedEventArgs e)
        {
            MathProblemInput.Text += "*";
        }

        private void MathProblemInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MathProblemInput.Text == String.Empty)
            {
                RunButton.IsEnabled = false;
            }
            else
            {
                RunButton.IsEnabled = true;
            }
        }
        static async void LogToFileAsync(string message)
        {
            using (StreamWriter file = new StreamWriter("log.txt", true))
            {
                var dateTime = DateTime.Now;
                var msg = $"[{dateTime:G}] INFO : {message}";
                await file.WriteLineAsync(msg);
            }
        }
        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            string expression;
            expression = MathProblemInput.Text;
            List<double> vs = new List<double>();
            List<char> action = new List<char>();
            string[] strArr = null;
            int count = 0;
            char[] splitchar = { '+', '-', '*', '/' };
            strArr = expression.Split(splitchar);

            for (count = 0; count <= strArr.Length - 1; count++)
            {
                string temp_str;
                temp_str = strArr[count];
                temp_str = temp_str.Trim(new char[] { '(', ')' });
                vs.Add(Convert.ToDouble(temp_str));

            }
            string copyexpression = expression;
            char[] arr = expression.ToCharArray();
            foreach (var s in arr)
            {
                if (s == '+' || s == '-' || s == '*' || s == '/')
                {
                    action.Add(s);

                }
            }
            List<char> action_withbracket = new List<char>();
            foreach (var s in arr)
            {
                if (s == '+' || s == '-' || s == '*' || s == '/' || s == '(' || s == ')')
                {
                    action_withbracket.Add(s);

                }
            }
            int move_index = 0;
            bool next = false;
            double result = 0;
            do
            {
                next = false;
                for (int i = 0; i < action.Count; i++)
                {
                    if (action[i] == '*' || action[i] == '/')
                    {
                        switch (action[i])
                        {
                            case '*':
                                result = vs[i] * vs[i + 1];

                                break;
                            case '/':
                                result = vs[i] / vs[i + 1];
                                break;
                        }
                        vs.RemoveAt(i);
                        vs.RemoveAt(i);
                        vs.Insert(i, result);

                        action.RemoveAt(i);

                        next = true;
                    }

                }
            } while (next == true);
            int count_action = action.Count;

            double temp_result = 0;
            double file_result;
            if (action.Count != 0)
            {
                for (int i = 0; i < count_action; i++)
                {
                    switch (action[i])
                    {
                        case '+':
                            temp_result = vs[i] + vs[i + 1];
                            break;
                        case '-':
                            temp_result = vs[i] - vs[i + 1];
                            break;
                        case '*':
                            temp_result = vs[i] * vs[i + 1];
                            break;
                        case '/':
                            temp_result = vs[i] / vs[i + 1];
                            break;
                    }
                    vs[i + 1] = temp_result;
                }

                MessageBox.Show($"{temp_result}", "ОТВЕТ:", MessageBoxButton.OK, MessageBoxImage.Information);
                file_result = temp_result;
            }
            else
            {
                MessageBox.Show($"{result}", "ОТВЕТ:", MessageBoxButton.OK, MessageBoxImage.Information);
                file_result = result;
            }

            string message;
            message = expression + " = " + file_result;
            LogToFileAsync(message);
        }
    }
}
