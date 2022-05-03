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
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tisinalite.Pages
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Page
    {
        private Microsoft.Win32.OpenFileDialog _openDialog = new Microsoft.Win32.OpenFileDialog();
        private Microsoft.Win32.SaveFileDialog _saveDialog = new Microsoft.Win32.SaveFileDialog();
        private Settings _settings = Settings.GetSettings();
        public Editor()
        {

            InitializeComponent();
            if (_settings.OpenNote != null)
            {
                OpenFile(Global.GetNoteFile(_settings.OpenNote));
            }
            
            UpdateTreeView();
            
        }

        private void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //if (isDirty)
            //{
            //    SaveExecute(sender, e);
            //}
            //tbEditor.Text = "";
        }
        private void OpenExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //if (isDirty)
            //{
            //    SaveExecute(sender, e);
            //}
            //if (_openDialog.ShowDialog() == true)
            //    OpenFile();
        }
        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //if (_saveDialog.FileName == "")
            //{
            //    SaveAsExecute(sender, e);
            //    return;
            //}
            //SaveFile();
        }
        private void SaveAsExecute(object sender, ExecutedRoutedEventArgs e)
        {
            //_saveDialog.FileName = note.Title;
            //if (_saveDialog.ShowDialog() == true)
            //{
            //    SaveFile();
            //}
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
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Items.Add(CreateDirNode(directory));

            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Items.Add(new TreeViewItem { Header = file.Name });

            return directoryNode;
        }


        private void SaveFile(string filePath = null)
        {
            
            //StreamWriter writer = new StreamWriter(_saveDialog.FileName);
            //Debug.WriteLine($"file name: {_saveDialog.FileName}");
            //writer.WriteLine(tbEditor.Text);
            //Debug.WriteLine($"Content: {tbEditor.Text}");
            //writer.Close();
            //isDirty = false;
        }
        private void OpenFile(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            tbEditor.Text = reader.ReadToEnd();
            reader.Close();
            //StreamReader reader = new StreamReader(_openDialog.FileName);
            //tbEditor.Text = reader.ReadToEnd();
            //reader.Close();
            //_saveDialog.FileName = _openDialog.FileName;
            //isDirty = false;
        }

        private void ContentChanged(object sender, TextChangedEventArgs e)
        {
            //isDirty = true;
        }

        private void tvNote_Changed(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Debug.WriteLine(tvNotes.SelectedItem);
            Debug.WriteLine(tvNotes.SelectedValue);
        }
    }
}
