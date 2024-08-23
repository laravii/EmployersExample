using System.ComponentModel.DataAnnotations;

using EmployersExample.Domain.Enums;

namespace EmployersExample.Domain.Dtos.Requests;

public class PostEmployerRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public long DocumentNumber { get; set; }

    [Required]
    public EmployerLevel EmployerLevel { get; set; }

    [Required]
    public DateTimeOffset BirthDate { get; set; }

    [Required]
    public int Agency { get; set; }

    [Required]
    public int Account { get; set; }
}
