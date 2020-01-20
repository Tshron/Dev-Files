using Ex03.GarageLogic;
using System;
using System.Text;
using Print;
using Exceptions;
using System.Text.RegularExpressions;
using System.Collections.Generic;


// $G$ CSS-999 (-5) Standards are not kept as required - you should use StyleCop

namespace Ex03.ConsoleUI
{
    // $G$ CSS-016 (-5) The main class should be called Program.


    // $G$ CSS-999 (-8) The Class must have an access modifier.
 
    class GarageManager
    {

        // $G$ NTT-999 (-5) This kind of field should be readonly.
        // $G$ CSS-004 (-5) Bad static members variable name (should be in the form of s_PamelCase).
        private static Garage m_Garage = new Garage();
        private static StringBuilder m_Header = new StringBuilder();
        private const int k_Width = 46;

        public static void Main()
        {
            anotherDayAtWork();
        }

        private static void anotherDayAtWork()
        {
            formatHeader();
            systemLoop();
        }


        // $G$ NTT-999 (-5) You should have use: Environment.NewLine instead of "\n".
        private static void formatHeader()
        {
            string date = DateTime.Now.ToString("dd MMMM HH:mm");
            m_Header.AppendFormat("{0}\n{1}{2}{3}{2}{1}\n{0}\n{1}  Tamir & Tomer Garage  {1}\n{0}\n", new string('~', k_Width), new string('~', date.Length - 1), new string(' ', k_Width / 7), date);
        }

        private static void systemLoop()
        {
            List<string> functions;
            int userChosen;
            
            while (true)
            {
                try
                {
                    Console.Clear();
                    Printer.PrintSystemMessage(m_Header.ToString());
                    Printer.PrintSystemMessage(Messages.GetMessage(0));
                    functions = GetFunctions();
                    Printer.PrintLists(functions);
                    Printer.PrintSystemMessage("\nChoose a number by the function you want to do:\n");
                    Printer.PrintMessage("> ");
                    try
                    {
                        if (int.TryParse(Console.ReadLine(), out userChosen))
                        {
                            if(userChosen <= functions.Count)
                            {
                                giveShmulikTheJob(userChosen);
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(0, functions.Count);
                            }
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    catch(ValueOutOfRangeException vore)
                    {
                        Printer.PrintError(vore.Message);
                    }
                    catch(ArgumentException ae)
                    {
                        Printer.PrintError("Please insert a number\n");
                    }
                }
                catch(BreakOutException e)
                {
                   continue;
                }
            }
        }


        // $G$ DSN-011 (-8) The component who responsible for creating vehicles, should be in a separate component.
        private static void giveShmulikTheJob(int i_UserChoosen)
        {
            Console.Clear();
            eFunctions task;
            Printer.PrintSystemMessage(m_Header.ToString());
            if (Enum.TryParse(i_UserChoosen.ToString(), out task))
            {
                switch (task)
                {
                    case eFunctions.RegisterNewVehicle:
                        m_Garage.Customers.Add(CustomerDepartment.NewCustomer(m_Garage));
                        break;
                    case eFunctions.PrintFullCarDetails:
                        CustomerDepartment.GetFullCustomerDetails(m_Garage);
                        break;
                    case eFunctions.LicensePlateList:
                        CustomerDepartment.licensePlateList(m_Garage);
                        break;
                    case eFunctions.IncreaseAirPressure:
                        RepairDepartment.IncreaseAirPressure(m_Garage);
                        break;
                    case eFunctions.ChargeBattery:
                        RepairDepartment.ChargeBattery(m_Garage);
                        break;
                    case eFunctions.FillGasToVehicle:
                        RepairDepartment.FillFuelToCar(m_Garage);
                        break;
                    case eFunctions.ChangeVehicleStatus:
                        RepairDepartment.ChangeVehicleStatus(m_Garage);
                        break;
                    default:
                        throw new ValueOutOfRangeException(0, Enum.GetValues(typeof(eFunctions)).Length);
                }
            }
        }

        public static List<string> GetFunctions()
        {
            List<string> functions = new List<string>();
            int count = 1;
            eFunctions f = eFunctions.ChangeVehicleStatus;
            foreach(string name in f.GetType().GetEnumNames())
            {
                functions.Add(count + "." + Regex.Replace(name, "([A-Z])"," $1")+"\n");
                count++;
            }
            return functions;
        }        
    }
}
