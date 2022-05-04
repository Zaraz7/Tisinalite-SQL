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
using System.IO;
using System.Diagnostics;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Page
    {
        private Settings _settings = Settings.GetSettings();
        private string notePath;
        public Editor()
        {
            InitializeComponent();
            if (_settings.OpenNote != null)
            {
                notePath = Global.GetNoteFile(_settings.OpenNote);
                OpenFile(notePath, _settings.OpenNote);
            }
            UpdateTreeView();
            
        }
        public void SaveSettings()
        {
            _settings.Save();
        }

        private void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile();
            notePath = null;
            tbEditor.Text = "";
            SaveFile();
            UpdateTreeView();
        }
        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile();
            UpdateTreeView();
        }
        private void DeleteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Уверены, что хоте удалить заметку?", "Подтверждение действия", 
                MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                File.Delete(notePath);
                notePath = null;
                UpdateTreeView();
                tbEditor.Clear();
            }
        }
        private void CloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //if (isDirty)
            //{
            //    SaveExecute(sender, e);
            //}
            //System.Windows.Application.Current.Shutdown();
        }
        private void UpdateTreeView()
        {
            tvNotes.Items.Clear();
            var rootDirectoryInfo = new DirectoryInfo(Global.NotesDir);
            tvNotes.Items.Add(CreateDirNode(rootDirectoryInfo));
        }
        private static TreeViewItem CreateDirNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeViewItem { Header = directoryInfo.Name };
            //foreach (var directory in directoryInfo.GetDirectories())
            //    directoryNode.Items.Add(CreateDirNode(directory));

            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Items.Add(new TreeViewItem { Header = file.Name });

            return directoryNode;
        }


        public void SaveFile()
        {
            if (notePath == null)
            {
                Input input = new Input();
                input.Title = "Введите имя фала";
                input.ShowDialog();
                try
                {
                    string path = Path.Combine(Global.NotesDir, input.Entry);
                    
                    //if (path == "Tisinalite") return;
                    notePath = path;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            if (notePath != null) { 
                using (StreamWriter writer = new StreamWriter(notePath))
                {
                    writer.WriteLine(tbEditor.Text);
                    writer.Close();
                }
            }
        }
        private void OpenFile(string _notePath, string note)
        {
            if (_notePath != notePath)
                SaveFile();
            try { 
                using (StreamReader reader = new StreamReader(_notePath))
                {
                    //_settings.OpenNote
                    tbEditor.Text = reader.ReadToEnd();
                    reader.Close();
                    notePath = _notePath;
                    _settings.OpenNote = note;
                }
            }
            catch
            {
            }
        }

        private void ContentChanged(object sender, TextChangedEventArgs e)
        {
        }


        private void tvNote_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)tvNotes.SelectedItem;
            Debug.WriteLine(item);
            if (item != null)
            {
                string note = item.Header.ToString();
                string tempPath = Global.GetNoteFile(note);
                OpenFile(tempPath, note);
            }
        }

        private void CanDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = File.Exists(notePath);
        }
    }
}
