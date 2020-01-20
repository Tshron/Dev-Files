
using System.Text;
using Exceptions;

namespace Ex03.GarageLogic
{
    public abstract class VehicleEnergy
    {
        private float m_CurrentAmountOfEnergy;
        private float m_MaxAmountOfEnergy;

        public float MaxAmountOfEnergy
        {
            get
            {
                return m_MaxAmountOfEnergy;
            }
        }

        public VehicleEnergy(float i_MaxAmountOfEnergy)
        {
           
            m_CurrentAmountOfEnergy = 0;
            m_MaxAmountOfEnergy = i_MaxAmountOfEnergy;
        }

        public float AmountOfLeftEnergy()
        {
            return m_CurrentAmountOfEnergy;
        }
        public void AddEnergy(float i_HowMuchToAdd)
        {
            if(m_CurrentAmountOfEnergy + i_HowMuchToAdd > m_MaxAmountOfEnergy)
            {
                throw new ValueOutOfRangeException(0, m_MaxAmountOfEnergy);
            }
            else 
            {
                m_CurrentAmountOfEnergy += i_HowMuchToAdd;
            }
        }
        
            
        public virtual StringBuilder toString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Current amount of energy: " + m_CurrentAmountOfEnergy);
            return sb;
        }
    }
}
