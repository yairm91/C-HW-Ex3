using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Energy
    {
        internal const string k_ValueType = "EnergyType";
        internal const string k_ValueToAdd = "Value";
        protected float m_MaxAmountOfEnergy;
        protected float m_CurrentAmountOfEnergy;

        public Energy(float i_MaxAmountOfEnergy, float i_CurrentAmountOfEnergy)
        {
            m_CurrentAmountOfEnergy = i_CurrentAmountOfEnergy;
            m_MaxAmountOfEnergy = i_MaxAmountOfEnergy;
        }

        public float CurrentAmountOfEnergy
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }
        }

        public float MaxAmountOfEnergy
        {
            get
            {
                return m_MaxAmountOfEnergy;
            }
        }

        internal abstract void AddEnergy(Dictionary<string, object> i_ValuesToAddEnergy);

        protected static float ValidateAndGetValueToAdd(Dictionary<string, object> i_ValuesToAddEnergy, string i_EnergyType)
        {
            object valueType;
            bool didGetWork = i_ValuesToAddEnergy.TryGetValue(k_ValueType, out valueType);
            if (!didGetWork)
            {
                throw new FormatException("Error while parsing the value");
            }

            if (valueType.ToString() != i_EnergyType)
            {
                throw new ArgumentException(string.Format("Wrong energy type, please provide {0}", i_EnergyType));
            }

            object valueToAddObjectForm;
            didGetWork = i_ValuesToAddEnergy.TryGetValue(k_ValueToAdd, out valueToAddObjectForm);
            if (!didGetWork)
            {
                throw new FormatException("Error while parsing the value");
            }

            float valueToAddFloatForm = (float)valueToAddObjectForm;

            return valueToAddFloatForm;
        }
    }
}
