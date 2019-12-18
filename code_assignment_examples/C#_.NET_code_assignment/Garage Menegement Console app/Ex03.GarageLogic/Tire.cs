using System.Collections.Generic;
using System.Text;
using Exceptions;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        private string m_ManufactureName;
        public string TireManufacturer
        {
            get
            {
                return m_ManufactureName;
            }
        }
        private float m_CurrentAirPressure;
        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }


        // $G$ DSN-999 (-4) The "maximum air pressure" field should be readonly member of class wheel.
        private float m_MaxAirPressure;
        public float MaxPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public Tire(float i_MaxAirPressure, string i_Manufacture)
        {
            m_CurrentAirPressure = 0;
            m_MaxAirPressure = i_MaxAirPressure;
            m_ManufactureName = i_Manufacture;
        }

        public static void IncreaseAirPressure(List<Tire> i_TireList, float i_NewPressure)
        {
            if (i_NewPressure + i_TireList[0].CurrentAirPressure > i_TireList[0].m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_TireList[0].m_MaxAirPressure - i_TireList[0].CurrentAirPressure);
            }
            else
            {
                foreach (Tire tire in i_TireList)
                {
                    tire.m_CurrentAirPressure += i_NewPressure;
                }
            }
        }

        public static List<Tire> MakeTireSet(int i_HowManyTires, float i_MaxTirePressure, string i_Manufacture)
        {
            List<Tire> tireSet = new List<Tire>();
            for(int i = 0; i < i_HowManyTires; i++)
            {
                tireSet.Add(new Tire(i_MaxTirePressure, i_Manufacture));
            }
            return tireSet;
        }

        public virtual StringBuilder ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Manufacture Name: " + m_ManufactureName);
            sb.AppendLine("MaxAirPressure: " + m_MaxAirPressure);
            return sb;
        }
    }
}