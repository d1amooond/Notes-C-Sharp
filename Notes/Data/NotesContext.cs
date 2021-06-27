using Microsoft.EntityFrameworkCore;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Todo> Todos { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
