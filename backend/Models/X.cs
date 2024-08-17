using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace backend.Models;

public class X 
{
    [Key]
    public string? XId { get; set; }
    [Required]
    public string? Text { get; set; }
    public string? Username { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}