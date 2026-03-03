using System;
using System.Windows;
using System.Windows.Controls;

namespace Pr4_Zasetsky_Kosarikov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputX.Text) ||
                string.IsNullOrWhiteSpace(InputY.Text) ||
                string.IsNullOrWhiteSpace(InputZ.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля (x, y, z).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(InputX.Text, out double x))
            {
                MessageBox.Show("Значение x должно быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(InputY.Text, out double y))
            {
                MessageBox.Show("Значение y должно быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(InputZ.Text, out double z))
            {
                MessageBox.Show("Значение z должно быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                double result = CalculateFunction(x, y, z);
                ResultBox.Text = result.ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CalculateFunction(double x, double y, double z)
        {
            double cubicRootX;
            if (x >= 0)
                cubicRootX = Math.Pow(x, 1.0 / 3.0);
            else
                cubicRootX = -Math.Pow(-x, 1.0 / 3.0);

            if (x < 0 && Math.Abs((y + 2) - Math.Round(y + 2)) > 0.000001)
            {
                throw new ArgumentException("При отрицательном x степень (y+2) должна быть целым числом");
            }
            double powerXY = Math.Pow(x, y + 2);

            double sumUnderRoot = cubicRootX + powerXY;
            double underFirstRoot = 10 * sumUnderRoot;

            if (underFirstRoot < 0)
            {
                throw new ArgumentException("Подкоренное выражение √(10*(∛x + x^(y+2))) не может быть отрицательным");
            }

            double firstPart = Math.Sqrt(underFirstRoot);

            if (Math.Abs(z) > 1)
            {
                throw new ArgumentException("Значение |z| должно быть <= 1 для вычисления arcsin(z)");
            }

            double arcsinZ = Math.Asin(z);
            double arcsinZSquare = arcsinZ * arcsinZ;
            double absXY = Math.Abs(x - y);
            double secondPart = arcsinZSquare - absXY;

            return firstPart * secondPart;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputX.Clear();
            InputY.Clear();
            InputZ.Clear();
            ResultBox.Text = "Результат";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();

            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }
    }
}