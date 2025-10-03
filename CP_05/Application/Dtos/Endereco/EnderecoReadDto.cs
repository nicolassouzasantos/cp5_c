using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Application.Dtos.Endereco;

[SwaggerSchema(Description = "Endereço associado à clínica.")]
public record EnderecoReadDto(
    int Id,
    string Rua,
    int Numero,
    string Bairro,
    string Cep
);
