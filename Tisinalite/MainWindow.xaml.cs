using System.Windows;


namespace Tisinalite
{
    // Это главное окно, где перелистываются почти все страницы
    public partial class MainWindow : Window
    {
        Pages.Login login;
        public MainWindow()
        {
            InitializeComponent();
            // Первой страницей будет открываться авторизация
            MainFrame.Navigate(new Pages.Login());
            Global.MainFrame = MainFrame;
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // При закрытии приложения
        }
    }
}
