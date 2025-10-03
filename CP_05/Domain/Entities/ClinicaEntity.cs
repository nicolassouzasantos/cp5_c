using System.ComponentModel.DataAnnotations;

namespace CP_05.Domain.Entities
{
    public class ClinicaEntity
    {
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    public string? Telefone { get; set; }

    // 1:N
    public List<ProfissionaisEntity> Profissionais { get; set; } = new();

    // 1:1
    public EnderecoEntity? Endereco { get; set; }
}
}
