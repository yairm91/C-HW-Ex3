using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Electric : Energy
    {
        private float m_TimeLeftOnBatteryInHours;
        private float m_MaxTimeInBatteryInHours;
        private const string k_EnergyType = "Electric";
       

        internal override void AddEnergy(Dictionary<string, object> i_ValuesToAddEnergy)
        {
            float valueToAddToBattery = ValidateAndGetValueToAdd(i_ValuesToAddEnergy, k_EnergyType);
            float tempNewValueOfBattery = m_TimeLeftOnBatteryInHours + valueToAddToBattery;

            if (tempNewValueOfBattery > m_MaxTimeInBatteryInHours)
            {
                throw new ArgumentException();
            }
            else
            {
                m_TimeLeftOnBatteryInHours = tempNewValueOfBattery;
            }
        }

        
    }
}
