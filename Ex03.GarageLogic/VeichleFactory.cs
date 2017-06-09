﻿using System;
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

        internal const string k_CurrentEnergyLevelKey = "CurrentEnergy";
        internal const string k_CurrentPressureInWheelsKey = "CurrentPressureInWheels";
        internal const string k_HasDangerousCargoKey = "HasDangerousCargo";
        internal const string k_WheelMakerKey = "WheelMaker";
        internal const string k_MaxCargoWeightForTruckKey = "MaxCargoWeight";
        internal const string k_ModelNameKey = "ModelName";
        internal const string k_LicenceNumberKey = "LicenceNumber";
        internal const string k_PercentOfEnergyLeftKey = "PercentOfEnergyLeft";
        internal const string k_NumberOfDoorsInCarKey = "NumberOfDoorsInCar";
        internal const string k_CarColorKey = "CarColor";
        internal const string k_TypeOfLicenceKey = "TypeOfLicence";
        internal const string k_EngineVolumeKey = "EngineVolume";

        internal static Veichle CreateNewVeichle(ePossibleVeichleTypes veichleType, Dictionary<string, object> i_VeichleCharaterstic)
        {
            Veichle newVeichle;
            switch (veichleType)
            {
                case ePossibleVeichleTypes.FuelBike:
                    newVeichle = createNewBike(i_VeichleCharaterstic, v_UsesFuel);
                    break;
                case ePossibleVeichleTypes.ElectricBike:
                    newVeichle = createNewBike(i_VeichleCharaterstic, !v_UsesFuel);
                    break;
                case ePossibleVeichleTypes.FuelCar:
                    newVeichle = createNewCar(i_VeichleCharaterstic, v_UsesFuel);
                    break;
                case ePossibleVeichleTypes.ElectricCar:
                    newVeichle = createNewCar(i_VeichleCharaterstic, !v_UsesFuel);
                    break;
                case ePossibleVeichleTypes.FuelTruck:
                    newVeichle = createNewTruck(i_VeichleCharaterstic, v_UsesFuel);
                    break;
                default:
                    throw new ArgumentException();
            }

            return newVeichle;
        }

        private static Veichle createNewBike(Dictionary<string, object> i_VeichleCharaterstic, bool i_UsesFuel)
        {
            Energy energyType;
            if (i_UsesFuel)
            {
                energyType = createEnergySource(i_VeichleCharaterstic, k_MaxTankSizeInBike, k_BikeFuelType);
            }
            else
            {
                energyType = createEnergySource(i_VeichleCharaterstic, k_MaxBatterySizeInBike);
            }

            List<Wheel> wheels = createWheelsList(i_VeichleCharaterstic, k_NumberOfWheelsInBike, k_MaxAirPressureInBikeWheel);

            Bike.eTypeOfLicence typeOfLicence;
            int engineVolume;
            float percentOfEnergyLeft;
            string modelName, licenceNumber;

            getInputParamtersForBike(i_VeichleCharaterstic, out typeOfLicence, out engineVolume, out modelName, out licenceNumber, out percentOfEnergyLeft);

            return new Bike(typeOfLicence, engineVolume, modelName, licenceNumber, percentOfEnergyLeft, energyType, wheels);
        }

        private static void getInputParamtersForBike(Dictionary<string, object> i_VeichleCharaterstic, out Bike.eTypeOfLicence o_TypeOfLicence, out int o_EngineVolume, out string o_ModelName, out string o_LicenceNumber, out float o_PercentOfEnergyLeft)
        {
            o_TypeOfLicence = getTypeOfLicenceValueFromDict(i_VeichleCharaterstic, k_TypeOfLicenceKey);
            o_EngineVolume = getIntFromDict(i_VeichleCharaterstic, k_EngineVolumeKey);
            getVeichleParameters(i_VeichleCharaterstic, out o_ModelName, out o_LicenceNumber, out o_PercentOfEnergyLeft);
        }

        private static void getVeichleParameters(Dictionary<string, object> i_VeichleCharaterstic, out string o_ModelName, out string o_LicenceNumber, out float o_PercentOfEnergyLeft)
        {
            o_ModelName = getStringFromDict(i_VeichleCharaterstic, k_ModelNameKey);
            o_LicenceNumber = getStringFromDict(i_VeichleCharaterstic, k_LicenceNumberKey);
            o_PercentOfEnergyLeft = getFloatFromDict(i_VeichleCharaterstic, k_PercentOfEnergyLeftKey);
        }

        private static int getIntFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object numberInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out numberInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            int numberInIntForm = (int)numberInObjectForm;

            return numberInIntForm;
        }

        private static Bike.eTypeOfLicence getTypeOfLicenceValueFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object typeOfLicenceInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out typeOfLicenceInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            Bike.eTypeOfLicence typeOfLicence = (Bike.eTypeOfLicence) typeOfLicenceInObjectForm;

            return typeOfLicence;
        }

        private static Veichle createNewCar(Dictionary<string, object> i_VeichleCharaterstic, bool i_UsesFuel)
        {
            Energy energyType;
            if (i_UsesFuel)
            {
                energyType = createEnergySource(i_VeichleCharaterstic, k_MaxTankSizeInCar, k_CarFuelType);
            }
            else
            {
                energyType = createEnergySource(i_VeichleCharaterstic, k_MaxBatterySizeInCar);
            }

            List<Wheel> wheels = createWheelsList(i_VeichleCharaterstic, k_NumberOfWheelsInCar, k_MaxAirPressureInCarWheel);

            Car.eNumberOfDoors numberOfDoors;
            Car.ePossibleCarColors carColor;
            float percentOfEnergyLeft;
            string modelName, licenceNumber;

            getInputParamtersForCar(i_VeichleCharaterstic, out numberOfDoors, out carColor, out modelName, out licenceNumber, out percentOfEnergyLeft);

            return new Car(carColor, numberOfDoors, modelName, licenceNumber, percentOfEnergyLeft, energyType, wheels);
        }

        private static void getInputParamtersForCar(Dictionary<string, object> i_VeichleCharaterstic, out Car.eNumberOfDoors o_NumberOfDoors, out Car.ePossibleCarColors o_CarColor, out string o_ModelName, out string o_LicenceNumber, out float o_PercentOfEnergyLeft)
        {
            o_NumberOfDoors = getNumberOfDoorsValueFromDict(i_VeichleCharaterstic, k_NumberOfDoorsInCarKey);
            o_CarColor = getCarColorValueFromDict(i_VeichleCharaterstic, k_CarColorKey);
            getVeichleParameters(i_VeichleCharaterstic, out o_ModelName, out o_LicenceNumber, out o_PercentOfEnergyLeft);
        }

        private static Car.ePossibleCarColors getCarColorValueFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object possibleCarColorInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out possibleCarColorInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            Car.ePossibleCarColors carColor = (Car.ePossibleCarColors) possibleCarColorInObjectForm;

            return carColor;
        }

        private static Car.eNumberOfDoors getNumberOfDoorsValueFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object numberOfDoorsInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out numberOfDoorsInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            Car.eNumberOfDoors numberOfDoors = (Car.eNumberOfDoors)numberOfDoorsInObjectForm;

            return numberOfDoors;
        }

        private static Veichle createNewTruck(Dictionary<string, object> i_VeichleCharaterstic, bool i_UsesFuel)
        {
            Energy energyType;
            if (i_UsesFuel)
            {
                energyType = createEnergySource(i_VeichleCharaterstic, k_MaxTankSizeInTruck, k_TruckFuelType);
            }
            else
            {
                energyType = createEnergySource(i_VeichleCharaterstic, k_MaxBatterySizeInTruck);
            }

            List<Wheel> wheels = createWheelsList(i_VeichleCharaterstic, k_NumberOfWheelsInTruck, k_MaxAirPressureInTruckWheel);

            bool hasDangerousCargo;
            float maxCargoWeight, percentOfEnergyLeft;
            string modelName, licenceNumber;
            getInputParamtersForTruck(i_VeichleCharaterstic, out hasDangerousCargo, out maxCargoWeight, out modelName, out licenceNumber, out percentOfEnergyLeft);

            return new Truck(hasDangerousCargo, maxCargoWeight, modelName, licenceNumber, percentOfEnergyLeft, energyType, wheels);
        }

        private static List<Wheel> createWheelsList(Dictionary<string, object> i_VeichleCharaterstic, int i_numberOfWheels, float i_MaxAirPressureInWheel)
        {
            List<float> currentAirPressureInWheels = getFloatListFromDict(i_VeichleCharaterstic, k_CurrentPressureInWheelsKey);
            string wheelMakerName = getStringFromDict(i_VeichleCharaterstic, k_WheelMakerKey);
            List<Wheel> wheels = createWheels(wheelMakerName, i_numberOfWheels, i_MaxAirPressureInWheel, currentAirPressureInWheels);
            return wheels;
        }

        private static Energy createEnergySource(Dictionary<string, object> i_VeichleCharaterstic, float i_MaxBatterySize)
        {
            float currentEnergyLevel = getFloatFromDict(i_VeichleCharaterstic, k_CurrentEnergyLevelKey);
            Energy energyType = createEnergyByType(currentEnergyLevel, i_MaxBatterySize);
            return energyType;
        }

        private static Energy createEnergySource(Dictionary<string, object> i_VeichleCharaterstic, float i_MaxTankSize, Fuel.eFuelType i_FuelType)
        {
            float currentEnergyLevel = getFloatFromDict(i_VeichleCharaterstic, k_CurrentEnergyLevelKey);
            Energy energyType = createEnergyByType(currentEnergyLevel, i_MaxTankSize, i_FuelType);
            return energyType;
        }

        private static void getInputParamtersForTruck(Dictionary<string, object> i_VeichleCharaterstic, out bool o_HasDangerousCargo, out float o_MaxCargoWeight, out string o_ModelName, out string o_LicenceNumber, out float o_PercentOfEnergyLeft)
        {
            o_HasDangerousCargo = getBoolValueFromDict(i_VeichleCharaterstic, k_HasDangerousCargoKey);
            o_MaxCargoWeight = getFloatFromDict(i_VeichleCharaterstic, k_MaxCargoWeightForTruckKey);
            getVeichleParameters(i_VeichleCharaterstic, out o_ModelName, out o_LicenceNumber, out o_PercentOfEnergyLeft);
        }

        private static string getStringFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfStringInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out valueOfStringInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            string stringInStringForm = valueOfStringInObjectForm.ToString();

            return stringInStringForm;
        }

        private static List<float> getFloatListFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfListInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out valueOfListInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            List<float> listInListForm = (List<float>) valueOfListInObjectForm;

            return listInListForm;
        }

        private static float getFloatFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfNumberInObjectFormat;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out valueOfNumberInObjectFormat);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            float numberInFloatFormat = (float) valueOfNumberInObjectFormat;

            return numberInFloatFormat;
        }

        private static bool getBoolValueFromDict(Dictionary<string, object> i_VeichleCharaterstic, string i_Key)
        {
            bool didGetWork;
            object valueOfBoolInObjectForm;
            didGetWork = i_VeichleCharaterstic.TryGetValue(i_Key, out valueOfBoolInObjectForm);
            if (!didGetWork)
            {
                throw new FormatException();
            }

            bool booleanInBoolForm = (bool)valueOfBoolInObjectForm;

            return booleanInBoolForm;
        }

        private static List<Wheel> createWheels(string i_WheelMakerName, int i_NumberOfWheelsInTruck, float i_MaxAirPressureInTruckWheel, List<float> i_CurrentAirPressureInWheels)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int wheelNumber = 0; wheelNumber < i_NumberOfWheelsInTruck; wheelNumber++)
            {
                Wheel newWheel = new Wheel(i_WheelMakerName, i_CurrentAirPressureInWheels[wheelNumber], i_MaxAirPressureInTruckWheel);
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

        internal enum ePossibleVeichleTypes
        {
            FuelBike,
            ElectricBike,
            FuelCar,
            ElectricCar,
            FuelTruck
        }
    }
}
