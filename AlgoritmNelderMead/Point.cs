using System;
namespace AlgoritmNelderMead
{
    public class Point
    {
        double[] coordinates;
        double funcValue;

        public double FuncValue { get { return funcValue; } set { funcValue = value; } }
        public double[] Coordinates { get { return coordinates; } set { coordinates = value; } }

        public Point(double[] coordinates, double funcValue)
        {
            this.coordinates = coordinates;
            this.funcValue = funcValue;
        }
    }
}

