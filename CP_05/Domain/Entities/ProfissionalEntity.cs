using System.ComponentModel.DataAnnotations;

namespace CP_05.Domain.Entities;

public class ProfissionalEntity
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "A idade deve ser um número positivo.")]
    public int Idade { get; set; }

    public string? Cargo { get; set; }

    public int ClinicaId { get; set; }

    public ClinicaEntity? Clinica { get; set; }
}
