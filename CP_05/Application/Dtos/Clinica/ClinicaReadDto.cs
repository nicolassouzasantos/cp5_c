using CP_05.Application.Dtos.Endereco;
using CP_05.Application.Dtos.Profissional;
using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Application.Dtos.Clinica;

[SwaggerSchema(Description = "Clínica retornada pela API.")]
public record ClinicaReadDto(
    int Id,
    string Nome,
    string Email,
    string? Telefone,
    EnderecoReadDto? Endereco,
    IReadOnlyCollection<ProfissionalReadDto> Profissionais
);
