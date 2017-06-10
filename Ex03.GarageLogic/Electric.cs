using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Electric : Energy
    {
        internal const string k_EnergyType = "Electric";
        private float m_TimeLeftOnBatteryInHours;
        private float m_MaxTimeInBatteryInHours;

        public float TimeLeftOnBatteryInHours
        {
            get { return m_TimeLeftOnBatteryInHours; }
        }

        public float MaxTimeInBatteryInHours
        {
            get { return m_MaxTimeInBatteryInHours; }
        }

        public Electric(float i_TimeLeftOnBatteryInHours, float i_MaxTimeInBatteryInHours)
        {
            m_TimeLeftOnBatteryInHours = i_TimeLeftOnBatteryInHours;
            m_MaxTimeInBatteryInHours = i_MaxTimeInBatteryInHours;
        }

        internal override void AddEnergy(Dictionary<string, object> i_ValuesToAddEnergy)
        {
            float valueToAddToBattery = ValidateAndGetValueToAdd(i_ValuesToAddEnergy, k_EnergyType);
            float tempNewValueOfBattery = m_TimeLeftOnBatteryInHours + valueToAddToBattery;

            if (tempNewValueOfBattery > m_MaxTimeInBatteryInHours)
            {
                throw new ValueOutOfRangeException(0, m_MaxTimeInBatteryInHours - m_TimeLeftOnBatteryInHours);
            }
            else
            {
                m_TimeLeftOnBatteryInHours = tempNewValueOfBattery;
            }
        }       
    }
}
