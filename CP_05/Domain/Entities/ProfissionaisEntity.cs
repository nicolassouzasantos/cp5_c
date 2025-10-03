using System.ComponentModel.DataAnnotations;

namespace CP_05.Domain.Entities
{
    public class ProfissionaisEntity
{
    public int Id { get; set; }

    [Required]
    public string Rua { get; set; } = null!;

    [Required]
    public int Numero { get; set; }

    [Required]
    public string Bairro { get; set; } = null!;

    [Required]
    public string CEP { get; set; } = null!;

    // FK 1:1 (única)
    public int ClinicaId { get; set; }
    public ClinicaEntity? Clinica { get; set; }
}
}
