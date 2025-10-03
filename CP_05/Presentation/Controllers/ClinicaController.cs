using CP_05.Application.Common;
using CP_05.Application.Dtos.Clinica;
using CP_05.Application.Dtos.Profissional;
using CP_05.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CP_05.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicasController(IClinicaUseCase useCase) : ControllerBase
{
    private readonly IClinicaUseCase _useCase = useCase;

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todas as clínicas cadastradas.")]
    [ProducesResponseType(typeof(IEnumerable<ClinicaReadDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _useCase.GetAllAsync(cancellationToken);
        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Busca uma clínica pelo identificador único.")]
    [ProducesResponseType(typeof(ClinicaReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _useCase.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Data) : HandleFailure(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria uma nova clínica." )]
    [ProducesResponseType(typeof(ClinicaReadDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ClinicaCreateDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _useCase.CreateAsync(dto, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data)
            : HandleFailure(result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualiza os dados de uma clínica existente.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ClinicaUpdateDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _useCase.UpdateAsync(id, dto, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Remove uma clínica do cadastro.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _useCase.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpPost("{clinicaId:int}/profissionais")]
    [SwaggerOperation(Summary = "Adiciona um profissional à clínica informada.")]
    [ProducesResponseType(typeof(ProfissionalReadDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AdicionarProfissional(int clinicaId, [FromBody] ProfissionalCreateDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _useCase.AddProfissionalAsync(clinicaId, dto, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = clinicaId }, result.Data)
            : HandleFailure(result);
    }

    [HttpPut("{clinicaId:int}/profissionais/{profissionalId:int}")]
    [SwaggerOperation(Summary = "Atualiza um profissional vinculado à clínica informada.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditarProfissional(int clinicaId, int profissionalId, [FromBody] ProfissionalUpdateDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _useCase.UpdateProfissionalAsync(clinicaId, profissionalId, dto, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    private ActionResult HandleFailure(OperationResult result)
    {
        var payload = new { result.Errors };
        return result.ErrorType switch
        {
            OperationErrorType.NotFound => NotFound(payload),
            OperationErrorType.Validation => BadRequest(payload),
            OperationErrorType.Conflict => Conflict(payload),
            _ => StatusCode(StatusCodes.Status500InternalServerError, payload)
        };
    }

    private ActionResult HandleFailure<T>(OperationResult<T> result) => HandleFailure((OperationResult)result);
}
