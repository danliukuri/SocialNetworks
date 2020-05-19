using System;
using System.Collections.Generic;

namespace Social_Networks
{
    struct Choose
    {
        public static SocialNetwork SocialNetwork(List<SocialNetwork> socialNetworks, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Need to have a user");
            if (socialNetworks.Count == 0)
                throw new ArgumentException("Need to have a social networks");
            for (int i = 0; i < socialNetworks.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + socialNetworks[i].Name);
            Console.WriteLine("Enter the command number :");
            SocialNetwork socialNetwork = null;
            bool alive = true;
            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < socialNetworks.Count; i++)
                if (command == i + 1) // The cycle where we are looking for a social network
                {
                    socialNetwork = socialNetworks[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Invalid command number, please try again");

            return socialNetwork;
        }

        public static GroupOfFriends GroupOfFriends(List<GroupOfFriends> groupOfFriends)
        {
            for (int i = 0; i < groupOfFriends.Count; i++)
                if (groupOfFriends[i].GetMembersInfo().Count == 0)
                    groupOfFriends.RemoveAt(i);
            
            if (groupOfFriends.Count == 0)
                throw new ArgumentException("Need to have a group of friends");
            for (int i = 0; i < groupOfFriends.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + groupOfFriends[i].Name);
            Console.WriteLine("Enter the command number :");
            GroupOfFriends groupOfFriends1 = null;
            bool alive = true;

            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < groupOfFriends.Count; i++)
                if (command == i + 1) // The cycle where we are looking for a group of friends
                {
                    groupOfFriends1 = groupOfFriends[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Invalid command number, please try again");

            return groupOfFriends1;
        }

        public static User User(List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Need to have a user");
            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + users[i].GetUserName());
            Console.WriteLine("Enter the command number :");
            User user = null;
            bool alive = true;

            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < users.Count; i++)
                if (command == i + 1) // The cycle where we are looking for a user
                {
                    user = users[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Invalid command number, please try again");

            return user;
        }

        public static string User(List<string> usernames, string message = "Need to have a user")
        {
            if (usernames.Count == 0)
                throw new ArgumentException(message);
            for (int i = 0; i < usernames.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + usernames[i]);
            Console.WriteLine("Enter the command number :");
            string user = null;
            bool alive = true;

            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < usernames.Count; i++)
                if (command == i + 1) // The cycle where we are looking for a user
                {
                    user = usernames[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Invalid command number, please try again");
 
            return user;
        }
    }
}