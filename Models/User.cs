using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class User : IEquatable<User>
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public byte[] Password { get; set; }

        public byte[] Key { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public UserType UserType { get; set; }

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public DateTime LastLoginDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation properties
        public List<Reservation> Reservations { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Admin> Admin { get; set; }
        public List<HotelOwner> HotelOwners { get; set; }



        public bool Equals(User? other)
        {
            var user = other ?? new User();
            return UserID.Equals(user.UserID);
        }
    }

    public enum UserType
    {
        Customer,
        Admin,
        HotelOwner
    }
}
