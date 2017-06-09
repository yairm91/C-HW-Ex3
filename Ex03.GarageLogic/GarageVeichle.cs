using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class GarageVeichle
    {
         private eVeichleState m_VeichleState;

        internal eVeichleState VeichleState
        {
            get { return m_VeichleState; }
            set { m_VeichleState = value; }
        }

        //TODO - define the enum
        internal enum eVeichleState
        {
         
        }
    }
}
