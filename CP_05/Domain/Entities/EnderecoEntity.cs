using System.ComponentModel.DataAnnotations;

namespace CP_05.Domain.Entities
{
    public class EnderecoEntity
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public int Idade { get; set; }

    public string? Cargo { get; set; }

    // FK 1:N
    public int ClinicaId { get; set; }
    public ClinicaEntity? Clinica { get; set; }
}
}
