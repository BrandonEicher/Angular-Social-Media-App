using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models;

public class User 
{
    [JsonIgnore]
    public string? UserId { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }   
    [Required] 
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; } 
    [Required]   
    public string? City { get; set; }
    [Required]
    public string? State { get; set; }
}