using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class Hotel : IEquatable<Hotel>
    {
        [Key]
        public int HotelID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Rating { get; set; }

        //[Required]
        //public List<string> ImageURLs { get; set; }


        [Required]
        public DateTime CheckInTime { get; set; }

        [Required]
        public DateTime CheckOutTime { get; set; }

        [Required]
        public int HotelOwnerID { get; set; }

        // Navigation properties
        [ForeignKey("HotelOwnerID")]
        public HotelOwner HotelOwner { get; set; }

        public List<Room> Rooms { get; set; }

        public List<Review> Reviews { get; set; }

        public List<HotelAmenity> HotelAmenities { get; set; }

        public bool Equals(Hotel? other)
        {
            var hotel = other ?? new Hotel();
            return HotelID.Equals(hotel.HotelID);
        }
    }
}
