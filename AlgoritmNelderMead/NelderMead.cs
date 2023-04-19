using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using org.mariuszgromada.math.mxparser;

namespace AlgoritmNelderMead
{
    public class NelderMead
	{
        public double Func(double[] point, string func)
        {            
            Function f = new Function(func);
            int a = f.getArgumentsNumber();
            for (int i = 0; i < point.Length; i++)
            {
                double temp = point[i];
                f.setArgumentValue(i, temp);
            }
            double result = f.calculate();          
            return result;
        }

        //для шага 3: центр тяжести
        public Point Center(int n, List<Point> points, string func)
        {           
            Point temp = new Point(new double[n], 0);
            for (int i = 0; i < n; i++) // X_worst последний в листе, его не учитываем
            {
                temp = temp + points[i];
            }
            temp = temp / n;

            Point X_centr = new Point(temp.Coordinates, Func(temp.Coordinates, func));
            return X_centr;
        }    

        //для шага 6: сжатие
        public Point Compression(int n, Point X_worst, Point X_centr, double betta, string func)
        {
            Point temp = betta * X_worst + (1 - betta) * X_centr;
            Point X_compression = new Point(temp.Coordinates, Func(temp.Coordinates, func));
            return X_compression;
        }

        //для шага 9: условие останова
        public void CheckingFinal(Points P, int n, List<Point> points, double eps, double alpha, double betta, double gamma, string func)
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
            if (z > eps) { Point answer = BasicCycle(P, eps, n, alpha, betta, gamma, func); }
        }

        //oсновной цикл
        public Point Algoritm(double eps, int n, double alpha, double betta, double gamma, string func)
        {
            //1 шаг: формируем начальный симплекс
            Points P = new Points(n, func);
            return BasicCycle(P, eps, n, alpha, betta, gamma, func);
        }

        //oсновной цикл
        public Point BasicCycle(Points P, double eps, int n, double alpha, double betta, double gamma, string func)
        {        
            //шаг 2: сортирауем точки в порядке возрастания
            P.Sort();

            Point X_best = P.points[0];
            Point X_good = P.points[n - 1];
            Point X_worst = P.points[n];

            //шаг 3: нахождение центра тяжести
            Point X_centr = Center(n, P.points, func);

            //шаг 4: отражение худшей точки относительно центра тяжести
            Point temp1 = (1 + alpha) * X_centr - alpha * X_worst;
            Point X_reflection = new Point(temp1.Coordinates, Func(temp1.Coordinates, func));

            //шаг 5: проверки и измениние
            Point X_compression = P.points[0];

            if (X_reflection.FuncValue < X_best.FuncValue)
            {
                Point temp = (1 - gamma) * X_centr + gamma * X_reflection;
                Point X_elongation = new Point(temp.Coordinates, Func(temp.Coordinates, func));

                if (X_elongation.FuncValue < X_reflection.FuncValue)
                {
                    X_worst = X_elongation;
                    P.points[n] = X_worst;
                    CheckingFinal(P, n, P.points, eps, alpha, betta, gamma, func);
                }
                else if (X_elongation.FuncValue > X_reflection.FuncValue)
                {
                    X_worst = X_reflection;
                    P.points[n] = X_worst;
                    CheckingFinal(P, n, P.points, eps, alpha, betta, gamma, func);
                }
            }
            else
            {
                if (X_best.FuncValue < X_reflection.FuncValue && X_reflection.FuncValue < X_good.FuncValue)
                {
                    X_worst = X_reflection;
                    P.points[n] = X_worst;
                    CheckingFinal(P, n, P.points, eps, alpha, betta, gamma, func);
                }
                else
                {
                    if (X_good.FuncValue < X_reflection.FuncValue && X_reflection.FuncValue < X_worst.FuncValue)
                    {
                        Point temp;
                        temp = X_worst;
                        X_worst = X_reflection;
                        X_reflection = temp;
                        P.points[n] = X_worst;
                        X_compression = Compression(n, X_worst, X_centr, betta, func);
                    }
                    else if (X_worst.FuncValue < X_reflection.FuncValue)
                    {
                        X_compression = Compression(n, X_worst, X_centr, betta, func);
                    }
                }
            }

            //шаг 7: Проверка 
            if (X_compression.FuncValue < X_worst.FuncValue)
            {
                X_worst = X_compression;
                P.points[n] = X_worst;
                CheckingFinal(P, n, P.points, eps, alpha, betta, gamma, func);
            }

            //шаг 8: Глобальное сжатие (вызывает 9)
            if (X_compression.FuncValue > X_worst.FuncValue)
            {
                for (int i = 1; i < n + 1; i++)
                {
                    P.points[i] = X_best + (P.points[i] - X_best) / 2;
                }
            }     

            P.Sort();
            return (P.points[0]);
        }
    }
}