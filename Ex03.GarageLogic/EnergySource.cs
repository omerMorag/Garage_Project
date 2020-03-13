using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public enum eSource
        {
            FuelSource = 1,
            ElectricSource,
        }

        private float m_CurrentEnergy;
        private float m_MaxEnergy;

        public float MaxEnergy
        {
            get { return m_MaxEnergy; }
            set { m_MaxEnergy = value; }
        }

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
            set { m_CurrentEnergy = value; }
        }

        public void UpdateEnergy(float i_EnergyToEnter)
        {
            if (m_CurrentEnergy + i_EnergyToEnter > m_MaxEnergy
                || m_CurrentEnergy + i_EnergyToEnter < 0)
            {
                throw new ValueOutOfRangeException(0, m_MaxEnergy);
            }

            m_CurrentEnergy += i_EnergyToEnter;
        }

        public abstract string GetEnergyMsg();

        public abstract string OutOfRangMsg();

        public abstract override string ToString();
    }
}