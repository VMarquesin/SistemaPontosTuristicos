using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontosTuristicos.Domain.Entities;

public class Cidade
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }

    [Required]
    [MaxLength(2)]
    public string EstadoSigla { get; set; }

    [ForeignKey("EstadoSigla")]
    public Estado Estado { get; set; }
    public List<PontoTuristico> PontosTuristicos { get; set; } = new();
}