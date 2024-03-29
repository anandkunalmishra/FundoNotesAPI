﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Repository_Layer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }

        [Required]
        public string LabelName { get; set; }

        [ForeignKey("LabelFor")]
        public int NoteId { get; set; }

        [ForeignKey("LabelBy")]
        public int UserId { get; set; }
 
        [JsonIgnore]
        public virtual NoteEntity LabelFor { get; set; }

        [JsonIgnore]
        public virtual UserEntity LabelBy { get; set; }
    }
}
