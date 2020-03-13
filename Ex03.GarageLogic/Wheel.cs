using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public enum eMaxAirPressure
        {
            Truck = 26,
            Car = 31,
            Motorcycle = 33,
        }

        private readonly string m_ManufacturerName;
        private float m_CurrentAirPressure;


        // $G$ DSN-999 (-4) The "maximum air pressure" field should be readonly member of class wheel.
        private float m_MaxAirPressure;

        public Wheel(string i_Manufacturer, float i_MaxAirPressure)
        {
            m_CurrentAirPressure = 0;
            m_ManufacturerName = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
            set { m_MaxAirPressure = value; }
        }

        public string Manufacturer
        {
            get { return m_ManufacturerName; }
        }

        public bool AddAirPressure(float i_airPressure)
        {
            bool isValid = true;
            if (CurrentAirPressure + i_airPressure < 0 || CurrentAirPressure + i_airPressure > MaxAirPressure)
            {
                isValid = false;
            }

            if (isValid)
            {
                CurrentAirPressure += i_airPressure;
            }

            return isValid;
        }

        public override string ToString()
        {
            string toPrint;

            toPrint = string.Format(
@"Current air pressure: {0}
Manufacturer name: {1}",
m_CurrentAirPressure,
m_ManufacturerName);

            return toPrint;
        }
    }
}