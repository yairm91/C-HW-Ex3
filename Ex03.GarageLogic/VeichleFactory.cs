using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal static class VeichleFactory
    {
        private const bool v_UsesFuel = true;

        private const float k_MaxBatterySizeInTruck = 0;
        private const float k_MaxTankSizeInTruck = 135;
        private const Fuel.eFuelType k_TruckFuelType = Fuel.eFuelType.Octan96;
        private const int k_NumberOfWheelsInTruck = 12;
        private const float k_MaxAirPressureInTruckWheel = 32;

        private const int k_NumberOfWheelsInCar = 4;
        private const float k_MaxAirPressureInCarWheel = 30;
        private const float k_MaxTankSizeInCar = 42;
        private const Fuel.eFuelType k_CarFuelType = Fuel.eFuelType.Octan98;
        private const float k_MaxBatterySizeInCar = (float) 2.5;

        private const int k_NumberOfWheelsInBike = 2;
        private const float k_MaxAirPressureInBikeWheel = 33;
        private const float k_MaxTankSizeInBike = (float) 5.5;
        private const Fuel.eFuelType k_BikeFuelType = Fuel.eFuelType.Octan95;
        private const float k_MaxBatterySizeInBike = (float)2.7;

        private const float k_MaxPercentPossible = 100;
        private const float k_MinPercentPossible = 0;

        internal const string k_CurrentEnergyLevelKey = "Current Energy Level";
        internal const string k_CurrentPressureInWheelsKey = "Current Pressure In Wheels divided by , like '25,21,25,19'";
        internal const string k_HasDangerousCargoKey = "Has Dangerous Cargo";
        internal const string k_WheelMakerKey = "The Wheel Makers divided by , like 'audi,seat,ford,audi'";
        internal const string k_MaxCargoWeightForTruckKey = "Max Cargo Weight";
        internal const string k_ModelNameKey = "Model Name";
        internal const string k_PercentOfEnergyLeftKey = "Percent Of Energy Left";
        internal const string k_NumberOfDoorsInCarKey = "Number Of Doors In Car";
        internal const string k_CarColorKey = "Car Color";
        internal const string k_TypeOfLicenceKey = "Type Of Licence";
        internal const string k_EngineVolumeKey = "Engine Volume";

        internal static Vehicle CreateNewVeichle(ePossibleVehicleTypes veichleType, Dictionary<string, object> i_VeichleCharaterstic)
        {
            Vehicle newVeichle;
            switch (veichleType)
            {
                case ePossibleVehicleTypes.FuelBike:
                    newVeichle = createNewBike(i_VeichleCharaterstic, v_UsesFuel);
                    break;
                case ePossibleVehicleTypes.ElectricBike:
                    newVeichle = createNewBike(i_VeichleCharaterstic, !v_UsesFuel);
                    break;
                case ePossibleVehicleTypes.FuelCar:
                    newVeichle = createNewCar(i_VeichleCharaterstic, v_UsesFuel);
                    break;
                case ePossibleVehicleTypes.ElectricCar:
                    newVeichle = createNewCar(i_VeichleCharaterstic, !v_UsesFuel);
                    break;
                case ePossibleVehicleTypes.FuelTruck:
                    newVeichle = createNewTruck(i_VeichleCharaterstic, v_UsesFuel);
                    break;
                default:
                    throw new ArgumentException("No Such Veichle Type");
            }

            return newVeichle;
        }

        private static Vehicle createNewBike(Dictionary<string, object> i_VeichleCharaterstic, bool i_UsesFuel)
        {
            Bike.eTypeOfLicence typeOfLicence;
            int engineVolume;
            float percentOfEnergyLeft;
            string modelName, licenceNumber;

            getInputParamtersForBike(i_VeichleCharaterstic, out typeOfLicence, out engineVolume, out modelName, out licenceNumber, out percentOfEnergyLeft);

            Energy energyType;
            if (i_UsesFuel)
            {
                energyType = createEnergySource(percentOfEnergyLeft, k_MaxTankSizeInBike, k_BikeFuelType);
            }
            else
            {
                energyType = createEnergySource(percentOfEnergyLeft, k_MaxBatterySizeInBike);
            }

            List<Wheel> wheels = createWheelsList(i_VeichleCharaterstic, k_NumberOfWheelsInBike, k_MaxAirPressureInBikeWheel);

            return new Bike(typeOfLicence, engineVolume, modelName, licenceNumber, percentOfEnergyLeft, energyType, wheels);
        }

        private static void getInputParamtersForBike(
            Dictionary<string, object> i_VeichleCharaterstic, 
            out Bike.eTypeOfLicence o_TypeOfLicence, 
            out int o_EngineVolume,
            out string o_ModelName, 
            out string o_LicenceNumber,
            out float o_PercentOfEnergyLeft)
        {
            o_TypeOfLicence = getTypeOfLicenceValueFromDict(i_VeichleCharaterstic, k_TypeOfLicenceKey);
            o_EngineVolume = getIntFromDict(i_VeichleCharaterstic, k_EngineVolumeKey);
            if (o_EngineVolume < 0)
            {
                throw new ArgumentException("Engine Volume cannot be negative");
            }

            getVehicleParameters(i_VeichleCharaterstic, out o_ModelName, out o_LicenceNumber, out o_PercentOfEnergyLeft);
        }

        private static void getVehicleParameters(Dictionary<string, object> i_VeichleCharaterstic, out string o_ModelName, out string o_LicenceNumber, out float o_PercentOfEnergyLeft)
        {
            o_ModelName = getStringFromDict(i_VeichleCharaterstic, k_ModelNameKey);
            o_LicenceNumber = getStringFromDict(i_VeichleCharaterstic, Garage.k_LicenceNumberKey);
            o_PercentOfEnergyLeft = getFloatFromDict(i_VeichleCharaterstic, k_PercentOfEnergyLeftKey);
            if (o_PercentOfEnergyLeft > k_MaxPercentPossible || o_PercentOfEnergyLeft < k_MinPercentPossible)
            {
                throw new ArgumentException("You enterd wrong percent of energy left");
            }
        }

        private static int getIntFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object numberInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out numberInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            int numberInIntForm = (int)numberInObjectForm;

            return numberInIntForm;
        }

        private static Bike.eTypeOfLicence getTypeOfLicenceValueFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object typeOfLicenceInObjectForm;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out typeOfLicenceInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            Bike.eTypeOfLicence typeOfLicence = (Bike.eTypeOfLicence) typeOfLicenceInObjectForm;

            return typeOfLicence;
        }

        private static Vehicle createNewCar(Dictionary<string, object> i_VehicleCharaterstic, bool i_UsesFuel)
        {
            Car.eNumberOfDoors numberOfDoors;
            Car.ePossibleCarColors carColor;
            float percentOfEnergyLeft;
            string modelName, licenceNumber;

            getInputParamtersForCar(i_VehicleCharaterstic, out numberOfDoors, out carColor, out modelName, out licenceNumber, out percentOfEnergyLeft);

            Energy energyType;
            if (i_UsesFuel)
            {
                energyType = createEnergySource(percentOfEnergyLeft, k_MaxTankSizeInCar, k_CarFuelType);
            }
            else
            {
                energyType = createEnergySource(percentOfEnergyLeft, k_MaxBatterySizeInCar);
            }

            List<Wheel> wheels = createWheelsList(i_VehicleCharaterstic, k_NumberOfWheelsInCar, k_MaxAirPressureInCarWheel);

            return new Car(carColor, numberOfDoors, modelName, licenceNumber, percentOfEnergyLeft, energyType, wheels);
        }

        private static void getInputParamtersForCar(
            Dictionary<string, object> i_VehicleCharaterstic, 
            out Car.eNumberOfDoors o_NumberOfDoors, 
            out Car.ePossibleCarColors o_CarColor, 
            out string o_ModelName, 
            out string o_LicenceNumber, 
            out float o_PercentOfEnergyLeft)
        {
            o_NumberOfDoors = getNumberOfDoorsValueFromDict(i_VehicleCharaterstic, k_NumberOfDoorsInCarKey);
            o_CarColor = getCarColorValueFromDict(i_VehicleCharaterstic, k_CarColorKey);
            getVehicleParameters(i_VehicleCharaterstic, out o_ModelName, out o_LicenceNumber, out o_PercentOfEnergyLeft);
        }

        private static Car.ePossibleCarColors getCarColorValueFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object possibleCarColorInObjectForm;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out possibleCarColorInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            Car.ePossibleCarColors carColor = (Car.ePossibleCarColors) possibleCarColorInObjectForm;

            return carColor;
        }

        private static Car.eNumberOfDoors getNumberOfDoorsValueFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object numberOfDoorsInObjectForm;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out numberOfDoorsInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            Car.eNumberOfDoors numberOfDoors = (Car.eNumberOfDoors)numberOfDoorsInObjectForm;

            return numberOfDoors;
        }

        private static Vehicle createNewTruck(Dictionary<string, object> i_VehicleCharaterstic, bool i_UsesFuel)
        {
            bool hasDangerousCargo;
            float maxCargoWeight, percentOfEnergyLeft;
            string modelName, licenceNumber;
            getInputParamtersForTruck(i_VehicleCharaterstic, out hasDangerousCargo, out maxCargoWeight, out modelName, out licenceNumber, out percentOfEnergyLeft);

            Energy energyType;
            if (i_UsesFuel)
            {
                energyType = createEnergySource(percentOfEnergyLeft, k_MaxTankSizeInTruck, k_TruckFuelType);
            }
            else
            {
                energyType = createEnergySource(percentOfEnergyLeft, k_MaxBatterySizeInTruck);
            }

            List<Wheel> wheels = createWheelsList(i_VehicleCharaterstic, k_NumberOfWheelsInTruck, k_MaxAirPressureInTruckWheel);

            return new Truck(hasDangerousCargo, maxCargoWeight, modelName, licenceNumber, percentOfEnergyLeft, energyType, wheels);
        }

        private static List<Wheel> createWheelsList(Dictionary<string, object> i_VehicleCharaterstic, int i_numberOfWheels, float i_MaxAirPressureInWheel)
        {
            List<float> currentAirPressureInWheels = getFloatListFromDict(i_VehicleCharaterstic, k_CurrentPressureInWheelsKey, i_MaxAirPressureInWheel);
            if(currentAirPressureInWheels.Count != i_numberOfWheels)
            {
                throw new ArgumentException(string.Format("Wrong Amount of Wheels, this vehicle needs {0} wheels.", i_numberOfWheels));
            }

            List<string> wheelMakerName = getStringListFromDict(i_VehicleCharaterstic, k_WheelMakerKey);
            List<Wheel> wheels = createWheels(wheelMakerName, i_numberOfWheels, i_MaxAirPressureInWheel, currentAirPressureInWheels);
            return wheels;
        }

        private static List<string> getStringListFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfListInObjectForm;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out valueOfListInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            List<string> listInListForm = (List<string>)valueOfListInObjectForm;

            return listInListForm;
        }

        private static Energy createEnergySource(float i_CurrentPercentOfElectricity, float i_MaxBatterySize)
        {
            float currentEnergyLevel = i_CurrentPercentOfElectricity * 1 / 100 * i_MaxBatterySize;
            if (currentEnergyLevel > i_MaxBatterySize)
            {
                throw new ArgumentException("You entered more electricity than the maximum.");
            }

            Energy energyType = createEnergyByType(currentEnergyLevel, i_MaxBatterySize);
            return energyType;
        }

        private static Energy createEnergySource(float i_CurrentPercentOfElectricity, float i_MaxTankSize, Fuel.eFuelType i_FuelType)
        {
            float currentEnergyLevel = i_CurrentPercentOfElectricity * 1 / 100 * i_MaxTankSize;
            if (currentEnergyLevel > i_MaxTankSize)
            {
                throw new ArgumentException("You entered more fuel than the maximum.");
            }

            Energy energyType = createEnergyByType(currentEnergyLevel, i_MaxTankSize, i_FuelType);
            return energyType;
        }

        private static void getInputParamtersForTruck(
            Dictionary<string, object> i_VehicleCharaterstic, 
            out bool o_HasDangerousCargo, 
            out float o_MaxCargoWeight, 
            out string o_ModelName, 
            out string o_LicenceNumber,
            out float o_PercentOfEnergyLeft)
        {
            o_HasDangerousCargo = getBoolValueFromDict(i_VehicleCharaterstic, k_HasDangerousCargoKey);
            o_MaxCargoWeight = getFloatFromDict(i_VehicleCharaterstic, k_MaxCargoWeightForTruckKey);
            getVehicleParameters(i_VehicleCharaterstic, out o_ModelName, out o_LicenceNumber, out o_PercentOfEnergyLeft);
        }

        private static string getStringFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfStringInObjectForm;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out valueOfStringInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            string stringInStringForm = valueOfStringInObjectForm.ToString();

            return stringInStringForm;
        }

        private static List<float> getFloatListFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key, float i_MaxAirPressureInWheel)
        {
            bool didGetWork;
            object valueOfListInObjectForm;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out valueOfListInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            List<float> listInListForm = (List<float>) valueOfListInObjectForm;

            foreach (float airPressureInWheel in listInListForm)
            {
                if (airPressureInWheel > i_MaxAirPressureInWheel)
                {
                    throw new ArgumentException("You entered more air pressure than the max!");
                }

                if(i_MaxAirPressureInWheel < 0)
                {
                    throw new ArgumentException("Air Pressure cannot be negative");
                }
            }

            return listInListForm;
        }

        private static float getFloatFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfNumberInObjectFormat;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out valueOfNumberInObjectFormat);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            float numberInFloatFormat = (float) valueOfNumberInObjectFormat;

            return numberInFloatFormat;
        }

        private static bool getBoolValueFromDict(Dictionary<string, object> i_VehicleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfBoolInObjectForm;
            didGetWork = i_VehicleCharaterstic.TryGetValue(i_Key, out valueOfBoolInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException(string.Format("Wrong Dictionary Format, no {0} field", i_Key));
            }

            bool booleanInBoolForm = (bool)valueOfBoolInObjectForm;

            return booleanInBoolForm;
        }

        private static List<Wheel> createWheels(List<string> i_WheelMakerName, int i_NumberOfWheelsInTruck, float i_MaxAirPressureInTruckWheel, List<float> i_CurrentAirPressureInWheels)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int wheelNumber = 0; wheelNumber < i_NumberOfWheelsInTruck; wheelNumber++)
            {
                Wheel newWheel = new Wheel(i_WheelMakerName[wheelNumber], i_CurrentAirPressureInWheels[wheelNumber], i_MaxAirPressureInTruckWheel);
                wheels.Add(newWheel);
            }

            return wheels;
        }

        private static Energy createEnergyByType(float i_CurrentEnergyLevel, float i_MaxTankSize, Fuel.eFuelType i_FuelType)
        {
            Energy energyType;
            energyType = new Fuel(i_CurrentEnergyLevel, i_MaxTankSize, i_FuelType);

            return energyType;
        }

        private static Energy createEnergyByType(float i_CurrentEnergyLevel, float i_MaxBatterySize)
        {
            Energy energyType;
            energyType = new Electric(i_CurrentEnergyLevel, i_MaxBatterySize);

            return energyType;
        }

        internal enum ePossibleVehicleTypes
        {
            FuelBike,
            ElectricBike,
            FuelCar,
            ElectricCar,
            FuelTruck
        }
    }
}
