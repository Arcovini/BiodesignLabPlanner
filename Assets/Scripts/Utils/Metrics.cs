using UnityEngine;

namespace BiodesignLab
{
    public static class Metrics
    {
        public static float Kilometers(this float value)
        {
            return value * 1000.0f;
        }

        public static float Hectometers(this float value)
        {
            return value * 100.0f;
        }

        public static float Decameters(this float value)
        {
            return value * 10.0f;
        }

        public static float Decimeters(this float value)
        {
            return value / 10.0f;
        }

        public static float Centimeters(this float value)
        {
            return value / 100.0f;
        }

        public static float Millimeters(this float value)
        {
            return value / 1000.0f;
        }
    }
}