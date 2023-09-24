using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Pr2Wpf
{
    public class NoteManager
    {
        private List<Note> notes;
        private string dataFilePath = "C:\\Users\\percy\\source\\repos\\Pr2Wpf\\Pr2Wpf\\notes.json";

        public NoteManager()
        {
            notes = LoadNotes();
        }

        public List<Note> GetNotes(DateTime date)
        {
            return notes.FindAll(note => note.Date.Date == date.Date);
        }

        public void AddOrUpdateNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            var existingNote = notes.Find(n => n.Date == note.Date && n.Title == note.Title);
            if (existingNote != null)
            {
                existingNote.Description = note.Description;
            }
            else
            {
                notes.Add(note);
            }

            SaveNotes();
        }

        public void DeleteNote(Note note)
        {
            notes.Remove(note);
            SaveNotes();
        }

        public List<Note> LoadNotes()
        {
            try
            {
                if (File.Exists(dataFilePath))
                {
                    var json = File.ReadAllText(dataFilePath);
                    return JsonSerializer.Deserialize<List<Note>>(json);
                }
                return new List<Note>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка чтения: " + ex.Message);
                return new List<Note>();
            }
        }

        public void SaveNotes()
        {
            try
            {
                var json = JsonSerializer.Serialize(notes);
                File.WriteAllText(dataFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи: " + ex.Message);
            }
        }

    }

}
