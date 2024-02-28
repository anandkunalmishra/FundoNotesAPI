using System;
namespace Common_Layer.RequestModel
{
	public class AddNotesModel
	{
        public string NoteText { get; set; }

        public string colour { get; set; }

        public string background { get; set; }

        public bool IsPin { get; set; }

        public bool IsTrash { get; set; }

        public bool IsArchive { get; set; }
    }
}

