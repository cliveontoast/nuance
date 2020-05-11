using Autofac;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public static class RunWaitExtensions
    {
        public static TResult RunWait<TResult>(this Task<TResult> asyncTask)
        {
            var task = Task.Run(() => asyncTask);
            task.Wait();
            return task.Result;
        }

        public static TResponse RunSync<TRequest, TResponse>(this ILifetimeScope scope, TRequest request) where TRequest : IRequest<TResponse>
        {
            var handler = scope.Resolve<IRequestHandler<TRequest, TResponse>>();
            return handler.Handle(request, CancellationToken.None).RunWait();
        }
    }
}
