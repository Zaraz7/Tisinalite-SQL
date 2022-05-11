using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для GroupEditor.xaml
    /// </summary>
    public partial class GroupEditor : Page
    {
        Groups group;
        public GroupEditor()
        {
            if (group == null)
                group = new Groups();
            InitializeComponent();
        }

        private void SaveGroup_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                MessageBox.Show("Неправильное имя");
                return;
            }
            group.Access = ((ComboBoxItem)cbAccess.SelectedItem).Tag.ToString();
            Debug.WriteLine(group.Title + " " + group.Access);
        }
    }
}
