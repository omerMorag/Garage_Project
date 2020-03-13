using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        // $G$ CSS-003 (-5) Bad readonly members variable name (should be in the form of r_PamelCase).
        private readonly Dictionary<string, VehicleTreatmentStatus> m_AllCarsInGarage = new Dictionary<string, VehicleTreatmentStatus>();

        public Dictionary<string, VehicleTreatmentStatus> AllCarsInGarage
        {
            get { return m_AllCarsInGarage; }
        }

        public void InsertNewVehicleToGarage(Vehicle i_NewVehicle, string i_OwnerName, string i_OwnerPhone)
        {
            VehicleTreatmentStatus VehicleToAdd = new VehicleTreatmentStatus(i_NewVehicle, i_OwnerName, i_OwnerPhone);

            m_AllCarsInGarage.Add(i_NewVehicle.LicenseNumber, VehicleToAdd);
        }

        public void ChangeStatus(string i_LicenseNumber, VehicleTreatmentStatus.eStatus i_NewStatus)
        {
            m_AllCarsInGarage[i_LicenseNumber].Status = i_NewStatus;
        }

        public void IsVehicleExists(string LicenseNumber)
        {
            if (m_AllCarsInGarage.ContainsKey(LicenseNumber) == false)
            {
                throw new ArgumentException(
                    string.Format(
                    "the vehicle with the license number {0} doesnt exists in the garage",
                    LicenseNumber));
            }
        }
    }
}