namespace MovieRentalSystem.WebUI.AppCode.Modules.CheckoutMovieModule
{
    public class CheckoutMovieViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MovieFrame { get; set; }

        public int Period { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
