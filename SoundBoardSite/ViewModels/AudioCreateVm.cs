using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class AudioCreateVm
{
    [Required, MaxLength(200)]
    public string Label{get;set;} = string.Empty;

    [MaxLength(2000)]
    public string? Description {get; set;}

    [Required]
    public int CategoryId {get;set;}

    [Required]
    public IFormFile Mp3 {get; set;} = null!;

}