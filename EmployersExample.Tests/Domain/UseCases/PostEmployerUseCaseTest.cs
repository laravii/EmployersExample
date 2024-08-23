using EmployersExample.Domain.Dtos.Requests;
using EmployersExample.Domain.Enums;
using EmployersExample.Domain.HttpServices;
using EmployersExample.Domain.HttpServices.Requests;
using EmployersExample.Domain.HttpServices.Responses;
using EmployersExample.Domain.Models;
using EmployersExample.Domain.Repositories;
using EmployersExample.Domain.UseCases;

using NSubstitute;

using Responses;

namespace EmployersExample.Tests.Domain.UseCases;

public class PostEmployerUseCaseTest
{
    private readonly IAccountManager _accountManager;
    private readonly IEmployerRepository _repository;
    private readonly PostEmployerUseCase _sut;
    public PostEmployerUseCaseTest()
    {
        _accountManager = Substitute.For<IAccountManager>();
        _repository = Substitute.For<IEmployerRepository>();

        _sut = new(_accountManager, _repository);
    }
    // Expectativa de salario: 
    // Jr: 1500
    // Pl: 3000
    // SR: 5000
    [Fact]
    public async Task ShouldPostUserSucess_PositiveFalse()
    {
        var request = new PostEmployerRequest()
        {
            Name = "Adalberto dos testes",
            DocumentNumber = 68178248000,
            Account = 659874,
            Agency = 95,
            BirthDate = DateTimeOffset.UtcNow.AddYears(-25),
            EmployerLevel = EmployerLevel.Pleno
        };
        EmployerModel model = new(request.Name, request.EmployerLevel, request.DocumentNumber, request.BirthDate);
        PostAccountRequest accountRequest = new(request.Agency, request.Account, model.Id);

        _repository.VerifyUserExistsAsync(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(Result.Fail("404", "User not exists"));
        _repository.InsertEmployerAsync(Arg.Is<EmployerModel>(m =>
         m.Name == model.Name && m.DocumentNumber == model.DocumentNumber && m.EmployerLevel == model.EmployerLevel), Arg.Any<CancellationToken>()).Returns(Result.Ok(model));
        _accountManager.PostAccountAsync(accountRequest, Arg.Any<CancellationToken>()).Returns(Result.Ok(new AccountResponse() { AccountId = Guid.NewGuid() }));

        var result = await _sut.PostEmployerAsync(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(0, result.Value.Remuneration);
    }

    [Fact]
    public async Task ShouldPostUserSucess_Real()
    {
        var request = new PostEmployerRequest()
        {
            Name = "Adalberto dos testes",
            DocumentNumber = 68178248000,
            Account = 659874,
            Agency = 95,
            BirthDate = DateTimeOffset.UtcNow.AddYears(-25),
            EmployerLevel = EmployerLevel.Pleno
        };
        EmployerModel model = new(request.Name, request.EmployerLevel, request.DocumentNumber, request.BirthDate);
        PostAccountRequest accountRequest = new(request.Agency, request.Account, model.Id);

        _repository.VerifyUserExistsAsync(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(Result.Fail("404", "User not exists"));
        _repository.InsertEmployerAsync(Arg.Is<EmployerModel>(m =>
         m.Name == model.Name && m.DocumentNumber == model.DocumentNumber && m.EmployerLevel == model.EmployerLevel), Arg.Any<CancellationToken>()).Returns(Result.Ok(model));
        _accountManager.PostAccountAsync(accountRequest, Arg.Any<CancellationToken>()).Returns(Result.Ok(new AccountResponse() { AccountId = Guid.NewGuid() }));

        var result = await _sut.PostEmployerAsync(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(3000, result.Value.Remuneration);
    }
}
