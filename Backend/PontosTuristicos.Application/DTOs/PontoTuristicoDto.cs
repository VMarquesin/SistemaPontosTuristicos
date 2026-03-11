using System.ComponentModel.DataAnnotations;

namespace PontosTuristicos.Application.DTOs;

public class PontoTuristicoDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [MaxLength(255, ErrorMessage = "A localização deve ter no máximo 255 caracteres.")]
    public string Localizacao { get; set; } = string.Empty;

    [MaxLength(8, ErrorMessage = "O CEP deve ter no máximo 8 caracteres.")]
    public string CEP { get; set; } = string.Empty;

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O estado (UF) é obrigatório.")]
    public string Uf { get; set; } = string.Empty;
}