using System;
using System.Collections.Generic;

namespace Social_Networks
{
    struct Control
    {
        public static void User(List<SocialNetwork> socialNetworks, List<GroupOfFriends> groupOfFriends, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Need to have a user");
            Console.WriteLine("Select the user you want to control");
            User user = Choose.User(users);
            Console.Clear();
            if (user.GetUserInvitationSenders().Count != 0)
                Message.SuccessOperationHandler("You have new invitations!\nCheck invitation list");

            bool alive = true;
            while (alive) // Commands loop
            {
                Console.WriteLine("List of commands :");
                Console.WriteLine("\t1. Send an invitation\n\t2. Respond to the invitation\n\t3. Remove friend");
                Console.WriteLine("\t4. Join social network\n\t5. Remove social network\n\t6. Remove group of friends");
                Console.WriteLine("\t7. Join friend to group of friends\n\t8. Check social network list\n\t9. Check group of friends list");
                Console.WriteLine("\t10. Check friend list\n\t11. Check the invitations\n\t12. Return to the main menu");
                Console.WriteLine("Enter the command number :");
                int command = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (command)
                    {
                        case 1:
                            SendAnInvitation(user, users);
                            break;
                        case 2:
                            RespondToTheInvitation(user, users);
                            break;
                        case 3:
                            RemoveFriend(user, users);
                            break;
                        case 4:
                            JoinSocialNetwork(user, users, socialNetworks);
                            break;
                        case 5:
                            RemoveSocialNetwork(user, users, socialNetworks);
                            break;
                        case 6:
                            {
                                Console.WriteLine("Select a group of friends");
                                GroupOfFriends groupOfFriends1 = Choose.GroupOfFriends(groupOfFriends);
                                user.RemoveGroupOfFriends(groupOfFriends1);
                                Message.SuccessOperationHandler("Operation was successfully completed");
                            }
                            break;
                        case 7:
                            {
                                Console.WriteLine("Select a group of friends");
                                GroupOfFriends groupOfFriends1 = Choose.GroupOfFriends(groupOfFriends);

                                Console.WriteLine("Choose a friend from your friends list :");
                                string friend = Choose.User(user.GetUserFriendNicknames());
                                for (int i = 0; i < users.Count; i++)
                                    if (users[i].GetUserName() == friend)
                                        user.JoinGroupOfFriends(users[i], groupOfFriends1);
                                Message.SuccessOperationHandler($"Friend {friend} successfully added to {groupOfFriends1.Name}");
                            }
                            break;
                        case 8:
                            {
                                Console.WriteLine("Your social networks list :");
                                List<string> names = user.GetUserSocialNetworkNames();
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i]);
                            }
                            break;
                        case 9:
                            {
                                Console.WriteLine("Your group of friends list :");
                                List<string> names = user.GetUserGroupOfFriendsNames();
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i]);
                            }
                            break;
                        case 10:
                            {
                                Console.WriteLine("Your friend list :");
                                List<string> names = user.GetUserFriendNicknames();
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i]);
                            }
                            break;
                        case 11:
                            {
                                Console.WriteLine("Your invitations list :");
                                List<string> names = user.GetUserInvitationSenders();

                                List<string> message = new List<string>();
                                for (int i = 0; i < user.Invitations?.Count; i++)
                                    message.Add(user.Invitations[i].Message);
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i] + "\n\tMessage : " + message[i]);
                            }
                            break;
                        case 12:
                            alive = false;
                            break;
                        default:
                            throw new ArgumentException("Invalid command number, please try again");
                    }
                }
                catch (Exception e) { Message.ExceptionHandler(e); }
            }
        }
        
        private static void SendAnInvitation(User user, List<User> users)
        {
            Console.WriteLine("Invitation to your friends or invitation to add yourself to the recipient's friends ?");
            Console.WriteLine("\t1. To my friend list\n\t2. To recipient's friendlist");
            bool toMyFriendList = false;
            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    toMyFriendList = true;
                    break;
                case 2:
                    break;
                default:
                    throw new ArgumentException("Invalid command number, please try again");
            }

            Console.Clear();
            if (toMyFriendList)
                Console.WriteLine("\"Invite to friends\"");
            else
                Console.WriteLine("\"Request an invitation\"");

            Console.WriteLine("Choose to whom to send an invitation");
            User recipient = Choose.User(users);

            string message = null;

            Console.WriteLine("Want to write a message that will be in the invitation ?");
            Console.WriteLine("\t1. Yes\n\t2. No");
            Console.WriteLine("Enter the command number :");
            command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    Console.WriteLine("Write :");
                    message = Console.ReadLine();
                    break;
                case 2:
                    break;
                default:
                    throw new ArgumentException("Invalid command number, please try again");
            }

            if (toMyFriendList)
                user.InviteToFriends(recipient, message);
            else
                user.RequestAnInvitation(recipient, message);
            Message.SuccessOperationHandler("The invitation is sent");
        }
        private static void RespondToTheInvitation(User user, List<User> users)
        {
            Console.WriteLine("Choose invitation sender :");
            string sender = Choose.User(user.GetUserInvitationSenders(), "Need to have an invitation and, accordingly, the sender");

            Console.WriteLine("Accept or decline invitation ?");
            Console.WriteLine("\t1. Accept\n\t2. Decline");
            Console.WriteLine("Enter the command number :");
            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    for (int i = 0; i < user.Invitations.Count; i++)
                        if (user.Invitations[i].GetUserName() == sender)
                            user.RespondToTheInvitation(user.Invitations[i], Invitation.Respond.Accept);
                    break;
                case 2:
                    for (int i = 0; i < user.Invitations.Count; i++)
                        if (user.Invitations[i].GetUserName() == sender)
                            user.RespondToTheInvitation(user.Invitations[i], Invitation.Respond.Decline);
                    break;
                default:
                    throw new ArgumentException("Invalid command number, please try again");
            }
            if (command1 == 1)
                Message.SuccessOperationHandler("The invitation accepted");
            if (command1 == 2)
                Message.SuccessOperationHandler("The invitation declined");
        }
        private static void RemoveFriend(User user, List<User> users)
        {
            Console.WriteLine("Select a friend whom you want to remove from your friends list :");
            string friend = Choose.User(user.GetUserFriendNicknames(), "Need to have a friend");
            for (int i = 0; i < users.Count; i++)
                if (users[i].GetUserName() == friend)
                    user.RemoveFriend(users[i]);
            Message.SuccessOperationHandler($"Friend {friend} successfully removed");
        }
        private static void JoinSocialNetwork(User user, List<User> users, List<SocialNetwork> socialNetworks)
        {
            Console.WriteLine("Choose a social network :");
            SocialNetwork socialNetwork = Choose.SocialNetwork(socialNetworks, users);

            Console.WriteLine("Do you want to add a user or yourself");
            Console.WriteLine("\t1. Yourself\n\t2. User");
            Console.WriteLine("Enter the command number :");
            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    user.JoinSocialNetwork(socialNetwork);
                    break;
                case 2:
                    {
                        Console.WriteLine("Choose a friend from your friends list :");
                        user.JoinSocialNetwork(socialNetwork, Choose.User(users));
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid command number, please try again");
            }
            Message.SuccessOperationHandler("Operation was successfully completed");
        }
        private static void RemoveSocialNetwork(User user, List<User> users, List<SocialNetwork> socialNetworks)
        {
            Console.WriteLine("Choose a social network :");
            SocialNetwork socialNetwork = Choose.SocialNetwork(socialNetworks, users);

            Console.WriteLine("Do you want to remove a user or yourself");
            Console.WriteLine("\t1. Yourself\n\t2. User");
            Console.WriteLine("Enter the command number :");
            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    user.RemoveSocialNetwork(socialNetwork);
                    break;
                case 2:
                    {
                        Console.WriteLine("Choose a friend from your friends list :");
                        user.RemoveSocialNetwork(socialNetwork, Choose.User(users));
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid command number, please try again");
            }
            Message.SuccessOperationHandler("Operation was successfully completed");
        }
    }
}