using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using MassTransit;

namespace MessagingBus.Publishers
{
    public class MessagePublisher: IMessagePublisher
    {
        private readonly IBus _bus;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public MessagePublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T message)
        {
            await Task.Run(() =>
            {
                _bus.Publish(message, _cancellationTokenSource.Token);
            });
        }

        public async Task SendToQueue<T>(T message, string queue, CancellationToken cancellationToken = default)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queue}"));

            await endpoint.Send(message, cancellationToken);
        }
    }
}