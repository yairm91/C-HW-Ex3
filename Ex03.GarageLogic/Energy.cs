﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Energy
    {
        private const string k_ValueType = "EnergyType";
        private const string k_ValueToAdd = "Value";

        internal abstract void AddEnergy(Dictionary<string, object> i_ValuesToAddEnergy);

        protected static float ValidateAndGetValueToAdd(Dictionary<string, object> i_ValuesToAddEnergy, string i_EnergyType)
        {
            object valueType;
            bool didGetWork = i_ValuesToAddEnergy.TryGetValue(k_ValueType, out valueType);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            if (valueType.ToString() != i_EnergyType)
            {
                throw new ArgumentException();
            }

            object valueToAddObjectForm;
            didGetWork = i_ValuesToAddEnergy.TryGetValue(k_ValueToAdd, out valueToAddObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            float valueToAddFloatForm = (float)valueToAddObjectForm;

            return valueToAddFloatForm;
        }
    }
}
