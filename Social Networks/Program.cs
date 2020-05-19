using System;
using System.Collections.Generic;

namespace Social_Networks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SocialNetwork> socialNetworks = new List<SocialNetwork>();
            List<GroupOfFriends> groupOfFriends = new List<GroupOfFriends>();
            List<User> users = new List<User>();

            bool alive = true;
            while (alive) // Command loop
            {
                Console.WriteLine("List of commands :");
                Console.WriteLine("\t1. Create user\n\t2. Create social network\n\t3. Create group of friends");
                Console.WriteLine("\t4. Go to user control\n\t5. Clear concole\n\t6. Exit the program");
                Console.WriteLine("Enter the command number :");
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            Create.User(users);
                            break;
                        case 2:
                            Create.SocialNetwork(socialNetworks, users);
                            break;
                        case 3:
                            Create.GroupOfFriends(socialNetworks, groupOfFriends, users);
                            break;
                        case 4:
                            Control.User(socialNetworks, groupOfFriends, users);
                            break;
                        case 5:
                            Console.Clear();
                            break;
                        case 6:
                            alive = false;
                            continue;
                        default:
                            throw new ArgumentException("Invalid command number, please try again");
                    }
                }
                catch (Exception e) { Message.ExceptionHandler(e); }
            }
        }
    }
}