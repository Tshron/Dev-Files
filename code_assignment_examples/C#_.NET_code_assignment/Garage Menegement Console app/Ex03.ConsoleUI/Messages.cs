using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal class Messages
    {
        public static string GetMessage(int i_Id)
        {
            string message = string.Empty;
            Dictionary<string, string> messagesDictionary = new Dictionary<string, string>();
            switch (i_Id)
            {
                case 0:
                    message = "Welcome back Sir!\n\nWhat would you like to do?\n";
                    break;
                case 1:
                    message = "Register New Vehicle:\n\n";
                    break;
                case 2:
                    message = "Ok, the new vehicle has been registered\n";
                    break;
                case 3:
                    message = "What is the new status of the vehicle ?\n";
                    break;
                case 4:
                    message = "OK, the vehicle's status has changed\n";
                    break;
                case 5:
                    message = "No such status available, please select from the above list\n ";
                    break;
                case 6:
                    message = "\npress 'enter' to go back \n";
                    break;
                case 7:
                    message = "Please enter car's license plate\n";
                    break;
                case 8:
                    message = "There is no such vehicle with that license plate in the garage, please type the plate number again\n";
                    break;
                case 10:
                    message = "\n Or, type Q to exit \n";
                    break;
                case 11:
                    message = "That's not a valid value, please type a positive number\n";
                    break;
                case 12:
                    message = "Ok, car's tires are at full pressure, and it's at: ";
                    break;
                case 13:
                    message = "This value is above the manufacture limit\nPlease type a valid number";
                    break;
                case 14:
                    message = "Ok, how much gas you would like to fil?\n";
                    break;
                case 15:
                    message = "Ok, how much engine run time you would like to add (in minutes)\n";
                    break;
                case 16:
                    message = "Ok, car was successfully charged \n";
                    break;
                case 17:
                    message = "Ok, car was successfully fueled\n";
                    break;
            }

            return message;
        }
    }
}
