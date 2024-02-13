using System.Runtime.Serialization;

namespace CozyHaven.Exceptions
{
    [Serializable]
    internal class RoomNotFoundException : Exception
    {
        public RoomNotFoundException()
        {
        }

        public RoomNotFoundException(string? message) : base(message)
        {
        }

        public RoomNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RoomNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}