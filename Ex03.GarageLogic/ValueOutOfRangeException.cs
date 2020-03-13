using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public float MaxValidValue
        {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }

        public float MinValidValue
        {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }

        public ValueOutOfRangeException(float i_MinValidValue, float i_MaxValidValue)
            : base(
                string.Format("The value you entered is invalid. Value should be between {0} to {1}", i_MinValidValue, i_MaxValidValue))
        {
            m_MaxValue = i_MaxValidValue;
            m_MinValue = i_MinValidValue;
        }
    }
}