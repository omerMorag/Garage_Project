using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eTypeOfVehicle
        {
            Car = 1,
            Motorcycle,
            Truck
        }

        private readonly EnergySource m_EnergySource;
        private readonly string m_ModelName;
        private readonly string m_LicenseNumber;
        private float m_CurrentEnergyPercent;

        // $G$ DSN-999 (-3) This List should be readonly.
        private List<Wheel> m_Wheels;
      
        public Vehicle(string i_ModelName, string i_LicensePlate, EnergySource.eSource i_Source)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicensePlate;
            m_Wheels = new List<Wheel>();

            if (i_Source == EnergySource.eSource.ElectricSource)
            {
                m_EnergySource = new ElectricSource();
            }
            else
            {
                m_EnergySource = new FuelSource();
            }
        }

        public string ModelName
        {
            get { return m_ModelName; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
        }

        public float CurrentEnergyPercent
        {
            get { return m_CurrentEnergyPercent; }
            set { m_CurrentEnergyPercent = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
        }

        public abstract void SetEnergySource();

        public string VehicleDetails()
        {
            string result;

            result = string.Format(
@"Vehicel license Number: {0}
Vehicel model name: {1}
Wheels information: 
{2}
Energy meter: {3}%
{4}",
m_LicenseNumber,
m_ModelName,
m_Wheels[0].ToString(),
m_CurrentEnergyPercent,
m_EnergySource.ToString());
            return result;
        }

        public void UpdateCurretEnergyPercent()
        {
            m_CurrentEnergyPercent = (EnergySource.CurrentEnergy / EnergySource.MaxEnergy) * 100;
        }

        public abstract override string ToString();
    }
}