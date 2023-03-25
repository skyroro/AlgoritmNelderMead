using System;
using System.Drawing;
using FunctionValue;

namespace AlgoritmNelderMead
{
	public class NelderMead
	{
        //для получения значения функции в точке
        public double Func(double[] point, string func)
        {
            //double result = Math.Pow(point[0], 2) + point[0] * point[1] + Math.Pow(point[1], 2) - 6 * point[0] - 9 * point[1];
            double result = FuncValue.ResultFuncWithLetters(func, point, point.Length);
            return result;
        }

        //для шага 1: подготовка 
        public List<Point> initalPoints(int n, string func)
        {
            List<Point> points = new List<Point>();

            //добавление начальной точки
            double[] coordinates = new double[n];
            for (int i = 0; i < n; i++)
            {
                coordinates[i] = 0;
            }
            Point point0 = new Point(coordinates, Func(coordinates, func));
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

                Point point = new Point(temp, Func(temp, func));
                points.Add(point);
            }

            return points;
        }

        //для шага 2: сортировка
        public List<Point> Sort(List<Point> points)
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

            return points;
        }

        //для шага 3: центр тяжести
        public Point Center(int n, List<Point> points, string func)
        {
            double[] temp = new double[n];

            for (int i = 0; i < n; i++) //X_worst последний в листе, мы его не считаем
            {
                for (int j = 0; j < n; j++)
                {
                    temp[j] = temp[j] + points[i].Coordinates[j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                temp[i] = temp[i] / n;
            }

            Point X_centr = new Point(temp, Func(temp, func));
            return X_centr;
        }

        //для шага 4: отражение
        public Point Reflection(int n, Point X_centr, Point X_worst, double alpha, string func)
        {
            double[] temp = new double[n];

            for (int i = 0; i < n; i++)
            {
                temp[i] = (1 + alpha) * X_centr.Coordinates[i] - alpha * X_worst.Coordinates[i];
            }

            Point X_reflection = new Point(temp, Func(temp, func));
            return X_reflection;
        }

        //для шага 9: условие останова
        public bool Final(int n, List<Point> points, double eps, string func)
        {
            Point X_center = Center(n, points, func);
            double cValue = X_center.FuncValue;

            double fsum = 0;

            for (int i = 0; i < n + 1; i++)
            {
                double pointValue = points[i].FuncValue;
                fsum = fsum + Math.Pow(pointValue - cValue, 2);
            }

            double z = Math.Sqrt(1.0 / (n + 1) * fsum);

            if (z <= eps) return true;
            else return false;
        }

        //для шага 6: сжатие
        public Point Compression(int n, Point X_worst, Point X_centr, double betta, string func)
        {
            double[] temp = new double[n];

            for (int i = 0; i < n; i++)
            {
                temp[i] = betta * X_worst.Coordinates[i] + (1 - betta) * X_centr.Coordinates[i];
            }

            Point X_compression = new Point(temp, Func(temp, func));
            return X_compression;
        }


        //oсновной цикл
        public Point Algoritm(double eps, int n, double alpha, double betta, double gamma, string func)
        {
            List<Point> points = new List<Point>();

            //1 шаг: формируем начальный симплекс
            points = initalPoints(n, func);

            //основной цикл
            while (!Final(n, points, eps, func))
            {
                //в качестве проверки окончания алгоритма вызывается шаг 9
                //шаг 2: сортирауем точки в порядке возрастания
                points = Sort(points);

                Point X_best = points[0];
                Point X_good = points[n - 1];
                Point X_worst = points[n];

                //шаг 3: нахождение центра тяжести
                Point X_centr = Center(n, points, func);

                //шаг 4: отражение худшей точки относительно центра тяжести
                Point X_reflection = Reflection(n, X_centr, X_worst, alpha, func);

                //шаг 5: проверки и измениние
                Point X_compression = points[0];

                if (X_reflection.FuncValue < X_best.FuncValue)
                {
                    double[] x_elongation = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        x_elongation[i] = (1 - gamma) * X_centr.Coordinates[i] + gamma * X_reflection.Coordinates[i];
                    }
                    Point X_elongation = new Point(x_elongation, Func(x_elongation, func));

                    if (X_elongation.FuncValue < X_reflection.FuncValue)
                    {
                        X_worst = X_elongation;
                        points[n] = X_worst;
                        //переход на шаг 9
                        continue;
                    }
                    else if (X_elongation.FuncValue > X_reflection.FuncValue)
                    {
                        X_worst = X_reflection;
                        points[n] = X_worst;
                        //переход на шаг 9
                        continue;
                    }
                }
                else
                {
                    if (X_best.FuncValue < X_reflection.FuncValue && X_reflection.FuncValue < X_good.FuncValue)
                    {
                        X_worst = X_reflection;
                        points[n] = X_worst;
                        //переход на шаг 9
                        continue;
                    }
                    else
                    {
                        if (X_good.FuncValue < X_reflection.FuncValue && X_reflection.FuncValue < X_worst.FuncValue)
                        {
                            Point temp;
                            temp = X_worst;
                            X_worst = X_reflection;
                            X_reflection = temp;
                            points[n] = X_worst;

                            //переход на шаг 6
                            X_compression = Compression(n, X_worst, X_centr, betta, func);
                        }
                        else if (X_worst.FuncValue < X_reflection.FuncValue)
                        {
                            //переход на шаг 6
                            X_compression = Compression(n, X_worst, X_centr, betta, func);
                        }
                    }
                }

                //шаг 7: Проверка 
                if (X_compression.FuncValue < X_worst.FuncValue)
                {
                    X_worst = X_compression;
                    points[n] = X_worst;
                    //переход на шаг 9
                    continue;
                }

                //шаг 8: Глобальное сжатие (вызывает 9)
                if (X_compression.FuncValue > X_worst.FuncValue)
                {
                    for (int i = 1; i < n + 1; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            points[i].Coordinates[j] = X_best.Coordinates[j] + (points[i].Coordinates[j] - X_best.Coordinates[j]) / 2;
                        }
                    }
                }
            }

            Sort(points);
            return (points[0]);
        }
    }
}

