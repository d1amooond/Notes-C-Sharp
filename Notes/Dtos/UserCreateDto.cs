using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Dtos
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class UserCreateDtoHashed
    {
        public string Username { get; set; }
        public Guid Password { get; set; }
    }

}
