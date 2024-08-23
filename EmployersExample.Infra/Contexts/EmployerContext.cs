using EmployersExample.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace EmployersExample.Infra.Contexts;

public class EmployerContext(DbContextOptions<EmployerContext> options) : DbContext(options)
{
    public DbSet<EmployerModel> Colaboradores { get; set; }
}
