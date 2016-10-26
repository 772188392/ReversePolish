using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {

        static void Main(string[] args)
        { 
            string str = "sdfsafsd";
            Library_Calculate.Calculate cal = new Library_Calculate.Calculate();
            Library_Calculate.Operate_Add cal_add = new Library_Calculate.Operate_Add();
            double d = cal_add.Operate(1, 4);
            Console.WriteLine(d);
            //Console.WriteLine("输入中缀表达式：");
            //string str = Console.ReadLine();
            //string result1 = ClearUp(str);
            //Console.WriteLine("计算所得后缀表达式：" + result1);
            //string result2 = Calculate(result1);
            //Console.WriteLine("计算结果：" + result2);
            Console.ReadLine();
        }

        public static Dictionary<string, int> dic = new Dictionary<string, int>() { { "+", 1 }, { "-", 1 }, { "*", 2 }, { "/", 2 } };//权重

        //逆波兰公式（推算后缀表达式）
        static string ClearUp(string str)
        {
            List<string> list = new List<string>();
            List<string> list_tmp = new List<string>();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb_tmp = new StringBuilder();
            bool status = true;//状态标示（在元素前添加空格标示完整元素）
            foreach (char item in str)
            {
                switch (item)
                {
                    case '+':
                        status = false;
                        Fun_Formula(item, list, sb, status);
                        break;
                    case '-':
                        status = false;
                        Fun_Formula(item, list, sb, status);
                        break;
                    case '*':
                        status = false;
                        Fun_Formula(item, list, sb, status);
                        break;
                    case '/':
                        status = false;
                        Fun_Formula(item, list, sb, status);
                        break;
                    case '('://使用临时栈保存原有数据（开辟新栈计算片段）
                        status = false;
                        for (int i = 0; i < list.Count; i++)
                        {
                            list_tmp.Add(list[i]);
                        }
                        list.Clear();
                        sb_tmp.Append(sb.ToString());
                        sb.Clear();
                        break;
                    case ')'://结束新内存（赋值给主栈）
                        status = false;
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            sb.Append(" " + list[i]);
                            list.RemoveAt(i);
                        }
                        for (int i = list_tmp.Count - 1; i >= 0; i--)
                        {
                            list.Add(list_tmp[i]);
                        }
                        list_tmp.Clear();
                        sb_tmp.Append(sb.ToString());
                        sb.Clear();
                        sb.Append(sb_tmp.ToString());
                        sb_tmp.Clear();
                        break;
                    default:
                        status = true;
                        sb.Append(item);
                        break;
                }
            }
            for (int i = list.Count - 1; i >= 0; i--)//计算结束栈内元素依次出栈
            {
                sb.Append(" " + list[i]);
            }
            return sb.ToString();
        }
        static void Fun_Formula(char item, List<string> list, StringBuilder sb, bool status)
        {
            if (list.Count == 0)
            {
                list.Add(item.ToString());
            }
            else
            {
                if (dic[item.ToString()] < dic[list[list.Count - 1].ToString()])//权重小于栈顶运算符全部出栈并新运算符进栈
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        sb.Append(" " + list[i]);
                        list.RemoveAt(i);
                    }
                    list.Add(item.ToString());
                }
                else if (dic[item.ToString()] == dic[list[list.Count - 1].ToString()])//权重一致进栈
                {
                    list.Add(item.ToString());
                }
                else
                {
                    list.Add(item.ToString());//权重大于栈顶元素进栈
                }
            }
            if (!status)
            {
                sb.Append(" ");
            }
        }
        //根据后缀表达式计算（以空格区分元素）
        static string Calculate(string str)
        {
            List<string> list = new List<string>();
            double result;
            foreach (string item in str.Split(' '))
            {
                switch (item)
                {
                    case "+":
                        result = double.Parse(list[list.Count - 2]) + double.Parse(list[list.Count - 1]);
                        list.RemoveAt(list.Count - 1);
                        list.RemoveAt(list.Count - 1);
                        list.Add(result.ToString());
                        break;
                    case "-":
                        result = double.Parse(list[list.Count - 2]) - double.Parse(list[list.Count - 1]);
                        list.RemoveAt(list.Count - 1);
                        list.RemoveAt(list.Count - 1);
                        list.Add(result.ToString());
                        break;
                    case "*":
                        result = double.Parse(list[list.Count - 2]) * double.Parse(list[list.Count - 1]);
                        list.RemoveAt(list.Count - 1);
                        list.RemoveAt(list.Count - 1);
                        list.Add(result.ToString());
                        break;
                    case "/":
                        result = double.Parse(list[list.Count - 2]) / double.Parse(list[list.Count - 1]);
                        list.RemoveAt(list.Count - 1);
                        list.RemoveAt(list.Count - 1);
                        list.Add(result.ToString());
                        break;
                    default:
                        list.Add(item.ToString());
                        break;
                }
            }
            return list[0].ToString();
        }
    }
}
