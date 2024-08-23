using System.Net;

using EmployersExample.Domain.Dtos.Requests;
using EmployersExample.Domain.Dtos.Responses;
using EmployersExample.Domain.HttpServices;
using EmployersExample.Domain.HttpServices.Requests;
using EmployersExample.Domain.Interfaces;
using EmployersExample.Domain.Models;
using EmployersExample.Domain.Repositories;

using Responses;

namespace EmployersExample.Domain.UseCases;

public class PostEmployerUseCase(IAccountManager accountManager, IEmployerRepository repository) : IPostEmployerUseCase
{
    public async Task<Result<EmployerResponse>> PostEmployerAsync(PostEmployerRequest request, CancellationToken cancellationToken)
    {
        var verifyExistUser = await repository.VerifyUserExistsAsync(request.DocumentNumber, cancellationToken);

        if (verifyExistUser.IsSuccess) return Result.Fail<EmployerResponse>(HttpStatusCode.BadRequest.ToString(), "Usuário já cadastrado, para alteração utilize a rota de edição");

        var model = new EmployerModel(request.Name, request.EmployerLevel, request.DocumentNumber, request.BirthDate);

        var insertEmployer = await repository.InsertEmployerAsync(model, cancellationToken);

        if (!insertEmployer.IsSuccess) return Result.Fail<EmployerResponse>(insertEmployer.Error.Code, "Parece que algo não ocorreu como deveria, por favor tente novamente");

        var accountRequest = new PostAccountRequest(request.Agency, request.Account, insertEmployer.Value.Id);
        var insertAccount = await accountManager.PostAccountAsync(accountRequest, cancellationToken);

        if (!insertAccount.IsSuccess) return Result.Fail<EmployerResponse>(insertAccount.Error.Code, "Usuário cadastro com sucesso, porém identificamos problema para cadastrar a conta do usuário, por favor tente o cadastro da conta novamente");

        var response = new EmployerResponse(insertEmployer.Value.Id, insertEmployer.Value.Name, insertEmployer.Value.Remuneration, insertEmployer.Value.EmployerLevel);
        return Result.Ok(response);
    }
}
