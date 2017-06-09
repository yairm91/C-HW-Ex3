using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Veichle
    {
        private bool m_HasDangerousCargo;
        private float m_MaxCargoWeight;

        public bool HasDangerousCargo
        {
            get { return m_HasDangerousCargo; }
        }

        public float MaxCargoWeight
        {
            get { return m_MaxCargoWeight; }
        }

        public Truck(
            bool i_HasDangerousCargo,
            float i_MaxCargoWeight,
            string i_ModelName,
            string i_LicenceNumber,
            float i_PercentOfEnergyLeft,
            Energy i_EnergyType,
            List<Wheel> i_Wheels) :
            base(
                i_ModelName,
                i_LicenceNumber,
                i_PercentOfEnergyLeft,
                i_EnergyType,
                i_Wheels)
        {
            m_HasDangerousCargo = i_HasDangerousCargo;
            m_MaxCargoWeight = i_MaxCargoWeight;
        }
    }
}
