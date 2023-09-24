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

namespace Pr2Wpf
{

    public partial class MainWindow : Window
    {
        private NoteManager noteManager;
        private DateTime selectedDate;

        public MainWindow()
        {
            noteManager = new NoteManager();
            DataContext = this;
            MinDate = DateTime.Today.AddYears(-1);
            MaxDate = DateTime.Today.AddYears(1);
            SelectedDate = DateTime.Today;
            RefreshNoteListView();
        }

        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                RefreshNoteListView();
            }
        }

        private void RefreshNoteListView()
        {
            if (noteListView != null)
            {
                noteListView.ItemsSource = noteManager.GetNotes(SelectedDate);
            }
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            var title = titleTextBox.Text;
            var description = descriptionTextBox.Text;
            if (!string.IsNullOrEmpty(title))
            {
                noteManager.AddOrUpdateNote(new Note { Title = title, Description = description, Date = SelectedDate });
                RefreshNoteListView();
                titleTextBox.Clear();
                descriptionTextBox.Clear();
                noteManager.SaveNotes(); 
            }
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            var selectedNote = noteListView.SelectedItem as Note;
            if (selectedNote != null)
            {
                selectedNote.Title = titleTextBox.Text;
                selectedNote.Description = descriptionTextBox.Text;
                noteManager.AddOrUpdateNote(selectedNote);
                RefreshNoteListView();
                noteManager.SaveNotes();
            }
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            var selectedNote = noteListView.SelectedItem as Note;
            if (selectedNote != null)
            {
                noteManager.DeleteNote(selectedNote);
                RefreshNoteListView();
                noteManager.SaveNotes();
            }
        }
    }
}
