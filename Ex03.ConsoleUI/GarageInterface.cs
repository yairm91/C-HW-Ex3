using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal class GarageInterface
    {
        private const int k_MinimumOptionIndex = 0;
        private const int k_MaximumOptionIndex = 7;
        private const int k_IndicatorOfAnError = -1;

        internal static Program.eFunctionalityOptionsOfGarageProgram GetFunctionalityFromUser()
        {
            Program.eFunctionalityOptionsOfGarageProgram FunctionalitySelectedByUser;
            int selectedOptionNumber;

            selectedOptionNumber = loopUntilGetValidOptionNumber();
            FunctionalitySelectedByUser = (Program.eFunctionalityOptionsOfGarageProgram)selectedOptionNumber;

            return FunctionalitySelectedByUser;
        }

        private static int loopUntilGetValidOptionNumber()
        {
            int validOptionNumber = k_IndicatorOfAnError;
            bool gotValidNumber = false;

            while (!gotValidNumber)
            {
                printSelectFunctionalityScreen();
                gotValidNumber = GetValidOptionNumber(out validOptionNumber);
            }

            return validOptionNumber;
        }

        private static bool GetValidOptionNumber(out int io_ValidOptionNumber)
        {
            string readedFromUser = Console.ReadLine();
            bool gotValidInput;

            gotValidInput = int.TryParse(readedFromUser, out io_ValidOptionNumber);
            gotValidInput = isBetweenMinimumAndMaximumIncludes(io_ValidOptionNumber, k_MinimumOptionIndex, k_MaximumOptionIndex);

            if (!gotValidInput)
            {
                Console.WriteLine(
                    @"The selected option number should be an integer between {0} to {1}",
                    k_MinimumOptionIndex,
                    k_MaximumOptionIndex);
            }

            return gotValidInput;
        }

        private static bool isBetweenMinimumAndMaximumIncludes(float i_NumberToCheckIfInRange, float i_Minimum, float i_Maximum)
        {
            bool isInRange = true;
            if (i_NumberToCheckIfInRange < i_Minimum || i_NumberToCheckIfInRange > i_Maximum)
            {
                isInRange = false;
            }

            return isInRange;
        }

        private static void printSelectFunctionalityScreen()
        {
            Console.WriteLine(string.Format(
@"Which function you want to use now?
To insert new veichle enter: {0}
To show license numbers of veichles enter: {1}
To change state of a veichle enter: {2}
To inflate air in wheels of a veichle to maximum enter: {3}
To fuel veichle which based on fuel enter: {4}
To charge veichle which based on electricity enter: {5}
To show full data of a veichle by license number enter: {6}
To Quit enter: {7}",
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            0));
        }


        internal static void ShowThisStringAsOutput(string i_TheStringToOutput)
        {
            Console.WriteLine(i_TheStringToOutput);
        }

        internal static float GetNumberOfMinuetsToChargeBattery()
        {
            Console.WriteLine("Please enter number of minuets to charge the battery:");
            return loopUntilGettingValidFloat(
                "Wrong input! number of minuets to charge the battery should be a float, please insert valid input:");
        }

        private static float loopUntilGettingValidFloat(string i_StringToPrintInEachFail)
        {
            float numberToGetFromUser;
            bool parseSucceed = float.TryParse(Console.ReadLine(), out numberToGetFromUser);
            while (!parseSucceed)
            {
                Console.WriteLine(i_StringToPrintInEachFail);
                parseSucceed = float.TryParse(Console.ReadLine(), out numberToGetFromUser);
            }

            return numberToGetFromUser;
        }

        internal static string GetVeichleLicenseNumberFromUser()
        {
            Console.WriteLine("Please enter veichle license number:");

            return Console.ReadLine();
        }

        internal static void ErrorCantFindVeichleByLicenseNumber(string i_VeichleLicenseNumber)
        {
            Console.WriteLine("The Veichle with the License Number: {0} does not exist in this garage data base", i_VeichleLicenseNumber);
        }

        internal static string GetTypeOfFuelFromUser()
        {
            Console.WriteLine("Please enter the type of fuel you want to fill:");

            return Console.ReadLine();
        }

        internal static float GetNumberOfLitersOfFuel()
        {
            Console.WriteLine("Please enter liters of fuel to fill the tank of the vehicle:");

            return loopUntilGettingValidFloat(
                "Wrong input! liters of fuel should be a float, please insert valid input:");
        }

        internal static void SendSucsses()
        {
            Console.WriteLine(@"Operation completed sucssesfuly!
");
        }

        internal static void QuitProgramWithMessage()
        {
            Console.WriteLine("You choose to exit - goodbye");
        }

        internal static string GetVehicleNewStateInGarageFromUser()
        {
            Console.WriteLine("Please enter the new garage state of the vehicle:");

            return Console.ReadLine();
        }

        // if user dont want to enter state return null 
        internal static string GetAndSetVeichleStateIfUserWantTo()
        {
            string stateToReturn = null;

            Console.WriteLine("Please enter 1 if you want to filter by garage state else press enter:");
            string userInput = Console.ReadLine();
            if (checkIfUserAnswerYesOnBooleanQuestion(userInput))
            {
                Console.WriteLine("Please enter the  garage state to filter by:");
                stateToReturn = Console.ReadLine();
            }

            return stateToReturn;
        }

        // if string equal 1 than it be considerd as a yes
        private static bool checkIfUserAnswerYesOnBooleanQuestion(string i_UserInput)
        {
            int checkIfEqualOne;
            bool isAnsweredYes = false;
            bool parseSucceed = int.TryParse(i_UserInput, out checkIfEqualOne);

            if (parseSucceed && checkIfEqualOne == 1)
            {
                isAnsweredYes = true;
            }

            return isAnsweredYes;
        }

        internal static string GetVehicleModelNameFromUser()
        {
            Console.WriteLine("Please enter the model name of the vehicle:");

            return Console.ReadLine();
        }

        internal static string GetVehicleOwnerNameFromUser()
        {
            Console.WriteLine("Please enter the owner name of the vehicle:");

            return Console.ReadLine();
        }

        internal static string GetVehicleOwnerPhoneNumberFromUser()
        {
            Console.WriteLine("Please enter the phone number of the vehicle owner:");

            return Console.ReadLine();
        }

        internal static float GetVehiclePercentageOfEnergyLeftFromUser()
        {
            Console.WriteLine("Please enter the percentage of energy left in vehicle energy source:");

            return GetValidFloatInRange(
                "Wrong input! the percentage of energy left should be a float between 0 to 100, please insert valid input:",
                Program.k_MimimumPercentOfRemiaingEnergy,
                Program.k_MaximumPercentOfRemiaingEnergy);
        }

        internal static string GetVehicleTypeFromUser()
        {
            string printToUser = "";
            List<string> optionalValues = new List<string>();

            optionalValues.Add(Program.k_CarType);
            optionalValues.Add(Program.k_BikeType);
            optionalValues.Add(Program.k_TruckType);
            printToUser = string.Format(
    @"Please enter the vehicle type:
{0}, {1}, {2}
Your input:",
optionalValues);

            return getStringWithLimitOnTheInputFromUser(printToUser, optionalValues);
        }

        private static string getStringWithLimitOnTheInputFromUser(string i_MessageToPrint, List<string> i_OptionalValues)
        {
            bool gotValidInput = false;
            string inputFromUser = "";

            while (!gotValidInput)
            {
                Console.WriteLine(i_MessageToPrint);
                inputFromUser = Console.ReadLine();
                if (i_OptionalValues.Contains(inputFromUser))
                {
                    gotValidInput = true;
                }
                else
                {
                    Console.WriteLine("Wrong input!");
                }
            }

            return inputFromUser;
        }

        internal static string GetCarColorFromUser()
        {
            List<string> optionalValues = new List<string>();

            optionalValues.Add(Program.k_CarColorWhite);
            optionalValues.Add(Program.k_CarColorBlue);
            optionalValues.Add(Program.k_CarColorBlack);
            optionalValues.Add(Program.k_CarColorYellow);
            string printToUser = string.Format(
@"Please enter the car color select from:
{0}, {1}, {2}, {3}
Your input:",
            optionalValues);

            return getStringWithLimitOnTheInputFromUser(printToUser, optionalValues);
        }

        internal static int GetCarNumberOfDoorsFromUser()
        {
            string stringNumberOfDoorsFromUser;
            int numberOfDoorsFromUse = k_IndicatorOfAnError;
            bool parseSucceed = false;
            List<string> optionalValues = new List<string>();

            optionalValues.Add(Program.k_NumberOfDoorsTwo);
            optionalValues.Add(Program.k_NumberOfDoorsThree);
            optionalValues.Add(Program.k_NumberOfDoorsFour);
            optionalValues.Add(Program.k_NumberOfDoorsFive);
            string printToUser = string.Format(
@"Please enter the car number of doors from:
{0}, {1}, {2}, {3}
Your input:",
            optionalValues);
            while (!parseSucceed)
            {
                stringNumberOfDoorsFromUser = getStringWithLimitOnTheInputFromUser(printToUser, optionalValues);
                parseSucceed = int.TryParse(stringNumberOfDoorsFromUser, out numberOfDoorsFromUse);
            }

            return numberOfDoorsFromUse;
        }

        internal static string GetVehicleEnergyTypeFromUser()
        {
            List<string> optionalValues = new List<string>();

            optionalValues.Add(Program.k_ElectricType);
            optionalValues.Add(Program.k_FuelType);
            string printToUser = string.Format(
@"Please enter the energy type of the vehicle:
{0}, {1}
Your input:",
            optionalValues);

            return getStringWithLimitOnTheInputFromUser(printToUser, optionalValues);
        }

        internal static string GetBikeTypeOfLicenceFromUser()
        {
            List<string> optionalValues = new List<string>();

            optionalValues.Add(Program.k_BikeLicenseTypeA);
            optionalValues.Add(Program.k_BikeLicenseTypeAB);
            optionalValues.Add(Program.k_BikeLicenseTypeATwo);
            optionalValues.Add(Program.k_BikeLicenseTypeBOne);
            string printToUser = string.Format(
@"Please enter the bike license type select from:
{0}, {1}, {2}, {3}
Your input:",
            optionalValues);

            return getStringWithLimitOnTheInputFromUser(printToUser, optionalValues);
        }

        internal static List<string> GetListOfWheelsMakerFromUser(int i_NumberOfWheels)
        {
            List<string> wheelsMakersNames = new List<string>();

            for (int i = 1; i <= i_NumberOfWheels; i++)
            {
                Console.WriteLine(string.Format(@"Please enter the {0}'th wheel manufacturer's name:"), i);
                wheelsMakersNames.Add(Console.ReadLine());
            }

            return wheelsMakersNames;
        }

        internal static List<float> GetListOfWheelsPressuresFromUser(int i_NumberOfWheels, float i_MinAirPressure, float i_MaxAirPressure)
        {
            List<float> wheelsAirPressures = new List<float>();

            for (int i = 1; i <= i_NumberOfWheels; i++)
            {
                Console.WriteLine(string.Format(@"Please enter the {0}'th wheel current air pressure:"), i);
                wheelsAirPressures.Add(GetValidFloatInRange(
                    string.Format(
                       @"Wrong input! Air Pressure should be float between {0} to {1} please enter new input:",
                       i_MinAirPressure,
                       i_MaxAirPressure),
                i_MinAirPressure, i_MaxAirPressure));
            }
            
            return wheelsAirPressures;
        }

        internal static int GetBikeEngineVolume(int k_MinimumEngineVolume)
        {
            return getPositiveInt("Please enter bike engine volume:");
        }

        private static float GetValidFloatInRange(string i_MessageToUser, float i_MinAirPressure, float i_MaxAirPressure)
        {
            string currentInputStringFromUser;
            float ValidFloatToReturn = k_IndicatorOfAnError;
            bool isInRange = false;
            bool isParseToFloatSucceed = false;

            while (!isInRange)
            {
                currentInputStringFromUser = Console.ReadLine();
                isParseToFloatSucceed = float.TryParse(currentInputStringFromUser, out ValidFloatToReturn);
                if (isParseToFloatSucceed)
                {
                    isInRange = isBetweenMinimumAndMaximumIncludes(ValidFloatToReturn, i_MinAirPressure, i_MaxAirPressure);
                }
                if (!isInRange)
                {
                    Console.WriteLine(i_MessageToUser); 
                }
            }

            return ValidFloatToReturn;
        }


        internal static int GetMaxCargoWeightFromUser(int k_MinimumCargoWeight)
        {
            return getPositiveInt("Please enter maximum cargo weight of truck:");
        }

        private static int getPositiveInt(string i_MessageToUser)
        {
            bool isParseToIntSucceed = false;
            int validIntToReturn = k_IndicatorOfAnError;

            while (!isParseToIntSucceed)
            {
                Console.WriteLine(i_MessageToUser);
                isParseToIntSucceed = int.TryParse(Console.ReadLine(), out validIntToReturn);
                if (isParseToIntSucceed && validIntToReturn > 0)
                {
                    isParseToIntSucceed = true;
                }
                else
                {
                    Console.WriteLine("Wrong input! should be integer and positive");
                }
            }

            return validIntToReturn;
        }

        internal static bool GetHasDangerousCargoKeyFromUser()
        {
            
            Console.WriteLine("Please enter 1 if the cargo is dangerous else press enter:");
            string userInput = Console.ReadLine();
            return (checkIfUserAnswerYesOnBooleanQuestion(userInput));
        }
    }
}
