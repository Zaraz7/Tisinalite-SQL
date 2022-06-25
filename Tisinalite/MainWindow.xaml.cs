using System.Windows;


namespace Tisinalite
{
    // Это главное окно, где перелистываются почти все страницы
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Первой страницей будет открываться авторизация
            MainFrame.Navigate(new Pages.Login());
            Global.MainFrame = MainFrame;
        }

    }
}
