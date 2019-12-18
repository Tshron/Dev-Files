using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : VehicleEnergy
    {
        public ElectricEnergy(float i_MaxBatteryCapability) : base(i_MaxBatteryCapability)
        {
        }

        public override StringBuilder toString()
        {
            StringBuilder sb = new StringBuilder();
            sb = base.toString();
            sb.AppendFormat("{0}: {1}\n", "Engine Type", "Electronic");
            return sb;
        }
    }
}
