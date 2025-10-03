using System.Linq;
using CP_05.Application.Dtos.Clinica;
using CP_05.Application.Dtos.Endereco;
using CP_05.Application.Dtos.Profissional;
using CP_05.Domain.Entities;

namespace CP_05.Application.Mappers;

public static class ClinicaMapper
{
    public static ClinicaEntity ToEntity(this ClinicaCreateDto dto)
    {
        var clinica = new ClinicaEntity
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone
        };

        if (dto.Endereco is not null)
        {
            clinica.Endereco = new EnderecoEntity
            {
                Rua = dto.Endereco.Rua,
                Numero = dto.Endereco.Numero,
                Bairro = dto.Endereco.Bairro,
                Cep = dto.Endereco.Cep
            };
        }

        return clinica;
    }

    public static void ApplyUpdate(this ClinicaEntity entity, ClinicaUpdateDto dto)
    {
        entity.Nome = dto.Nome;
        entity.Email = dto.Email;
        entity.Telefone = dto.Telefone;

        if (dto.Endereco is null)
        {
            return;
        }

        if (entity.Endereco is null)
        {
            entity.Endereco = new EnderecoEntity
            {
                Rua = dto.Endereco.Rua,
                Numero = dto.Endereco.Numero,
                Bairro = dto.Endereco.Bairro,
                Cep = dto.Endereco.Cep,
                ClinicaId = entity.Id
            };
        }
        else
        {
            entity.Endereco.Rua = dto.Endereco.Rua;
            entity.Endereco.Numero = dto.Endereco.Numero;
            entity.Endereco.Bairro = dto.Endereco.Bairro;
            entity.Endereco.Cep = dto.Endereco.Cep;
        }
    }

    public static ClinicaReadDto ToReadDto(this ClinicaEntity entity)
    {
        var enderecoDto = entity.Endereco is null
            ? null
            : new EnderecoReadDto(entity.Endereco.Id, entity.Endereco.Rua, entity.Endereco.Numero, entity.Endereco.Bairro, entity.Endereco.Cep);

        var profissionais = entity.Profissionais
            .Select(p => new ProfissionalReadDto(p.Id, p.Nome, p.Email, p.Idade, p.Cargo, p.ClinicaId))
            .ToList();

        return new ClinicaReadDto(entity.Id, entity.Nome, entity.Email, entity.Telefone, enderecoDto, profissionais);
    }
}
