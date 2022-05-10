using System.Windows;


namespace Tisinalite
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pages.Login login;
        public MainWindow()
        {
            InitializeComponent();

            login = new Pages.Login();
            MainFrame.Navigate(login);
            Global.MainFrame = MainFrame;

        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // При закрытии приложения
        }
    }
}
