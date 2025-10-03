using System.ComponentModel.DataAnnotations;

namespace CP_05.Domain.Entities;

public class ClinicaEntity
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Telefone { get; set; }

    public EnderecoEntity? Endereco { get; set; }

    public List<ProfissionalEntity> Profissionais { get; set; } = new();
}
