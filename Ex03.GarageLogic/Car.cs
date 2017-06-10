using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private ePossibleCarColors m_CarColor;
        private eNumberOfDoors m_NumberOfDoorsInCar;

        public ePossibleCarColors CarColor
        {
            get { return m_CarColor; }
        }

        public eNumberOfDoors NumberOfDoorsInCar
        {
            get { return m_NumberOfDoorsInCar; }
        }

        public Car(
            ePossibleCarColors i_CarColor,
            eNumberOfDoors i_NumberOfDoorsInCar,
            string i_ModelName,
            string i_LicenceNumber,
            float i_PercentOfEnergyLeft,
            Energy i_EnergyType,
            List<Wheel> i_Wheels) :
            base(
                i_ModelName,
                i_LicenceNumber,
                i_PercentOfEnergyLeft,
                i_EnergyType,
                i_Wheels)
        {
            m_CarColor = i_CarColor;
            m_NumberOfDoorsInCar = i_NumberOfDoorsInCar;
        }

        public enum ePossibleCarColors
        {
            Yellow,
            White,
            Black,
            Blue
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }
    }
}
