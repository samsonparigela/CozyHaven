using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class Payment : IEquatable<Payment>
    {
        [Key]
        public int PaymentID { get; set; }



        [Required]
        public float Amount { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int ReservationID { get; set; }
        // Navigation properties
        [ForeignKey("ReservationID")]
        public Reservation Reservation { get; set; }

        public bool Equals(Payment? other)
        {
            var payments = other ?? new Payment();
            return PaymentID.Equals(payments.PaymentID);
        }
    }
}
