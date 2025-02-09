namespace SDP_T01_Group06.Observer
{
    public class Notification
    {
        public string Message { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsRead { get; private set; }

        public Notification(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
            IsRead = false;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }

        public override string ToString()
        {
            return $"[{(IsRead ? "Read" : "Unread")}] {Timestamp:g}: {Message}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Notification other)
            {
                return Message == other.Message;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Message.GetHashCode();
        }
    }
}