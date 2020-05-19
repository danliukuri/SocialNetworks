using System;
using System.Collections.Generic;

namespace Social_Networks
{
    struct Create
    {
        public static void User(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("Enter the user's nickname :");
            string nickname = Console.ReadLine();
            User user = new User(nickname);
            Message.SuccessOperationHandler("User created successfully");
            user.AddedNewSocialWork += Message.EventHandler;
            user.AddedNewGroupOfFriends += Message.EventHandler;
            user.AddedNewFriend += Message.EventHandler;
            user.RemovedSocialWork += Message.EventHandler;
            user.RemovedGroupOfFriends += Message.EventHandler;
            user.RemovedFriend += Message.EventHandler;
            users.Add(user);
        }

        public static void GroupOfFriends(List<SocialNetwork> socialNetworks, List<GroupOfFriends> groupOfFriends, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Need to have a user");
            if (socialNetworks.Count == 0)
                throw new ArgumentException("This operation requires a have a social network");

            Console.Clear();
            Console.WriteLine("Select the user who will be the first member of this group of friends");
            User firstmember = Choose.User(users);

            Console.WriteLine("Select the social network in which the group will be created");
            SocialNetwork socialNetwork = Choose.SocialNetwork(socialNetworks, users);

            Console.WriteLine("Enter the group of friends name :");
            string name = Console.ReadLine();
            GroupOfFriends groupOfFriends1 = new GroupOfFriends(name, firstmember, socialNetwork);

            Message.SuccessOperationHandler("Group of friends created successfully");
            groupOfFriends.Add(groupOfFriends1);
        }

        public static void SocialNetwork(List<SocialNetwork> socialNetworks, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Need to have a user");

            Console.Clear();
            Console.WriteLine("Select the user who will be the administrator of this network");
            User admin = Choose.User(users);

            Console.WriteLine("Enter the social network's name :");
            string name = Console.ReadLine();
            SocialNetwork socialNetwork = new SocialNetwork(name, admin);
            Message.SuccessOperationHandler("Social network created successfully");

            socialNetworks.Add(socialNetwork);
        }
    }
}