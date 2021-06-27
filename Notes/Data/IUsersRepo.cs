using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Data
{
    public interface IUsersRepo
    {
        bool SaveChanges();

        void CreateUser(User user);

        User GetUserByUsername(string username);

        User Login(User user);
    }
}
