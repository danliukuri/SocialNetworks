using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public partial class User
    {
        public void JoinSocialNetwork(SocialNetwork socialNetwork)
        {
            CheckForNull(socialNetwork);
            if (socialNetwork.IsMember(this))
                throw new ArgumentException($"User {nickname} belongs to network {socialNetwork.Name}");
            else
            {
                if (SocialNetwork == null)
                    SocialNetwork = new List<SocialNetwork>();
                SocialNetwork.Add(socialNetwork);
                socialNetwork.AddNewUser(this);
                AddedNewSocialWork?.Invoke(new UserEventsArgs($"{nickname}, you have a new social network {socialNetwork.Name}", this));
            }
        }
        public void JoinSocialNetwork(SocialNetwork socialNetwork, User user)
        {
            CheckForNull(socialNetwork);
            CheckForNull(user);
            if (socialNetwork.IsAdmin(this))
                user.JoinSocialNetwork(socialNetwork);
            else
                throw new ArgumentException($"{nickname}, you are not authorized for this operation" +
                    $" you need to be a {socialNetwork.Name} administrator");
          
        }
        public static User operator +(User user, SocialNetwork socialNetwork)
        {
            user.JoinSocialNetwork(socialNetwork);
            return user;
        }
        public void RemoveSocialNetwork(SocialNetwork socialNetwork)
        {
            CheckForNull(socialNetwork);
            if (!socialNetwork.IsMember(this))
                throw new ArgumentException($"{nickname}, you aren't a member of the {socialNetwork.Name} network");
            for (int i = 0; i < this?.SocialNetwork?.Count; i++)
                if (socialNetwork == SocialNetwork[i]) // The cycle where we are looking for a social network
                {
                    SocialNetwork.RemoveAt(i);
                    socialNetwork.RemoveUser(this);
                    RemovedSocialWork?.Invoke(new UserEventsArgs($"{nickname}, you have social network {socialNetwork.Name} removed", this));
                }
        }
        public void RemoveSocialNetwork(SocialNetwork socialNetwork, User user)
        {
            CheckForNull(socialNetwork);
            CheckForNull(user);
            if (!socialNetwork.IsAdmin(this))
                throw new ArgumentException($"{nickname}, you are not authorized for this operation" +
                    $" you need to be a {socialNetwork.Name} administrator");
            user.RemoveSocialNetwork(socialNetwork);
        }
        public static User operator -(User user, SocialNetwork socialNetwork)
        {
            user.RemoveSocialNetwork(socialNetwork);
            return user;
        }

        public void RemoveGroupOfFriends(GroupOfFriends groupOfFriends)
        {
            CheckForNull(groupOfFriends);
            if (!groupOfFriends.IsMember(this))
                throw new ArgumentException($"{nickname}, you aren't a member of the {groupOfFriends.Name} group of friends");
            for (int i = 0; i < this?.groupOfFriends?.Count; i++)
                if (groupOfFriends == this.groupOfFriends[i]) // Cycle where we are looking for a groupOfFriends
                {
                    groupOfFriends.RemoveUser(this);
                    this.groupOfFriends.RemoveAt(i);
                    RemovedGroupOfFriends?.Invoke(new UserEventsArgs($"{nickname}, you have group of friends {groupOfFriends.Name} removed", this));

                }
        }
        public static User operator -(User user, GroupOfFriends groupOfFriends)
        {
            user.RemoveGroupOfFriends(groupOfFriends);
            return user;
        }
        public void RemoveFriend(User user)
        {
            CheckForNull(user);
            if (!IsFriend(user))
                throw new ArgumentException($"User {user.GetUserName()} is not a friend");
            for (int i = 0; i < FriendList?.Count; i++)
                if (user == FriendList[i]) // The cycle where we are looking for a friend
                    FriendList.RemoveAt(i);
            RemovedFriend?.Invoke(new UserEventsArgs($"{nickname}, you removed {user.GetUserName()} from your friends list", user));
        }

        public void JoinGroupOfFriends(User friend, GroupOfFriends groupOfFriends)
        {
            CheckForNull(groupOfFriends);
            CheckForNull(friend, "Friend must be user not null");
            if (!IsFriend(friend))
                throw new ArgumentException($"{nickname}, you doesn't have such a friend {friend.GetUserName()}");
            if (!groupOfFriends.IsMember(this))
                throw new ArgumentException($"{nickname}, you must be member of group {groupOfFriends.Name}");
            friend.JoinGroupOfFriends(groupOfFriends);
            AddedNewGroupOfFriends?.Invoke(new UserEventsArgs($"{friend.GetUserName()}, you have a new group of friends {groupOfFriends.Name}"
                + $"\n{nickname} added you ", friend));   
        }
        internal void JoinGroupOfFriends(GroupOfFriends groupOfFriends)
        {
            CheckForNull(groupOfFriends);
            if (!groupOfFriends.IsMember(this))
                groupOfFriends.AddNewUser(this);
            if (this.groupOfFriends == null)
                this.groupOfFriends = new List<GroupOfFriends>();
            this.groupOfFriends.Add(groupOfFriends);
        }

        protected static void CheckForNull(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Name must be not null");
        }
        protected static void CheckForNull(User user, string message = "User must not be null")
        {
            if (user == null)
                throw new ArgumentNullException(message);
        }
        protected static void CheckForNull(SocialNetwork socialNetwork)
        {
            if (socialNetwork == null)
                throw new ArgumentException("Social Network must not be null");
        }
        protected static void CheckForNull(GroupOfFriends groupOfFriends)
        {
            if (groupOfFriends == null)
                throw new ArgumentNullException("Group of friends must not be null");
        }
    }
}