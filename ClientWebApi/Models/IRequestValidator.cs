using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClientWebApi.Models
{
    public interface IRequestValidator<T>
    {
        Task<List<ErrorResult>> ValidateAsync(T request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
