using System.Windows;
using System.Windows.Controls;


namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для Input.xaml
    /// </summary>
    public partial class Input : Window
    {
        public string Entry;
        public bool Result = false;
        public Input()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Entry = inputEntry.Text;
            if ( Entry.Length < 2 || Entry.Contains(@"\"))
            {
                Warning();
                return;
            }
            Result = true;
            this.Close();
        }
        public void Warning(string warningText = "Поле было пустым, или имело специальные символы")
        {
            warning.Text = warningText;
            warning.Visibility = Visibility.Visible;
        }
    }
}
