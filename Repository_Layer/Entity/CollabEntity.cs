using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
	public class CollabEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CollabId { get; set; }

		public string CollabEmail { get; set; }

		[ForeignKey("CollabBy")]
		public int userId { get; set; }

		[ForeignKey("CollabFor")]
		public int NoteId { get; set; }

		[JsonIgnore]
		public virtual UserEntity CollabBy { get; set; }

		[JsonIgnore]
		public virtual NoteEntity CollabFor { get; set; }

	}
}

