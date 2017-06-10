using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Fuel : Energy
    {
        internal const string k_EnergyType = "Fuel";
        internal const string k_FuelType = "FuelType";
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

        public float AmountOfFuelInTankInLiters
        {
            get { return m_AmountOfFuelInTankInLiters; }
        }

        public float MaxCapacityOfFuelTankInLiters
        {
            get { return m_MaxCapacityOfFuelTankInLiters; }
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
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
                throw new FormatException("Error while parsing the value");
            }

            if(fuelTypeInObjectForm.ToString() != Enum.GetName(typeof(eFuelType), m_FuelType))
            {
                throw new ArgumentException("Wrong Energy Type, please provide fuel");
            }

            float tempAmountOfFuelInTank = m_AmountOfFuelInTankInLiters + valueToAddToFuelTank;

            if (tempAmountOfFuelInTank > m_MaxCapacityOfFuelTankInLiters)
            {
                throw new ValueOutOfRangeException(0, m_MaxCapacityOfFuelTankInLiters - m_AmountOfFuelInTankInLiters);
            }
            else
            {
                m_AmountOfFuelInTankInLiters = tempAmountOfFuelInTank;
            }
        }
    }
}
