using PontosTuristicos.Domain.Entities;

namespace PontosTuristicos.Domain.Interfaces;

public interface IPontoTuristicoRepository
{
    Task<(IEnumerable<PontoTuristico> Itens, int ContadorTotal)> ObterTodosAsync(string termoBusca, int pagina, int tamanhoPagina);
    Task<PontoTuristico> ObterPorIdAsync(int id);
    Task AdicionarAsync(PontoTuristico pontoTuristico);
    Task AtualizarAsync(PontoTuristico pontoTuristico);
    Task DeletarAsync(PontoTuristico pontoTuristico);
}