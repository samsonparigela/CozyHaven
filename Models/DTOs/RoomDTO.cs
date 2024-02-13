namespace CozyHaven.Models.DTOs
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        // Add any other necessary properties
    }
}
