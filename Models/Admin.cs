using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models
{
    public class Admin : IEquatable<Admin>
    {
        [Key]
        public int AdminID { get; set; }



        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public bool Equals(Admin? other)
        {
            var admin = other ?? new Admin();
            return AdminID.Equals(admin.AdminID);
        }
    }
}
