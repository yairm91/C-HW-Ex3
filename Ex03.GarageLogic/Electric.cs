using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Electric : Energy
    {
        internal const string k_EnergyType = "Electric";

        public Electric(float i_TimeLeftOnBatteryInHours, float i_MaxTimeInBatteryInHours) : base(i_MaxTimeInBatteryInHours, i_TimeLeftOnBatteryInHours)
        {
        }

        internal override void AddEnergy(Dictionary<string, object> i_ValuesToAddEnergy)
        {
            float valueToAddToBattery = ValidateAndGetValueToAdd(i_ValuesToAddEnergy, k_EnergyType);
            float tempNewValueOfBattery = m_CurrentAmountOfEnergy + valueToAddToBattery;

            if (tempNewValueOfBattery > m_MaxAmountOfEnergy)
            {
                throw new ValueOutOfRangeException(0, m_MaxAmountOfEnergy - m_CurrentAmountOfEnergy);
            }
            else
            {
                m_CurrentAmountOfEnergy = tempNewValueOfBattery;
            }
        }       
    }
}
