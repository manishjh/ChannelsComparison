using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface ICalculator
    {
        Task AddEmp(Data empInfo);
        Task CalculateCTC(Data empInfo);
        ICalculator Clone(int parallel);
        Task Complete();
    }
}
