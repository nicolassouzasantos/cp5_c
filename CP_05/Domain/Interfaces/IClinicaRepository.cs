using CP_05.Domain.Entities;

namespace CP_05.Domain.Interfaces;

public interface IClinicaRepository
{
    Task<List<ClinicaEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ClinicaEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ClinicaEntity> AddAsync(ClinicaEntity clinica, CancellationToken cancellationToken = default);
    Task UpdateAsync(ClinicaEntity clinica, CancellationToken cancellationToken = default);
    Task DeleteAsync(ClinicaEntity clinica, CancellationToken cancellationToken = default);

    Task<bool> ClinicaExistsAsync(int clinicaId, CancellationToken cancellationToken = default);
    Task<EnderecoEntity?> GetEnderecoByClinicaIdAsync(int clinicaId, CancellationToken cancellationToken = default);
    Task RemoveEnderecoAsync(EnderecoEntity endereco, CancellationToken cancellationToken = default);

    Task<ProfissionalEntity> AddProfissionalAsync(ProfissionalEntity profissional, CancellationToken cancellationToken = default);
    Task<ProfissionalEntity?> GetProfissionalByIdAsync(int profissionalId, int clinicaId, CancellationToken cancellationToken = default);
    Task UpdateProfissionalAsync(ProfissionalEntity profissional, CancellationToken cancellationToken = default);
}
