using System;
using System.Collections.Generic;


namespace Ex03.GarageLogic
{
    public class Garage
    {
        private const float k_MultiplyByHundredToTurnIntoPrecantage = 100;
        private const string k_HasDangerousCargoForTruck = "carries";
        private const string k_DoesNotHaveDangerousCargoForTruck = "does not carry";
        private Dictionary<string, GarageVehicle> m_VehiclesInGarage;

        public Garage()
        {
            m_VehiclesInGarage = new Dictionary<string, GarageVehicle>();
        }

        private static float calculateNewPrecanteOfEnergy(float i_MaxAmountOfEnergy, float i_CurrentAmountOfEnergy)
        {
            float newPrecantgeOfEnergy = (i_CurrentAmountOfEnergy / i_MaxAmountOfEnergy) * k_MultiplyByHundredToTurnIntoPrecantage;

            return newPrecantgeOfEnergy;
        }

        private static void getElectricInformation(GarageVehicle i_VehicleInGarageToWorkOn, StringBuilder i_EnergyInformation)
        {
            Electric energyType = (Electric)i_VehicleInGarageToWorkOn.OwnerVehicle.EnergyType;
            float amountOfEnergyLeftInBatteryInHours = energyType.TimeLeftOnBatteryInHours;
            i_EnergyInformation.AppendLine(string.Format("It uses an electric battery and has {0} hours left in it", amountOfEnergyLeftInBatteryInHours));
        }

        private static void getFuelInformation(GarageVehicle i_VehicleInGarageToWorkOn, StringBuilder i_EnergyInformation)
        {
            Fuel energyType = (Fuel)i_VehicleInGarageToWorkOn.OwnerVehicle.EnergyType;
            string fuelType = Enum.GetName(typeof(Fuel.eFuelType), energyType.FuelType);
            float amountLeftInTankInLiters = energyType.AmountOfFuelInTankInLiters;
            i_EnergyInformation.AppendLine(string.Format("It uses Fuel type {0} and has {1} liters left in the tank", fuelType, amountLeftInTankInLiters));
        }

        private static string getWheelsInformation(GarageVehicle i_VehicleInGarageToWorkOn)
        {
            StringBuilder wheelsInformation = new StringBuilder();

            for (int wheelNumber = 0; wheelNumber < i_VehicleInGarageToWorkOn.OwnerVehicle.Wheels.Count; wheelNumber++)
            {
                wheelsInformation.AppendLine(string.Format(
                    "Wheel Number {0} maker is: {1} and air pressure {2}.",
                    wheelNumber,
                    i_VehicleInGarageToWorkOn.OwnerVehicle.Wheels[wheelNumber].MakerName,
                    i_VehicleInGarageToWorkOn.OwnerVehicle.Wheels[wheelNumber].CurrentAirPressureInWheel));
            }

            return wheelsInformation.ToString();
        }

        private static void getTruckInformation(GarageVehicle i_VehicleInGarageToWorkOn, StringBuilder i_VehicleSpecificInformation)
        {
            Truck ownerTruck = (Truck)i_VehicleInGarageToWorkOn.OwnerVehicle;
            string dangerousCargo;
            if (ownerTruck.HasDangerousCargo)
            {
                dangerousCargo = k_HasDangerousCargoForTruck;
            }
            else
            {
                dangerousCargo = k_DoesNotHaveDangerousCargoForTruck;
            }

            float maxCargoWeight = ownerTruck.MaxCargoWeight;
            i_VehicleSpecificInformation.AppendLine(string.Format("This truck {0} dangerous cargo, and it can carry a maximum of {1}.", dangerousCargo, maxCargoWeight));
        }

        private static void getBikeInformation(GarageVehicle i_VehicleInGarageToWorkOn, StringBuilder i_VehicleSpecificInformation)
        {
            Bike ownerBike = (Bike)i_VehicleInGarageToWorkOn.OwnerVehicle;
            string licenceType = Enum.GetName(typeof(Bike.eTypeOfLicence), ownerBike.TypeOfLicence);
            int engineVolume = ownerBike.EngineVolume;
            i_VehicleSpecificInformation.AppendLine(string.Format("This Bike needs a {0} type licence and its engine volume is {1}", licenceType, engineVolume));
        }

        private static void getCarInformation(GarageVehicle i_VehicleInGarageToWorkOn, StringBuilder i_VehicleSpecificInformation)
        {
            Car ownerCar = (Car)i_VehicleInGarageToWorkOn.OwnerVehicle;
            string numberOfDoorsInCar = Enum.GetName(typeof(Car.eNumberOfDoors), ownerCar.NumberOfDoorsInCar);
            string carColor = Enum.GetName(typeof(Car.ePossibleCarColors), ownerCar.CarColor);
            i_VehicleSpecificInformation.AppendLine(string.Format("This car has {0} doors and is {1}.", numberOfDoorsInCar, carColor));
        }

        internal GarageVehicle GetGarageVehicleByLicenseNumber(string i_VeichleLicenseNumber)
        {
            GarageVehicle tempVehicleInGarage;
            bool didGetWork = m_VehiclesInGarage.TryGetValue(i_VeichleLicenseNumber, out tempVehicleInGarage);
            if (!didGetWork)
            {
                tempVehicleInGarage = null;
            }

            return tempVehicleInGarage;
        }

        public void InflateAirInWheelsOfChoosenVehicleToMaximum(string i_VehicleLicenseNumber)
        {
            GarageVehicle vehicleInGarageToWorkOn = GetGarageVehicleByLicenseNumber(i_VehicleLicenseNumber);
            vehicleInGarageToWorkOn.OwnerVehicle.inflateAirInWheelsToMaximum();
        }

        public string getFullVehicleDetailsByLicenseNumber(string i_VehicleLicenseNumber)
        {
            GarageVehicle vehicleInGarageToWorkOn = GetGarageVehicleByLicenseNumber(i_VehicleLicenseNumber);
            StringBuilder fullDetailsMessege = new StringBuilder();
            fullDetailsMessege.AppendLine(string.Format("The Model name of this vehicle is: {0}", vehicleInGarageToWorkOn.OwnerVehicle.ModelName));
            fullDetailsMessege.AppendLine(string.Format("The Owner's name is: {0}", vehicleInGarageToWorkOn.OwnerName));
            fullDetailsMessege.AppendLine(string.Format("The state of the vehicle in the gargae is: {0}", Enum.GetName(typeof(GarageVehicle.eVehicleState), vehicleInGarageToWorkOn.VehicleState)));
            fullDetailsMessege.AppendLine(string.Format("The State of the Wheels is: {0}{1}", Environment.NewLine, getWheelsInformation(vehicleInGarageToWorkOn)));
            fullDetailsMessege.AppendLine(getEnergyInformation(vehicleInGarageToWorkOn));
            fullDetailsMessege.AppendLine(getVehicleSpecficInformation(vehicleInGarageToWorkOn));
            
            return fullDetailsMessege.ToString();
        }

        private string getVehicleSpecficInformation(GarageVehicle i_VehicleInGarageToWorkOn)
        {
            StringBuilder vehicleSpecificInformation = new StringBuilder();

            if (i_VehicleInGarageToWorkOn.OwnerVehicle.GetType() == typeof(Car))
            {
                getCarInformation(i_VehicleInGarageToWorkOn, vehicleSpecificInformation);
            }
            else if (i_VehicleInGarageToWorkOn.OwnerVehicle.GetType() == typeof(Bike))
            {
                getBikeInformation(i_VehicleInGarageToWorkOn, vehicleSpecificInformation);
            }
            else if(i_VehicleInGarageToWorkOn.OwnerVehicle.GetType() == typeof(Truck))
            {
                getTruckInformation(i_VehicleInGarageToWorkOn, vehicleSpecificInformation);
            }

            return vehicleSpecificInformation.ToString();
        }

        private string getEnergyInformation(GarageVehicle i_VehicleInGarageToWorkOn)
        {
            StringBuilder energyInformation = new StringBuilder();

            if (i_VehicleInGarageToWorkOn.OwnerVehicle.EnergyType.GetType() == typeof(Fuel))
            {
                getFuelInformation(i_VehicleInGarageToWorkOn, energyInformation);
            }
            else
            {
                getElectricInformation(i_VehicleInGarageToWorkOn, energyInformation);
            }

            return energyInformation.ToString();
        }

        public bool IsVehicleInThisGarage(string i_VehicleLicenseNumber)
        {
            bool vechileInGarage = true;
            GarageVehicle ownerVehicle;
            bool didGetWork = m_VehiclesInGarage.TryGetValue(i_VehicleLicenseNumber, out ownerVehicle);
            if (!didGetWork)
            {
                vechileInGarage = !vechileInGarage;
            }

            return vechileInGarage;
        }

        public void ChargeElectricityBasedVehicle(string i_VehicleLicenseNumber, float i_NumberOfHoursToCharge)
        {
            GarageVehicle ownerVehicle = GetGarageVehicleByLicenseNumber(i_VehicleLicenseNumber);

            Dictionary<string, object> parametersForEnergyCharging = new Dictionary<string, object>();
            parametersForEnergyCharging.Add(Energy.k_ValueType, Electric.k_EnergyType);
            parametersForEnergyCharging.Add(Energy.k_ValueToAdd, i_NumberOfHoursToCharge);

            Electric ownerBattery = (Electric)ownerVehicle.OwnerVehicle.EnergyType;

            ownerVehicle.OwnerVehicle.EnergyType.AddEnergy(parametersForEnergyCharging);
            ownerVehicle.OwnerVehicle.PercentOfEnergyLeft = calculateNewPrecanteOfEnergy(ownerBattery.MaxTimeInBatteryInHours, ownerBattery.TimeLeftOnBatteryInHours);
        }

        public void FuelNumberOfLitersToFuelBasedVehicle(string i_VehicleLicenseNumber, string i_TypeOfFuel, float i_LitersOfFuelToFill)
        {
            GarageVehicle ownerVehicle = GetGarageVehicleByLicenseNumber(i_VehicleLicenseNumber);

            try
            {
                Fuel.eFuelType fuelType = (Fuel.eFuelType)Enum.Parse(typeof(Fuel.eFuelType), i_TypeOfFuel);
            }
            catch (OverflowException ex)
            {
                throw new FormatException("Wrong fuel type", ex.InnerException);
            }

            Dictionary<string, object> parametersForEnergyCharging = new Dictionary<string, object>();
            parametersForEnergyCharging.Add(Energy.k_ValueType, Fuel.k_EnergyType);
            parametersForEnergyCharging.Add(Energy.k_ValueToAdd, i_LitersOfFuelToFill);
            parametersForEnergyCharging.Add(Fuel.k_FuelType, i_TypeOfFuel);

            Fuel ownerFuelTank = (Fuel)ownerVehicle.OwnerVehicle.EnergyType;

            ownerVehicle.OwnerVehicle.EnergyType.AddEnergy(parametersForEnergyCharging);
            ownerVehicle.OwnerVehicle.PercentOfEnergyLeft = calculateNewPrecanteOfEnergy(ownerFuelTank.MaxCapacityOfFuelTankInLiters, ownerFuelTank.AmountOfFuelInTankInLiters);
        }

        public void ChangeInGargeStateOfVehicle(string i_VehicleLicenseNumber, string i_NewInGargeStateOfVehicle)
        {
            GarageVehicle ownerVehicle = GetGarageVehicleByLicenseNumber(i_VehicleLicenseNumber);
            try
            {
                GarageVehicle.eVehicleState newVehicleState = (GarageVehicle.eVehicleState)Enum.Parse(typeof(GarageVehicle.eVehicleState), i_NewInGargeStateOfVehicle);
                ownerVehicle.VehicleState = newVehicleState;
            }
            catch (OverflowException ex)
            {
                throw new FormatException("Wrong vehicle state", ex.InnerException);
            }            
        }

        public string ShowLicenseNumbersOfVehiclesInGarageWithFilterByState(string i_InGargeStateOfVehicleToFilterWith)
        {
            StringBuilder listOfVehiclesInGarage = new StringBuilder();

            if (i_InGargeStateOfVehicleToFilterWith == null)
            {
                showAllVehiclesInGarage(listOfVehiclesInGarage);
            }
            else
            {
                showVehiclesAccordingToState(i_InGargeStateOfVehicleToFilterWith, listOfVehiclesInGarage);
            }

            return listOfVehiclesInGarage.ToString();
        }

        private void showVehiclesAccordingToState(string i_InGargeStateOfVehicleToFilterWith, StringBuilder i_ListOfVehiclesInGarage)
        {
            try
            {
                GarageVehicle.eVehicleState filterVehicleState = (GarageVehicle.eVehicleState)Enum.Parse(typeof(GarageVehicle.eVehicleState), i_InGargeStateOfVehicleToFilterWith);
                foreach (string key in m_VehiclesInGarage.Keys)
                {
                    GarageVehicle currentVehicle = m_VehiclesInGarage[key];
                    if (currentVehicle.VehicleState == filterVehicleState)
                    {
                        i_ListOfVehiclesInGarage.AppendLine(currentVehicle.OwnerVehicle.LicenceNumber);
                    }
                }
            }
            catch (OverflowException ex)
            {
                throw new FormatException("Wrong vehicle state", ex.InnerException);
            }  
        }

        private void showAllVehiclesInGarage(StringBuilder i_ListOfVehiclesInGarage)
        {
            foreach (string key in m_VehiclesInGarage.Keys)
            {
                i_ListOfVehiclesInGarage.AppendLine(m_VehiclesInGarage[key].OwnerVehicle.LicenceNumber);
            }
        }


        //TODO
        public void InsertNewVehicle(Dictionary<string, object> newVehicleData)
        {
            throw new NotImplementedException();
        }
    }
}
