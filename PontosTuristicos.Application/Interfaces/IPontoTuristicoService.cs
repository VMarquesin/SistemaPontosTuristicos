using PontosTuristicos.Application.DTOs;
using PontosTuristicos.Domain.Entities;


namespace PontosTuristicos.Application.Interfaces;

public interface IPontoTuristicoService
{
    Task<(IEnumerable<PontoTuristico> Itens, int TotalContador)> ObterTodosAtivosAsync(string termoBusca, int pagina, int tamanhoPagina);
    
    Task<PontoTuristico> ObterPorIdAtivoAsync(int id);
    
    Task<PontoTuristico> AdicionarAsync(PontoTuristicoDto dto);
    
    Task<PontoTuristico> AtualizarAsync(int id, PontoTuristicoDto dto);
    
    Task<bool> DesativarAsync(int id);

    Task<bool> ReativarAsync(int id);

    Task<bool> DeletarAsync(int id);
}