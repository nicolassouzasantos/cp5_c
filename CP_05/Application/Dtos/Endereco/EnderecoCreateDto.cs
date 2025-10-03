using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Application.Dtos.Endereco;

[SwaggerSchema(Description = "Dados para cadastrar o endereço da clínica.")]
public class EnderecoCreateDto
{
    [Required]
    [SwaggerSchema(Description = "Nome da rua.", Example = "Av. Paulista")]
    public string Rua { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "O número deve ser positivo.")]
    [SwaggerSchema(Description = "Número do endereço.", Example = 1000)]
    public int Numero { get; set; }

    [Required]
    [SwaggerSchema(Description = "Bairro da clínica.", Example = "Bela Vista")]
    public string Bairro { get; set; } = string.Empty;

    [Required]
    [SwaggerSchema(Description = "CEP no formato brasileiro.", Example = "01310-100")]
    public string Cep { get; set; } = string.Empty;
}
