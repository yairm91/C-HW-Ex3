using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Fuel : Energy
    {
        private const string k_EnergyType = "Fuel";
        private const string k_FuelType = "FuelType";
        private eFuelType m_FuelType;
        private float m_AmountOfFuelInTankInLiters;
        private float m_MaxCapacityOfFuelTankInLiters;

        internal enum eFuelType
        {
            Soler, 
            Octan95,
            Octan96,
            Octan98
        }

        public Fuel(float i_AmountOfFuelInTankInLiters, float i_MaxCapacityOfFuelTankInLiters, eFuelType i_FuelType)
        {
            m_AmountOfFuelInTankInLiters = i_AmountOfFuelInTankInLiters;
            m_MaxCapacityOfFuelTankInLiters = i_MaxCapacityOfFuelTankInLiters;
            m_FuelType = i_FuelType;
        }

        internal override void AddEnergy(Dictionary<string, object> i_ValuesToAddEnergy)
        {
            float valueToAddToFuelTank = ValidateAndGetValueToAdd(i_ValuesToAddEnergy, k_EnergyType);

            object fuelTypeInObjectForm;
            bool didGetWork = i_ValuesToAddEnergy.TryGetValue(k_FuelType, out fuelTypeInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            if(fuelTypeInObjectForm.ToString() != m_FuelType.ToString())
            {
                throw new ArgumentException();
            }

            float tempAmountOfFuelInTank = m_AmountOfFuelInTankInLiters + valueToAddToFuelTank;

            if (tempAmountOfFuelInTank > m_MaxCapacityOfFuelTankInLiters)
            {
                throw new ArgumentException();
            }
            else
            {
                m_AmountOfFuelInTankInLiters = tempAmountOfFuelInTank;
            }
        }
    }
}
