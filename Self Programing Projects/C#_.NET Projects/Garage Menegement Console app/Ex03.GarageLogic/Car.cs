using System.Text;

namespace Ex03.GarageLogic
{

    // $G$ DSN-001 (-10) There is no appropriate use of polymorphism. - this why so many parameters in the constructor
    internal abstract class Car : Vehicle
    {
        private const int k_HowManyTires = 4;
        private const float k_MaxTirePressure = 31;

        private eColor m_Color;
        public eColor CarColor
        {
            get
            {
                return m_Color;
            }
        }
        private eCarDoors m_NumberOfDoors;
        public eCarDoors NumberOfCarDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
        }

        public Car(string i_ModelName, string i_LicensePlate, eColor i_Color, eCarDoors i_NumberOfDoors, VehicleEnergy i_VehicleEnergy, string i_Manufacture)
            : base(i_ModelName, i_LicensePlate, i_VehicleEnergy, k_HowManyTires, k_MaxTirePressure, i_Manufacture)
        {
            m_Color = i_Color;
            m_NumberOfDoors = i_NumberOfDoors;
        }        

        public override StringBuilder ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb = base.ToString();
            sb.AppendFormat("{0}: {1}\n", "Vehicle Type", "Car");
            sb.AppendFormat("{0}: {1}\n", "Car Color", m_Color);
            sb.AppendFormat("{0}: {1}\n", "Number of doors", m_NumberOfDoors);
            return sb;
        }
    }
}
