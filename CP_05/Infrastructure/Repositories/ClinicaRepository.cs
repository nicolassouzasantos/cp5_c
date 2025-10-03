using CP_05.Domain.Entities;
using CP_05.Domain.Interfaces;
using CP_05.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP_05.Infrastructure.Repositories;

public class ClinicaRepository(ApplicationDbContext context) : IClinicaRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<ClinicaEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Clinicas
            .Include(c => c.Endereco)
            .Include(c => c.Profissionais)
            .AsNoTracking()
            .OrderBy(c => c.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<ClinicaEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Clinicas
            .Include(c => c.Endereco)
            .Include(c => c.Profissionais)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<ClinicaEntity> AddAsync(ClinicaEntity clinica, CancellationToken cancellationToken = default)
    {
        _context.Clinicas.Add(clinica);
        await _context.SaveChangesAsync(cancellationToken);
        return clinica;
    }

    public async Task UpdateAsync(ClinicaEntity clinica, CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ClinicaEntity clinica, CancellationToken cancellationToken = default)
    {
        _context.Clinicas.Remove(clinica);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ClinicaExistsAsync(int clinicaId, CancellationToken cancellationToken = default)
    {
        return _context.Clinicas.AnyAsync(c => c.Id == clinicaId, cancellationToken);
    }

    public async Task<EnderecoEntity?> GetEnderecoByClinicaIdAsync(int clinicaId, CancellationToken cancellationToken = default)
    {
        return await _context.Enderecos.FirstOrDefaultAsync(e => e.ClinicaId == clinicaId, cancellationToken);
    }

    public async Task RemoveEnderecoAsync(EnderecoEntity endereco, CancellationToken cancellationToken = default)
    {
        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProfissionalEntity> AddProfissionalAsync(ProfissionalEntity profissional, CancellationToken cancellationToken = default)
    {
        _context.Profissionais.Add(profissional);
        await _context.SaveChangesAsync(cancellationToken);
        return profissional;
    }

    public Task<ProfissionalEntity?> GetProfissionalByIdAsync(int profissionalId, int clinicaId, CancellationToken cancellationToken = default)
    {
        return _context.Profissionais.FirstOrDefaultAsync(p => p.Id == profissionalId && p.ClinicaId == clinicaId, cancellationToken);
    }

    public async Task UpdateProfissionalAsync(ProfissionalEntity profissional, CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
