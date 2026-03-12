using PontosTuristicos.Application.DTOs;
using PontosTuristicos.Application.Interfaces;
using PontosTuristicos.Domain.Entities;
using PontosTuristicos.Domain.Interfaces;

namespace PontosTuristicos.Application.Services;

public class PontoTuristicoService : IPontoTuristicoService
{
    private readonly IPontoTuristicoRepository _repository;

    public PontoTuristicoService(IPontoTuristicoRepository repository)
    {
        _repository = repository;
    }
     public async Task<(IEnumerable<PontoTuristico> Itens, int TotalContador)> ObterTodosAtivosAsync(string termoBusca, int pagina, int tamanhoPagina)
    {
        return await _repository.ObterTodosAsync(termoBusca, pagina, tamanhoPagina);
    }
     public async Task<PontoTuristico> ObterPorIdAtivoAsync(int id)
    {
        return await _repository.ObterPorIdAsync(id);
    }
    
    public async Task<PontoTuristico> AdicionarAsync(PontoTuristicoDto dto)
    {
    
        int idDaCidade = await _repository.ObterOuCriarCidadeAsync(dto.Cidade, dto.Uf);
    
        var pontoTuristico = new PontoTuristico
        {
            Nome         = dto.Nome,
            Descricao    = dto.Descricao,
            Localizacao  = dto.Localizacao,
            CEP          = dto.CEP,
            IdCidade     = idDaCidade, 
            DataInclusao = DateTime.Now,
            Ativo        = true 
        };
    
        await _repository.AdicionarAsync(pontoTuristico);
        return pontoTuristico;
    }   

    public async Task<PontoTuristico> AtualizarAsync(int id, PontoTuristicoDto dto)
    {
        var pontoExistente = await _repository.ObterPorIdAsync(id);

        if (pontoExistente == null)
        {
            return null; 
        }

        int idDaCidade = await _repository.ObterOuCriarCidadeAsync(dto.Cidade, dto.Uf);

        pontoExistente.Nome        = dto.Nome;
        pontoExistente.Descricao   = dto.Descricao;
        pontoExistente.Localizacao = dto.Localizacao;
        pontoExistente.CEP         = dto.CEP;
        pontoExistente.IdCidade    = idDaCidade;
        
        await _repository.AtualizarAsync(pontoExistente);
        
        return pontoExistente;
    }
    
    public async Task<bool> DesativarAsync(int id)
    {
        var pontoExistente = await _repository.ObterPorIdAsync(id);

        if (pontoExistente == null || !pontoExistente.Ativo)
        return false;

        pontoExistente.Ativo = false;

        await _repository.AtualizarAsync(pontoExistente);
        return true;
    }
    public async Task<bool> ReativarAsync(int id)
    {
        var pontoExistente = await _repository.ObterPorIdAsync(id);

        if (pontoExistente == null || pontoExistente.Ativo)
        return false;

        pontoExistente.Ativo = true;

        await _repository.AtualizarAsync(pontoExistente);
        return true;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var pontoExistente = await _repository.ObterPorIdAsync(id);

        if (pontoExistente == null)
        return false;

        await _repository.DeletarAsync(pontoExistente);
        return true;
    }
}