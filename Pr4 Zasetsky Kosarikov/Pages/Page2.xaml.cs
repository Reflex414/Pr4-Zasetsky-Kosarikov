using System;
using System.Windows;
using System.Windows.Controls;

namespace Pr4_Zasetsky_Kosarikov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputX.Text) ||
                string.IsNullOrWhiteSpace(InputY.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля (x и b).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(InputX.Text, out double x))
            {
                MessageBox.Show("Значение x должно быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(InputY.Text, out double b))
            {
                MessageBox.Show("Значение b должно быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                double result = CalculateFunction(x, b);
                ResultBox.Text = result.ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateFunction(double x, double b)
        {
            double fx;
            if (FuncSh.IsChecked == true)
            {
                fx = (Math.Exp(x) - Math.Exp(-x)) / 2;
            }
            else if (FuncX2.IsChecked == true)
            {
                fx = x * x;
            }
            else
            {
                fx = Math.Exp(x);
            }

            double xb = x * b;

            if (xb > 0.5 && xb < 10)
            {
                return Math.Exp(fx - Math.Abs(b));
            }
            else if (xb > 0.1 && xb < 0.5)
            {
                return Math.Sqrt(Math.Abs(fx) + Math.Abs(b));
            }
            else
            {
                return 2 * fx * fx;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputX.Clear();
            InputY.Clear();
            ResultBox.Text = "Результат";
            FuncSh.IsChecked = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            Application.Current.MainWindow?.Show();
        }
    }
}