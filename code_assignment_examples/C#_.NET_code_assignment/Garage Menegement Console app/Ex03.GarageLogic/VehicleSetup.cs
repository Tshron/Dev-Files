using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public static class VehicleSetup
    {
        public static Dictionary<string, string> BuiltPropertiesList(eVehicleTypes i_Type)
        {

            Dictionary<string, string> i_Properties = new Dictionary<string, string>();
            i_Properties.Add("Model Name", "string");
            i_Properties.Add("License Plate", "string");
            i_Properties.Add("Manufacture", "string");
            switch (i_Type)
            {
                case eVehicleTypes.FuelCar:
                case eVehicleTypes.ElectronicCar:
                    i_Properties.Add("Car Color", "eColor");
                    i_Properties.Add("Number of Doors", "eCarDoors");
                    break;
                case eVehicleTypes.Truck:
                    i_Properties.Add("Is carrying dangerous load", "bool");
                    i_Properties.Add("Load Volume", "float");
                    break;
                case eVehicleTypes.FuelMotorCycle:
                case eVehicleTypes.ElectronicMotorCycle:
                    i_Properties.Add("Engine Capacity CC", "int");
                    i_Properties.Add("License Type", "eLicenseType");
                    break;
                default:
                    break;
            }

            return i_Properties;
        }

        public static bool CheckProperty(Garage i_Garage, string i_Value, string i_Field, string i_Type)
        {
            bool boolField = false;
            string stringField = "";
            float floatField = 0;
            int intField = 0;
            eCarDoors doorField = eCarDoors.Five;
            eColor colorField = eColor.Black;
            eLicenseType licenseTypeField = eLicenseType.A;
            string colorError = "This Field must be one of those: " + String.Join(" / ", Enum.GetNames(typeof(eColor)).ToList().Select(x => x.ToString()));
            string carDoorError = "This Field must be one of those: " + String.Join(" / ", Enum.GetNames(typeof(eCarDoors)).ToList().Select(x => x.ToString()));
            string licenseTypeError = "This Field must be one of those: " + String.Join(" / ", Enum.GetNames(typeof(eLicenseType)).ToList().Select(x => x.ToString()));

            switch (i_Type)
            {
                case "int":
                    intField = !int.TryParse(i_Value, out intField) ? 
                        throw new FormatException("This Field must be a number") : 
                        intField < 0 ? throw new FormatException("Positive number only") : intField;
                    break;
                case "eColor":
                    colorField = !Enum.IsDefined(typeof(eColor), i_Value) ?
                        throw new FormatException(colorError) : colorField;
                    break;
                case "eCarDoors":
                    doorField = !Enum.IsDefined(typeof(eCarDoors), i_Value) ?
                        throw new FormatException(carDoorError) : doorField;
                    break;
                case "eLicenseType":
                    licenseTypeField = !Enum.IsDefined(typeof(eLicenseType), i_Value) ?
                        throw new FormatException(licenseTypeError) : licenseTypeField;
                    break;
                case "bool":
                    boolField = !bool.TryParse(i_Value, out boolField) ? throw new FormatException("This Field must be a True of False") : boolField;
                    break;
                case "float":
                    floatField = !float.TryParse(i_Value, out floatField) ? 
                        throw new FormatException("This Field must be a float") :
                        floatField < 0 ? throw new FormatException("Positive number only") : floatField;
                    break;
                case "string":
                    if (i_Field == "License Plate")
                    {
                        if (i_Garage.IsLicensePlateExists(i_Value))
                        {
                            throw new ArgumentException("There is already a Vehicle with the same license plate");
                        }
                    }
                    if (i_Value.Replace(" ", "").Length == 0)
                    {
                        throw new FormatException("This Field can't be empty");
                    }

                    break;
            }

            return true;
        }
        public static Vehicle CreateNewVehicle(Garage i_Garage, eVehicleTypes vehicleType, Dictionary<string, string> i_Properties)
        {
            Vehicle i_Vehicle;
            bool creationSuccess = false;
            string modelName = "", licensePlate = "", manufacture = "";
            float engineCapacity = 0, loadVolume = 0;
            bool isCarryingDangerousLoad = false;
            eCarDoors door = eCarDoors.Five;
            eColor color = eColor.Black;
            eLicenseType licenseType = eLicenseType.A;
            
            foreach (KeyValuePair<string, string> pairs in i_Properties)
            {
                switch (pairs.Key)
                {
                    case "Model Name":
                        modelName = pairs.Value;
                        break;
                    case "License Plate":
                        licensePlate = pairs.Value;
                        break;
                    case "Manufacture":
                        manufacture = pairs.Value;
                        break;
                    case "Car Color":
                        eColor.TryParse(pairs.Value, out color);
                        break;
                    case "Number of Doors":
                        eCarDoors.TryParse(pairs.Value, out door);
                        break;
                    case "Engine Capacity CCS":
                        engineCapacity = float.Parse(pairs.Value);
                        break;
                    case "License Type":
                        eLicenseType.TryParse(pairs.Value, out licenseType);
                        break;
                    case "Is carrying dangerous load":
                        isCarryingDangerousLoad = bool.Parse(pairs.Value);
                        break;
                    case "Load Volume":
                        loadVolume = float.Parse(pairs.Value);
                        break;
                }
            }

            switch (vehicleType)
            {
                case eVehicleTypes.ElectronicCar:
                    i_Vehicle = new ElectricCar(modelName, licensePlate, color, door, manufacture);
                    //ElectricCar.TryParse(i_Properties, out newVehicle);
                    break;
                case eVehicleTypes.ElectronicMotorCycle:
                    i_Vehicle = new ElectricMotorCycle(modelName, licensePlate, engineCapacity, eLicenseType.A1, manufacture);
                    break;
                case eVehicleTypes.FuelCar:
                    i_Vehicle = new FuelCar(modelName, licensePlate, color, door, manufacture);
                    //FuelCar.TryParse(i_Properties, out newVehicle);
                    break;
                case eVehicleTypes.FuelMotorCycle:
                    i_Vehicle = new FuelMotorCycle(modelName, licensePlate, engineCapacity, licenseType, manufacture);
                    break;
                case eVehicleTypes.Truck:
                    i_Vehicle = new Truck(modelName, licensePlate, manufacture, isCarryingDangerousLoad, loadVolume);
                    break;
                default:
                    i_Vehicle = null;
                    break;
            }
            return i_Vehicle;
        }
    }
}
