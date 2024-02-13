using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class Reservation : IEquatable<Reservation>
    {
        [Key]
        public int ReservationID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int RoomID { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int NumAdults { get; set; }

        [Required]
        public int NumChildren { get; set; }

        [Required]
        public float TotalPrice { get; set; }

        [Required]
        public bool IsCancelled { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("RoomID")]
        public Room Room { get; set; }

        public Payment Payment { get; set; }

        public bool Equals(Reservation? other)
        {
            var reservation = other ?? new Reservation();
            return ReservationID.Equals(reservation.ReservationID);
        }
    }
}
