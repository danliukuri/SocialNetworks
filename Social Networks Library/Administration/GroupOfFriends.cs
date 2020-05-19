using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public class GroupOfFriends : Administration
    {
        public GroupOfFriends(string name, User creator, SocialNetwork socialNetwork)
        {
            CheckTheNameIsCorrect(name);
            CheckForNull(creator);
            CheckForNull(socialNetwork);
            Name = name;
            if (names == null)
                names = new List<string>();
            names.Add(name);
            membersList = new List<User> { creator };
            creator.JoinGroupOfFriends(this);
            if (!socialNetwork.IsMember(creator))
                creator.JoinSocialNetwork(socialNetwork);
            this.socialNetwork = socialNetwork;
            socialNetwork.AddGroupOfFriends(this);
        }

        internal override void AddNewUser(User user)
        {
            CheckForNull(user);
            if (IsMember(user))
                throw new ArgumentException($"Friend {user.GetUserName()} is already a member of group {Name}");
            membersList.Add(user);
            for (int i = 0; i < membersList.Count - 1; i++)
            {
                int countOfInvitations = 0;
                if (!user.IsFriend(membersList[i]))
                {
                    user.InviteToFriends(membersList[i]);
                    countOfInvitations++;
                }   
                if (!membersList[i].IsFriend(user))
                {
                    user.RequestAnInvitation(membersList[i]);
                    countOfInvitations++;
                }
                // The loop in which we are looking for an invitation from the user
                for (int j = membersList[i].Invitations.Count - 1; j >= 0; j--)
                    if (membersList[i].Invitations[j].Sender == user && countOfInvitations > 0) 
                    {
                        membersList[i].RespondToTheInvitation(membersList[i].Invitations[j], Invitation.Respond.Accept);
                        countOfInvitations--;
                    }
            }
            if (!socialNetwork.IsMember(user))
                user.JoinSocialNetwork(socialNetwork);
        }
        internal override void RemoveUser(User user)
        {
            CheckForNull(user);
            if (!IsMember(user))
                throw new ArgumentException($"User,{user.GetUserName()} does not belong to this {Name} group of friends");
            for (int i = 0; i < membersList?.Count; i++)
                if (user == membersList[i]) // Cycle in which we are looking for a user
                {
                    membersList.RemoveAt(i);
                    for (int j = 0; j < membersList.Count; j++) // Cycle in which among the list of members 
                        if (user.IsFriend(membersList[j]))      // look for friends of the user
                            user.RemoveFriend(membersList[j]);
                }
        }

        internal SocialNetwork socialNetwork;
    }
}