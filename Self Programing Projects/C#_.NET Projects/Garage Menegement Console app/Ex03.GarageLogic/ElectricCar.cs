namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        private const float k_MaxBatteryCapacity = (float)1.8;

        public ElectricCar(string i_ModelName, string i_LicensePlate, eColor i_Color, eCarDoors i_Doors, string i_Manufacture) 
            : base(i_ModelName, i_LicensePlate, i_Color, i_Doors, new ElectricEnergy(k_MaxBatteryCapacity), i_Manufacture)
        {
        }
    }
}