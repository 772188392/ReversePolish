using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library_Calculate
{
    public class Calculate
    {
        Calculate operate;
        public void setOperate(Calculate operate)
        {
            this.operate = operate;
        }
        public virtual double Operate(double num1, double num2)
        {
            if (operate != null)
            {
                switch (operate.GetType().Name)
                {
                    case "Operate_Add":
                        break;
                    default:
                        break;
                }
                return operate.Operate(num1, num2);
            }
            return 0;
        }
    }
    public class Operate_Add : Calculate
    {
        public override double Operate(double num1, double num2)
        {

            Assembly assem = Assembly.Load("Library_Calculate");
            Type type = assem.GetType("Library_Calculate.Operate_Add");
            // 创建无参数实例
            Calculate class_assembly = (Calculate)type.InvokeMember("Operate_Add", BindingFlags.CreateInstance, null, null, null);
            base.setOperate(class_assembly);
            return base.Operate(num1, num2);
        }
    }
}
