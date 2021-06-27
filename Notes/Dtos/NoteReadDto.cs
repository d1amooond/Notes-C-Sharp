using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Dtos
{
    public class NoteReadDto
    {
        public int Id { get; set; }

        public Guid OwnerId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Todos { get; set; }
        [NotMapped]
        public object[] TodoList { get; set; }
    }
}