using System;
namespace AlgoritmNelderMead
{
    public class Points
	{
		public List<Point> points;

        public Points(int n, string func)
		{
            AlgoritmNelderMead.NelderMead NM = new NelderMead();
            points = new List<Point>();

            //добавление начальной точки
            double[] coordinates = new double[n];
            for (int i = 0; i < n; i++)
            {
                coordinates[i] = 0;
            }
            Point point0 = new Point(coordinates, NM.Func(coordinates, func));
            points.Add(point0);

            //генрация всех остальных точек
            Random random = new Random();
            for (int j = 1; j < n + 1; j++)
            {
                double[] temp = new double[n];

                for (int i = 0; i < n; i++)
                {
                    temp[i] = random.NextDouble() + point0.Coordinates[i];
                }

                Point point = new Point(temp, NM.Func(temp, func));
                points.Add(point);
            }
        }

        public void Sort()
        {
            for (int i = 0; i < points.Count(); i++)
            {
                for (int j = 0; j < points.Count(); j++)
                {
                    if (points[i].FuncValue < points[j].FuncValue && i != j)
                    {
                        Point temp;
                        temp = points[i];
                        points[i] = points[j];
                        points[j] = temp;
                    }
                }
            }
        }
    }
}