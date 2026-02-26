using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class CategoryCreateVm
{
    [Required, MaxLength(200)]
    public string Name{get;set;} = string.Empty;

    [MaxLength(2000)]
    public string? Description {get; set;}

    [Required]
    public int CategoryId {get;set;}


}