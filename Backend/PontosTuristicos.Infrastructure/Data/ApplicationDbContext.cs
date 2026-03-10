using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using PontosTuristicos.Domain.Entities;

namespace PontosTuristicos.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<PontoTuristico> PontosTuristicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Carga de Estados
        modelBuilder.Entity<Estado>().HasData(
            new Estado { Sigla = "SP", Nome = "São Paulo" },
            new Estado { Sigla = "RJ", Nome = "Rio de Janeiro" },
            new Estado { Sigla = "MG", Nome = "Minas Gerais" },
            new Estado { Sigla = "PR", Nome = "Paraná" },
            new Estado { Sigla = "SC", Nome = "Santa Catarina" },
            new Estado { Sigla = "RS", Nome = "Rio Grande do Sul" }
        );

        // Carga de Cidades
        modelBuilder.Entity<Cidade>().HasData(
            new Cidade { Id = 1, Nome = "São Paulo", EstadoSigla = "SP" },
            new Cidade { Id = 2, Nome = "Rio de Janeiro", EstadoSigla = "RJ" },
            new Cidade { Id = 3, Nome = "Pompeia", EstadoSigla = "SP" },
            new Cidade { Id = 4, Nome = "Tupã", EstadoSigla = "SP" }
        );

        // Carga de Pontos Turísticos
        modelBuilder.Entity<PontoTuristico>().HasData(
            new PontoTuristico 
            { 
                IdPontosTuristicos = 1, 
                IdCidade = 1, 
                Nome = "Museu de Arte de São Paulo (MASP)", 
                Descricao = "Famoso museu na Avenida Paulista com arquitetura icônica.", 
                Localizacao = "Avenida Paulista, 1578", 
                CEP = "01310200", 
                DataInclusao = new DateTime(2026, 3, 1, 10, 0, 0)
            },
            new PontoTuristico 
            { 
                IdPontosTuristicos = 2, 
                IdCidade = 2, 
                Nome = "Cristo Redentor", 
                Descricao = "Estátua Jesus Cristo, símbolo do Rio de Janeiro.", 
                Localizacao = "Parque Nacional da Tijuca", 
                CEP = "22241120", 
                DataInclusao = new DateTime(2026, 3, 2, 14, 30, 0) 
            },
            new PontoTuristico 
            { 
                IdPontosTuristicos = 3, 
                IdCidade = 3, 
                Nome = "Museu de Tecnologia", 
                Descricao = "Um museu sobre a evolução dos sistemas e hardware.", 
                Localizacao = "Centro, perto da praça principal", 
                CEP = "17580000", 
                DataInclusao = new DateTime(2026, 3, 5, 9, 15, 0) 
            },
            new PontoTuristico
            {
                IdPontosTuristicos = 4, 
                IdCidade = 4, 
                Nome = "Praça da Bandeira", 
                Descricao = "Bela praça com a igreja Matriz.", 
                Localizacao = "Centro, perto da Câmara Municipal", 
                CEP = "17600380", 
                DataInclusao = new DateTime(2026, 3, 8, 10, 15, 0) 
            }
        );
    }
}