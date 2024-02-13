using System.Runtime.Serialization;

namespace CozyHaven.Repository
{
    [Serializable]
    internal class HotelNotFoundException : Exception
    {
        public HotelNotFoundException()
        {
        }

        public HotelNotFoundException(string? message) : base(message)
        {
        }

        public HotelNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HotelNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}