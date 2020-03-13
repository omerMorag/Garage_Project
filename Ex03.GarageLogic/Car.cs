using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_QuantityOfWheels = 4;
        private const float k_MaxBatteryTime = 1.8f;

        public enum eColor
        {
            Red = 1,
            Blue,
            Black,
            Grey
        }

        public enum eNumberOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private eColor m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        public Car(string i_LicenseNumber, string i_ModelName, string i_WheelManufacturerName, EnergySource.eSource i_energySource)
            : base(i_ModelName, i_LicenseNumber, i_energySource)
        {
            for (int i = 0; i < k_QuantityOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturerName, (float)Wheel.eMaxAirPressure.Car));
            }

            SetEnergySource();
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public eColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public override void SetEnergySource()
        {
            if (EnergySource is FuelSource)
            {
                ((FuelSource)EnergySource).FuelType = FuelSource.eFuelType.Octan96;
                EnergySource.MaxEnergy = (float)FuelSource.eMaxFuelCapacity.Car;
            }
            else
            {
                EnergySource.MaxEnergy = k_MaxBatteryTime;
            }
        }

        public override string ToString()
        {
            string toPrint;

            toPrint = string.Format(
@"{0}
Car's Color: {1}
Car's door quantity: {2}
",
VehicleDetails(),
m_Color.ToString(),
m_NumberOfDoors.ToString());
            return toPrint;
        }
    }
}