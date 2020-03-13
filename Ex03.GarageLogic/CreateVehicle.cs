using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class CreateVehicle
    {
        public static Vehicle CreateNewVehicle(Vehicle.eTypeOfVehicle i_TypeOfVehicle, EnergySource.eSource i_SourceOfEnergy, string i_LicenseNumber, string i_ModelName, string i_WheelManufacturerName)
        {
            Vehicle newVehicle = null;

            if (Vehicle.eTypeOfVehicle.Motorcycle == i_TypeOfVehicle)
            {
                newVehicle = new Motorcycle(i_LicenseNumber, i_ModelName, i_WheelManufacturerName, i_SourceOfEnergy);
            }

            if (Vehicle.eTypeOfVehicle.Car == i_TypeOfVehicle)
            {
                newVehicle = new Car(i_LicenseNumber, i_ModelName, i_WheelManufacturerName, i_SourceOfEnergy);
            }

            if (Vehicle.eTypeOfVehicle.Truck == i_TypeOfVehicle)
            {
                newVehicle = new Truck(i_LicenseNumber, i_ModelName, i_WheelManufacturerName, i_SourceOfEnergy);
            }

            return newVehicle;
        }
    }
}