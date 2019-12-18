using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class MotorCycle : Vehicle
    {
        private const int k_HowManyTires = 2;
        private const int k_MaxTirePressure = 33;

        private eLicenseType m_LicenseType;
        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }
        private float m_EngineCapacityCC;
        public float EngineCapacity
        {
            get
            {
                return m_EngineCapacityCC;
            }
        }

        public MotorCycle(string i_ModelName, string i_LicensePlate , float i_EngineCapacity, eLicenseType i_LicenseType, VehicleEnergy i_VehicleEnergy, string i_Manufacture) 
            : base(i_ModelName, i_LicensePlate, i_VehicleEnergy, k_HowManyTires, k_MaxTirePressure, i_Manufacture)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacityCC = i_EngineCapacity;
        }

        public override StringBuilder ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb = base.ToString();
            sb.AppendFormat("{0}: {1}\n", "Vehicle Type", "MotorCycle");
            sb.AppendFormat("{0}: {1}\n", "License Type", m_LicenseType);
            sb.AppendFormat("{0}: {1} CC\n", "Engine Capacity", m_EngineCapacityCC);
            return sb;
        }
    }
}
