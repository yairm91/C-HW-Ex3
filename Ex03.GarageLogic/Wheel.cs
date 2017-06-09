using System;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private string m_MakerName;
        private float m_CurrentAirPressureInWheel;
        private float m_MaxAirPressureInWheelAccordingToMaker;

        internal float CurrentAirPressureInWheel
        {
            get { return m_CurrentAirPressureInWheel; }
        }

        internal float MaxAirPressureInWheelAccordingToMaker
        {
            get { return m_MaxAirPressureInWheelAccordingToMaker; }
        }

        public Wheel(string i_MakerName, float i_CurrentAirPressureInWheel, float i_MaxAirPressureInWheelAccordingToMaker)
        {
            m_MakerName = i_MakerName;
            m_CurrentAirPressureInWheel = i_CurrentAirPressureInWheel;
            m_MaxAirPressureInWheelAccordingToMaker = i_MaxAirPressureInWheelAccordingToMaker;
        }

        internal void AddAirPressure(float i_AmountOfPressureToAdd)
        {
            float tempCurrentAirPressureInWheel = m_CurrentAirPressureInWheel + i_AmountOfPressureToAdd;
            if (tempCurrentAirPressureInWheel > m_MaxAirPressureInWheelAccordingToMaker)
            {
                throw new ArgumentException();
            }
            else
            {
                m_CurrentAirPressureInWheel = tempCurrentAirPressureInWheel;
            }
        }
    }
}
