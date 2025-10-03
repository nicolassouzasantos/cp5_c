using System.ComponentModel.DataAnnotations;
using CP_05.Application.Dtos.Endereco;
using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Application.Dtos.Clinica;

[SwaggerSchema(Description = "Dados para cadastrar uma nova clínica.")]
public class ClinicaCreateDto
{
    [Required]
    [SwaggerSchema(Description = "Nome da clínica.", Example = "Clínica Bem-Estar")]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress]
    [SwaggerSchema(Description = "E-mail da clínica.", Example = "contato@bemestar.com")]
    public string Email { get; set; } = string.Empty;

    [SwaggerSchema(Description = "Telefone de contato.", Example = "+55 11 4002-8922")]
    public string? Telefone { get; set; }

    [SwaggerSchema(Description = "Endereço completo da clínica.")]
    public EnderecoCreateDto? Endereco { get; set; }
}
