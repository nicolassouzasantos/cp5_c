using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Application.Dtos.Profissional;

[SwaggerSchema(Description = "Profissional associado à clínica.")]
public record ProfissionalReadDto(
    int Id,
    string Nome,
    string Email,
    int Idade,
    string? Cargo,
    int ClinicaId
);
