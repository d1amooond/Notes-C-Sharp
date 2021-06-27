using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class SqlUsersRepo : IUsersRepo
    {
        private readonly NotesContext _context;
        public SqlUsersRepo(NotesContext context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
           return _context.Users.FirstOrDefault(user => user.Username == username);
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public User Login(User user)
        {
            return _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
