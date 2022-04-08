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



            sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }
    }
}
