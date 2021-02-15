using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    abstract class Calculator : ICalculator
    {
        public virtual async Task AddEmp(Data empInfo)
        {
            await Task.Delay(1);
        }

        public async Task CalculateCTC(Data empInfo)
        {
           Console.Write(":");  //consumer
           await Task.Delay(1);
        }

        public virtual ICalculator Clone(int p)
        {
            return null;
        }

        public virtual async Task Complete()
        {
            await Task.Delay(1);
        }
    }
}
