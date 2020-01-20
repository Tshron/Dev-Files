using System.Text;

namespace Ex03.GarageLogic
{
    public class Customer
    {
        private string m_CustomerName;
        private string m_CustomerPhoneNumber;

        private Vehicle m_Vehicle;
        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        private eGarageOrderStatus m_Status;
        public eGarageOrderStatus CustomerStatus
        {
            get
            {
                return m_Status;
            }
        }

        public Customer(string i_CustomerName, string i_CustomerPhoneNumber, Vehicle i_Vehicle)
        {
            m_Vehicle = i_Vehicle;
            m_Status = eGarageOrderStatus.UnderRepair;
            m_CustomerName = i_CustomerName;
            m_CustomerPhoneNumber = i_CustomerPhoneNumber;
        }
        
        public void UpdateStatus(eGarageOrderStatus i_NewStatus)
        {
            m_Status = i_NewStatus;
        }

        public override string ToString()
        {
            StringBuilder customerString = new StringBuilder();
            customerString.AppendFormat("{0}: {1}\n", "Owner name is", m_CustomerName);
            customerString.AppendFormat("{0}: {1}\n", "CustomerPhoneNumber", m_CustomerPhoneNumber);
            customerString.Append(m_Vehicle.ToString());
            customerString.Append(m_Vehicle.Energy.toString());
            customerString.AppendFormat("{0}: {1}\n", "Number of tires", m_Vehicle.Tires.Count);
            customerString.Append(m_Vehicle.Tires[0].ToString());
            return customerString.ToString();
        }
    }
}
