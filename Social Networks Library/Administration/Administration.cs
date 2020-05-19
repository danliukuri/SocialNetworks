using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public abstract class Administration
    {
        public string Name { get; protected set; }

        public List<string> GetMembersInfo()
        {
            List<string> membersNames = new List<string>();
            for (int i = 0; i < membersList.Count; i++)
                membersNames.Add(membersList[i].GetUserName());
            return membersNames;
        }
        public bool IsMember(User user)
        {
            for (int j = 0; j < membersList?.Count; j++)
                if (user == membersList[j])
                    return true;
            return false;
        }

        internal abstract void AddNewUser(User user);
        internal abstract void RemoveUser(User user);
        protected internal List<User> membersList;

        protected static void CheckTheNameIsCorrect(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Name must be not null");
            if (name.Length < 5 || name.Length > 20)
                throw new ArgumentException("Name must be at least 5 and at most 20 characters long");
            for (int i = 0; i < name.Length; i++) // The cycle in which we check each character is a letter, number or separator
                if (!Char.IsLetterOrDigit(name[i]) && !Char.IsSeparator(name[i]))
                    throw new ArgumentException("Name must contain only number, letters or separators");
            for (int i = 0; i < names?.Count; i++)
                if (names[i] == name)
                    throw new ArgumentException("This name is already in use");
        }
        protected static void CheckForNull(User user)
        {
            if (user == null)
                throw new ArgumentNullException("User must not be null");
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
        protected static List<string> names;
    }
}