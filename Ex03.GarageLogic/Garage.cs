using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        List<GarageVeichle> m_VeichlesinGarage;

        // TODO implement the Garage empty constructor
        public Garage()
        {

        }

        // TODO - get GarageVeichle in m_VeichlesinGarage by  LicenseNumber if not found return null
        internal GarageVeichle GetGarageVeichleByLicenseNumber(string veichleLicenseNumber)
        {
            throw new NotImplementedException();
        }

        // TODO - if GarageVeichle with licsence number not found torw null pointer exception  
        //find by licsence number the GarageVeichle and call to function in GarageVeichle to get Vehicle and 
        //call a function from Vehicle to inflate wheels and send the vechile capacity of dalak tank - current fual 
        //- Vehicle trow exception if  air not in range - but it should be in range
        public static bool InflateAirInWheelsOfChoosenVehicleToMaximum(string veichleLicenseNumber)
        {
            throw new NotImplementedException();
        }
    }
}
