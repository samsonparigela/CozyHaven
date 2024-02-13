namespace CozyHaven.Models.DTOs
{
    public class ReservationDTO
    {
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
