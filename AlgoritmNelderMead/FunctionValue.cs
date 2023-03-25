using System;
using System.Linq;

namespace FunctionValue
{
	public class FuncValue
	{
        public FuncValue() { }

        public static double ResultFuncWithLetters(string funcWithLetters, double[] point, int n)
        {
            string func = ReplaceLetters(funcWithLetters, point, n); //получаем арифметическое выражение
            double result = Result(func);
            return result;
        }

        public static string ReplaceLetters(string funcWithLetters, double[] point, int n)
        {
            string[] func = new string[funcWithLetters.Length];
            int j = 0;
            int k = 0;
            //int index;

            while (k < funcWithLetters.Length)  //замена буковок на цифры точки и перевод в массив
            {
                if (funcWithLetters[k] == 'x') //после x стоит индекс нужной координаты в точке)
                {
                    int index = int.Parse(funcWithLetters[k + 1].ToString());
                    if (index < 0 || index >= n) Console.WriteLine("Введен недопустимый индекс ");
                    func[j] = point[index].ToString();
                    j++;
                    k += 2;
                }
                else if (funcWithLetters[k] == ' ') //добавить символы паразиты, которые нужно игнорировать
                {
                    k++;
                }
                else
                {
                    func[j] = funcWithLetters[k].ToString();
                    j++;
                    k++;
                }
            }

            string resultFunc = string.Join("", func);
            return resultFunc;
        }


        public static double Result(string funcStr)
        {
            int k = 0;

            List<string> tempFunc = new List<string>(); //это по сути функция разбитая на массив строк
            List<string> tempList = new List<string>(); //заменить названия (эта хранит число)

            //перевод в массив
            while (k < funcStr.Length)  
            {
                if (funcStr[k] == ' ')
                {
                    k++;
                }
                else if (int.TryParse(funcStr[k].ToString(), out int number) || funcStr[k] == '.')
                {
                    tempList.Add(funcStr[k].ToString());
                    k++;
                }
                else //это оператор
                {
                    if (tempList.Count > 0)
                    {
                        tempFunc.Add(String.Join("", tempList));
                        tempList.Clear();
                    }
                    tempFunc.Add(funcStr[k].ToString());
                    k++;
                }
            }
            if (tempList.Count > 0)
            {
                tempFunc.Add(String.Join("", tempList));
                tempList.Clear();
            }

            string[] funcInPrefix = new string[tempFunc.Count];

            for (int i = 0; i < tempFunc.Count; i++)
            {
                funcInPrefix[i] = tempFunc[i];
                //Console.WriteLine(funcInPrefix[i]);
            }

            string[] func = ConvertToPostfix(funcInPrefix);

            Stack<string> stack = new Stack<string>(); //стек для промежуточной работы

            for (int i = 0; i < func.Length; i++)
            {
                if (double.TryParse(func[i], out double number)) //если текущий символ цифра, в стек
                {
                    stack.Push(func[i]);
                }
                else
                {
                    double sum = 0;
                    switch (func[i])
                    {
                        case "+":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                sum = a + b;
                                break;
                            }
                        case "-":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                sum = b - a;
                                break;
                            }
                        case "*":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                sum = b * a;
                                break;
                            }
                        case "/":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                sum = b / a;
                                break;
                            }
                        case "^":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                sum = Math.Pow(b, a);
                                break;
                            }
                    }
                    stack.Push(sum.ToString());
                }
            }
            double result = Convert.ToDouble(stack.Pop());
            return result;
        }

        public static string[] ConvertToPostfix(string[] func)
        {
            List<string> funcInPostfix = new List<string>(); //строка ответ
            Stack<string> stack = new Stack<string>(); //стек для промежуточной работы

            for (int i = 0; i < func.Length; i++)          
            {
                if (double.TryParse(func[i], out double number)) //если текущий символ цифра, сразу в ответ
                {
                    funcInPostfix.Add(func[i]);
                }
                else //это какой-то оператор
                {
                    if (stack.Count == 0 || func[i] == "(")
                    {
                        stack.Push(func[i]);
                    }
                    else
                    {
                        if (func[i] == ")")
                        {
                            string temp = stack.Pop();

                            while (temp != "(")
                            {
                                funcInPostfix.Add(temp);
                                temp = stack.Pop();
                            }
                        }
                        else if (GetPriority(func[i]) > GetPriority(stack.Peek()))
                        {
                            stack.Push(func[i]);
                        }
                        else
                        {
                            while (stack.Count > 0 && GetPriority(func[i]) <= GetPriority(stack.Peek()))
                            {
                                funcInPostfix.Add(stack.Pop());
                            }
                            stack.Push(func[i]);
                        }
                    }                 
                }
            }
            
            if (stack.Count > 0)
                foreach (string c in stack)
                    funcInPostfix.Add(c);

            string[] result = new string[funcInPostfix.Count];

            for (int j = 0; j < funcInPostfix.Count; j++)
            {
                result[j] = funcInPostfix[j];
            }
            return result;
        }


        public static byte GetPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }
    }
}