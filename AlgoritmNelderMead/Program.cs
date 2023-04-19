using System;
using System.Drawing;
using AlgoritmNelderMead;
using org.mariuszgromada.math.mxparser;

NelderMead NelderMead = new NelderMead();
bool isCallSuccessful = License.iConfirmNonCommercialUse("skyroro");

//string func = "f(x,y)=(x^2+y-11)^2+(x+y^2-7)^2"; //функция Химмельблау
string func = "f(x0,x1) = (1 - x0)^2 + 100*(x1 - x0^2)^2";

int n = 2;

double eps = 0.00001;//погрешность
double alpha = 1; //коэффициент отражения
double betta = 0.5; //коэффициент сжатия
double gamma = 2; //коэффициент растяжения

AlgoritmNelderMead.Point point = NelderMead.Algoritm(eps, n, alpha, betta, gamma, func);

for (int i = 0; i < n; i++) //вывод координат полученной точки
{
    Console.Write(point.Coordinates[i] + " ");
}

Console.WriteLine(" ");
Console.WriteLine("Результат: " + point.FuncValue);
Console.WriteLine("Результат int: " + (int)(point.FuncValue));