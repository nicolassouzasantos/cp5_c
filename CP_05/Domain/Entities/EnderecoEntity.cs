using System.ComponentModel.DataAnnotations;

namespace CP_05.Domain.Entities;

public class EnderecoEntity
{
    public int Id { get; set; }

    [Required]
    public string Rua { get; set; } = string.Empty;

    [Required]
    public int Numero { get; set; }

    [Required]
    public string Bairro { get; set; } = string.Empty;

    [Required]
    [Display(Name = "CEP")]
    public string Cep { get; set; } = string.Empty;

    public int ClinicaId { get; set; }

    public ClinicaEntity? Clinica { get; set; }
}
