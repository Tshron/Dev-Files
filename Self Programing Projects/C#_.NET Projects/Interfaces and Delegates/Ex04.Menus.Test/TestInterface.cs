using System.Linq;
using System;
using System.Collections.Generic;
using Ex04.Menus.Interfaces;
using Print;
using Exceptions;

namespace Ex04.Menus.Test
{
    public class TestInterface : IShowDate, IShowTime, ICountDigits, IShowVersion
    {
        public MainMenu BuildMenu()
        {
            MainMenu mainMenu = new MainMenu("Main");
            mainMenu.AddTab(new MenuItem("Show Date/Time",
                            new List<MenuItem>()
                                {
                                new ActionItem("Show Date", this as IShowDate),
                                new ActionItem("Show Time", this as IShowTime)
                                }));

            mainMenu.AddTab(new MenuItem("Version and Digits",
                            new List<MenuItem>()
                                {
                                new ActionItem("Count Digits", this as ICountDigits),
                                new ActionItem("Show Version", this as IShowVersion)
                                }));

            return mainMenu;
        }

        public void LoopMenu(MenuItem i_MenuItem, int i_MenuDepth)
        {
            while (true)
            {
                int userInput = -1;
                string exitOrBack = i_MenuDepth == 1 ? "Exit" : "Back";
                if (i_MenuItem is ActionItem)
                {
                    i_MenuItem.Show();
                    return;
                }

                Printer.PrintSystemMessage("This is an Interfaces based menu\n");

                printMenu(i_MenuItem, exitOrBack);

                Printer.PrintFormat("Please choose a submenu or {0}", exitOrBack);
                checkValidInput(i_MenuItem.ItemsUnderThis.Count, out userInput);
                Console.Clear();

                if (userInput != 0)
                {
                    LoopMenu(i_MenuItem.ItemsUnderThis[userInput - 1], i_MenuDepth + 1);
                }
                else
                {
                    return;
                }
            }
        }

        private void printMenu(MenuItem i_MenuItem, string i_Param)
        {
            Printer.PrintFormat("You are in the \"{0}\" menu", i_MenuItem.DisplayName);
            Printer.PrintFormat("-> 0.{0}", i_Param);
            i_MenuItem.Show();
        }

        private int checkValidInput(int i_Length, out int i_UserInput)
        {
            while (true)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out i_UserInput))
                    {
                        if ((i_UserInput <= i_Length) && (0 <= i_UserInput))
                        {
                            break;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(0, i_Length);
                        }
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch(ValueOutOfRangeException ve)
                {
                    Printer.PrintError(ve.Message);
                }
                catch(FormatException fe)
                {
                    Printer.PrintError("Please insert an integer");
                }
            }

            return i_UserInput;
        }

        public void CountDigits()
        {
            Console.WriteLine("Please write a sentence");
            Printer.PrintFormatSuccess("Number of digits in your input is: {0}", Console.ReadLine().Count(x => char.IsDigit(x)).ToString());
        }

        public void ShowDate()
        {
            Printer.PrintFormatSuccess("Today is : {0}", DateTime.Today.ToString("D"));
        }

        public void ShowTime()
        {
            Printer.PrintFormatSuccess("Current time is: {0}", DateTime.Now.ToString("t"));
        }

        public void ShowVersion()
        {
            Printer.PrintSuccess("Version: 19.2.4.32");
        }
    }
}
