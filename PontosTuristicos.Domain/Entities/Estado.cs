using System.ComponentModel.DataAnnotations;

namespace PontosTuristicos.Domain.Entities;

public class Estado
{
    [Key]
    [MaxLength(2)]
    public string Sigla { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nome { get; set; }

    public List<Cidade> Cidades { get; set; } = new();
}