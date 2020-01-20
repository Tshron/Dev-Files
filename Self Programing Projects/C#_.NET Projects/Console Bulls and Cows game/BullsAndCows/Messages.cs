namespace BullsAndCows
{
    public class Messages
    {
        public static string GetMessage(int i_Id)
        {
            string message = string.Empty;
            switch (i_Id)
            {
                case 0:
                    message = "Please type your next guess <A B C D E F G H> or 'Q' to quit";
                    break;
                case 1:
                    message = ", please see instructions above";
                    break;
                case 2:
                    message = "How many times would you like to guess?";
                    break;  
                case 3:
                    message = "Value out of range. Please choose number between 4 and 10";
                    break;
                case 4:
                    message = "You guessed after {0} steps!";
                    break;
                case 5:
                    message = "No more guesses allowed. You lost.";
                    break;
                case 6:
                    message = "Would you like to start a new game? (Y/N)";
                    break;
                case 7:
                    message = "Invalid input. Please type: 'Y' (Yes) / 'N' (No)";
                    break;
                case 8:
                    message = "Unexpected input. Please insert a number.";
                    break;
                case 9:
                    message = "One or more letters invalid.";
                    break;
                case 10:
                    message = "You are a MASTER! you guessed after 1 step!";
                    break;
                case 11:
                    message = "Your guess too long";
                    break;
                case 12:
                    message = "Your guess too short";
                    break;
                case 13:
                    message = "You've Typed an invalid guess";
                    break;
                default:
                    message = "Something wrong";
                    break;
            }

            return message;
        }
    }
}
