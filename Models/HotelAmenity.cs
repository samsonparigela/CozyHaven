using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHaven.Models
{
    public class HotelAmenity
    {
        public HotelAmenity(int iD, int hotelID, int amenityID)
        {
            ID = iD;
            HotelID = hotelID;
            AmenityID = amenityID;
        }

        [Key]
        public int ID { set; get; }

        [ForeignKey("HotelID")]
        public int HotelID { get; set; }
        public List<Hotel> Hotels { get; set; }

        [ForeignKey("AmenityID")]
        public int AmenityID { get; set; }
        public List<Amenity> Amenity { get; set; }
    }
}
