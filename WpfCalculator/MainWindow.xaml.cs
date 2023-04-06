using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;


namespace WpfCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double lastNumber, result;
        string selectedOperation = "";
        SelectedOperator selectedOperator;

        private Dictionary<Button, double> buttonsValues;

        public MainWindow()
        {
            InitializeComponent();

            cButton.Click += CButton_Click;
            backButton.Click += Back_Click;
            pointButton.Click += PointButton_Click;
            equalButton.Click += EqualButton_Click;

            


            buttonsValues = new Dictionary<Button, double>
            {
            {zeroButton, 0},
            {oneButton, 1},
            {twoButton, 2},
            {threeButton, 3},
            {fourButton, 4},
            {fiveButton, 5},
            {sixButton, 6},
            {sevenButton, 7},
            {eightButton, 8},
            {nineButton, 9},
            };
            
        }


        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if(double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.subtraction:
                        result = SimpleMath.Subtraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                    case SelectedOperator.multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }


        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (buttonsValues.TryGetValue(sender as Button, out double selectedValue))
            {
                if (resultLabel.Content.ToString() == "0")
                {
                    resultLabel.Content = $"{selectedValue}";
                }
                else
                {
                    resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
                }
            }
            
            if (selectedOperator == SelectedOperator.multiplication)
                operationLabel.Content = "*";
            else if (selectedOperator == SelectedOperator.division)
                operationLabel.Content = "/";
            else if (selectedOperator == SelectedOperator.addition)
                operationLabel.Content = "+";
            else if (selectedOperator == SelectedOperator.subtraction)
                operationLabel.Content = "-";
            else
                operationLabel.Content = "";

            operationLabel.Content = "";
        }


        private enum SelectedOperator
        {
            multiplication,
            division,
            addition,
            subtraction
        }


        private void OperationButton_Click(Object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out lastNumber))
            {
                resultLabel.Content = "0";
            }


            string operationText = "";
            if (sender == multiplicationButton)
            {
                selectedOperator = SelectedOperator.multiplication;
                operationText = "*";
            }
            else if (sender == divisionButton)
            {
                selectedOperator = SelectedOperator.division;
                operationText = "/";
            }
            else if (sender == plusButton)
            {
                selectedOperator = SelectedOperator.addition;
                operationText = "+";
            }
            else if (sender == minusButton)
            {
                selectedOperator = SelectedOperator.subtraction;
                operationText = "-";
            }

            operationLabel.Content = operationText;

            /*switch (sender)
            {
                case Button multiplicationButton:
                    selectedOperator = SelectedOperator.Multiplication;
                    break;
                case Button divisionButton:
                    selectedOperator = SelectedOperator.Division;
                    break;
                case Button plusButton:
                    selectedOperator = SelectedOperator.Addition;
                    break;
                case Button minusButton:
                    selectedOperator = SelectedOperator.Subtraction;
                    break;
            }*/
        }


        


        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            string currentText = resultLabel.Content.ToString();
            if(currentText.Length > 0)
            {
                currentText = currentText.Substring(0, currentText.Length - 1);
            }
            resultLabel.Content = currentText;
            
        }


        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains("."))
            {
                resultLabel.Content += ".";

            }
        }


        private void doubleZeroButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что введенное число не равно нулю и содержит хотя бы одну цифру
            if (resultLabel.Content.ToString() != "0" && resultLabel.Content.ToString() != "-0" && !string.IsNullOrEmpty(resultLabel.Content.ToString()))
            {
                // Добавляем два нуля к текущему содержимому label'а
                resultLabel.Content = $"{resultLabel.Content}00";
            }
        }


    
    }

}
