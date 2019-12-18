using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : VehicleEnergy
    {

        // $G$ DSN-999 (-4) The "fuel type" field should be readonly member of class FuelEnergyProvider.
        private eFuel m_FuelType;
        public eFuel FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public FuelEnergy(eFuel i_FuelType, float i_MaxAmountOfFuel) : base(i_MaxAmountOfFuel)
        {
            m_FuelType = i_FuelType;
        }        

        public override StringBuilder toString()
        {
            StringBuilder patternForString = new StringBuilder();
            patternForString = base.toString();
            patternForString.AppendFormat("{0}: {1}\n", "Fuel",m_FuelType);
            return patternForString;
        }
    }    
}