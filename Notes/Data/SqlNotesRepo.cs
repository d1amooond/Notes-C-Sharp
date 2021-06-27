using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class SqlNotesRepo : INotesRepo
    {
        private readonly NotesContext _context;
        public SqlNotesRepo(NotesContext context)
        {
            _context = context;
        }

        public Note GetNoteById(int id)
        {
            var note = _context.Notes.FirstOrDefault(p => p.Id == id);
            var todos = _context.Todos.Where(p => p.NoteId == note.Id).ToArray();
            note.TodoList = todos;
            return note;
        }

        public IEnumerable<Note> GetNotes(Guid userId)
        {
            var notes = _context.Notes.Where(p => p.OwnerId == userId).ToList();
            foreach (Note note in notes)
            {
                note.TodoList = _context.Todos.Where(p => p.NoteId == note.Id).ToArray();
            }
            return notes;
        }

        public void CreateNote(Note note)
        {
            if (note == null) 
            {
                throw new ArgumentNullException(nameof(note));
            }

            _context.Notes.Add(note);
        }

        public void UpdateNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            _context.Notes.Update(note);
        }

        public void DeleteNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            _context.Notes.Remove(note);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
