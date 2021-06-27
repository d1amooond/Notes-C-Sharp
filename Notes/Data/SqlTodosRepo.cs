using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class SqlTodosRepo : ITodosRepo
    {
        private readonly NotesContext _context;
        public SqlTodosRepo(NotesContext context)
        {
            _context = context;
        }

        public Todo GetTodoItemById(int id)
        {
            return _context.Todos.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void CreateTodo(int noteId, Todo todo)
        {
            Note note = _context.Notes.FirstOrDefault(p => p.Id == noteId);
            if (note == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            _context.Todos.Add(todo);

        }

        public void UpdateTodo(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            _context.Todos.Update(todo);
        }


    }
}
