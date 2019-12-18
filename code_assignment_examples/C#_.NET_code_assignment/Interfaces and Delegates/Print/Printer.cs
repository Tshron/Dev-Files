using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print
{
    public class Printer
    {
        public static void PrintMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public static void PrintUser(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public static void PrintFormat(string i_Format, object i_ObjForPrint)
        {
            string forPrint = string.Format(i_Format, i_ObjForPrint);
            PrintMessage(forPrint);
        }

        public static void PrintFormat(string i_Format, object i_ObjForPrint, object i_secondObjForPrint)
        {
            string forPrint = string.Format(i_Format, i_ObjForPrint, i_secondObjForPrint);
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

        public static void PrintFormatSuccess(string i_Format, params string[] i_ObjectsForPrint)
        {
            string forPrint = string.Format(i_Format, i_ObjectsForPrint);
            PrintSuccess(forPrint);
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
            foreach (string str in i_List)
            {
                PrintMessage(str);
            }
        }
    }
}
