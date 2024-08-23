namespace EmployersExample.Domain.HttpServices.Requests;

public record PostAccountRequest(long Agency, long Account, Guid EmployerId);
