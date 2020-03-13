using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricSource : EnergySource
    {
        public override string GetEnergyMsg()
        {
            return "Please enter the amount of battery time you want to add";
        }

        public override string OutOfRangMsg()
        {
            string result;

            result = string.Format(
@"your battery was about to get charged inappropriately,
your battery has {0} hour's of runuing time left
and at most you can charge the battery up to {1} hour's.",
CurrentEnergy,
MaxEnergy);
            return result;
        }

        public override string ToString()
        {
            return string.Format(
@"Battery running time left : {0}
Max battery running time : {1}",
CurrentEnergy,
MaxEnergy);
        }

        public void AddHoursToBatery(float i_MinuteslToEnter)
        {
            float convertToHour = i_MinuteslToEnter / (float)60;
            if (CurrentEnergy + convertToHour > MaxEnergy || CurrentEnergy + convertToHour < 0)
            {
                throw new ValueOutOfRangeException(0, MaxEnergy);
            }
            else
            {
                CurrentEnergy += convertToHour;
            }
        }
    }
}