 public class XViewModel
    {
        public int Id { get; set; }

        [NotMapped]
        public ImageItemFormModel[] Files { get; set; }
    }