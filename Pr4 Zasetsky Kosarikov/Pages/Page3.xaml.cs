using System;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Pr4_Zasetsky_Kosarikov.Pages
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

            ChartFunction.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "y = 9·x⁴ + sin(57.2 + x)",
                    Values = new ChartValues<double>(),
                    PointGeometry = null, 
                    LineSmoothness = 1    
                }
            };
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputXMin.Text) ||
                string.IsNullOrWhiteSpace(InputXMax.Text) ||
                string.IsNullOrWhiteSpace(InputDx.Text))
            {
                MessageBox.Show("Заполните все поля (x начальное, x конечное, шаг).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(InputXMin.Text, out double xMin))
            {
                MessageBox.Show("x начальное должно быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(InputXMax.Text, out double xMax))
            {
                MessageBox.Show("x конечное должно быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!double.TryParse(InputDx.Text, out double dx))
            {
                MessageBox.Show("Шаг должен быть числом (используйте запятую для дробной части).", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dx <= 0)
            {
                MessageBox.Show("Шаг должен быть положительным числом.", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (xMin >= xMax)
            {
                MessageBox.Show("Начальное значение x должно быть меньше конечного.", "Ошибка ввода",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int maxPoints = 1000;
            if ((xMax - xMin) / dx > maxPoints)
            {
                MessageBox.Show($"Слишком много точек. Максимально допустимое количество: {maxPoints}. Увеличьте шаг или уменьшите интервал.", "Предупреждение",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var series = (LineSeries)ChartFunction.Series[0];
                var values = new ChartValues<double>();
                ResultBox.Clear();

                for (double x = xMin; x <= xMax; x += dx)
                {
                    double y = CalculateFunction(x);
                    values.Add(y);
                    ResultBox.AppendText($"x = {x:F4}\t y = {y:F4}\n");
                }

                series.Values = values;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private double CalculateFunction(double x)
        {
            return 9 * Math.Pow(x, 4) + Math.Sin(57.2 + x);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputXMin.Clear();
            InputXMax.Clear();
            InputDx.Clear();
            ResultBox.Clear();
            ((LineSeries)ChartFunction.Series[0]).Values.Clear();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            Application.Current.MainWindow?.Show();
        }
    }
}