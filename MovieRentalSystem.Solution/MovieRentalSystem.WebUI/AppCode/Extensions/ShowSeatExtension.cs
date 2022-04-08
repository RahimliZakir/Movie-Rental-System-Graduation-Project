using Microsoft.AspNetCore.Html;
using MovieRentalSystem.WebUI.Models.Entities;
using System.Text;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static HtmlString FillSeats(this Show show)
        {
            StringBuilder sb = new();

            sb.Append("<div class='theatre'>");

            int seatCount = show.Room.Seats.Count;

            sb.Append("<div class='cinema-seats left col-6 col-xl-6 col-lg-6 col-md-6 col-sm-6'>");

            sb.Append("</div>");

            sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }
    }
}
