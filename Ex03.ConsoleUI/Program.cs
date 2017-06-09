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
                        insertNewVeichleToGarage(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ShowLicenseNumbersOfVeichlesInGarage:
                        showLicenseNumbersOfVeichlesInGarage(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ChangeStateOfVeichle:
                        changeStateOfVeichle(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.InflateAirInWheelsOfVeichleToMaximum:
                        inflateAirInWheelsOfVeichleToMaximum(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.FuelVeichleBasedOnFuel:
                        fuelVeichleBasedOnFuel(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ChargeVeichleBasedOnElectricity:
                        chargeVeichleBasedOnElectricity(thisGarage);
                        break;
                    case eFunctionalityOptionsOfGarageProgram.ShowFullDataOfVeichleByLicenseNumber:
                        showFullDataOfVeichleByLicenseNumber(thisGarage);
                        break;
                    default:
                        break;
                }

                currentChosenFunctionality = GarageInterface.GetFunctionalityFromUser();
            }
            GarageInterface.QuitProgramWithMessage();
        }

        //TODO -do the Functionality of step 7 
        private static void showFullDataOfVeichleByLicenseNumber(Garage io_ThisGarage)
        {

            Console.WriteLine("7");
        }

        //TODO -do the Functionality of step 6 
        private static void chargeVeichleBasedOnElectricity(Garage io_ThisGarage)
        {
            string veichleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            float numberOfHoursToCharge = GarageInterface.GetNumberOfMinuetsToChargeBattery() 
                / k_numberOfMinutesInOneHour;
            Veichle chosenVeichleByUser = io_ThisGarage.GetByLicenseNumber(veichleLicenseNumber);

            if (chosenVeichleByUser != null)
            {

            } 
            else
            {
                GarageInterface.ErrorCantFindVeichleByLicenseNumber(veichleLicenseNumber);
            }


        }

        //TODO -do the Functionality of step 5 
        private static void fuelVeichleBasedOnFuel(Garage io_ThisGarage)
        {
            Console.WriteLine("5");
        }

        //TODO -do the Functionality of step 4 
        private static void inflateAirInWheelsOfVeichleToMaximum(Garage io_ThisGarage)
        {
            string veichleLicenseNumber = GarageInterface.GetVeichleLicenseNumberFromUser();
            try
            {
                io_ThisGarage.GetGarageVeichleByLicenseNumber(veichleLicenseNumber).GetVeichle.inflateAirInWheelsToMaximum();
                GarageInterface.printSucsses();
            }
            catch (NullReferenceException)
            {
                GarageInterface.ErrorCantFindVeichleByLicenseNumber(veichleLicenseNumber);
            }
        }

        //TODO -do the Functionality of step 3 
        private static void changeStateOfVeichle(Garage io_ThisGarage)
        {
            Console.WriteLine("3");
        }

        //TODO -do the Functionality of step 2 
        private static void showLicenseNumbersOfVeichlesInGarage(Garage io_ThisGarage)
        {
            Console.WriteLine("2");
        }

        //TODO -do the Functionality of step 1 
        private static void insertNewVeichleToGarage(Garage io_ThisGarage)
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
