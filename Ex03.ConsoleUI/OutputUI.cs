using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class OutputUI
    {
        public string buildEnumList(string[] i_enumNames)
        {
            StringBuilder result = new StringBuilder();
            int currentIndex = 1;

            foreach (string currentEnum in i_enumNames)
            {
                result.Append(string.Format("{0}. {1} {2}", currentIndex++, currentEnum, Environment.NewLine));
            }

            return CreateSpaces(result.ToString());
        }

        public string CreateSpaces(string i_input)
        {
            StringBuilder result = new StringBuilder();
            char previous = i_input[0];

            foreach (char currentChar in i_input)
            {
                if (currentChar >= 'A' && currentChar <= 'Z' && (previous >= 'a' && previous <= 'z'))
                {
                    result.Append(' ');
                }

                result.Append(currentChar);
                previous = currentChar;
            }

            return result.ToString();
        }

        public void InvalidInputTryAgainMsg()
        {
            Console.WriteLine("Invalid input, try again:");
        }

        public void PrintStringToScreen(string i_ToPrint)
        {
            Console.WriteLine(i_ToPrint);
        }

        internal void VehicleIsAlreadyExsistsMsg()
        {
            Console.WriteLine("The vehicle exsits in the system, his status changed to in treatment");
        }

        internal void SomethingWrongMsg()
        {
            Console.WriteLine("Something went wrong. Please try another input");
        }

        public void PrintLicenseNumber(int i_UserChoice, Dictionary<string, VehicleTreatmentStatus> i_TreatmentList, bool i_DisplayAll)
        {
            VehicleTreatmentStatus.eStatus wantedType = (VehicleTreatmentStatus.eStatus)i_UserChoice;
            Console.WriteLine("The list is:");
            foreach (VehicleTreatmentStatus current in i_TreatmentList.Values)
            {
                if (current.Status == wantedType || i_DisplayAll)
                {
                    Console.WriteLine(current.Vehicle.LicenseNumber);
                }
            }
        }
    }
}
