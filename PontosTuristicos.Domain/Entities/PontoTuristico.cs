using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontosTuristicos.Domain.Entities;

public class PontoTuristico 
{
    [Key]
    public int IdPontosTuristicos { get; set; }

    [Required]
    public int IdCidade { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nome  { get; set; }

    [Required]
    [MaxLength(100)]
    public string Descricao { get; set; }

    [Required]
    [MaxLength(255)]
    public string Localizacao { get; set; }

    [MaxLength(9)]
    public string? CEP { get; set; }

    public DateTime DataInclusao { get; set; } = DateTime.Now;

    [Required]
    public bool Ativo { get; set; } = true;

    [ForeignKey("IdCidade")]
    public Cidade Cidade { get; set; }
}