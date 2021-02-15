using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApp1
{
    class DataflowCalculator : Calculator
    {
        private ActionBlock<Data> _inputBlock;

        public DataflowCalculator(int parallel = 1)
        {
            _inputBlock = new ActionBlock<Data>(async (s) => await CalculateCTC(s), new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = parallel,
                SingleProducerConstrained = true
            });
        }

        public override async Task AddEmp(Data empInfo)
        {
            await _inputBlock.SendAsync(empInfo); // producer
        }

        public override async Task Complete()
        {
            _inputBlock.Complete();
            await _inputBlock.Completion;
        }

        public override ICalculator Clone(int parallel)
        {
            return new DataflowCalculator(parallel);
        }
    }
}
