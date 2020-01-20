using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Text;
using Print;
using Exceptions;
using System.Text.RegularExpressions;

namespace Ex03.ConsoleUI
{
    class CustomerDepartment
    {
        private static Dictionary<string, string> m_SetProperites;

        public static Customer NewCustomer(Garage i_Garage)
        {
            m_SetProperites = new Dictionary<string, string>();
            string customerName, customerPhoneNumber;
            Printer.PrintSystemMessage(Messages.GetMessage(1));
            Printer.PrintSystemMessage("Please fill the following fields:\n");
            Printer.PrintSystemMessage("Customer Details:\n");
            Printer.PrintSystemMessage("Name: ");
            customerName = Console.ReadLine();
            
            
            Printer.PrintSystemMessage("Phone number: ");
            Regex phoneNubmer = new Regex("([+]*)([0-9]+[-]*)+");
            Match match = null;
            customerPhoneNumber = Console.ReadLine();
            while (customerPhoneNumber is null || !(phoneNubmer.Match(customerPhoneNumber)).Success)
            {
                Printer.PrintError("Phone number must be digits, + or - only\n");
                customerPhoneNumber = Console.ReadLine();
            }

            Customer newCutomer = new Customer(customerName, customerPhoneNumber, newVehicle(i_Garage));
            Printer.PrintSuccess("Success create a new "+ newCutomer.Vehicle.GetType().Name + "\n");
            Console.ReadLine();
            return newCutomer;
        }

        private static Vehicle newVehicle(Garage i_Garage)
        {
            Vehicle newVehicle = null;
            eVehicleTypes vehicleType = ChooseVehicle();
            string userInput;
            bool checkInput = true;
            Dictionary<string, string> properites = VehicleSetup.BuiltPropertiesList(vehicleType);
            Printer.PrintSystemFormat("\n{0} Details:\n", vehicleType);
            foreach (KeyValuePair<string, string> str in properites)
            {
                
                while (checkInput)
                {
                    Printer.PrintMessage(str.Key + ": ");
                    userInput = Console.ReadLine();
                    try
                    {
                        if(VehicleSetup.CheckProperty(i_Garage, userInput, str.Key, str.Value))
                        {
                            m_SetProperites.Add(str.Key, userInput);
                            checkInput = false;
                        }
                    }

                    catch (FormatException fx)
                    {
                        Printer.PrintError(fx.Message + "\n");
                    }
                    catch (ArgumentException ae)
                    {
                        Printer.PrintError(ae.Message + "\n");
                    }
                }

                checkInput = true;
            }

            newVehicle = VehicleSetup.CreateNewVehicle(i_Garage, vehicleType, m_SetProperites);

            return newVehicle;
        }
        
    
        private static eVehicleTypes ChooseVehicle()
        {

            int index = 1, vehicleType = 0;
            Printer.PrintSystemMessage("\nWhich type of car would you like to add?\n");
            foreach (eVehicleTypes type in Enum.GetValues(typeof(eVehicleTypes)))
            {
                Printer.PrintMessage(index + ". " + type + "\n");
                index++;
            }

            Printer.PrintMessage("> ");
            
            bool checkInput = true;
            while (checkInput)
            {
                try
                {
                    while (!int.TryParse(Console.ReadLine(), out vehicleType))
                    {
                        Printer.PrintError("Please insert a number\n");
                        Printer.PrintMessage("> ");
                    }

                    if (!Enum.IsDefined(typeof(eVehicleTypes), vehicleType))
                    {
                        throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eVehicleTypes)).Length);
                    }

                    checkInput = false;
                }
                catch (ValueOutOfRangeException ve)
                {
                    Printer.PrintError(ve.Message+"\n");
                }
            }
            
            return (eVehicleTypes)vehicleType;
        }

        public static void GetFullCustomerDetails(Garage i_Garage)
        {
            string licensePlate = string.Empty;
            Printer.PrintMessage(Messages.GetMessage(7));
            licensePlate = Console.ReadLine();
            Customer customer = i_Garage.GetCustomerByLicensePlate(licensePlate);
            if (customer != null)
            {
                Printer.PrintMessage(customer.ToString());
            }

            else
            {
                Printer.PrintError(Messages.GetMessage(8));
            }
            Printer.PrintMessage(Messages.GetMessage(6));
            Console.ReadLine();
            Console.Clear();
        }

        public static void licensePlateList(Garage i_Garage)
        {
            int count, userInput;
            StringBuilder sb;
            string status;
            eGarageOrderStatus m_Status = eGarageOrderStatus.All;
            while (true)
            {
                sb = new StringBuilder();
                count = 1;
                sb.AppendLine("List of all license plates: ");
                sb.AppendLine("----------------------------");
                foreach (KeyValuePair<string, eGarageOrderStatus> s in i_Garage.GetLicensePlateList())
                {
                    if ((m_Status == eGarageOrderStatus.All) || (m_Status == s.Value))
                    {
                        sb.AppendLine("Plate: " + s.Key + "\tStatus: " + s.Value);
                    }
                }

                sb.AppendLine("----------------------------");
                sb.AppendLine("Choose number of status to filter or 'q' to return\n");
                foreach (eGarageOrderStatus s in Enum.GetValues(typeof(eGarageOrderStatus)))
                {
                    sb.Append(count + "." + s + "\t");
                    count++;
                }

                Printer.PrintMessage(sb.ToString());
                status = Console.ReadLine();
                if (status.ToLower() == "q")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    if (int.TryParse(status, out userInput))
                    {
                        switch ((eGarageOrderStatus)int.Parse(status))
                        {
                            case eGarageOrderStatus.Fixed:
                                m_Status = eGarageOrderStatus.Fixed;
                                break;
                            case eGarageOrderStatus.Paid:
                                m_Status = eGarageOrderStatus.Paid;
                                break;
                            case eGarageOrderStatus.UnderRepair:
                                m_Status = eGarageOrderStatus.UnderRepair;
                                break;
                            default:
                                m_Status = eGarageOrderStatus.All;
                                break;
                        }
                    }
                }
            }
        }
    }
}
