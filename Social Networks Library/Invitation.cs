using System;

namespace Social_Networks
{
    public class Invitation : UserEventsArgs
    {
        public Invitation(User sender, bool toSenderFriendList, string message = null) : base(message, sender)
        {
            if (sender == null)
                throw new ArgumentNullException("Sender must not be null");
            ToSenderFriendList = toSenderFriendList;
            if (message == null)
                Message = DefaultMessage(sender);
            else
            {
                if (message.Length > 100)
                    throw new ArgumentException("Message must be no more than 100 characters");
                Message = message;
            }    
        }
        private string DefaultMessage(User sender)
        {
            string message = $"Hi, my name {sender.GetUserName()} and ";
            if (ToSenderFriendList == true)
                message += $"I want to add you to my friends";
            if (ToSenderFriendList == false)
                message += "I want you to add me to your friends";
            return message;
        }
        public enum Respond
        {
            Accept,
            Decline
        }
        public bool ToSenderFriendList { get; }
        internal User Sender { get { return user; } }
    }
}