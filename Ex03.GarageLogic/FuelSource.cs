using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelSource : EnergySource
    {
        public enum eMaxFuelCapacity
        {
            Car = 55,
            Motorcycle = 8,
            Truck = 110,
        }

        public enum eFuelType
        { 
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }


        // $G$ DSN-999 (-4) The "fuel type" field should be readonly member of class FuelEnergyProvider.
        private eFuelType m_FuelType;

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public override string GetEnergyMsg()
        {
            return "Please enter the amount of fuel you want to add to the tank:";
        }

        public override string OutOfRangMsg()
        {
            string toPrint;

            toPrint = string.Format(
@"Amount of fuel in the gas tank is out of range
you have {0} liters of gas in your gas tank at this moment 
and at most you can fill up to {1} liters.",
CurrentEnergy,
MaxEnergy);
            return toPrint;
        }

        public override string ToString()
        {
            string toPrint;

            toPrint = string.Format(
@"Current amount of fuel : {0}
Max amount of fuel : {1}
Fuel Type: {2}",
CurrentEnergy,
MaxEnergy,
m_FuelType);
            return toPrint;
        }

        public bool CheckFuelType(eFuelType i_FuelType)
        {
            bool isValidFuelType = true;
            if (i_FuelType != m_FuelType)
            {
                isValidFuelType = false;
            }

            return isValidFuelType;
        }

        public void AddFuel(float i_FuelToEnter, eFuelType i_FuelType )
        {
            bool isValidFuelType = CheckFuelType(i_FuelType);
            if (!isValidFuelType)
            {
                throw new ArgumentException(
                             string.Format(
                             "You enterd an improper fuel type, {0} insted of {1}", i_FuelType, m_FuelType));
            }
            else if (CurrentEnergy + i_FuelToEnter > MaxEnergy || CurrentEnergy + i_FuelToEnter < 0)
                {
                    throw new ValueOutOfRangeException(0, MaxEnergy);
                }

            CurrentEnergy += i_FuelToEnter;
        }
    }
}