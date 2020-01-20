using System;
using System.Collections.Generic;

namespace Print
{
    public class Printer
    {
        public static void PrintMessage(string i_Message)
        {
            Console.Write(i_Message);
        }

        public static void PrintUser(string i_Message)
        {
            Console.Write(i_Message);
        }

        public static void PrintFormat(string i_Format, object i_ObjForPrint)
        {
            string forPrint = string.Format(i_Format, i_ObjForPrint);
            PrintMessage(forPrint);
        }

        public static void PrintSystemFormat(string i_Format, object i_ObjForPrint)
        {
            string forPrint = string.Format(i_Format, i_ObjForPrint);
            PrintSystemMessage(forPrint);
        }

        public static void PrintFormatError(string i_Format, object i_ObjForPrint)
        {
            string forPrint = string.Format(i_Format, i_ObjForPrint);
            PrintError(forPrint);
        }

        public static void PrintFormatError(string i_Format, object i_FirstObjForPrint, object i_SecondObjForPrint)
        {
            string forPrint = string.Format(i_Format, i_FirstObjForPrint, i_SecondObjForPrint);
            PrintError(forPrint);
        }

        public static void PrintSystemMessage(string i_Message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            PrintMessage(i_Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintError(string i_Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMessage(i_Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintSuccess(string i_Message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintMessage(i_Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintLists(List<string> i_List)
        {
            foreach(string str in i_List)
            {
                PrintMessage(str);
            }
        }
    }
}
