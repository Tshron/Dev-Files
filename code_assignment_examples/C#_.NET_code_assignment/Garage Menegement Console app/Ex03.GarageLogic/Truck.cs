using System.Text;

namespace Ex03.GarageLogic
{
    // $G$ DSN-999 (-10) Truck is no different from car or motorcycle
    public class Truck : Vehicle
    {
        private const int k_HowManyTires = 12;
        private const float k_MaxTirePressure = 26;
        private const eFuel k_FuelType = eFuel.Soler;
        private const float k_MaxAmountOfFuel = (float)110;

        private bool m_IsCarryingDangerousLoad;
        public bool IsCarryingDangerousLoad
        {
            get
            {
                return m_IsCarryingDangerousLoad;
            }
        }
        private float m_LoadVolume;
        public float LoadVolume
        {
            get
            {
                return m_LoadVolume;
            }
        }

        public Truck(string i_ModelName, string i_LicensePlate, string i_Manufacture, bool i_IsCarryingDangerousLoad, float i_LoadVolume) :
            base(i_ModelName, i_LicensePlate, new FuelEnergy(k_FuelType, k_MaxAmountOfFuel), k_HowManyTires, k_MaxTirePressure, i_Manufacture)
        {
            m_IsCarryingDangerousLoad = i_IsCarryingDangerousLoad;
            m_LoadVolume = i_LoadVolume;
        }

        public override StringBuilder ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}\n", "Vehicle Type", "Truck");
            sb.AppendFormat("{0}: {1}\n", "Is carrying dangerous load", m_IsCarryingDangerousLoad);
            sb.AppendFormat("{0}: {1}\n", "Load Volume", m_LoadVolume);
            return sb;
        }
    }
}
