using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public class SocialNetwork : Administration
    {
        public SocialNetwork(string name, User admin)
        {
            CheckTheNameIsCorrect(name);
            CheckForNull(admin);
            Name = name;
            if (names == null)
                names = new List<string>();
            names.Add(name);
            admin.JoinSocialNetwork(this);
        }

        public bool IsAdmin(User user)
        {
            if (user == membersList?[0])
                return true;
            return false;
        }

        internal override void AddNewUser(User user)
        {
            CheckForNull(user);
            if (membersList == null)
                membersList = new List<User>();
            membersList.Add(user);
        }
        internal override void RemoveUser(User user)
        {
            CheckForNull(user);
            if (!IsMember(user))
                throw new ArgumentException($"User,{user.GetUserName()} does not belong to this {Name} network");
            for (int i = 0; i < membersList?.Count; i++)
                if (user == membersList[i]) //The cycle in which we are looking for the user
                {
                    for (int j = 0; j < groupList?.Count; j++)
                        if (groupList[j].IsMember(user)) // The cycle in which we check whether the user belongs to some group
                            user.RemoveGroupOfFriends(groupList[j]);
                    membersList.RemoveAt(i);
                }
        }
        internal void AddGroupOfFriends(GroupOfFriends groupOfFriends)
        {
            CheckForNull(groupOfFriends);
            if (groupList == null)
                groupList = new List<GroupOfFriends>();
            groupList.Add(groupOfFriends);
        }

        private List<GroupOfFriends> groupList;
    }
}