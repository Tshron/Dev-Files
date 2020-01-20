namespace Ex03.GarageLogic
{
    public class FuelMotorCycle : MotorCycle
    {
        private const eFuel k_FuelType = eFuel.Octan95;
        private const int k_MaxAmountOfFuel = 8;

        public FuelMotorCycle(string i_ModelName, string i_LicensePlate, float i_EngineCapacity, eLicenseType i_LicenseType, string i_Manufacture) 
            : base(i_ModelName, i_LicensePlate, i_EngineCapacity, i_LicenseType, new FuelEnergy(k_FuelType, k_MaxAmountOfFuel), i_Manufacture)
        {
        }        
    }
}
