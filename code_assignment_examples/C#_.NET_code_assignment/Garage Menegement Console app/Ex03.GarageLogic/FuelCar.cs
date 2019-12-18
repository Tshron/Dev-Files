namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        private const eFuel k_FuelType = eFuel.Octan96;
        private const int k_MaxAmountOfFuel = 55;

        public FuelCar(string i_ModelName, string i_LicensePlate, eColor i_Color, eCarDoors i_Doors, string i_Manufacture) 
            : base(i_ModelName, i_LicensePlate, i_Color, i_Doors, new FuelEnergy(k_FuelType, k_MaxAmountOfFuel), i_Manufacture)
        {
        }
    }
}