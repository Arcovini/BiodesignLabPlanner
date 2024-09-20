using System;
using System.Globalization;

namespace BiodesignLab
{
    public static class TypeConverter
    {
        public static float StringToFloat(string value)
        {
            return Convert.ToSingle(value, CultureInfo.InvariantCulture);
        }

        public static float[] StringArrayToFloat(string[] values)
        {
            float[] floatValues = new float[values.Length];

            for(int i = 0; i < values.Length; ++i)
            {
                floatValues[i] = Convert.ToSingle(values[i], CultureInfo.InvariantCulture);
            }

            return floatValues;
        }
    }
}