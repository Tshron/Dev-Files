using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        private string m_LicensePlate;
        public string LicensePlate
        {
            get
            {
                return m_LicensePlate;
            }
        }

        private VehicleEnergy m_Energy;
        public VehicleEnergy Energy
        {
            get
            {
                return m_Energy;
            }
        }

        // $G$ DSN-999 (-3) This List should be readonly
        private List<Tire> m_Tires;
        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
        }

        public Vehicle(string i_ModelName, string i_LicensePlate, VehicleEnergy i_Energy, int i_HowManyTires, float i_MaxTirePressure, string i_Manufacture)
        {
            m_ModelName = i_ModelName;
            m_LicensePlate = i_LicensePlate;
            m_Energy = i_Energy;
            m_Tires = Tire.MakeTireSet(i_HowManyTires, i_MaxTirePressure, i_Manufacture);
        }
        
        public virtual StringBuilder ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}\n", "Model name", m_ModelName);
            sb.AppendFormat("{0}: {1}\n", "License plate", m_LicensePlate);
            return sb;
        }
        
    }
}
