using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ValueOutOfRangeException : Exception
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

        public ValueOutOfRangeException(int i_MinimumValue, int i_MaximumValue)
            : base(string.Format(
                "the input is not in the range between {0} to {1}",
                i_MinimumValue,
                i_MaximumValue))
        {
        }
    }

}
