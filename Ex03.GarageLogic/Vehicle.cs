using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenceNumber;
        private float m_PercentOfEnergyLeft;
        private Energy m_EnergyType;
        private List<Wheel> m_Wheels;

        internal float PercentOfEnergyLeft
        {
            get
            {
                return m_PercentOfEnergyLeft;
            }

            set
            {
                m_PercentOfEnergyLeft = value;
            }
        }

        internal string ModelName
        {
            get { return m_ModelName; }
        }

        internal string LicenceNumber
        {
            get { return m_LicenceNumber; }
        }

        internal Energy EnergyType
        {
            get { return m_EnergyType; }
        }

        internal List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        internal Vehicle(string i_ModelName, string i_LicenceNumber, float i_PercentOfEnergyLeft, Energy i_EnergyType, List<Wheel> i_Wheels)
        {
            m_EnergyType = i_EnergyType;
            m_LicenceNumber = i_LicenceNumber;
            m_ModelName = i_ModelName;
            m_PercentOfEnergyLeft = i_PercentOfEnergyLeft;
            m_Wheels = new List<Wheel>();
            m_Wheels.AddRange(i_Wheels);
        }

        public void inflateAirInWheelsToMaximum()
        {
            foreach (Wheel currentWheel in m_Wheels)
            {
                currentWheel.AddAirPressure(currentWheel.MaxAirPressureInWheelAccordingToMaker - currentWheel.CurrentAirPressureInWheel);
            }
        }
    }
}
