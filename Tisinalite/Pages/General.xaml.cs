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
                    var group = db.Groups.AsNoTracking().FirstOrDefault(g=> g.ID == link.GroupID);
                    var groupNode = new TreeViewItem { Header = group.Title };
                    foreach (var note in TisinaliteDBEntities.GetContext().Notes.Where(n => n.GroupID == group.ID).ToList())
                    {
                        var noteNode = new TreeViewItem { Header = note.Title, Foreground = Brushes.White};
                        groupNode.Items.Add(noteNode);
                    }
                    tvNotes.Items.Add(groupNode);
                }

            }
            /*
            Debug.WriteLine("Searching groups");
            var groups = TisinaliteDBEntities.GetContext().Groups.ToList();
            groups = groups.Where(g => g.UsersOfGroups == user.UsersOfGroups).ToList();
            Debug.WriteLine(groups.Count());
            
            var mainNode = new TreeViewItem();
            foreach (var group in groups)
            {
                Debug.WriteLine(group.Title);
                var groupNode = new TreeViewItem { Header = group.Title };
                mainNode.Items.Add(groupNode);
            }
            tvNotes.Items.Add(mainNode);*/
            /* ------------------ */
            //tvNotes.Items.Clear();
            //var rootDirectoryInfo = new DirectoryInfo(Global.NotesDir);
            //tvNotes.Items.Add(CreateDirNode(rootDirectoryInfo));
        }
        //private static TreeViewItem CreateDirNode(DirectoryInfo directoryInfo)
        //{
        //    var directoryNode = new TreeViewItem { Header = directoryInfo.Name };
        //    foreach (var file in directoryInfo.GetFiles())
        //        directoryNode.Items.Add(new TreeViewItem { Header = file.Name });

        //    return directoryNode;
        //}

        public void NewFile()
        {
            //Pages.Input input = new Pages.Input
            //{
            //    Title = "Введите имя фала"
            //};
            //input.ShowDialog();
            //try
            //{
            //    string path = Path.Combine(Global.NotesDir, input.Entry);

            //    if (!input.Result) return;
            //    File.Create(path);
            //    notePath = path;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
        }
        public void SaveFile()
        {
            //if (notePath == null)
            //{
            //    NewFile();
            //}
            //if (notePath != null)
            //{
            //    using (StreamWriter writer = new StreamWriter(notePath))
            //    {
            //        writer.WriteLine(tbEditor.Text);
            //        writer.Close();
            //    }
            //}
        }
        private void OpenFile(string note)
        {
            //string _notePath = Global.GetNoteFile(note);
            //if (notePath != null && _notePath != notePath)
            //    SaveFile();
            //try
            //{
            //    using (StreamReader reader = new StreamReader(_notePath))
            //    {
            //        string noteText = reader.ReadToEnd();
            //        tbEditor.Text = noteText;
            //        reader.Close();
            //        notePath = _notePath;
            //        _settings.OpenNote = note;
            //    }
            //}
            //catch
            //{
            //}
        }

        private void tvNote_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //TreeViewItem item = (TreeViewItem)tvNotes.SelectedItem;
            //Debug.WriteLine(item);
            //if (item != null)
            //{
            //    string note = item.Header.ToString();
            //    OpenFile(note);
            //}
        }

        private void CanDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = File.Exists(notePath);
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //SaveFile();
            //SaveSettings();
        }

        private void PasteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            tbEditor.Paste();
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            //var fileContent = string.Empty;
            //var filePath = string.Empty;

            //OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.Filter = "Файлы изображений(*.PNG;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF;*.PNG|Все файлы (*.*)|*.*";
            //openFileDialog.FilterIndex = 1;
            //openFileDialog.RestoreDirectory = true;

            //if (openFileDialog.ShowDialog() == true)
            //{
            //    filePath = openFileDialog.FileName;
            //    tbEditor.SelectedText = $" ![Название картинки]({filePath}) ";
            //}
        }
    }
}
