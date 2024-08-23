using EmployersExample.Domain.Dtos.Requests;
using EmployersExample.Domain.Dtos.Responses;

using Responses;

namespace EmployersExample.Domain.Interfaces;

public interface IPostEmployerUseCase
{
    public Task<Result<EmployerResponse>> PostEmployerAsync(PostEmployerRequest request, CancellationToken cancellationToken);
}
