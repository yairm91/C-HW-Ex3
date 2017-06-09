﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Bike : Veichle
    {    
        private eTypeOfLicence m_TypeOfLicence;
        private int m_TankVolume;

        public int TankVolume
        {
            get { return m_TankVolume; }
        }

        public eTypeOfLicence TypeOfLicence
        {
            get { return m_TypeOfLicence; }
        }

        internal Bike(
            eTypeOfLicence i_TypeOfLicence, 
            int i_TankVolume, 
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
            m_TankVolume = i_TankVolume;
            m_TypeOfLicence = i_TypeOfLicence;
        }

        internal enum eTypeOfLicence
        {
            A,
            AB, 
            A2,
            B1
        }
    }
}
