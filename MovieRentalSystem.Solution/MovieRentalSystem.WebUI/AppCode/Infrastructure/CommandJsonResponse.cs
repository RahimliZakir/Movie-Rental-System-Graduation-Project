namespace MovieRentalSystem.WebUI.AppCode.Infrastructure
{
    public class CommandJsonResponse
    {
        public string Message { get; set; }

        public string Temp { get; set; }

        public string ReturnUrl { get; set; }

        public int LikeCount { get; set; }

        public int UnlikeCount { get; set; }

        public bool Error { get; set; }
    }
}
