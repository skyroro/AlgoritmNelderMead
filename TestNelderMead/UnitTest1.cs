using System;
using System.Drawing;
using AlgoritmNelderMead;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestNelderMead;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        AlgoritmNelderMead.NelderMead NM = new NelderMead();

        string func = "f(x0,x1) = x0^2 + x0*x1 + x1^2 - 6*x0 - 9*x1";
        double eps = 0.00001;//погрешность
        double alpha = 1; //коэффициент отражения
        double betta = 0.5; //коэффициент сжатия
        double gamma = 2; //коэффициент растяжения
        int n = 2;
        AlgoritmNelderMead.Point point = NM.Algoritm(eps, n, alpha, betta, gamma, func);

        double result = (int)(point.FuncValue);
        double expected = (int)(-20.99999);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TestMethod2() //функция Химмельблау
    {
        AlgoritmNelderMead.NelderMead NM = new NelderMead();

        string func = "f(x0,x1) = (x0 ^ 2 + x1 - 11) ^ 2 + (x0 + x1 ^ 2 - 7) ^ 2";
        double eps = 0.00001;//погрешность
        double alpha = 1; //коэффициент отражения
        double betta = 0.5; //коэффициент сжатия
        double gamma = 2; //коэффициент растяжения
        int n = 2;
        AlgoritmNelderMead.Point point = NM.Algoritm(eps, n, alpha, betta, gamma, func);

        double result = (int)(point.FuncValue);
        double expected = (int)(0);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TestMethod3() //функция Розенброка
    {
        AlgoritmNelderMead.NelderMead NM = new NelderMead();

        string func = "f(x0,x1) = (1 - x0)^2 + 100*(x1 - x0^2)^2";
        double eps = 0.00001;//погрешность
        double alpha = 1; //коэффициент отражения
        double betta = 0.5; //коэффициент сжатия
        double gamma = 2; //коэффициент растяжения
        int n = 2;
        AlgoritmNelderMead.Point point = NM.Algoritm(eps, n, alpha, betta, gamma, func);

        double result = (int)(point.FuncValue);
        double expected = (int)(0);
        Assert.AreEqual(expected, result);
    }
}
