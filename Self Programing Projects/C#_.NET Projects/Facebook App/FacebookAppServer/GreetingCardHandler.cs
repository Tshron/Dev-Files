using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GreetingCard;

namespace FacebookAppServer
{
    internal class GreetingCardHandler
    {
        internal static List<string> GetAllCardsOptions()
        {
            List<string> cardsNames = new List<string>();
            Enum.GetNames(typeof(eGreetingCards)).ToList().ForEach(x => cardsNames.Add(x));

            return cardsNames;
        }

        internal static GreetingCard.GreetingCard GetGreetingCard(string i_CardName, FBUser.FBUser i_FriendToSend)
        {
            IGreetingCardBuilder cardChoice = null;
            List<string> stringForCardBuild = GreetingCardsUtils.GetUserAndFriendsNames(i_FriendToSend);
            List<Image> imageToSend = GreetingCardsUtils.GetUserAndFriendPhotos(i_FriendToSend);
            switch (i_CardName)
            {
                case "Birthday":
                    cardChoice = new BirthdayGreetingCard();
                    stringForCardBuild.Add(GreetingCardsUtils.GetFriendAge(i_FriendToSend));
                    break;
                case "FacebookFriends":
                    cardChoice = new FriendsAnniversaryGreetingCard();
                    stringForCardBuild.Add(GreetingCardsUtils.GetFriendshipLength(i_FriendToSend));
                    imageToSend.AddRange(GreetingCardsUtils.GetFriendTaggedPhotosOfUser(i_FriendToSend));
                    break;
                case "Love":
                    cardChoice = new LoveGreetingCard();
                    break;
            }

            GreetingCardMaker cardMaker = new GreetingCardMaker(cardChoice, imageToSend, stringForCardBuild);
            cardMaker.AssemblyCard();

            return cardMaker.GetGreetingCard();
        }
    }
}
