using EmployersExample.Domain.Enums;

namespace EmployersExample.Domain.Models;

public class EmployerModel(string name, EmployerLevel level, long documentNumber, DateTimeOffset birthDate)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public long DocumentNumber { get; set; } = documentNumber;
    public string Name { get; set; } = name;
    public bool IsActive { get; set; } = true;
    public EmployerLevel EmployerLevel { get; set; } = level;
    public decimal Remuneration { get; private set; } = DefinirSalarioInicial(level);
    public DateTimeOffset BirthDate { get; set; } = birthDate;
    public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.UtcNow;

    private static readonly Dictionary<EmployerLevel, decimal> InitialRemuneration = new()
{
    { EmployerLevel.Junior, 3000 },
    { EmployerLevel.Pleno,  1500 },
    { EmployerLevel.Senior, 5000 }
};

    private static decimal DefinirSalarioInicial(EmployerLevel level)
    {
        if (InitialRemuneration.TryGetValue(level, out var remuneration))
        {
            return remuneration;
        }
        throw new ArgumentException($"Salário não definido para o tipo de colaborador: {level}");
    }
}
