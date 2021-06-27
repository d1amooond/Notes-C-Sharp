using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NoteId { get; set; }
        [Required]
        public string Description { get; set; }

        public bool Checked { get; set; } = false;
    }
}
