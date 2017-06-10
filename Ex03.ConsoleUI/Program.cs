using Ex03.GarageLogic;
using System;


namespace Ex03.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            runGarageProgram();
        }

        private const int k_NumberOfMinutesInOneHour = 60;

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
                    io_ThisGarage.ChargeElectricityBasedVeichle(vehicleLicenseNumber, numberOfHoursToCharge);
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
                catch(FormatException formatException)
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
                    io_ThisGarage.ChangeInGargeStateOfVeichle(vehicleLicenseNumber, newInGargeStateOfVehicle);
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
                                   (i_ThisGarage.ShowLicenseNumbersOfVeichlesInGarageWithFilterByState(InGargeStateOfVehicleToFilterWith));
            }
            catch (FormatException formatException)
            {
                GarageInterface.ShowThisStringAsOutput(formatException.Message);
            }
        }

        //TODO -do the Functionality of step 1 
        private static void insertNewVeichleToGarageOption(Garage io_ThisGarage)
        {
            Console.WriteLine("TODO");
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
