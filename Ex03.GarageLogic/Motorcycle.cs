using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_QuantityOfWheels = 2;
        private const float k_MaxBatteryTime = 1.4f;

        public enum eLisenceType
        {
            A = 1,
            A1,
            A2,
            B,
        }

        private eLisenceType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(string i_LicenseNumber, string i_ModelName, string i_WheelManufacturerName, EnergySource.eSource i_EnergySource)
            : base(i_ModelName, i_LicenseNumber, i_EnergySource)
        {
            for (int i = 0; i < k_QuantityOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturerName, (float)Wheel.eMaxAirPressure.Motorcycle));
            }

            SetEnergySource();
        }

        public eLisenceType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override void SetEnergySource()
        {
            if (EnergySource is FuelSource)
            {
                ((FuelSource)EnergySource).FuelType = FuelSource.eFuelType.Octan95;
                EnergySource.MaxEnergy = (int)FuelSource.eMaxFuelCapacity.Motorcycle;
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
Motorcycle's license type: {1}
Motorcycle's engine cpacity: {2}",
VehicleDetails(),
m_LicenseType.ToString(),
m_EngineCapacity);
            return toPrint;
        }
    }
}