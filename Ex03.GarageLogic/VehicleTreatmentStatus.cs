using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleTreatmentStatus
    {
        private const int k_ValidPhoneNumberLength = 10;
        private const int k_MinPhoneNumberLength = 1;

        public enum eStatus
        {
            InTreatment = 1,
            Fixed,
            Paid,
        }

        private eStatus m_Status;
        private Vehicle m_Vehicle;

        private string m_OwnerName;
        private string m_OwnerPhone;

        public VehicleTreatmentStatus(Vehicle i_Vehcile, string i_OwnerName, string i_OwnerPhone)
        {
            m_Vehicle = i_Vehcile;
            m_Status = eStatus.InTreatment;
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
            set { m_OwnerPhone = value; }
        }

        public eStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        public static bool IsValidPhoneNumber(string i_Input)
        {
            long.Parse(i_Input);
            if (i_Input.Length > 10)
            {
                throw new ValueOutOfRangeException(k_MinPhoneNumberLength, k_ValidPhoneNumberLength);
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder ToPrint = new StringBuilder();
            string Details;

            Details = string.Format(
@"Owner name: {0}
Owner phone: {1}
Vehicle status: {2}
",
m_OwnerName,
m_OwnerPhone,
m_Status.ToString());
            ToPrint.Append(Details);
            ToPrint.Append(m_Vehicle.ToString());
            return ToPrint.ToString();
        }

        public void CompareStatus(eStatus userChoice)
        {
            if ((VehicleTreatmentStatus.eStatus)userChoice == m_Status)
            {
                throw new ArgumentException(
                    string.Format(
                    "The vehicle is already in {0} status", m_Status));
            }
        }
    }
}