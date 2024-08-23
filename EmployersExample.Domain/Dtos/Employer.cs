using EmployersExample.Domain.Enums;

namespace EmployersExample.Domain.UseCases
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmployerLevel EmployerLevel { get; set; }
        public decimal Remuneration { get; private set; }

        private static readonly Dictionary<EmployerLevel, decimal> InitialRemuneration = new()
    {
        { EmployerLevel.Junior, 1500 },
        { EmployerLevel.Pleno, 3000 },
        { EmployerLevel.Senior, 5000 }
    };

        public Employer(string name, EmployerLevel level)
        {
            Name = name;
            EmployerLevel = level;
            Remuneration = DefinirSalarioInicial(level);
        }

        private decimal DefinirSalarioInicial(EmployerLevel level)
        {
            if (InitialRemuneration.TryGetValue(level, out var remuneration))
            {
                return remuneration;
            }
            throw new ArgumentException($"Salário não definido para o tipo de colaborador: {level}");
        }
    }
}
