using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        static async Task Main(string[] args)
        {

            await Compute(new DataflowCalculator());

            Console.WriteLine($"");

            await Compute(new ChannelsCalculator());

            Console.ReadKey();
        }

        private static async Task Compute(ICalculator calc)
        {
            Console.WriteLine($"Started compute: {calc.GetType()}");
            long msVal = 0;

            for (var i = 0; i < 10; i++)
                msVal += await RunProgram(calc.Clone(parallel: 4));

            Console.WriteLine($"Final : {msVal / 10} ms");
        }

        private static async Task<long> RunProgram(ICalculator calc)
        {
            try
            {
                Stopwatch s1 = new Stopwatch();

                s1.Start();

                for (int i = 0; i < 100; i++)
                {
                    await calc.AddEmp(new Data(i.ToString(), i + 1000, i % 50));
                }

                await calc.Complete();

                s1.Stop();

                Console.WriteLine("> " + s1.ElapsedMilliseconds);

                return s1.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

    }
}
