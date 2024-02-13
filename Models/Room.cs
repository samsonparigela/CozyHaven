using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public enum RoomType
    {
        Single,
        Double,
        Suite
    }
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        public int HotelID { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public RoomType Type { get; set; }

        [Required]
        public float PricePerNight { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        // Navigation properties
        [ForeignKey("HotelID")]
        public Hotel Hotel { get; set; }

        public List<Reservation> Reservations { get; set; }

        public Room()
        {
            PricePerNight = 0;
        }

    }
}
