using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class Amenity : IEquatable<Amenity>
    {
        [Key]
        public int AmenityID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        // Navigation properties
        public List<HotelAmenity> HotelAmenities { get; set; }

        public bool Equals(Amenity? other)
        {
            var amenity = other ?? new Amenity();
            return AmenityID.Equals(amenity.AmenityID);
        }
    }
}
