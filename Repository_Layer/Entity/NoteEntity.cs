using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
	public class NoteEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NoteId { get; set; }

		public string NoteText { get; set; }

		public string colour { get; set; }

		public string background { get; set; }

		public bool IsPin { get; set; }

		public bool IsTrash { get; set; }

		public bool IsArchive { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		[ForeignKey("NotesUser")]
        public int userId { get; set; }

		[JsonIgnore]
		public virtual UserEntity NotesUser { get; set; }
    }
}

