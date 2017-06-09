using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    //TODO Fix vehicle typo in all project
    public abstract class Veichle

    {
        string m_ModelName;
        string m_LicenceNumber;
        float m_PercentOfEnergyLeft;
        Energy m_EnergyType;
        List<Wheel> m_Wheels;

        internal Veichle(string i_ModelName, string i_LicenceNumber, float i_PercentOfEnergyLeft, Energy i_EnergyType, List<Wheel> i_Wheels)
        {
            m_EnergyType = i_EnergyType;
            m_LicenceNumber = i_LicenceNumber;
            m_ModelName = i_ModelName;
            m_PercentOfEnergyLeft = i_PercentOfEnergyLeft;
            m_Wheels.AddRange(i_Wheels);
        }

        // TODO - full the air in all wheels to max capacity
        public void inflateAirInWheelsToMaximum()
        {
            throw new NotImplementedException();
        }
    }
}
