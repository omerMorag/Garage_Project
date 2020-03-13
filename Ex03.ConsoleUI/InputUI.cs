using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{ 
    public class InputUI
    {
        public enum eUserChoice
        {
            InsertVehicleToGarage = 1,
            DisplayLicenseNumber,
            ChangeVehicleStatus,
            InflateWheels,
            FillFuelTank,
            CargeBattary,
            DisplayFullVehicleData,
            Exit,
        }

         private OutputUI outPutAsistance = new OutputUI();

        public eUserChoice PrintMenuAndGetChoiceFromUser()
        {
            int userChoice;
            eUserChoice userChoiceOption = new eUserChoice();
            string header = string.Format("Please select one of the options:");

            userChoice = MakeArrayOfStringFromEnum(header, userChoiceOption);
            Console.Clear();

            return (InputUI.eUserChoice)userChoice;
        }

        public int MakeArrayOfStringFromEnum<T>(string i_HeaderBeforeOptions, T i_EnumInput)
        {
            string[] listOfChoice;
            int userChoice;

            listOfChoice = (string[])Enum.GetNames(typeof(T));
            userChoice = GetInputFromUser(outPutAsistance.buildEnumList(listOfChoice), i_EnumInput, i_HeaderBeforeOptions);
            return userChoice;
        }
      
        private int GetInputFromUser<T>(string i_Message, T i_Enum, string i_OptionsHeaderMsg)
        {
            string input;
            bool isValidInput = true;
            int userChoice = 0;

            Console.WriteLine(i_OptionsHeaderMsg);
            Console.WriteLine(i_Message);

            do
            {
                if (!isValidInput)
                {
                    outPutAsistance.InvalidInputTryAgainMsg();
                }

                try
                {
                    input = Console.ReadLine();
                    userChoice = int.Parse(input);
                    isValidInput = Enum.IsDefined(typeof(T), userChoice);
                }
                catch (FormatException)
                {
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return userChoice;
        }

        public void GetVehicleLicenseNumberFromUser(Garage i_Garage, out string i_LicenseNumber)
        {
            do
            {
                Console.WriteLine("Insert vehicle license number please:");
                i_LicenseNumber = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(i_LicenseNumber));
            i_Garage.IsVehicleExists(i_LicenseNumber);
        }

        public string GetVehicleModelName()
        {
            string inputUser;
            do
            {
                Console.WriteLine("Insert vehicle model name please:");
                inputUser = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(inputUser));

            return inputUser;
        }

        public string GetWheelManufacturerFromUser()
        {
            string input;

            do
            {
                Console.WriteLine("Insert wheel manufacturer model name");
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));
            return input;
        }

        public string GetOwnerNameFromUser()
        {
            string input;

            do
            {
                Console.WriteLine(" Please enter the full name of the vehicle owner:");
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));
            return input;
        }

        public string GetOwnerPhoneFromUser()
        {
            string input = null;
            bool isValidInput = true;

            Console.WriteLine("Please enter the owner phone number (Limited to 10 digits)");
            do
            {
                try
                {
                    input = Console.ReadLine();
                    isValidInput = VehicleTreatmentStatus.IsValidPhoneNumber(input);
                }
                catch (FormatException)
                {
                    outPutAsistance.InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
                catch (ValueOutOfRangeException ofr)
                {
                    Console.WriteLine(ofr.Message);
                    Console.WriteLine("try again");
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return input;
        }

        public int GetEngineCapacityFfromUser()
        {
            string input;
            bool isValidInput = false;
            int engineCapacity = 0;

            Console.WriteLine("Please insert capacity of engine of Motorcycle");
            do
            {
                try
                {
                    input = Console.ReadLine();
                    engineCapacity = int.Parse(input);
                    isValidInput = true;
                    if (engineCapacity <= 0)
                    {
                        outPutAsistance.InvalidInputTryAgainMsg();
                        isValidInput = false;
                    }
                }
                catch (FormatException)
                {
                    outPutAsistance.InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
            }
            while (!isValidInput);
            return engineCapacity;
        }

        public float GetVolumeOfCargoFromUusr()
        {
            string input;
            bool isValidInput = false;
            float volumeOfCargo = 0;

            outPutAsistance.PrintStringToScreen("Please insert volume of cargo");
            do
            {
                try
                {
                    input = Console.ReadLine();
                    volumeOfCargo = float.Parse(input);
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    outPutAsistance.InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return volumeOfCargo;
        }

        public float GetDataFloatFromUser(string i_ToPrint)
        {
            string input;
            float result = 0;
            bool isValidInput = false;

            Console.WriteLine("Please enter {0} you want to enter", i_ToPrint);
            do
            {
                try
                {
                    input = Console.ReadLine();
                    result = float.Parse(input);
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    outPutAsistance.InvalidInputTryAgainMsg();
                    isValidInput = false;
                }
            }
            while (!isValidInput);

            return result;
        }
    }
}