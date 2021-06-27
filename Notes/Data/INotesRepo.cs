using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notes.Models;

namespace Notes.Data
{
    public interface INotesRepo
    {
        bool SaveChanges();
        IEnumerable<Note> GetNotes(Guid userId);
        Note GetNoteById(int id);
        void CreateNote(Note note);

        void UpdateNote(Note note);

        void DeleteNote(Note note);
    }
}
