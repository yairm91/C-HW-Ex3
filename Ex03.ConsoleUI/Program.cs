using Ex03.GarageLogic;
using System;


namespace Ex03.ConsoleUI
{
    class Program
    {
        private const int k_numberOfMinutesInOneHour = 60;

        public static void Main()
        {
            runGarageProgram();
        }

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
                        changeStateOfVeichleOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.InflateAirInWheelsOfVeichleToMaximum:
                        inflateAirInWheelsOfVeichleToMaximumOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.FuelVeichleBasedOnFuel:
                        fuelVeichleBasedOnFuelOption(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ChargeVeichleBasedOnElectricity:
                        chargeVeichleBasedOnElectricityOption(thisGarage);
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

        //TODO -do the Functionality of step 7 
        private static void showFullDataOfVeichleByLicenseNumberOption(Garage io_ThisGarage)
        {

            Console.WriteLine("7");
        }

        //TODO -do the Functionality of step 6 
        private static void chargeVeichleBasedOnElectricityOption(Garage io_ThisGarage)
        {
            string veichleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            float numberOfHoursToCharge = GarageInterface.GetNumberOfMinuetsToChargeBattery() 
                / k_numberOfMinutesInOneHour;
           


        }

        //TODO -do the Functionality of step 5 
        private static void fuelVeichleBasedOnFuelOption(Garage io_ThisGarage)
        {
            Console.WriteLine("5");
        }

        private static void inflateAirInWheelsOfVeichleToMaximumOption(Garage io_ThisGarage)
        {
            string veichleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();

            try
            {
                Garage.InflateAirInWheelsOfChoosenVehicleToMaximum(veichleLicenseNumber);
                GarageInterface.printSucsses();
            }
            catch (NullReferenceException)
            {
                GarageInterface.ErrorCantFindVeichleByLicenseNumber(veichleLicenseNumber);
            }
        }

        //TODO -do the Functionality of step 3 
        private static void changeStateOfVeichleOption(Garage io_ThisGarage)
        {
            Console.WriteLine("3");
        }

        //TODO -do the Functionality of step 2 
        private static void showLicenseNumbersOfVeichlesInGarageOption(Garage io_ThisGarage)
        {
            Console.WriteLine("2");
        }

        //TODO -do the Functionality of step 1 
        private static void insertNewVeichleToGarageOption(Garage io_ThisGarage)
        {
            Console.WriteLine("TODO");
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
