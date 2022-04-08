namespace MovieRentalSystem.WebUI.AppCode.Modules.CheckoutShowModule
{
    public class CheckoutShowViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public int Period { get; set; }

        public int PeopleCount { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
