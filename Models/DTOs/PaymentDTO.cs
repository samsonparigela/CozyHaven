namespace CozyHaven.Models.DTOs
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public int ReservationID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
