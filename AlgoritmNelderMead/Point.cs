using System;
namespace AlgoritmNelderMead
{
    public class Point
    {
        double[] coordinates;
        double funcValue;

        public double FuncValue
        {
            get { return funcValue; }
            private set { funcValue = value; }
        }

        public double[] Coordinates
        {
            get { return coordinates; }
            private set { coordinates = value; }
        }

        public Point(double[] coordinates, double funcValue)
        {
            this.coordinates = coordinates;
            this.funcValue = funcValue;
        }

        public static Point operator *(double coefficient, Point x)
        {
            double[] temp = new double[x.coordinates.Length];
            for (int i = 0; i < x.coordinates.Length; i++)
            {
                temp[i] = coefficient * x.coordinates[i];
            }
            return new Point(temp, x.funcValue);
        }

        public static Point operator /(Point x, double coefficient)
        {
            double[] temp = new double[x.coordinates.Length];
            for (int i = 0; i < x.coordinates.Length; i++)
            {
                temp[i] = x.coordinates[i] / coefficient;
            }
            return new Point(temp, x.funcValue);
        }

        public static Point operator -(Point x1, Point x2)
        {
            double[] temp = new double[x1.coordinates.Length];
            for (int i = 0; i < x1.coordinates.Length; i++)
            {
                temp[i] = x1.coordinates[i] - x2.coordinates[i];
            }
            return new Point(temp, x1.funcValue);
        }

        public static Point operator +(Point x1, Point x2)
        {
            double[] temp = new double[x1.coordinates.Length];
            for (int i = 0; i < x1.coordinates.Length; i++)
            {
                temp[i] = x1.coordinates[i] + x2.coordinates[i];
            }
            return new Point(temp, x1.funcValue);
        }
    }
}