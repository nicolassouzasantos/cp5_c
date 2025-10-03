using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Application.Dtos.Endereco;

[SwaggerSchema(Description = "Dados para atualizar o endereço da clínica.")]
public class EnderecoUpdateDto
{
    [Required]
    [SwaggerSchema(Description = "Nome da rua.", Example = "Av. Brigadeiro Faria Lima")]
    public string Rua { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "O número deve ser positivo.")]
    [SwaggerSchema(Description = "Número do endereço.", Example = 4500)]
    public int Numero { get; set; }

    [Required]
    [SwaggerSchema(Description = "Bairro da clínica.", Example = "Itaim Bibi")]
    public string Bairro { get; set; } = string.Empty;

    [Required]
    [SwaggerSchema(Description = "CEP no formato brasileiro.", Example = "04538-132")]
    public string Cep { get; set; } = string.Empty;
}
