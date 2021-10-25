using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TodoApi3.Models
{
    public class Mediator : IMediator
    {
        private ServiceFactory serviceFactory { get; }



        public Mediator(ServiceFactory ServiceFactory)
        {
            serviceFactory = ServiceFactory;
        }




        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            serviceFactory.BeginInvoke();
            throw new NotImplementedException();
        }
    }
}
