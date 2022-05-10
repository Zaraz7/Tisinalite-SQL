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
//using System.Windows.Shapes;
using System.Diagnostics;
using MdXaml;
using System.IO;
using Microsoft.Win32;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для General.xaml
    /// </summary>
    public partial class General : Page
    {
        private Settings _settings = Settings.GetSettings();
        //private string notePath;
        Users user;
        Notes openNote;
        public General(Users _user)
        {
            user = _user;
            InitializeComponent();

            UpdateTreeView();
            Binding binding = new Binding();
            binding.ElementName = "tbEditor";
            binding.Path = new PropertyPath("Text");
            Markdownview.SetBinding(MarkdownScrollViewer.MarkdownProperty, binding);

        }
        public void SaveSettings()
        {
            _settings.Save();
        }

        private void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //if (notePath != null)
            //    SaveFile();
            //NewFile();
            //tbEditor.Text = "";
            //UpdateTreeView();
        }
        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //SaveFile();
            //SaveSettings();
            //UpdateTreeView();
        }
        private void DeleteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //if (MessageBox.Show("Уверены, что хоте удалить заметку?", "Подтверждение действия",
            //    MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            //{
            //    File.Delete(notePath);
            //    notePath = null;
            //    UpdateTreeView();
            //    tbEditor.Clear();
            //    _settings.OpenNote = null;
            //}
        }
        private void CloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveExecute(sender, e);
            Application.Current.Shutdown();
        }
        private void UpdateTreeView()
        {
            tvNotes.Items.Clear();
            var userOfGroups = TisinaliteDBEntities.GetContext().UsersOfGroups.ToList();
            var groups = TisinaliteDBEntities.GetContext().UsersOfGroups.ToList();

            userOfGroups = userOfGroups.Where(g => g.UserID == user.ID).ToList();


            foreach (var link in userOfGroups)
            {
                using (var db = new TisinaliteDBEntities())
                {
                    var group = db.Groups.AsNoTracking().FirstOrDefault(g => g.ID == link.GroupID);
                    var groupNode = new TreeViewItem { Header = group.Title, Tag = group };

                    foreach (var note in TisinaliteDBEntities.GetContext().Notes
                        .Where(n => n.GroupID == group.ID).ToList())
                    {
                        var noteNode = new TreeViewItem { Header = note.Title, Foreground = Brushes.White, Tag = note };
                        groupNode.Items.Add(noteNode);
                    }
                    tvNotes.Items.Add(groupNode);
                }
            }
        }


        public void NewFile()
        {

        }
        public void SaveFile()
        {

        }
        private void OpenNote()
        {

        }

        private void tvNote_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)tvNotes.SelectedItem;
            if (item.Foreground.Equals(Brushes.White))
            {
                Debug.WriteLine("Note");
                Notes note = item.Tag as Notes;
                Debug.WriteLine(note.Title);
            }
            else
            {
                Debug.WriteLine("Group");
                Groups group = item.Tag as Groups;
                Debug.WriteLine(group.Title);
            }

        }

        private void CanDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = File.Exists(notePath);
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void PasteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            tbEditor.Paste();
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
