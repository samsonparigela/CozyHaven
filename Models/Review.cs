using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class Review : IEquatable<Review>
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int HotelID { get; set; }

        [Required]
        public int Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("HotelID")]
        public Hotel Hotel { get; set; }
        public Review()
        {
            Rating = 0;
        }

        public bool Equals(Review? other)
        {
            var review = other ?? new Review();
            return ReviewID.Equals(review.ReviewID);
        }
    }
}
