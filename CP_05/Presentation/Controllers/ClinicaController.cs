using CP_05.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CP_05.Presentation.Controllers
[ApiController]
[Route("api/[controller]")]
public class ClinicasController(AppDbContext db) : ControllerBase
{
    // GET: /api/clinicas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClinicaReadDto>>> GetAll()
    {
        var clinicas = await db.Clinicas
            .Include(c => c.Endereco)
            .Include(c => c.Profissionais)
            .ToListAsync();

        var result = clinicas.Select(MapToReadDto).ToList();
        return Ok(result); // 200
    }

    // GET: /api/clinicas/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClinicaReadDto>> GetById(int id)
    {
        var clinica = await db.Clinicas
            .Include(c => c.Endereco)
            .Include(c => c.Profissionais)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clinica is null) return NotFound(); // 404
        return Ok(MapToReadDto(clinica)); // 200
    }

    // POST: /api/clinicas
    [HttpPost]
    public async Task<ActionResult<ClinicaReadDto>> Create([FromBody] ClinicaCreateDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState); // 400

        var clinica = new Clinica
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone
        };

        if (dto.Endereco is not null)
        {
            clinica.Endereco = new Endereco
            {
                Rua = dto.Endereco.Rua,
                Numero = dto.Endereco.Numero,
                Bairro = dto.Endereco.Bairro,
                CEP = dto.Endereco.CEP
            };
        }

        db.Clinicas.Add(clinica);
        await db.SaveChangesAsync();

        var read = MapToReadDto(clinica);
        return CreatedAtAction(nameof(GetById), new { id = clinica.Id }, read); // 201
    }

    // PUT: /api/clinicas/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClinicaUpdateDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var clinica = await db.Clinicas
            .Include(c => c.Endereco)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clinica is null) return NotFound(); // 404

        clinica.Nome = dto.Nome;
        clinica.Email = dto.Email;
        clinica.Telefone = dto.Telefone;

        if (dto.Endereco is null)
        {
            if (clinica.Endereco is not null) db.Enderecos.Remove(clinica.Endereco);
        }
        else
        {
            if (clinica.Endereco is null)
            {
                clinica.Endereco = new Endereco
                {
                    Rua = dto.Endereco.Rua,
                    Numero = dto.Endereco.Numero,
                    Bairro = dto.Endereco.Bairro,
                    CEP = dto.Endereco.CEP
                };
            }
            else
            {
                clinica.Endereco.Rua = dto.Endereco.Rua;
                clinica.Endereco.Numero = dto.Endereco.Numero;
                clinica.Endereco.Bairro = dto.Endereco.Bairro;
                clinica.Endereco.CEP = dto.Endereco.CEP;
            }
        }

        await db.SaveChangesAsync();
        return NoContent(); // 204
    }

    // DELETE: /api/clinicas/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var clinica = await db.Clinicas.FindAsync(id);
        if (clinica is null) return NotFound(); // 404

        db.Clinicas.Remove(clinica);
        await db.SaveChangesAsync();
        return NoContent(); // 204
    }

    // ===== Métodos extras exigidos =====

    // POST: /api/clinicas/{clinicaId}/profissionais
    // Adicionar profissional passando IdClinica
    [HttpPost("{clinicaId:int}/profissionais")]
    public async Task<ActionResult<ProfissionalReadDto>> AdicionarProfissional(int clinicaId, [FromBody] ProfissionalCreateDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var clinica = await db.Clinicas.FindAsync(clinicaId);
        if (clinica is null) return NotFound(); // 404

        var prof = new Profissional
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Idade = dto.Idade,
            Cargo = dto.Cargo,
            ClinicaId = clinicaId
        };

        db.Profissionais.Add(prof);
        await db.SaveChangesAsync();

        var read = new ProfissionalReadDto(prof.Id, prof.Nome, prof.Email, prof.Idade, prof.Cargo, prof.ClinicaId);
        return Created($"/api/clinicas/{clinicaId}/profissionais/{prof.Id}", read); // 201
    }

    // PUT: /api/clinicas/{clinicaId}/profissionais/{profissionalId}
    // Editar profissional passando IdProfissional e IdClinica
    [HttpPut("{clinicaId:int}/profissionais/{profissionalId:int}")]
    public async Task<IActionResult> EditarProfissional(int clinicaId, int profissionalId, [FromBody] ProfissionalUpdateDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        // Garante que a clínica exista
        var clinica = await db.Clinicas.FindAsync(clinicaId);
        if (clinica is null) return NotFound(); // 404

        var prof = await db.Profissionais.FirstOrDefaultAsync(p => p.Id == profissionalId && p.ClinicaId == clinicaId);
        if (prof is null) return NotFound(); // 404 (não pertence à clínica informada)

        prof.Nome = dto.Nome;
        prof.Email = dto.Email;
        prof.Idade = dto.Idade;
        prof.Cargo = dto.Cargo;

        await db.SaveChangesAsync();
        return NoContent(); // 204
    }

    private static ClinicaReadDto MapToReadDto(Clinica c)
    {
        var end = c.Endereco is null ? null :
            new EnderecoReadDto(c.Endereco.Id, c.Endereco.Rua, c.Endereco.Numero, c.Endereco.Bairro, c.Endereco.CEP);

        var profs = c.Profissionais
            .Select(p => new ProfissionalReadDto(p.Id, p.Nome, p.Email, p.Idade, p.Cargo, p.ClinicaId))
            .ToList();

        return new ClinicaReadDto(c.Id, c.Nome, c.Email, c.Telefone, end, profs);
    }
}
