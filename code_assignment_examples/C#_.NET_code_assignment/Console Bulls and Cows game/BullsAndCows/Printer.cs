using System;
using System.Collections.Generic;
using System.Text;

namespace BullsAndCows
{
    public class Printer
    {
        public static void PrintMessage(int i_Id)
        {
           Console.WriteLine(Messages.GetMessage(i_Id));
        }

        public static void PrintErrorMessage(int i_Id)
        {
            FailureMode();
            PrintMessage(i_Id);
            InfoMode();
        }

        public static void PrintErrorMessage(string i_Message)
        {
            FailureMode();
            Console.WriteLine(i_Message);
            InfoMode();
        }

        public static void PrintFormatMessage(int i_Id, List<string> i_Args)
        {
            string buildMessage = string.Format(Messages.GetMessage(i_Id), i_Args.ToArray());
            Console.WriteLine(buildMessage);
        }
        
        public static void PrintBoard(Guess[] i_Board, int i_WidthColumn)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Ex02.ConsoleUtils.Screen.Clear();
            string border = "\u00A6";
            string[] titles = new string[2] { "Pins:", "Result:" };

            int leftColumn = (i_WidthColumn * 2) + 1;
            int rightColumn = i_WidthColumn > 4 ? (i_WidthColumn * 2) - 1 : 7;
            
            StringBuilder sb = new StringBuilder("Current board status:\n\n");
            sb.AppendFormat(border + titles[0] + "{0}" + border + titles[1] + "{1}" + border + "\n", new string(' ', leftColumn - 5), new string(' ', rightColumn - 7));
            sb.AppendFormat(border + "{0}" + border + "{1}" + border + "\n", new string('=', leftColumn), new string('=', rightColumn));
            sb.AppendFormat(border + "{0}" + border + "{1}" + border + "\n", BlowUpWord("####", true), BlowUpWord("    ", false));
            sb.AppendFormat(border + "{0}" + border + "{1}" + border + "\n", new string('=', leftColumn), new string('=', rightColumn));

            foreach(Guess guess in i_Board)
            {
                sb.AppendFormat(border + "{0}" + border + "{1}" + border + "\n", BlowUpWord(guess.UserGuess, true), BlowUpWord(new string(guess.m_Match), false));
                sb.AppendFormat(border + "{0}" +  border + "{1}" + border + "\n", new string('=', leftColumn), new string('=', rightColumn));
            }

            Console.WriteLine(sb);
        }

        public static string BlowUpWord(string i_Word, bool i_WrapWithSpaces)
        {
            string blowUpWord = string.Empty;
            string addSpaces = i_WrapWithSpaces ? " " : string.Empty;
            blowUpWord += addSpaces;
            for(int i = 0; i < i_Word.Length - 1; i++)
            {
                blowUpWord += i_Word[i] + " ";
            }

            blowUpWord += i_Word[i_Word.Length - 1] + addSpaces;
            return blowUpWord;
        }

        public static void InfoMode()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessMode()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void FailureMode()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        
        public static void EndOfGame()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            string[] sadFace =
                {
                    "                       ====                            ====                  ",
                    "                      ||  ||                          ||  ||                 ",
                    "                      ||  ||                          ||  ||                 ",
                    "                       ====                            ====                  ",
                    "                                       | |                                   ",
                    "                                       | |                                   ",
                    "                                       | |                                   ",
                    "                                       | |                                   ",
                    "                                                                             ",
                    "                              ______________________                         ",
                    "                             / ____________________ \\                       ",
                    "                            / /                    \\ \\                     ",
                    "                           / /                      \\ \\                    ",
                    "                          / /                        \\ \\                   ",
                    "                         / /                          \\ \\                  ",
                    "                        / /                            \\ \\                 ",
                    "                                    Bye - Bye :(                             "
                };
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            foreach(string line in sadFace)
            {
                Console.WriteLine(line);
            }
        }    
    }
}
