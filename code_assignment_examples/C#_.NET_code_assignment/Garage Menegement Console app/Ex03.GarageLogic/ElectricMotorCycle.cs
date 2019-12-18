namespace Ex03.GarageLogic
{
    internal class ElectricMotorCycle : MotorCycle
    {
        private const float k_MaxBatteryCapacity = (float)1.4;

        public ElectricMotorCycle(string i_ModelName, string i_LicensePlate, float i_EngineCapacity, eLicenseType i_LicenseType, string i_Manufacture) 
            : base(i_ModelName, i_LicensePlate, i_EngineCapacity, i_LicenseType, new ElectricEnergy(k_MaxBatteryCapacity), i_Manufacture)
        {
        }
    }
}
