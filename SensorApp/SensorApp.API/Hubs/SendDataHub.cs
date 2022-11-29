using System;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SensorApp.API.Querys.GetAverageandLastSensors;

namespace SensorApp.API.Hubs
{
    public class SendDataHub : Hub
    {
        private readonly IMediator _mediator;
        public SendDataHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async IAsyncEnumerable<IEnumerable<SensorLastAndAvergeDTO>> streaming(CancellationToken stopToken)
        {
            while (!stopToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stopToken);
                var resault = _mediator.Send(new GetAverageandLastSensorsQuery());
                //yield return DateTime.UtcNow.ToString("HH:mm:ss");
                yield return resault.Result;
            }
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("update", message).ConfigureAwait(false);
        }
    }
}
