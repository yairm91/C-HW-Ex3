using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public const string k_LicenceNumberKey = "LicenceNumber";
        private const float k_MultiplyByHundredToTurnIntoPrecantage = 100;
        private const string k_HasDangerousCargoForTruck = "carries";
        private const string k_DoesNotHaveDangerousCargoForTruck = "does not carry";
        private readonly char[] k_SplitAccordingToThisDelimiter = { ',' };
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
            float amountOfEnergyLeftInBatteryInHours = energyType.CurrentAmountOfEnergy;
            i_EnergyInformation.AppendLine(string.Format("It uses an electric battery and has {0} hours left in it", amountOfEnergyLeftInBatteryInHours));
        }

        private static void getFuelInformation(GarageVehicle i_VehicleInGarageToWorkOn, StringBuilder i_EnergyInformation)
        {
            Fuel energyType = (Fuel)i_VehicleInGarageToWorkOn.OwnerVehicle.EnergyType;
            string fuelType = Enum.GetName(typeof(Fuel.eFuelType), energyType.FuelType);
            float amountLeftInTankInLiters = energyType.CurrentAmountOfEnergy;
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

        private static void insertDefaultFieldsToList(List<string> parametersForTheUserAccordingToVehicleType)
        {
            parametersForTheUserAccordingToVehicleType.Add(GarageVehicle.k_OwnerNameKey);
            parametersForTheUserAccordingToVehicleType.Add(GarageVehicle.k_OwnerPhoneNumberKey);
            parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_WheelMakerKey);
            parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_PercentOfEnergyLeftKey);
            parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_ModelNameKey);
            parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_CurrentPressureInWheelsKey);
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

            if (i_VehicleInGarageToWorkOn.OwnerVehicle is Car)
            {
                getCarInformation(i_VehicleInGarageToWorkOn, vehicleSpecificInformation);
            }
            else if (i_VehicleInGarageToWorkOn.OwnerVehicle is Bike)
            {
                getBikeInformation(i_VehicleInGarageToWorkOn, vehicleSpecificInformation);
            }
            else if(i_VehicleInGarageToWorkOn.OwnerVehicle is Truck)
            {
                getTruckInformation(i_VehicleInGarageToWorkOn, vehicleSpecificInformation);
            }

            return vehicleSpecificInformation.ToString();
        }

        private string getEnergyInformation(GarageVehicle i_VehicleInGarageToWorkOn)
        {
            StringBuilder energyInformation = new StringBuilder();

            if (i_VehicleInGarageToWorkOn.OwnerVehicle.EnergyType is Fuel)
            {
                getFuelInformation(i_VehicleInGarageToWorkOn, energyInformation);
            }
            else
            {
                getElectricInformation(i_VehicleInGarageToWorkOn, energyInformation);
            }

            return energyInformation.ToString();
        }

        public List<string> GetListOfPossibleVehicles()
        {
            List<string> listOfPossibleVehicles = new List<string>();

            listOfPossibleVehicles.AddRange(Enum.GetNames(typeof(VeichleFactory.ePossibleVehicleTypes)));

            return listOfPossibleVehicles;
        }

        public List<string> GetParametersDict(string newVehicleType)
        {
            List<string> parametersForTheUserAccordingToVehicleType = new List<string>();

            insertDefaultFieldsToList(parametersForTheUserAccordingToVehicleType);

            VeichleFactory.ePossibleVehicleTypes vehicleType = (VeichleFactory.ePossibleVehicleTypes)Enum.Parse(typeof(VeichleFactory.ePossibleVehicleTypes), newVehicleType);

            switch (vehicleType)
            {
                case VeichleFactory.ePossibleVehicleTypes.FuelBike:
                    parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_TypeOfLicenceKey);
                    parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_EngineVolumeKey);
                    break;
                case VeichleFactory.ePossibleVehicleTypes.ElectricBike:
                    goto case VeichleFactory.ePossibleVehicleTypes.FuelBike;
                case VeichleFactory.ePossibleVehicleTypes.FuelCar:
                    parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_CarColorKey);
                    parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_NumberOfDoorsInCarKey);
                    break;
                case VeichleFactory.ePossibleVehicleTypes.ElectricCar:
                    goto case VeichleFactory.ePossibleVehicleTypes.FuelCar;
                case VeichleFactory.ePossibleVehicleTypes.FuelTruck:
                    parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_MaxCargoWeightForTruckKey);
                    parametersForTheUserAccordingToVehicleType.Add(VeichleFactory.k_HasDangerousCargoKey);
                    break;
                default:
                    break;
            }

            return parametersForTheUserAccordingToVehicleType;
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
            try
            {
                Electric ownerBattery = (Electric)ownerVehicle.OwnerVehicle.EnergyType;

                ownerVehicle.OwnerVehicle.EnergyType.AddEnergy(parametersForEnergyCharging);
                ownerVehicle.OwnerVehicle.PercentOfEnergyLeft = calculateNewPrecanteOfEnergy(ownerBattery.MaxAmountOfEnergy, ownerBattery.CurrentAmountOfEnergy);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException("Wrong Energy Type!", ex);
            }
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
            ownerVehicle.OwnerVehicle.PercentOfEnergyLeft = calculateNewPrecanteOfEnergy(ownerFuelTank.MaxAmountOfEnergy, ownerFuelTank.CurrentAmountOfEnergy);
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
            catch (ArgumentException ex)
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

        public void InsertNewVehicle(Dictionary<string, string> i_NewVehicleData, string i_VehicleType)
        {
            Dictionary<string, object> parametersForFactory = new Dictionary<string, object>();
            foreach (string key in i_NewVehicleData.Keys)
            {
                validateAndInsertToParametersDict(parametersForFactory, i_NewVehicleData, key);
            }
      
            VeichleFactory.ePossibleVehicleTypes typeOfVehicleToCreate = (VeichleFactory.ePossibleVehicleTypes)Enum.Parse(typeof(VeichleFactory.ePossibleVehicleTypes), i_VehicleType);
            Vehicle ownerVehicle = VeichleFactory.CreateNewVeichle(typeOfVehicleToCreate, parametersForFactory);

            GarageVehicle newVehicleInGarage = new GarageVehicle(i_NewVehicleData[GarageVehicle.k_OwnerNameKey], i_NewVehicleData[GarageVehicle.k_OwnerPhoneNumberKey], ownerVehicle);

            m_VehiclesInGarage.Add(i_NewVehicleData[k_LicenceNumberKey], newVehicleInGarage);
        }

        private void validateAndInsertToParametersDict(Dictionary<string, object> parametersForFactory, Dictionary<string, string> parametersFromUser, string key)
        {
            if (key.Equals(VeichleFactory.k_CarColorKey))
            {
                try
                {
                    Car.ePossibleCarColors newValueToEnterToDict = (Car.ePossibleCarColors)Enum.Parse(typeof(Car.ePossibleCarColors), parametersFromUser[key]);
                    parametersForFactory.Add(key, newValueToEnterToDict);
                }
                catch (OverflowException)
                {
                    throw new FormatException("Wrong Car Color");
                }      
            }
            else if (key.Equals(VeichleFactory.k_CurrentEnergyLevelKey)
                || key.Equals(VeichleFactory.k_MaxCargoWeightForTruckKey) 
                || key.Equals(VeichleFactory.k_PercentOfEnergyLeftKey))
            {
                float newValueToEnterToDict;
                bool didParseWork = float.TryParse(parametersFromUser[key], out newValueToEnterToDict);
                parametersForFactory.Add(key, newValueToEnterToDict);
                if (!didParseWork)
                {
                    throw new ArgumentException(string.Format("Wrong {0} Type! Must be a Decimal Number!", key));
                }
            }
            else if (key.Equals(VeichleFactory.k_CurrentPressureInWheelsKey))
            {
                List<float> newValueToEnterToDict = new List<float>();
                string[] parametersFromUserDivided = parametersFromUser[key].Split(k_SplitAccordingToThisDelimiter);
                foreach (string airPressure in parametersFromUserDivided)
                {
                    float tempAirPressurePlaceHolder;
                    bool didParseWork = float.TryParse(airPressure, out tempAirPressurePlaceHolder);

                    if (!didParseWork)
                    {
                        throw new ArgumentException(string.Format("Wrong {0} Type! Must be a Decimal Number!", key));
                    }

                    newValueToEnterToDict.Add(tempAirPressurePlaceHolder);
                }

                parametersForFactory.Add(key, newValueToEnterToDict);
            }
            else if (key.Equals(VeichleFactory.k_EngineVolumeKey))
            {
                int newValueToEnterToDict;
                bool didParseWork = int.TryParse(parametersFromUser[key], out newValueToEnterToDict);
                parametersForFactory.Add(key, newValueToEnterToDict);
                if (!didParseWork)
                {
                    throw new ArgumentException(string.Format("Wrong {0} Type! Must be an Integer Number!", key));
                }
            }
            else if (key.Equals(VeichleFactory.k_HasDangerousCargoKey))
            {
                bool newValueToEnterToDict;
                bool didParseWork = bool.TryParse(parametersFromUser[key], out newValueToEnterToDict);
                parametersForFactory.Add(key, newValueToEnterToDict);
                if (!didParseWork)
                {
                    throw new ArgumentException(string.Format("Wrong {0} Type! Must be an Integer Number!", key));
                }
            }
            else if (key.Equals(VeichleFactory.k_NumberOfDoorsInCarKey))
            {
                try
                {
                    Car.eNumberOfDoors newValueToEnterToDict = (Car.eNumberOfDoors)Enum.Parse(typeof(Car.eNumberOfDoors), parametersFromUser[key]);
                    parametersForFactory.Add(key, newValueToEnterToDict);
                }
                catch (OverflowException)
                {
                    throw new FormatException("Wrong Number Of Doors");
                }
            }
            else if (key.Equals(VeichleFactory.k_TypeOfLicenceKey))
            {
                try
                {
                    Bike.eTypeOfLicence newValueToEnterToDict = (Bike.eTypeOfLicence)Enum.Parse(typeof(Bike.eTypeOfLicence), parametersFromUser[key]);
                    parametersForFactory.Add(key, newValueToEnterToDict);
                }
                catch (OverflowException)
                {
                    throw new FormatException("Wrong Licence Type");
                }
            }
            else if (key.Equals(VeichleFactory.k_WheelMakerKey))
            {
                List<string> newValueToEnterToDict = new List<string>();
                string[] parametersFromUserDivided = parametersFromUser[key].Split(k_SplitAccordingToThisDelimiter);
                newValueToEnterToDict.AddRange(parametersFromUserDivided);
                parametersForFactory.Add(key, newValueToEnterToDict);
            }
            else
            {
                string newValueToEnterToDict = parametersFromUser[key];
                parametersForFactory.Add(key, newValueToEnterToDict);
            }
        }
    }
}
