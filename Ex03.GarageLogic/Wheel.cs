using System;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private string m_MakerName;
        private float m_CurrentAirPressureInWheel;
        private float m_MaxAirPressureInWheelAccordingToMaker;

        internal float CurrentAirPressureInWheel { get { return m_CurrentAirPressureInWheel; }}
        internal float MaxAirPressureInWheelAccordingToMaker { get { return m_MaxAirPressureInWheelAccordingToMaker; } }

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
