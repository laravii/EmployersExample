using EmployersExample.Domain.Enums;

namespace EmployersExample.Domain.Dtos.Responses;

public record EmployerResponse(Guid Id, string Name, decimal Remuneration, EmployerLevel EmployerLevel);
