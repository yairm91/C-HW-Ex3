using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal class GarageInterface
    {
        private const int k_MinimumOptionIndex = 0;
        private const int k_MaximumOptionIndex = 7;
        private const int k_IndicatorOfAnError = -1;
        private const string k_startOfQuestionSelectingVehicleType = "Please select your vehicle type from the following: ";
        private const string k_FieldQuestion = "Please enter";

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
            Console.WriteLine("Please enter the state of the vehicle in the garage:");

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

        internal static string GetVehicleTypeFromUser(List<string> i_ListOfPossibleVehicleTypes)
        {
            StringBuilder questionForUser = new StringBuilder();

            questionForUser.Append(k_startOfQuestionSelectingVehicleType);

            foreach (string possibleVehicleType in i_ListOfPossibleVehicleTypes)
            {
                questionForUser.Append(string.Format("{0} ", possibleVehicleType));
            }

            return getStringWithLimitOnTheInputFromUser(questionForUser.ToString(), i_ListOfPossibleVehicleTypes);
        }

        private static string getStringWithLimitOnTheInputFromUser(string i_MessageToPrint, List<string> i_OptionalValues)
        {
            bool gotValidInput = false;
            string inputFromUser = string.Empty;

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

        internal static Dictionary<string, string> getParametersForFactoryFromUser(List<string> i_NameOfFieldsPerVehicle)
        {
            Dictionary<string, string> parametersForFactory = new Dictionary<string, string>();

            foreach (string key in i_NameOfFieldsPerVehicle)
            {
                ShowThisStringAsOutput(string.Format("{0} {1}:", k_FieldQuestion, key));
                string answerFromUser = Console.ReadLine();
                parametersForFactory.Add(key, answerFromUser);
            }

            return parametersForFactory;
        }
    }
}
