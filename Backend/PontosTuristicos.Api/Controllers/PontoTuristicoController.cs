using Microsoft.AspNetCore.Mvc;
using PontosTuristicos.Application.DTOs;
using PontosTuristicos.Application.Interfaces;


namespace PontosTuristicos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class pontosTuristicosController : ControllerBase
{
    private readonly IPontoTuristicoService _service;

    public pontosTuristicosController(IPontoTuristicoService service)
    {
        _service = service;
    }
    // GET: api/pontosturisticos?termoBusca=praia&pagina=1&tamanhoPagina=10
    [HttpGet]
    public async Task<IActionResult> ObterTodos(
        [FromQuery] string? termoBusca, 
        [FromQuery] int pagina = 1, 
        [FromQuery] int tamanhoPagina = 10)
    {
        var (itens, ContadorTotal) = await _service.ObterTodosAtivosAsync(termoBusca, pagina, tamanhoPagina);

        return Ok( new
        {
           Dados = itens,
           TotalRegistros = ContadorTotal,
           PaginaAtual = pagina,
           tamanhoPagina = tamanhoPagina 
        });
    }
    // GET: api/pontosturisticos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var pontoTuristico = await _service.ObterPorIdAtivoAsync(id);
        
        if (pontoTuristico == null)
        {
            return NotFound(new {Mensagem = "Ponto turístico não encontrado."});
        }
        return Ok(pontoTuristico);
    }
    // POST: api/pontosturisticos
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] PontoTuristicoDto dto)
    {
        if ( !ModelState.IsValid )
        {
            return BadRequest( ModelState );
        }

        var Salvar = await _service.AdicionarAsync(dto);
        return CreatedAtAction(nameof( ObterPorId ), new { id = Salvar.IdPontosTuristicos }, Salvar);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] PontoTuristicoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var atualizado = await _service.AtualizarAsync(id, dto);

        if (atualizado == null)
        {
            return NotFound(new {Mensagem = "Ponto turistico não encontrado ou inativo."});
        }
        return Ok(atualizado) ;
    }
    // DELETE: api/pontosturisticos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var sucesso = await _service.DesativarAsync(id);

        if (!sucesso)
        {
            return NotFound(new { Mensagem = "Ponto turistico não encontrado ou inativo." });
        }
        return NoContent();
    }
     // DELETE: api/pontosturisticos/5/reativar
    [HttpPatch("{id}/reativar")]
    public async Task<IActionResult> Reativar(int id)
    {
        var sucesso = await _service.ReativarAsync(id);

        if(!sucesso)
        {
            return NotFound(new { Mensagem = "Ponto turístico não encontrado ou já está ativo." });
        }
        return NoContent();
    }
    // DELETE: api/pontosturisticos/5/fisico
    [HttpDelete("{id}/fisico")]
    public async Task<IActionResult> Deletar(int id)
    {
        var sucesso = await _service.DeletarAsync(id);

        if (!sucesso)
        {
            return NotFound(new { Mensagem = "Ponto turístico não encontrado." });
        }
        
        return NoContent();
    }
}