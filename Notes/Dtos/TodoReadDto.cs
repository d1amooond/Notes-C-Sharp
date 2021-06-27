using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Dtos
{
    public class TodoReadDto
    {
        public int Id { get; set; }

        public int NoteId { get; set; }
        public string Description { get; set; }

        public bool Checked { get; set; } = false;
    }
}
