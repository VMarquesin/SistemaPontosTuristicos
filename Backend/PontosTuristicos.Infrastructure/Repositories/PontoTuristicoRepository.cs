using Dapper;
using Microsoft.EntityFrameworkCore;
using PontosTuristicos.Domain.Entities;
using PontosTuristicos.Domain.Interfaces;
using PontosTuristicos.Infrastructure.Data;

namespace PontosTuristicos.Infrastructure.Repositories;

public class PontoTuristicoRepository : IPontoTuristicoRepository
{
    private readonly ApplicationDbContext _context;

    public PontoTuristicoRepository(ApplicationDbContext context)
    {
        _context = context;
    }
   public async Task<(IEnumerable<PontoTuristico> Itens, int ContadorTotal)> ObterTodosAsync(string termoBusca, int pagina, int tamanhoPagina)
    {
        var query = _context.PontosTuristicos.Where(p => p.Ativo).AsQueryable();

        if (!string.IsNullOrWhiteSpace(termoBusca))
        {
            query = query.Where(p => 
            p.Nome.Contains(termoBusca) || 
            p.Descricao.Contains(termoBusca) ||
            p.Localizacao.Contains(termoBusca));
        }
        
        var contadorTotal = await query.CountAsync();
        var termo = string.IsNullOrWhiteSpace(termoBusca) ? "" : termoBusca;
        var connection = _context.Database.GetDbConnection(); 
        var sql = "EXEC stp_BuscarPontosTuristicos @Termo, @Pagina, @TamanhoPagina";

        var itens = await connection.QueryAsync<PontoTuristico, Cidade, Estado, PontoTuristico>(
            sql,
            map: (ponto, cidade, estado) => 
            {
                cidade.Estado = estado;
                ponto.Cidade = cidade;  
                return ponto;
            },
            param: new { Termo = termo, Pagina = pagina, TamanhoPagina = tamanhoPagina },
            splitOn: "Id, Sigla" 
        );

        return (itens, contadorTotal);
    }

    public async Task<PontoTuristico?> ObterPorIdAsync(int id)
    {
        return await _context.PontosTuristicos
            .Include(p => p.Cidade)
            .ThenInclude(c => c.Estado)
            .FirstOrDefaultAsync(p => p.IdPontosTuristicos == id);
    }

    public async Task AdicionarAsync(PontoTuristico pontoTuristico)
    {
        await _context.PontosTuristicos.AddAsync(pontoTuristico);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(PontoTuristico pontoTuristico)
    {
        _context.PontosTuristicos.Update(pontoTuristico);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(PontoTuristico pontoTuristico)
    {
        _context.PontosTuristicos.Remove(pontoTuristico);
        await _context.SaveChangesAsync();
    }

    public async Task<int> ObterOuCriarCidadeAsync(string nomeCidade, string siglaUf)
    {
        var estado = await _context.Set<Estado>()
                                   .FirstOrDefaultAsync(e => e.Sigla == siglaUf);
        
        if (estado == null)
        {
            estado = new Estado { Sigla = siglaUf, Nome = siglaUf }; 
            await _context.Set<Estado>().AddAsync(estado);
            await _context.SaveChangesAsync(); 
        }

        var cidade = await _context.Set<Cidade>()
                                   .FirstOrDefaultAsync(c => c.Nome == nomeCidade && c.Estado.Sigla == siglaUf);
        
        if (cidade == null)
        {
            cidade = new Cidade { Nome = nomeCidade, Estado = estado };
            await _context.Set<Cidade>().AddAsync(cidade);
            await _context.SaveChangesAsync(); 
        }

        return cidade.Id; 
    }
}


