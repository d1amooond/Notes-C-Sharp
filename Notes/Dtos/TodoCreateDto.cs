using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Dtos
{
    public class TodoCreateDto
    {
        public int NoteId { get; internal set; }
        public string Description { get; set; }
    }
}
