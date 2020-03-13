using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public enum eTruckContain
        {
            ContainsHazardousSubstances = 1,
            NotContainsHazardousSubstances,
        }

        private const int k_QuantityOfWheels = 12;
        private bool m_isContainsHazardousSubstances;
        private float m_CapicityCargo;

        public Truck(string i_LicenseNumber, string i_ModelName, string i_WheelManufacturerName, EnergySource.eSource i_energySource)
            : base(i_ModelName, i_LicenseNumber, i_energySource)
        {
            for (int i = 0; i < k_QuantityOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturerName, (float)Wheel.eMaxAirPressure.Truck));
            }

            SetEnergySource();
        }

        public bool isContainsHazardousSubstances
        {
            get { return m_isContainsHazardousSubstances; }
            set { m_isContainsHazardousSubstances = value; }
        }

        public float CapicityCargo
        {
            get { return m_CapicityCargo; }
            set { m_CapicityCargo = value; }
        }

        public override void SetEnergySource()
        {
            ((FuelSource)EnergySource).FuelType = FuelSource.eFuelType.Soler;
            EnergySource.MaxEnergy = (float)FuelSource.eMaxFuelCapacity.Truck;
        }

        public override string ToString()
        {
            string toPrint;

            toPrint = string.Format(
@"{0}
Is the truck cargo cooled: {1}
Truck's volume of cargo is: {2}",
VehicleDetails(),
m_isContainsHazardousSubstances,
m_CapicityCargo);
            return toPrint;
        }
    }
}