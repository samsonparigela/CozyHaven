using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class HotelOwner : IEquatable<HotelOwner>
    {
        [Key]
        public int HotelOwnerID { get; set; }

        [Required]
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        // Navigation property
        public User User { get; set; }

        public List<Hotel> Hotels { get; set; }

        public bool Equals(HotelOwner? other)
        {
            var owner = other ?? new HotelOwner();
            return HotelOwnerID.Equals(owner.HotelOwnerID);
        }
    }
}
