using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        Dictionary<string, GarageVeichle> m_VeichlesinGarage;

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
        public void InflateAirInWheelsOfChoosenVehicleToMaximum(string vehicleLicenseNumber)
        {
            throw new NotImplementedException();
        }

        // TODO - get a liscense number return 'nice' string with all the detail
        //(each value in separte line with the prefix <field name:> < field value>
        public string getFullVehicleDetailsByLicenseNumber(string vehicleLicenseNumber)
        {
            throw new NotImplementedException();
        }

        // TODO - search Vehicle by its veichleLicenseNumber - if found return true..
        public bool IsVehicleInThisGarage(string vehicleLicenseNumber)
        {
            throw new NotImplementedException();
        }

        /// TODO : 1. if GarageVeichle with licsence number not found torw null pointer exception (from find)
        /// if not an elctricity vechile trow ArgumentException from The sutible Energy function for filing fuel
        /// talk before implement!!!
        public void ChargeElectricityBasedVeichle(string vehicleLicenseNumber, float numberOfHoursToCharge)
        {
            throw new NotImplementedException();
        }

        //TODO get the vehicle and full the tank
        // if type of fuel isnt like the car fuel trow argument exception
        //if liters to fuel is more than we can trow range 
        // if cant parse to one of the fule option we have trow parse 
        //(pass all the trows) 
        public void FuelNumberOfLitersToFuelBasedVehicle(string vehicleLicenseNumber, string typeOfFuel, float litersOfFuelToFill)
        {
            throw new NotImplementedException();
        }

        //TODO get the Garge vehicle with  vehicleLicenseNumber
        //change its state to newInGargeStateOfVehicle try parse if fail trow parsing (not one of the states..) 
        public void ChangeInGargeStateOfVeichle(string vehicleLicenseNumber, string newInGargeStateOfVehicle)
        {
            throw new NotImplementedException();
        }


        //TODO if string null dont filter if not null try parse to state of a vechile in garge
        // if parse failed  - if not one of the states trow FormatException 
        //else filter by it:
        // 
        public string ShowLicenseNumbersOfVeichlesInGarageWithFilterByState(string inGargeStateOfVehicleToFilterWith)
        {
            throw new NotImplementedException();
        }
    }
}
