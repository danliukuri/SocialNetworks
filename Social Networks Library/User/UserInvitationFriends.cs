using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public partial class User : IUserInfo
    {
        public User(string nickname)
        {
            CheckForNull(nickname);
            if (nickname.Length < 5 || nickname.Length > 20)
                throw new ArgumentException("Nickname must be at least 5 and at most 20 characters long");
            for (int i = 0; i < nickname.Length; i++)
                if (!Char.IsLetter(nickname[i]))
                    throw new ArgumentException("Nickname must contain only letters");
            for (int i = 0; i < userNames?.Count; i++)
                if (userNames[i] == nickname)
                    throw new ArgumentException("This nickname is already in use");
            this.nickname = nickname;
            if (userNames == null)
                userNames = new List<string>();
            userNames.Add(nickname);
        }

        public delegate void UserHandler(UserEventsArgs info);

        public event UserHandler AddedNewSocialWork;
        public event UserHandler AddedNewGroupOfFriends;
        public event UserHandler AddedNewFriend;
        public event UserHandler RemovedSocialWork;
        public event UserHandler RemovedGroupOfFriends;
        public event UserHandler RemovedFriend;

        public List<string> GetUserSocialNetworkNames()
        {
            List<string> socialNetworkName = new List<string>();
            for (int i = 0; i < SocialNetwork?.Count; i++)
                socialNetworkName.Add(SocialNetwork[i].Name);
            return socialNetworkName;
        }
        public List<string> GetUserGroupOfFriendsNames()
        {
            List<string> GroupOfFriendsName = new List<string>();
            for (int i = 0; i < groupOfFriends?.Count; i++)
                GroupOfFriendsName.Add(groupOfFriends[i].Name);
            return GroupOfFriendsName;
        }
        public List<string> GetUserFriendNicknames()
        {
            List<string> listOfFrendsName = new List<string>();
            for (int i = 0; i < FriendList?.Count; i++)
                listOfFrendsName.Add(FriendList[i].nickname);
            return listOfFrendsName;
        }
        public List<string> GetUserInvitationSenders()
        {
            List<string> senders = new List<string>();
            for (int i = 0; i < Invitations?.Count; i++)
                senders.Add(Invitations[i].GetUserName());
            return senders;
        }
        public string GetUserName() => nickname;

        public void InviteToFriends(User recipient, string message = null)
        {   // Want to add other user to friendList this user
            if (IsFriend(recipient))
                throw new ArgumentException($"{recipient.GetUserName()} are already {nickname}'s friend");
            SendAnInvitation(recipient, true, message);
        }
        public void RequestAnInvitation(User recipient, string message = null)
        {   // Want add this user to friendList other user
            if (recipient.IsFriend(this))
                throw new ArgumentException($"{nickname} are already {recipient.GetUserName()}'s friend");
            SendAnInvitation(recipient, false, message);
                
        }
        public void RespondToTheInvitation(Invitation invitation, Invitation.Respond respond)
        {
            bool haveAnInvitation = false;
            for (int i = 0; i < Invitations?.Count; i++)
                if (invitation == Invitations[i]) // The cycle in which we are looking for the required invitation
                {
                    haveAnInvitation = true;
                    if (respond == Invitation.Respond.Accept)
                    {
                        if (Invitations[i].ToSenderFriendList)
                        {
                            if (Invitations[i].Sender.Invitations == null)
                                Invitations[i].Sender.Invitations = new List<Invitation>();
                            Invitations[i].Sender.Invitations.Add(new Invitation(this, true)); // Add answer 
                            Invitations[i].Sender.UpdateFriendList();
                            AddedNewFriend?.Invoke(new UserEventsArgs($"{Invitations[i].Sender.GetUserName()}, you have a new friend", Invitations[i].Sender));
                        }
                        if (!Invitations[i].ToSenderFriendList && !IsFriend(Invitations[i].Sender))
                        {
                            if (FriendList == null)
                                FriendList = new List<User>();
                            FriendList.Add(Invitations[i].Sender);
                            AddedNewFriend?.Invoke(new UserEventsArgs($"{nickname}, you have a new friend {Invitations[i].GetUserName()}", this));
                        }
                    }
                    Invitations.RemoveAt(i);
                }
            if (!haveAnInvitation)
                throw new ArgumentException("No such invitation");
        }

        public bool IsFriend(User user)
        {
            for (int i = 0; i < FriendList?.Count; i++)
                if (user == FriendList[i])
                    return true;
            return false;
        }

        private string nickname;
        public List<Invitation> Invitations { get; private set; }
        internal List<SocialNetwork> SocialNetwork { get; private set; }
        internal List<User> FriendList { get; private set; }
        private List<GroupOfFriends> groupOfFriends;
        private static List<string> userNames;

        private void SendAnInvitation(User recipient, bool toSenderFriendList, string message)
        {
            CheckForNull(recipient);
            if (recipient == this)
                throw new ArgumentException($"{recipient.GetUserName()} cannot send an invitation to yourself");
            if (recipient.Invitations == null)
                recipient.Invitations = new List<Invitation>();
            recipient.Invitations.Add(new Invitation(this, toSenderFriendList, message));
        }
        private void UpdateFriendList()
        {
            for (int i = 0; i < Invitations?.Count; i++)
                if (Invitations[i].ToSenderFriendList == true)
                {
                    if (FriendList == null)
                        FriendList = new List<User>();
                    FriendList.Add(Invitations[i].Sender);
                    Invitations.RemoveAt(i);
                }
        }
    }
}