using EmployersExample.Domain.HttpServices.Requests;
using EmployersExample.Domain.HttpServices.Responses;

using Responses;

namespace EmployersExample.Domain.HttpServices;

public interface IAccountManager
{
    Task<Result<AccountResponse>> PostAccountAsync(PostAccountRequest request, CancellationToken cancellationToken);
}
