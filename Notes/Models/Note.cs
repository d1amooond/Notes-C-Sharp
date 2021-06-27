using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        
        public Guid OwnerId { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Todos { get; set; } = 0;
        [NotMapped]
        public object[] TodoList { get; set; } = { };
    }
}