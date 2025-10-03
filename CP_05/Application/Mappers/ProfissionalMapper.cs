using CP_05.Application.Dtos.Profissional;
using CP_05.Domain.Entities;

namespace CP_05.Application.Mappers;

public static class ProfissionalMapper
{
    public static ProfissionalEntity ToEntity(this ProfissionalCreateDto dto, int clinicaId) => new()
    {
        Nome = dto.Nome,
        Email = dto.Email,
        Idade = dto.Idade,
        Cargo = dto.Cargo,
        ClinicaId = clinicaId
    };

    public static void ApplyUpdate(this ProfissionalEntity entity, ProfissionalUpdateDto dto)
    {
        entity.Nome = dto.Nome;
        entity.Email = dto.Email;
        entity.Idade = dto.Idade;
        entity.Cargo = dto.Cargo;
    }

    public static ProfissionalReadDto ToReadDto(this ProfissionalEntity entity)
        => new(entity.Id, entity.Nome, entity.Email, entity.Idade, entity.Cargo, entity.ClinicaId);
}
