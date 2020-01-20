using Ex03.GarageLogic;
using System;
using System.Text;
using Print;
using Exceptions;

namespace Ex03.ConsoleUI
{
    public class RepairDepartment
    {
        public static void ChangeVehicleStatus(Garage i_Garage)
        {
            Printer.PrintMessage(Messages.GetMessage(7));
            Customer customer = matchLicensePlateToCustomer(i_Garage);
            int count = 1;
            int chooseOfUser = 0;
            StringBuilder sb = new StringBuilder();
            foreach (eGarageOrderStatus status in Enum.GetValues(typeof(eGarageOrderStatus)))
            {
                if(status == eGarageOrderStatus.All)
                {
                    continue;
                }

                sb.Append(count + "." + status + "\n");
                count++;
            }

            Printer.PrintMessage(Messages.GetMessage(3) + sb);
            bool flag = false;
            while (!flag)
            {
                try
                {
                    flag = int.TryParse(Console.ReadLine(), out chooseOfUser);
                    if (!flag || chooseOfUser > count - 1 || chooseOfUser == 0) 
                    {
                        flag = false;
                        throw new FormatException();
                    }
                    else
                    {
                        flag = true;
                    }
                }
                catch (FormatException exp)
                {
                    Printer.PrintError(Messages.GetMessage(5));
                }
            }
            
            switch ((eGarageOrderStatus)(++chooseOfUser))
            {
                case eGarageOrderStatus.Fixed:
                    customer.UpdateStatus(eGarageOrderStatus.Fixed);
                    break;
                case eGarageOrderStatus.Paid:
                    customer.UpdateStatus(eGarageOrderStatus.Paid);
                    break;
                case eGarageOrderStatus.UnderRepair:
                    customer.UpdateStatus(eGarageOrderStatus.UnderRepair);
                    break;
                default:
                    Console.Clear();
                    Printer.PrintError(Messages.GetMessage(5));
                    break;
            }

            Printer.PrintSuccess(Messages.GetMessage(4));
            Printer.PrintMessage(Messages.GetMessage(6));
            Console.ReadLine();
        }

        public static void IncreaseAirPressure(Garage i_Garage)
        {
            Printer.PrintMessage(Messages.GetMessage(7));

            Customer customer = matchLicensePlateToCustomer(i_Garage);
            try
            {
                i_Garage.IncreaseAirPressure(customer, customer.Vehicle.Tires[0].MaxPressure - customer.Vehicle.Tires[0].CurrentAirPressure);
            }
            catch (ValueOutOfRangeException e)
            {
                Printer.PrintError(
                        Messages.GetMessage(13) + ", Tire's limit is: " + customer.Vehicle.Tires[0].MaxPressure
                        + " and right now pressure is " + customer.Vehicle.Tires[0].CurrentAirPressure);
            }

            Printer.PrintSuccess(Messages.GetMessage(12) + customer.Vehicle.Tires[0].CurrentAirPressure + "PSI");
            Printer.PrintMessage(Messages.GetMessage(6));
            Console.ReadLine();
            Console.Clear();
        }

        public static void FillFuelToCar(Garage i_Garage)
        {
            Printer.PrintMessage(Messages.GetMessage(7));
            Customer customer = matchLicensePlateToCustomer(i_Garage);
            if(customer.Vehicle.Energy is ElectricEnergy)
            {
                Printer.PrintError("This is Electronic vehicle, you can't fill fuel in it\n");
                FillFuelToCar(i_Garage);
            }
            else
            {
                AddEnergy(i_Garage, customer);
            }
            
        }

        public static void ChargeBattery(Garage i_Garage)
        {
            Printer.PrintMessage(Messages.GetMessage(7));
            Customer customer = matchLicensePlateToCustomer(i_Garage);
            if (customer.Vehicle.Energy is FuelEnergy)
            {
                Printer.PrintError("This is Gas vehicle, you can't charge it's battery\n");
                ChargeBattery(i_Garage);
            }
            else
            {
                AddEnergy(i_Garage, customer);
            }
        }

        public static void AddEnergy(Garage i_Garage, Customer i_Customer)
        {
            float howMuchToAdd = 0;
            bool shouldConvertToMinutes = false;
            if (i_Customer.Vehicle.Energy is FuelEnergy)
            {
                Printer.PrintMessage(Messages.GetMessage(14));
            }
            else
            {
                shouldConvertToMinutes = true;
                Printer.PrintMessage(Messages.GetMessage(15));
            }

            bool flag = false;
            while(!flag)
            {
                try
                {
                    flag = float.TryParse(Console.ReadLine(), out howMuchToAdd);
                    if(!flag || howMuchToAdd < 0)
                    {
                        flag = false;
                        throw new FormatException();
                    }
                    else
                    {
                        try
                        {
                            if(shouldConvertToMinutes)
                            {
                                i_Garage.AddEnergy(i_Customer, howMuchToAdd / 60f);
                            }
                            else
                            {
                                i_Garage.AddEnergy(i_Customer, howMuchToAdd);
                            }
                            
                        }
                        catch (ValueOutOfRangeException e)
                        {
                            if (i_Customer.Vehicle.Energy is FuelEnergy)
                            {
                                Printer.PrintFormatError("{0}, Gas Tank can hold {1} liters more\n", Messages.GetMessage(13), (i_Customer.Vehicle.Energy.MaxAmountOfEnergy - i_Customer.Vehicle.Energy.AmountOfLeftEnergy()));
                            }
                            else
                            {
                                Printer.PrintFormatError("{0}, Battery maximum limit is  {1} hours\n", Messages.GetMessage(13), (i_Customer.Vehicle.Energy.MaxAmountOfEnergy - i_Customer.Vehicle.Energy.AmountOfLeftEnergy()));
                            }

                            flag = false;
                        }
                    }
                }
                catch(FormatException exp)
                {
                    Printer.PrintError(Messages.GetMessage(11));
                }
                
            }

            int id = 0;
            id = i_Customer.Vehicle.Energy is FuelEnergy ? 17 : 16;
            Printer.PrintSuccess(Messages.GetMessage(id));
            Printer.PrintMessage(Messages.GetMessage(6));
            Console.ReadLine();
        }

        private static Customer matchLicensePlateToCustomer(Garage i_Garage)
        {
            string licensePlate = Console.ReadLine();
            Customer customer = null;
            while ((customer = i_Garage.GetCustomerByLicensePlate(licensePlate)) == null)
            {
                Printer.PrintError(Messages.GetMessage(8));
                Printer.PrintMessage(Messages.GetMessage(7));
                Printer.PrintMessage(Messages.GetMessage(10));
                licensePlate = Console.ReadLine();

                if(licensePlate.ToLower().Equals("q"))
                {
                    throw new BreakOutException();
                }
            }

            return customer;
        }
    }
}
