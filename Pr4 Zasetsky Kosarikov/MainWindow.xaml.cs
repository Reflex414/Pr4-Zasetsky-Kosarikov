using Pr4_Zasetsky_Kosarikov.Pages;
using System.Windows;

namespace Pr4_Zasetsky_Kosarikov
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page1Button_Click(object sender, RoutedEventArgs e)
        {
            Window pageWindow = new Window
            {
                Title = "Страница 1",
                Content = new Page1(),
                Width = 650,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            pageWindow.Show();
            this.Hide();
        }

        private void Page2Button_Click(object sender, RoutedEventArgs e)
        {
            Window pageWindow = new Window
            {
                Title = "Страница 2",
                Content = new Page2(),
                Width = 650,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            pageWindow.Show();
            this.Hide();
        }

        private void Page3Button_Click(object sender, RoutedEventArgs e)
        {
            Window pageWindow = new Window
            {
                Title = "Страница 3",
                Content = new Page3(),
                Width = 800,
                Height = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            pageWindow.Show();
            this.Hide();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из приложения?",
                                         "Подтверждение выхода",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}