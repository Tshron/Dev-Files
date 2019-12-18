using System.Collections.Generic;
namespace Ex03.GarageLogic
{
    public class Garage
    {
        private List<Customer> m_Customers;
        public List<Customer> Customers
        {
            get
            {
                return m_Customers;
            }
        }

        public Garage()
        {
            m_Customers = new List<Customer>();
        }

        public Customer GetCustomerByLicensePlate(string i_LicensePlate)
        {
            Customer customer = null;
            foreach(Customer c in m_Customers)
            {
                if(c.Vehicle.LicensePlate == i_LicensePlate)
                {
                    customer = c;
                }
            }

            return customer;
        }

        public void IncreaseAirPressure(Customer i_Customer, float i_NewPressure)
        {
            Tire.IncreaseAirPressure(i_Customer.Vehicle.Tires, i_NewPressure);
        }

        public void AddEnergy(Customer i_Customer, float i_AmountToAdd)
        {
            i_Customer.Vehicle.Energy.AddEnergy(i_AmountToAdd);
        }

        public Dictionary<string, eGarageOrderStatus> GetLicensePlateList()
        {
            Dictionary<string, eGarageOrderStatus> listOfAllPlates = new Dictionary<string, eGarageOrderStatus>();
            foreach (Customer customer in m_Customers)
            {
                listOfAllPlates.Add(customer.Vehicle.LicensePlate, customer.CustomerStatus);
            }

            return listOfAllPlates;
        }

        public bool IsLicensePlateExists(string i_LicensePlateToCheck)
        {
            return !(GetCustomerByLicensePlate(i_LicensePlateToCheck) is null);
        }
    }
}
