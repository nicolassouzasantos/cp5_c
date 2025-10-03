using System.Linq;
using CP_05.Application.Common;
using CP_05.Application.Dtos.Clinica;
using CP_05.Application.Dtos.Profissional;
using CP_05.Application.Interfaces;
using CP_05.Application.Mappers;
using CP_05.Domain.Interfaces;

namespace CP_05.Application.UseCases;

public class ClinicaUseCase(IClinicaRepository repository) : IClinicaUseCase
{
    private readonly IClinicaRepository _repository = repository;

    public async Task<OperationResult<IEnumerable<ClinicaReadDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var clinicas = await _repository.GetAllAsync(cancellationToken);
        var data = clinicas.Select(c => c.ToReadDto()).ToList();
        return OperationResult<IEnumerable<ClinicaReadDto>>.Success(data);
    }

    public async Task<OperationResult<ClinicaReadDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var clinica = await _repository.GetByIdAsync(id, cancellationToken);
        return clinica is null
            ? OperationResult<ClinicaReadDto>.Failure(OperationErrorType.NotFound, "Clínica não encontrada.")
            : OperationResult<ClinicaReadDto>.Success(clinica.ToReadDto());
    }

    public async Task<OperationResult<ClinicaReadDto>> CreateAsync(ClinicaCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var created = await _repository.AddAsync(entity, cancellationToken);
        return OperationResult<ClinicaReadDto>.Success(created.ToReadDto());
    }

    public async Task<OperationResult> UpdateAsync(int id, ClinicaUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var clinica = await _repository.GetByIdAsync(id, cancellationToken);
        if (clinica is null)
        {
            return OperationResult.Failure(OperationErrorType.NotFound, "Clínica não encontrada.");
        }

        var enderecoAtual = clinica.Endereco ?? await _repository.GetEnderecoByClinicaIdAsync(id, cancellationToken);

        clinica.Endereco = enderecoAtual;
        clinica.ApplyUpdate(dto);

        if (dto.Endereco is null && enderecoAtual is not null)
        {
            await _repository.RemoveEnderecoAsync(enderecoAtual, cancellationToken);
            clinica.Endereco = null;
        }

        await _repository.UpdateAsync(clinica, cancellationToken);
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var clinica = await _repository.GetByIdAsync(id, cancellationToken);
        if (clinica is null)
        {
            return OperationResult.Failure(OperationErrorType.NotFound, "Clínica não encontrada.");
        }

        await _repository.DeleteAsync(clinica, cancellationToken);
        return OperationResult.Success();
    }

    public async Task<OperationResult<ProfissionalReadDto>> AddProfissionalAsync(int clinicaId, ProfissionalCreateDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _repository.ClinicaExistsAsync(clinicaId, cancellationToken))
        {
            return OperationResult<ProfissionalReadDto>.Failure(OperationErrorType.NotFound, "Clínica não encontrada.");
        }

        var entity = dto.ToEntity(clinicaId);
        var created = await _repository.AddProfissionalAsync(entity, cancellationToken);
        return OperationResult<ProfissionalReadDto>.Success(created.ToReadDto());
    }

    public async Task<OperationResult> UpdateProfissionalAsync(int clinicaId, int profissionalId, ProfissionalUpdateDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _repository.ClinicaExistsAsync(clinicaId, cancellationToken))
        {
            return OperationResult.Failure(OperationErrorType.NotFound, "Clínica não encontrada.");
        }

        var profissional = await _repository.GetProfissionalByIdAsync(profissionalId, clinicaId, cancellationToken);
        if (profissional is null)
        {
            return OperationResult.Failure(OperationErrorType.NotFound, "Profissional não encontrado para a clínica informada.");
        }

        profissional.ApplyUpdate(dto);
        await _repository.UpdateProfissionalAsync(profissional, cancellationToken);
        return OperationResult.Success();
    }
}
