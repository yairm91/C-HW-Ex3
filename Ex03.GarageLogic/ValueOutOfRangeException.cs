﻿using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        internal float MinValue
        {
            get { return m_MinValue; }
        }

        internal float MaxValue
        {
            get { return m_MaxValue; }
        }

        public ValueOutOfRangeException(float i_MinimumValue, float i_MaximumValue)
            : base(string.Format(
                "the input is not in the range between {0} to {1}",
                i_MinimumValue,
                i_MaximumValue))
        {
            m_MaxValue = i_MaximumValue;
            m_MinValue = i_MinimumValue;
        }
    }
}
