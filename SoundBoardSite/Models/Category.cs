using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<AudioFileCategory> AudioFileCategories { get; set; } = new List<AudioFileCategory>();
}