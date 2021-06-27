using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public interface ITodosRepo 
    {
        bool SaveChanges();

        Todo GetTodoItemById(int id);

        void CreateTodo(int noteId, Todo todo);

        void UpdateTodo(Todo todo);
    }
}
