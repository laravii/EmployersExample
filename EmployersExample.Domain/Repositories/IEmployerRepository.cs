using EmployersExample.Domain.Models;

using Responses;

namespace EmployersExample.Domain.Repositories
{
    public interface IEmployerRepository
    {
        Task<EmployerModel> GetEmployerAsync(long documentNumber, CancellationToken cancellationToken);
        Task<List<EmployerModel>> GetAllEmployerAsync(CancellationToken cancellationToken);
        Task<Result> VerifyUserExistsAsync(long documentNumber, CancellationToken cancellationToken);
        Task<Result<EmployerModel>> InsertEmployerAsync(EmployerModel model, CancellationToken cancellationToken);
        Task<Result<EmployerModel>> UpdateEmployerAsync(EmployerModel model, CancellationToken cancellationToken);
    }
}
