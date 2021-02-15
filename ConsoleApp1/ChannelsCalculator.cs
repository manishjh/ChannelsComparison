using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ChannelsCalculator : Calculator
    {
        private Channel<Data> _channel;

        public ChannelsCalculator(int parallel = 1)
        {
            _channel = Channel.CreateBounded<Data>(new BoundedChannelOptions(200) { SingleWriter = true });

            StartReaders(parallel);
        }

        private void StartReaders(int parallel)
        {
            for (int i = 0; i < parallel; i++)
            {
                Task.Run(async () =>
                {
                    await foreach (var data in _channel.Reader.ReadAllAsync())
                    {
                        await CalculateCTC(data);
                    }
                });
            }
        }

        public override async Task AddEmp(Data empInfo)
        {
            await _channel.Writer.WriteAsync(empInfo); // producer
        }

        public override async Task Complete()
        {
            _channel.Writer.Complete();
            await _channel.Reader.Completion;
        }
        public override ICalculator Clone(int parallel)
        {
            return new ChannelsCalculator(parallel);
        }
    }
}
