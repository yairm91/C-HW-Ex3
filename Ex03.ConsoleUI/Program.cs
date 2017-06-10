using Ex03.GarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            runGarageProgram();
        }

        internal const string k_CarType = "Car";
        internal const string k_BikeType = "Bike";
        internal const string k_TruckType = "Truck";
        internal const string k_CarColorYellow = "Yellow";
        internal const string k_CarColorWhite = "White";
        internal const string k_CarColorBlack = "Black";
        internal const string k_CarColorBlue = "Blue";
        internal const string k_NumberOfDoorsTwo = "2";
        internal const string k_NumberOfDoorsThree = "3";
        internal const string k_NumberOfDoorsFour = "4";
        internal const string k_NumberOfDoorsFive = "5";
        internal const string k_FuelType = "Fuel";
        internal const string k_ElectricType = "Electric";
        internal const string k_BikeLicenseTypeA = "A";
        internal const string k_BikeLicenseTypeAB = "AB";
        internal const string k_BikeLicenseTypeATwo = "A2";
        internal const string k_BikeLicenseTypeBOne = "B1";
        internal const int k_MaximumPercentOfRemiaingEnergy = 0;
        internal const int k_MimimumPercentOfRemiaingEnergy = 100;


        private const int k_NumberOfMinutesInOneHour = 60;
        private const int k_NumberOfWheelsInCar = 4;
        private const int k_NumberOfWheelsInBike = 2;
        private const int k_NumberOfWheelsInTruck = 12;
        private const float k_MaxAirPressureInCarWheel = 30;
        private const float k_MaxAirPressureInTruckWheel = 32;
        private const float k_MaxAirPressureInBikeWheel = 33;
        private const float k_MinimumAirPressureOfWheel = 0;
        private const int k_MinimumEngineVolume = 0;
        private const int k_MinimumCargoWeight = 0;

        private const string k_InRepairState = "InRepair";
        private const string k_LicenceNumberKey = "LicenceNumber";
        private const string k_ModelNameKey = "ModelName";
        private const string k_VehicleOwnerNameKey = "VehicleOwnerName";
        private const string k_VehicleOwnerPhoneNumberKey = "VehicleOwnerPhoneNumber";
        private const string k_PercentOfEnergyLeftKey = "PercentOfEnergyLeft";
        private const string k_VehicleTypeKey = "VehicleType";
        private const string k_CarColorKey = "CarColor";
        private const string k_NumberOfDoorsInCarKey = "NumberOfDoorsInCar";
        private const string k_VehicleEnergyTypeKey = "VehicleEnergyType";
        private const string k_CurrentPressureInWheelsKey = "CurrentPressureInWheels";
        private const string k_WheelMakerKey = "WheelMaker";
        private const string k_TypeOfLicenceKey = "TypeOfLicence";
        private const string k_EngineVolumeKey = "EngineVolume";
        private const string k_MaxCargoWeightForTruckKey = "MaxCargoWeight";
        internal const string k_HasDangerousCargoKey = "HasDangerousCargo";

        private static void runGarageProgram()
        {
            Garage thisGarage = new Garage();
            eFunctionalityOptionsOfGarageProgram currentChosenFunctionality = GarageInterface.GetFunctionalityFromUser();

            while (currentChosenFunctionality != eFunctionalityOptionsOfGarageProgram.QuitProgram)
            {
                switch (currentChosenFunctionality)
                {
                    case eFunctionalityOptionsOfGarageProgram.InsertNewVeichleToGarage:
                        insertNewVeichleToGarageOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ShowLicenseNumbersOfVeichlesInGarage:
                        showLicenseNumbersOfVeichlesInGarageOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ChangeStateOfVeichle:
                        changeInGargeStateOfVeichleOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.InflateAirInWheelsOfVeichleToMaximum:
                        inflateAirInWheelsOfVeichleToMaximumOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.FuelVeichleBasedOnFuel:
                        fuelVeichleBasedOnFuelOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ChargeVeichleBasedOnElectricity:
                        chargeVehicleBasedOnElectricityOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ShowFullDataOfVeichleByLicenseNumber:
                        showFullDataOfVeichleByLicenseNumberOption(thisGarage);
                        break;
                    default:
                        break;
                }

                currentChosenFunctionality = GarageInterface.GetFunctionalityFromUser();
            }
            GarageInterface.QuitProgramWithMessage();
        }


        private static void showFullDataOfVeichleByLicenseNumberOption(Garage i_ThisGarage)
        {
            string vehicleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            bool isVehicleInThisGarage = isInGarageIfNotSendError(i_ThisGarage, vehicleLicenseNumber);

            if (isVehicleInThisGarage)
            {
                GarageInterface.ShowThisStringAsOutput
                    (i_ThisGarage.getFullVehicleDetailsByLicenseNumber(vehicleLicenseNumber));
            }
        }

        private static void chargeVehicleBasedOnElectricityOption(Garage io_ThisGarage)
        {
            string vehicleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            bool isVehicleInThisGarage = isInGarageIfNotSendError(io_ThisGarage, vehicleLicenseNumber);

            if (isVehicleInThisGarage)
            {
                float numberOfHoursToCharge = GarageInterface.GetNumberOfMinuetsToChargeBattery() / k_NumberOfMinutesInOneHour;
                try
                {
                    io_ThisGarage.ChargeElectricityBasedVehicle(vehicleLicenseNumber, numberOfHoursToCharge);
                    GarageInterface.SendSucsses();
                }
                catch (ArgumentException argumentException)
                {
                    GarageInterface.ShowThisStringAsOutput(argumentException.Message);
                }
            }
        }

        private static void fuelVeichleBasedOnFuelOption(Garage io_ThisGarage)
        {
            string vehicleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            bool isVehicleInThisGarage = isInGarageIfNotSendError(io_ThisGarage, vehicleLicenseNumber);

            if (isVehicleInThisGarage)
            {
                string typeOfFuel = GarageInterface.GetTypeOfFuelFromUser();
                float litersOfFuelToFill = GarageInterface.GetNumberOfLitersOfFuel();
                try
                {
                    io_ThisGarage.FuelNumberOfLitersToFuelBasedVehicle
                        (vehicleLicenseNumber, typeOfFuel, litersOfFuelToFill);
                }
                catch (FormatException formatException)
                {
                    GarageInterface.ShowThisStringAsOutput(formatException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    GarageInterface.ShowThisStringAsOutput(argumentException.Message);
                }
                catch (ValueOutOfRangeException outOfRangeException)
                {
                    GarageInterface.ShowThisStringAsOutput(outOfRangeException.Message);
                }
            }
        }

        private static void inflateAirInWheelsOfVeichleToMaximumOption(Garage io_ThisGarage)
        {
            string vehicleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            bool isVehicleInThisGarage = isInGarageIfNotSendError(io_ThisGarage, vehicleLicenseNumber);

            if (isVehicleInThisGarage)
            {
                io_ThisGarage.InflateAirInWheelsOfChoosenVehicleToMaximum(vehicleLicenseNumber);
                GarageInterface.SendSucsses();
            }
        }

        private static void changeInGargeStateOfVeichleOption(Garage io_ThisGarage)
        {
            string vehicleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            bool isVehicleInThisGarage = isInGarageIfNotSendError(io_ThisGarage, vehicleLicenseNumber);

            if (isVehicleInThisGarage)
            {
                string newInGargeStateOfVehicle = GarageInterface.GetVehicleNewStateInGarageFromUser();
                try
                {
                    io_ThisGarage.ChangeInGargeStateOfVehicle(vehicleLicenseNumber, newInGargeStateOfVehicle);
                    GarageInterface.SendSucsses();
                }
                catch (FormatException formatException)
                {
                    GarageInterface.ShowThisStringAsOutput(formatException.Message);
                }
            }
        }

        private static void showLicenseNumbersOfVeichlesInGarageOption(Garage i_ThisGarage)
        {
            string InGargeStateOfVehicleToFilterWith = GarageInterface.GetAndSetVeichleStateIfUserWantTo();

            try
            {
                GarageInterface.ShowThisStringAsOutput
                                   (i_ThisGarage.ShowLicenseNumbersOfVehiclesInGarageWithFilterByState(InGargeStateOfVehicleToFilterWith));
            }
            catch (FormatException formatException)
            {
                GarageInterface.ShowThisStringAsOutput(formatException.Message);
            }
        }

        private static void insertNewVeichleToGarageOption(Garage io_ThisGarage)
        {
            string vehicleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            if (io_ThisGarage.IsVehicleInThisGarage(vehicleLicenseNumber))
            {
                io_ThisGarage.ChangeInGargeStateOfVeichle(vehicleLicenseNumber, k_InRepairState);
            }
            else
            {
                Dictionary<string, object> newVehicleData = new Dictionary<string, object>();
                setVehicleBasicParameters(newVehicleData, vehicleLicenseNumber);
                setMoreVehicleParametersByVehicleType(newVehicleData);
                try
                {
                    io_ThisGarage.InsertNewVehicle(newVehicleData);
                    GarageInterface.SendSucsses();
                }
                catch (FormatException formatException)
                {
                    GarageInterface.ShowThisStringAsOutput(formatException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    GarageInterface.ShowThisStringAsOutput(argumentException.Message);
                }
                catch (ValueOutOfRangeException outOfRangeException)
                {
                    GarageInterface.ShowThisStringAsOutput(outOfRangeException.Message);
                }


            }
        }

        private static void setMoreVehicleParametersByVehicleType(Dictionary<string, object> io_NewVehicleData)
        {
            object vehicleTypeObject;
            bool parseSucceed = io_NewVehicleData.TryGetValue(k_VehicleTypeKey, out vehicleTypeObject);

            if (parseSucceed)
            {
                string vehicleTypeString = (string)vehicleTypeObject;

                if (vehicleTypeString.Equals(k_CarType))
                {
                    setCarTypeParameters(io_NewVehicleData);
                }
                else if (vehicleTypeString.Equals(k_BikeType))
                {
                    setBikeTypeParameters(io_NewVehicleData);
                }
                else if (vehicleTypeString.Equals(k_TruckType))
                {
                    setTruckTypeParameters(io_NewVehicleData);
                }
            }
        }

        private static void setTruckTypeParameters(Dictionary<string, object> io_NewVehicleData)
        {
            io_NewVehicleData.Add(k_VehicleEnergyTypeKey, k_FuelType);
            io_NewVehicleData.Add(k_WheelMakerKey, GarageInterface.GetListOfWheelsMakerFromUser(k_NumberOfWheelsInTruck));
            io_NewVehicleData.Add(
                k_CurrentPressureInWheelsKey,
                GarageInterface.GetListOfWheelsPressuresFromUser(
                    k_NumberOfWheelsInTruck,
                    k_MinimumAirPressureOfWheel,
                    k_MaxAirPressureInTruckWheel));
            io_NewVehicleData.Add(k_MaxCargoWeightForTruckKey, GarageInterface.GetMaxCargoWeightFromUser
                (k_MinimumCargoWeight));
            io_NewVehicleData.Add(k_HasDangerousCargoKey, GarageInterface.GetHasDangerousCargoKeyFromUser());
        }

        
        private static void setBikeTypeParameters(Dictionary<string, object> io_NewVehicleData)
        {
            io_NewVehicleData.Add(k_VehicleEnergyTypeKey, GarageInterface.GetVehicleEnergyTypeFromUser());
            io_NewVehicleData.Add(k_WheelMakerKey, GarageInterface.GetListOfWheelsMakerFromUser(k_NumberOfWheelsInBike));
            io_NewVehicleData.Add(
                k_CurrentPressureInWheelsKey,
                GarageInterface.GetListOfWheelsPressuresFromUser(
                    k_NumberOfWheelsInBike,
                    k_MinimumAirPressureOfWheel,
                    k_MaxAirPressureInBikeWheel));
            io_NewVehicleData.Add(k_TypeOfLicenceKey, GarageInterface.GetBikeTypeOfLicenceFromUser());
            io_NewVehicleData.Add(k_EngineVolumeKey, GarageInterface.GetBikeEngineVolume(k_MinimumEngineVolume));
        }

        
        private static void setCarTypeParameters(Dictionary<string, object> io_NewVehicleData)
        {
            io_NewVehicleData.Add(k_CarColorKey, GarageInterface.GetCarColorFromUser());
            io_NewVehicleData.Add(k_NumberOfDoorsInCarKey, GarageInterface.GetCarNumberOfDoorsFromUser());
            io_NewVehicleData.Add(k_WheelMakerKey, GarageInterface.GetListOfWheelsMakerFromUser(k_NumberOfWheelsInCar));
            io_NewVehicleData.Add(
                k_CurrentPressureInWheelsKey,
                GarageInterface.GetListOfWheelsPressuresFromUser(
                    k_NumberOfWheelsInCar,
                    k_MinimumAirPressureOfWheel,
                    k_MaxAirPressureInCarWheel));
            io_NewVehicleData.Add(k_VehicleEnergyTypeKey, GarageInterface.GetVehicleEnergyTypeFromUser());
        }

        private static void setVehicleBasicParameters(Dictionary<string, object> io_NewVehicleData, string i_LicenseNumber)
        {
            io_NewVehicleData.Add(k_LicenceNumberKey, i_LicenseNumber);
            io_NewVehicleData.Add(k_ModelNameKey, GarageInterface.GetVehicleModelNameFromUser());
            io_NewVehicleData.Add(k_VehicleOwnerNameKey, GarageInterface.GetVehicleOwnerNameFromUser());
            io_NewVehicleData.Add(k_VehicleOwnerPhoneNumberKey, GarageInterface.GetVehicleOwnerPhoneNumberFromUser());
            io_NewVehicleData.Add(k_PercentOfEnergyLeftKey, GarageInterface.GetVehiclePercentageOfEnergyLeftFromUser());
            io_NewVehicleData.Add(k_VehicleTypeKey, GarageInterface.GetVehicleTypeFromUser());

            object vehicleTypeObject;
            bool parseSucceed = io_NewVehicleData.TryGetValue(k_VehicleTypeKey, out vehicleTypeObject);
        }

        private static bool isInGarageIfNotSendError(Garage i_ThisGarage, string i_VehicleLicenseNumber)
        {
            bool isInGarage = i_ThisGarage.IsVehicleInThisGarage(i_VehicleLicenseNumber);

            if (!isInGarage)
            {
                GarageInterface.ErrorCantFindVeichleByLicenseNumber(i_VehicleLicenseNumber);
            }

            return isInGarage;
        }
        internal enum eFunctionalityOptionsOfGarageProgram
        {
            QuitProgram,
            InsertNewVeichleToGarage,
            ShowLicenseNumbersOfVeichlesInGarage,
            ChangeStateOfVeichle,
            InflateAirInWheelsOfVeichleToMaximum,
            FuelVeichleBasedOnFuel,
            ChargeVeichleBasedOnElectricity,
            ShowFullDataOfVeichleByLicenseNumber
        }

    }
}
