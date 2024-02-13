namespace CozyHaven.Models.DTOs
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        public int HotelID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
