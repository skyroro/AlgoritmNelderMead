using AlgoritmNelderMead;

NelderMead NelderMead = new NelderMead();

/* 
Console.WriteLine("Введите выражение: ");
string? func = Console.ReadLine();
if (string.IsNullOrEmpty(F)) return;
*/
string func = "x0^2 + x0*x1 + x1^2 - 6*x0 - 9*x1";
/*
Console.WriteLine("Введите количество переменных: n >= 2");
int n = Convert.ToInt32(Console.ReadLine());
*/
int n = 2;

double eps = 0.00001;//погрешность
double alpha = 1; //коэффициент отражения
double betta = 0.5; //коэффициент сжатия
double gamma = 2; //коэффициент растяжения


Point point = NelderMead.Algoritm(eps, n, alpha, betta, gamma, func);

for (int i = 0; i < n; i++)
{
    Console.Write(point.Coordinates[i] + " ");
}

Console.WriteLine(" ");
Console.WriteLine("Результат: " + point.FuncValue);