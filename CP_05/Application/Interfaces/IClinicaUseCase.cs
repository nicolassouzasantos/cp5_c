using CP_05.Application.Common;
using CP_05.Application.Dtos.Clinica;
using CP_05.Application.Dtos.Profissional;

namespace CP_05.Application.Interfaces;

public interface IClinicaUseCase
{
    Task<OperationResult<IEnumerable<ClinicaReadDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OperationResult<ClinicaReadDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<OperationResult<ClinicaReadDto>> CreateAsync(ClinicaCreateDto dto, CancellationToken cancellationToken = default);
    Task<OperationResult> UpdateAsync(int id, ClinicaUpdateDto dto, CancellationToken cancellationToken = default);
    Task<OperationResult> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<OperationResult<ProfissionalReadDto>> AddProfissionalAsync(int clinicaId, ProfissionalCreateDto dto, CancellationToken cancellationToken = default);
    Task<OperationResult> UpdateProfissionalAsync(int clinicaId, int profissionalId, ProfissionalUpdateDto dto, CancellationToken cancellationToken = default);
}
