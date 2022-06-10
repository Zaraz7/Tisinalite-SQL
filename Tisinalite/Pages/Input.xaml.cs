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
        public Input(string messegeText = "Введите имя заметки", string title = "Введите данные")
        {
            InitializeComponent();
            // Опциональная информация, вводимые при создание окна
            messege.Text = messegeText;
            Title = title;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Entry = inputEntry.Text;
            if ( string.IsNullOrWhiteSpace(Entry) || Entry.Contains(@"\"))
            {
                Warning();
                return;
            }
            Result = true;
            this.Close();
        }
        // Если значения будут неправельными, то красным по белому будет выведено сообщение об ошибке
        public void Warning(string warningText = "Поле было пустым, или имело специальные символы")
        {
            warning.Text = warningText;
            warning.Visibility = Visibility.Visible;
        }
    }
}
