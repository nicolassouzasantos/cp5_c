using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Application.Dtos.Profissional;

[SwaggerSchema(Description = "Dados para cadastrar um profissional em uma clínica.")]
public class ProfissionalCreateDto
{
    [Required]
    [SwaggerSchema(Description = "Nome completo do profissional.", Example = "Maria Souza")]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress]
    [SwaggerSchema(Description = "E-mail de contato.", Example = "maria.souza@exemplo.com")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(18, 120, ErrorMessage = "A idade deve estar entre 18 e 120 anos.")]
    [SwaggerSchema(Description = "Idade do profissional.", Example = 32)]
    public int Idade { get; set; }

    [SwaggerSchema(Description = "Cargo exercido na clínica.", Example = "Fisioterapeuta")]
    public string? Cargo { get; set; }
}
